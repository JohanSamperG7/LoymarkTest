using Loymark.Back.Domain.Services;
using MediatR;

namespace Loymark.Back.Application.Feature.Users.Commands
{
    public class UserDeleteCommandHandler
        : AsyncRequestHandler<UserDeleteCommand>
    {
        private readonly UserService service;

        public UserDeleteCommandHandler(
            UserService service
        )
        {
            this.service = service;
        }

        protected override async Task Handle(
            UserDeleteCommand command,
            CancellationToken cancellationToken
        )
        {
            await service.DeleteUserAsync(command.Id);
        }
    }
}
