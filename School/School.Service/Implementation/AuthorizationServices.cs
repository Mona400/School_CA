using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using School.Data.Dtos;
using School.Data.Entities.Identity;
using School.Service.Abstracts;

namespace School.Service.Implementation
{

    public class AuthorizationServices : IAuthorizationServices
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        public AuthorizationServices(RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
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
    }
}
