using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using School.Data.Dtos;
using School.Data.Entities.Identity;
using School.Data.Helpers;
using School.Data.Requests;
using School.Data.Results;
using School.Infrastructure.Data;
using School.Service.Abstracts;
using System.Security.Claims;

namespace School.Service.Implementation
{

    public class AuthorizationServices : IAuthorizationServices
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _applicationDbContext;
        public AuthorizationServices(RoleManager<Role> roleManager, UserManager<User> userManager, ApplicationDbContext applicationDbContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _applicationDbContext = applicationDbContext;
        }
        public async Task<string> AddRoleAsync(string roleName)
        {
            var identityRole = new Role();
            identityRole.Name = roleName;
            var result = await _roleManager.CreateAsync(identityRole);
            if (result.Succeeded)
            {
                return "Success";
            }
            return "Faild";


        }

        public async Task<bool> IsRoleExistByName(string roleName)
        {
            //var role = await _roleManager.FindByNameAsync(roleName);
            //if (role == null)
            //    return false;
            //return true;

            return await _roleManager.RoleExistsAsync(roleName);

        }
        public async Task<string> EditRoleAsync(EditRoleRequest request)
        {
            //check role is exst or not
            var role = await _roleManager.FindByIdAsync(request.Id.ToString());
            if (role == null)
            {
                return "notFound";
            }
            role.Name = request.Name;
            var result = await _roleManager.UpdateAsync(role);
            //if not exist return notfound
            if (result.Succeeded)
            {
                return "Success";
            }
            var errors = string.Join("_", result.Errors);
            return errors;


        }

        public async Task<string> DeleteRoleAsync(int Id)
        {
            var role = await _roleManager.FindByIdAsync(Id.ToString());
            if (role == null)
                return "NotFound";
            //check if role Exist or Not
            var users = await _userManager.GetUsersInRoleAsync(role.Name);
            //return Exception
            if (users != null && users.Count() > 0)
            {
                return "Used";
            }
            //delete
            var result = await _roleManager.DeleteAsync(role);
            //success
            if (result.Succeeded)
            {
                return "Sucess";
            }
            //Problem
            var errors = string.Join("_", result.Errors);
            return errors;
        }

        public async Task<bool> IsRoleExistById(int Id)
        {
            var role = await _roleManager.FindByIdAsync(Id.ToString());
            if (role == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<List<Role>> GetRolesList()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<Role> GetRolesById(int id)
        {
            return await _roleManager.FindByIdAsync(id.ToString());
        }

        public async Task<UpdateUserRolesResult> ManageUserRolesData(User user)
        {
            var response = new UpdateUserRolesResult();
            var rolesList = new List<UserRoles>();
            //UserRoles

            //Roles
            var roles = await _roleManager.Roles.ToListAsync();
            response.UserId = user.Id;
            foreach (var role in roles)
            {
                var userRole = new UserRoles();
                userRole.Id = role.Id;
                userRole.Name = role.Name;
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRole.HasRole = true;
                }
                else
                {
                    userRole.HasRole = false;
                }
                rolesList.Add(userRole);

            }
            response.userRoles = rolesList;
            return response;

            //If Role Contain Userroles true false

        }

        public async Task<string> UpdateUserRoles(UpdateUserRolesRequest requset)
        {
            var transact = await _applicationDbContext.Database.BeginTransactionAsync();
            try
            {
                //Get User
                var user = await _userManager.FindByIdAsync(requset.UserId.ToString());
                if (user == null)
                {
                    return "UserIsNull";
                }
                //Get User Old Role
                var userRoles = await _userManager.GetRolesAsync(user);
                //Delete OldRole
                var removeResult = await _userManager.RemoveFromRolesAsync(user, userRoles);
                if (!removeResult.Succeeded)
                {
                    return "FaildToRemoveOldRoles";
                }
                var selectedRoles = requset.userRoles.Where(x => x.HasRole == true).Select(x => x.Name).ToString();
                //Add The Roles HasRole=True
                var addRoleResult = await _userManager.AddToRoleAsync(user, selectedRoles);
                if (!addRoleResult.Succeeded)
                {
                    return "FailedToAddNewRoles";
                }

                //Return Result
                return "Success";
                await transact.CommitAsync();
            }
            catch (Exception ex)
            {
                await transact.RollbackAsync();
                return "FailedToUpdateUserRoles";
            }




        }
        public async Task<MangeUserClaimResult> ManageUserClaimData(User user)
        {

            var response = new MangeUserClaimResult();
            var userClaimsList = new List<UserClaims>();
            response.UserId = user.Id;
            var userClaims = await _userManager.GetClaimsAsync(user);
            foreach (var claim in ClaimsStore.claims)
            {
                var userClaim = new UserClaims();
                userClaim.Type = claim.Type;
                if (userClaims.Any(x => x.Type == claim.Type))
                {
                    userClaim.Value = true;
                }
                else
                {
                    userClaim.Value = false;
                }
                userClaimsList.Add(userClaim);
            }
            response.userClaims = userClaimsList;
            return response;
        }

        public async Task<string> UpdateUserClaims(UpdateUserClaimsRequest request)
        {
            var transact = await _applicationDbContext.Database.BeginTransactionAsync();

            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId.ToString());

                if (user == null)
                {
                    return "UserIsNull";
                }
                var userClaims = await _userManager.GetClaimsAsync(user);
                var removeClaimdsResult = await _userManager.RemoveClaimsAsync(user, userClaims);
                if (!removeClaimdsResult.Succeeded)
                {
                    return "FailedToRemmoveOldClaims";
                }
                var claims = request.userClaims.Where(x => x.Value == true).Select(x => new Claim(type: x.Type, value: x.Value.ToString()));
                var addUserClaimResult = await _userManager.AddClaimsAsync(user, claims);
                if (!addUserClaimResult.Succeeded)
                {
                    return "FailedToAddNewClaims";
                }
                await transact.CommitAsync();
                return "Success";

            }
            catch (Exception ex)
            {
                await transact.RollbackAsync();
                return "FailedToUpdateClaims";
            }
        }
    }
}
