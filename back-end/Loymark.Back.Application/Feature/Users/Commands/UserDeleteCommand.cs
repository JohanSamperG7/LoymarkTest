using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Loymark.Back.Application.Feature.Users.Commands
{
    public record UserDeleteCommand(
        [Required] int Id
    ) : IRequest;
}
