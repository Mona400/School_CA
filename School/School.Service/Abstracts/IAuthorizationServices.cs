using School.Data.Dtos;
using School.Data.Entities.Identity;
using School.Data.Requests;
using School.Data.Results;

namespace School.Service.Abstracts
{
    public interface IAuthorizationServices
    {
        public Task<string> AddRoleAsync(string roleName);
        public Task<string> EditRoleAsync(EditRoleRequest request);
        public Task<string> DeleteRoleAsync(int Id);
        public Task<bool> IsRoleExistByName(string roleName);
        public Task<bool> IsRoleExistById(int Id);
        public Task<List<Role>> GetRolesList();
        public Task<Role> GetRolesById(int id);
        public Task<UpdateUserRolesResult> ManageUserRolesData(User user);
        public Task<string> UpdateUserRoles(UpdateUserRolesRequest request);
        public Task<string> UpdateUserClaims(UpdateUserClaimsRequest request);
        public Task<MangeUserClaimResult> ManageUserClaimData(User user);


    }
}
