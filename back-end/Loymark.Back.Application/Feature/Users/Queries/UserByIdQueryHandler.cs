using AutoMapper;
using Loymark.Back.Application.DTOs;
using Loymark.Back.Domain.Services;
using MediatR;

namespace Loymark.Back.Application.Feature.Users.Queries
{
    public class UserByIdQueryHandler
        : IRequestHandler<UserByIdQuery, UserDto>
    {
        private readonly UserService service;
        private readonly IMapper mapper;

        public UserByIdQueryHandler(
            UserService service,
            IMapper mapper
        )
        {
            this.service = service;
            this.mapper = mapper;
        }

        public async Task<UserDto> Handle(
            UserByIdQuery query,
            CancellationToken cancellationToken
        ) => mapper.Map<UserDto>(await service.GetUserByIdAsync(query.Id));
    }
}
