using AutoMapper;

namespace School.Core.Mapping.Roles
{
    public partial class RoleProfile : Profile
    {
        public RoleProfile()
        {
            GetRoleListMapping();
            GetRoleByIdMapping();
        }
    }
}
