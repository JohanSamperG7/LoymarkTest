using AutoMapper;
using Loymark.Back.Application.DTOs;
using Loymark.Back.Domain.Entities;
using Loymark.Back.Domain.Services;
using MediatR;

namespace Loymark.Back.Application.Feature.Users.Commands
{
    public class UserUpdateCommandHandler
        : IRequestHandler<UserUpdateCommand, UserDto>
    {
        private readonly UserService service;
        private readonly IMapper mapper;

        public UserUpdateCommandHandler(
            UserService service,
            IMapper mapper
        )
        {
            this.service = service;
            this.mapper = mapper;
        }

        public async Task<UserDto> Handle(
            UserUpdateCommand command,
            CancellationToken cancellationToken
        )
        {
            return mapper.Map<UserDto>(
                await service.UpdateUserAsync(
                    new User(
                        command.Id,
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
