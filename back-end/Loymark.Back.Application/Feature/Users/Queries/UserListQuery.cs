using Loymark.Back.Application.DTOs;
using MediatR;

namespace Loymark.Back.Application.Feature.Users.Queries
{
    public record UserListQuery 
        : IRequest<IEnumerable<UserDto>>;
}
