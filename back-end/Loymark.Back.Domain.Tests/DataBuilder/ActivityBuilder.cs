using Loymark.Back.Domain.Entities;

namespace Loymark.Back.Domain.Tests
{
    public class ActivityBuilder
    {
        int Id;
        DateTime CreatedDate;
        int UserId;
        string ActivityType;
        User User;

        public ActivityBuilder()
        {
            Id = 1;
            UserId = 1;
            ActivityType = "Test";
            CreatedDate = DateTime.Now;
            User = new UserBuilder().Build();
        }

        public Activity Build()
        {
            return new Activity()
            {
                Id= Id,
                CreatedDate = CreatedDate,
                UserId = UserId,
                ActivityType = ActivityType,
                User = User
            };
        }

        public IEnumerable<Activity> BuildList()
        {
            return new List<Activity>()
            {
                Build(),
                Build(),
                Build()
            };
        }

        public ActivityBuilder WithId(int id)
        {
            Id = id;
            return this;
        }

        public ActivityBuilder WithCreatedDate(DateTime createdDate)
        {
            CreatedDate = createdDate;
            return this;
        }

        public ActivityBuilder WithUserId(int userId)
        {
            UserId = userId;
            return this;
        }

        public ActivityBuilder WithActivityType(string activityType)
        {
            ActivityType = activityType;
            return this;
        }

        public ActivityBuilder WithUser(User user)
        {
            User = user;
            return this;
        }
    }
}
