using Loymark.Back.Application.DTOs;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Loymark.Back.Application.Feature.Users.Commands
{
    public record UserUpdateCommand(
        [Required] int Id,
        [Required] string Name,
        [Required] string LastName,
        [Required] string Email,
        [Required] DateTime BirthDay,
        [Required] string CountryCode,
        [Required] bool ReceiveInformation,
        long? Number
    ) : IRequest<UserDto>;
}
