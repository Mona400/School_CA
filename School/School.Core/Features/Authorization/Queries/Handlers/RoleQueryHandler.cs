using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Authorization.Commands.Models;
using School.Core.Features.Authorization.Queries.Models;
using School.Core.Features.Authorization.Queries.Results;
using School.Core.Resources;
using School.Data.Entities.Identity;
using School.Data.Results;
using School.Service.Abstracts;

namespace School.Core.Features.Authorization.Queries.Handlers
{
    public class RoleQueryHandler : ResponseHandler,
        IRequestHandler<GetRolesListQuery, Response<List<GetRolesListResult>>>,
        IRequestHandler<GetRoleByIdQuery, Response<GetRoleByIdResult>>,
        IRequestHandler<ManageUserRolesQuery, Response<UpdateUserRolesResult>>,
        IRequestHandler<UpdatetUserRolesCommand, Response<string>>

    {
        private readonly IStringLocalizer<SharedResourses> _stringLocalizer;
        private readonly IAuthorizationServices _authorizationService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public RoleQueryHandler(IStringLocalizer<SharedResourses> stringLocalizer, IAuthorizationServices authorizationService, IMapper mapper, UserManager<User> userManager) : base(stringLocalizer)
        {
            _authorizationService = authorizationService;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
        }

        public async Task<Response<List<GetRolesListResult>>> Handle(GetRolesListQuery request, CancellationToken cancellationToken)
        {
            var roles = await _authorizationService.GetRolesList();
            var result = _mapper.Map<List<GetRolesListResult>>(roles);
            return Success(result);

        }

        public async Task<Response<GetRoleByIdResult>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _authorizationService.GetRolesById(request.Id);
            if (role == null)
            {
                return NotFound<GetRoleByIdResult>(_stringLocalizer[SharedResoursesKeys.RoleNotExist]);
            }
            var result = _mapper.Map<GetRoleByIdResult>(role);
            return Success(result);

        }

        public async Task<Response<UpdateUserRolesResult>> Handle(ManageUserRolesQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null) { return NotFound<UpdateUserRolesResult>(_stringLocalizer[SharedResoursesKeys.UserIsNotFound]); }
            var result = await _authorizationService.ManageUserRolesData(user);
            return Success(result);
        }

        public Task<Response<string>> Handle(UpdatetUserRolesCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
