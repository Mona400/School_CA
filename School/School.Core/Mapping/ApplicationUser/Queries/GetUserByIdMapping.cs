using School.Core.Features.AppicationUser.Queries.Results;
using School.Data.Entities.Identity;

namespace School.Core.Mapping.ApplicationUser
{
    public partial class ApplicationUserProfile
    {
        public void GetUserByIdMapping()
        {
            CreateMap<User, GetUserByIdResponse>();


        }
    }
}
