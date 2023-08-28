using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.AppicationUser.Commands.Models;
using School.Core.Resources;
using School.Data.Entities.Identity;

namespace School.Core.Features.AppicationUser.Commands.Handlers
{
    public class UserCommandHandler : ResponseHandler,
        IRequestHandler<AddUserCommand, Response<string>>,
        IRequestHandler<EditUserCommand, Response<string>>,
        IRequestHandler<DeleteUserCommand, Response<string>>,
        IRequestHandler<ChangeUserPasswordCommand, Response<string>>

    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResourses> _stringLocalizer;
        private readonly UserManager<User> _userManager;


        public UserCommandHandler(IStringLocalizer<SharedResourses> stringLocalizer, IMapper mapper, UserManager<User> userManager) : base(stringLocalizer)
        {
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
        }

        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            //If Email Is Exist
            var user = await _userManager.FindByEmailAsync(request.Email);
            //Email is Exist
            if (user != null)
            {
                return BadRequest<string>(_stringLocalizer[SharedResoursesKeys.EmailIsExist]);
            }
            var userByUserName = await _userManager.FindByNameAsync(request.UserName);
            //username is exist
            if (userByUserName != null)
            {
                return BadRequest<string>(_stringLocalizer[SharedResoursesKeys.UserNameIsExist]);
            }
            //Mapping
            var IdentityUser = _mapper.Map<User>(request);
            //Create
            var createResult = await _userManager.CreateAsync(IdentityUser, request.Password);
            //Failed
            if (!createResult.Succeeded)
            {
                return BadRequest<string>(createResult.Errors.FirstOrDefault().Description);
            }
            //Message
            var users = await _userManager.Users.ToListAsync();

            await _userManager.AddToRoleAsync(IdentityUser, "User");




            //create
            return Created("");
            //success
        }

        public async Task<Response<string>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            //Check if user exist
            var olduser = await _userManager.FindByIdAsync(request.Id.ToString());
            //If not found
            if (olduser == null) return NotFound<string>();
            //mapping
            var newUsr = _mapper.Map(request, olduser);

            var userByUserName = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == newUsr.UserName && x.Id != newUsr.Id);
            //username is exist
            if (userByUserName != null)
            {
                return BadRequest<string>(_stringLocalizer[SharedResoursesKeys.UserNameIsExist]);
            }

            //update
            var result = await _userManager.UpdateAsync(newUsr);
            //result is not success
            if (!result.Succeeded) return BadRequest<string>(_stringLocalizer[SharedResoursesKeys.UpdateFailed]);
            //message
            return Success((string)_stringLocalizer[SharedResoursesKeys.Updated]);
        }

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            //check if user exist
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            //if not exist not found
            if (user == null) return NotFound<string>();
            //delete the user
            var result = await _userManager.DeleteAsync(user);
            //in case of failure
            if (!result.Succeeded) return BadRequest<string>(_stringLocalizer[SharedResoursesKeys.DeleteFailed]);

            //message
            return Success((string)_stringLocalizer[SharedResoursesKeys.Deleted]);
        }

        public async Task<Response<string>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            //get user
            //check if user is exist
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            //if not exist nofound
            if (user == null) return NotFound<string>();
            //check user password
            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            //var user1=await _userManager.HasPasswordAsync(user);
            //await _userManager.RemovePasswordAsync(user);
            if (!result.Succeeded) return BadRequest<string>(result.Errors.FirstOrDefault().Description);

            //message
            return Success((string)_stringLocalizer[SharedResoursesKeys.Success]);

        }
    }
}
