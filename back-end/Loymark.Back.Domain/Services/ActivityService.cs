using Loymark.Back.Domain.Entities;
using Loymark.Back.Domain.Ports;

namespace Loymark.Back.Domain.Services
{
    public class ActivityService
    {
        private readonly IGenericRepository<Activity> _repository;

        public ActivityService(IGenericRepository<Activity> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Activity>> GetActivitiesAsync()
        {
            return await _repository
                .GetAsync(
                    includeStringProperties: $"{nameof(Activity.User)}",
                    orderBy: activity => activity.OrderByDescending(activity => activity.CreatedDate)
                );
        }

        public async Task<Activity> CreateActivityAsync(Activity activity)
        {
            return await _repository.AddAsync(activity);
        }
    }
}
