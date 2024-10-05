using AutoMapper;
using Loymark.Back.Application.DTOs;
using Loymark.Back.Domain.Services;
using MediatR;

namespace Loymark.Back.Application.Feature.Activities.Queries
{
    public class ActivityListQueryHandler
        : IRequestHandler<ActivityListQuery, IEnumerable<ActivityDto>>
    {
        private readonly ActivityService service;
        private readonly IMapper mapper;

        public ActivityListQueryHandler(
            ActivityService service,
            IMapper mapper
        )
        {
            this.service = service;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ActivityDto>> Handle(
            ActivityListQuery query,
            CancellationToken cancellationToken
        ) => mapper.Map<IEnumerable<ActivityDto>>(await service.GetActivitiesAsync());
    }
}
