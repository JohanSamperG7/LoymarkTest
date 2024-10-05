using Loymark.Back.Application.DTOs;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Loymark.Back.Application.Feature.Users.Queries
{
    public record UserByIdQuery(
        [Required] int Id
    ) : IRequest<UserDto>;
}
