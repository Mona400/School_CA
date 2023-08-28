using School.Core.Features.Authorization.Queries.Results;
using School.Data.Entities.Identity;

namespace School.Core.Mapping.Roles
{
    public partial class RoleProfile
    {

        public void GetRoleByIdMapping()
        {
            CreateMap<Role, GetRoleByIdResult>();

        }
    }
}
