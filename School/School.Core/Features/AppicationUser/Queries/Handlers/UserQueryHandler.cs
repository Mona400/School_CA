using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.AppicationUser.Queries.Models;
using School.Core.Features.AppicationUser.Queries.Results;
using School.Core.Resources;
using School.Core.Wrappers;
using School.Data.Entities.Identity;

namespace School.Core.Features.AppicationUser.Queries.Handlers
{
    public class UserQueryHandler : ResponseHandler,
        IRequestHandler<GetUserPaginationQuery, PaginatedResult<GetUserPaginationListResponse>>,
        IRequestHandler<GetUserByIdQuery, Response<GetUserByIdResponse>>

    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResourses> _stringLocalizer;
        private readonly UserManager<User> _userManager;
        public UserQueryHandler(IMapper mapper, IStringLocalizer<SharedResourses> stringLocalizer, UserManager<User> userManager) : base(stringLocalizer)
        {
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
        }

        public async Task<PaginatedResult<GetUserPaginationListResponse>> Handle(GetUserPaginationQuery request, CancellationToken cancellationToken)
        {
            var users = _userManager.Users.AsQueryable();
            var PaginatedList = await _mapper.ProjectTo<GetUserPaginationListResponse>(users).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return PaginatedList;


        }

        public async Task<Response<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (user == null) return NotFound<GetUserByIdResponse>(_stringLocalizer[SharedResoursesKeys.NotFound]);
            var result = _mapper.Map<GetUserByIdResponse>(user);
            return Success(result);

        }
    }
}
