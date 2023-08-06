using School.Core.Features.AppicationUser.Commands.Models;
using School.Data.Entities.Identity;

namespace School.Core.Mapping.ApplicationUser
{
    public partial class ApplicationUserProfile
    {
        public void AddUserMapping()
        {
            CreateMap<AddUserCommand, User>();



        }
    }
}
