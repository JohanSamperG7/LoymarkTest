using Loymark.Back.Application.DTOs;
using MediatR;

namespace Loymark.Back.Application.Feature.Activities.Queries
{
    public record ActivityListQuery 
        : IRequest<IEnumerable<ActivityDto>>;
}
