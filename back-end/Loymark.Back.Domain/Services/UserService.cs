using Loymark.Back.Domain.Entities;
using Loymark.Back.Domain.Enums;
using Loymark.Back.Domain.Helper;
using Loymark.Back.Domain.Ports;

namespace Loymark.Back.Domain.Services
{
    public class UserService
    {
        private readonly IGenericRepository<User> _repository;
        private readonly ActivityService _activityService;

        public UserService(
            IGenericRepository<User> repository,
            ActivityService activityService
        )
        {
            _repository = repository;
            _activityService = activityService;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _repository.GetAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            User userSaved = await _repository.AddAsync(user);
            await CreateHistoryActivityByUser(activityType: ActivityType.CreateUser, userId: userSaved.Id);
            return userSaved;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            User userUpdated = await _repository.UpdateAsync(user);
            await CreateHistoryActivityByUser(activityType: ActivityType.UpdateUser, userId: userUpdated.Id);
            return userUpdated;
        }

        public async Task DeleteUserAsync(int id)
        {
            await CreateHistoryActivityByUser(activityType: ActivityType.DeleteUser, userId: id);
            await _repository.DeleteAsync(user => user.Id == id);
        }

        private async Task CreateHistoryActivityByUser(ActivityType activityType, int userId)
        {
            await _activityService.CreateActivityAsync(
                new Activity()
                {
                    ActivityType = activityType.GetDescription(),
                    CreatedDate = DateTime.Now,
                    UserId = userId
                }
            );
        }
    }
}
