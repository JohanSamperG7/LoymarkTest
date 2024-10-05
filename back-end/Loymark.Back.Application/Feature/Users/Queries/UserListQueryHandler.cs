using AutoMapper;
using Loymark.Back.Application.DTOs;
using Loymark.Back.Domain.Services;
using MediatR;

namespace Loymark.Back.Application.Feature.Users.Queries
{
    public class UserListQueryHandler
        : IRequestHandler<UserListQuery, IEnumerable<UserDto>>
    {
        private readonly UserService service;
        private readonly IMapper mapper;

        public UserListQueryHandler(
            UserService service,
            IMapper mapper
        )
        {
            this.service = service;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> Handle(
            UserListQuery query,
            CancellationToken cancellationToken
        ) => mapper.Map<IEnumerable<UserDto>>(await service.GetUsersAsync());
    }
}
