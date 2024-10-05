using Loymark.Back.Application.DTOs;
using Loymark.Back.Application.Feature.Activities.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Loymark.Back.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ActivityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<ActivityDto>> GetAllActivitiesAsync()
        {
            return await _mediator.Send(new ActivityListQuery());
        }
    }
}
