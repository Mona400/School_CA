using AutoMapper;

namespace School.Core.Mapping.ApplicationUser
{
    public partial class ApplicationUserProfile : Profile
    {
        public ApplicationUserProfile()
        {
            AddUserMapping();
            UpdateUserMapping();
            GetUserPaginationMapping();
            GetUserByIdMapping();

        }

    }
}
