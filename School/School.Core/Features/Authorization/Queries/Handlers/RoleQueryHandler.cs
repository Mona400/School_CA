using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Authorization.Queries.Models;
using School.Core.Features.Authorization.Queries.Results;
using School.Core.Resources;
using School.Service.Abstracts;

namespace School.Core.Features.Authorization.Queries.Handlers
{
    public class RoleQueryHandler : ResponseHandler,
        IRequestHandler<GetRolesListQuery, Response<List<GetRolesListResult>>>,
        IRequestHandler<GetRoleByIdQuery, Response<GetRoleByIdResult>>

    {
        private readonly IStringLocalizer<SharedResourses> _stringLocalizer;
        private readonly IAuthorizationServices _authorizationService;
        private readonly IMapper _mapper;
        public RoleQueryHandler(IStringLocalizer<SharedResourses> stringLocalizer, IAuthorizationServices authorizationService, IMapper mapper) : base(stringLocalizer)
        {
            _authorizationService = authorizationService;
            _mapper = mapper;
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
    }
}
