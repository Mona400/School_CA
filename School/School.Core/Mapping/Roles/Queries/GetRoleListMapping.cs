using School.Core.Features.Authorization.Queries.Results;
using School.Data.Entities.Identity;

namespace School.Core.Mapping.Roles
{
    public partial class RoleProfile
    {

        public void GetRoleListMapping()
        {
            CreateMap<Role, GetRolesListResult>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        }
    }
}
