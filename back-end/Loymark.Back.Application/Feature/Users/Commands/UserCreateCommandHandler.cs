using AutoMapper;
using Loymark.Back.Application.DTOs;
using Loymark.Back.Domain.Entities;
using Loymark.Back.Domain.Services;
using MediatR;

namespace Loymark.Back.Application.Feature.Users.Commands
{
    public class UserCreateCommandHandler
        : IRequestHandler<UserCreateCommand, UserDto>
    {
        private readonly UserService service;
        private readonly IMapper mapper;

        public UserCreateCommandHandler(
            UserService service,
            IMapper mapper
        )
        {
            this.service = service;
            this.mapper = mapper;
        }

        public async Task<UserDto> Handle(
            UserCreateCommand command,
            CancellationToken cancellationToken
        )
        {
            return mapper.Map<UserDto>(
                await service.CreateUserAsync(
                    new User(
                        command.Name,
                        command.LastName,
                        command.Email,
                        command.BirthDay,
                        command.CountryCode,
                        command.ReceiveInformation,
                        command.Number
                    )
                )
            );
        }
    }
}
