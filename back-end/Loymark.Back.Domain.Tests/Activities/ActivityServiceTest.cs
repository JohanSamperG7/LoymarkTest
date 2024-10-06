using Loymark.Back.Domain.Entities;
using Loymark.Back.Domain.Ports;
using Loymark.Back.Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NSubstitute.ReturnsExtensions;
using System.Linq.Expressions;

namespace Loymark.Back.Domain.Tests.Users
{
    [TestClass]
    public class ActivityServiceTest
    {
        private IGenericRepository<Activity> Repository { get; set; } = default!;
        private ActivityService Service { get; set; } = default!;
        private ActivityBuilder Builder { get; set; } = default!;

        [TestInitialize]
        public void Initialize()
        {
            Repository = Substitute.For<IGenericRepository<Activity>>();

            Service = new(Repository);

            Builder = new();
        }

        [TestMethod]
        public async Task GetActivitiesAsync_Empty()
        {
            // Arrange
            IEnumerable<Activity> activities = Array.Empty<Activity>();
            
            Repository
                .GetAsync(
                    includeStringProperties: $"{nameof(Activity.User)}",
                    orderBy: activity => activity.OrderByDescending(activity => activity.CreatedDate)
                ).ReturnsNullForAnyArgs();

            // Act
            IEnumerable<Activity> activityResponse = await Service.GetActivitiesAsync();

            // Assert
            Assert.IsNull(activityResponse);
            await Repository.ReceivedWithAnyArgs(1)
                .GetAsync(
                    Arg.Any<Expression<Func<Activity, bool>>>(),
                    orderBy: Arg.Any<Func<IQueryable<Activity>, IOrderedQueryable<Activity>>>()
                );
        }

        [TestMethod]
        public async Task GetActivitiesAsync_Ok()
        {
            // Arrange
            IEnumerable<Activity> activities = Builder.BuildList();
            
            Repository
                .GetAsync(
                    includeStringProperties: $"{nameof(Activity.User)}",
                    orderBy: activity => activity.OrderByDescending(activity => activity.CreatedDate)
                ).ReturnsForAnyArgs(activities);

            // Act
            IEnumerable<Activity> activityResponse = await Service.GetActivitiesAsync();

            // Assert
            Assert.IsNotNull(activityResponse);
            Assert.AreEqual(activities, activityResponse);
            Assert.AreEqual(activities.Count(), activityResponse.Count());
            await Repository.ReceivedWithAnyArgs(1)
                .GetAsync(
                    Arg.Any<Expression<Func<Activity, bool>>>(),
                    orderBy: Arg.Any<Func<IQueryable<Activity>, IOrderedQueryable<Activity>>>()
                );
        }

        [TestMethod]
        public async Task CreateActivityAsync_Ok()
        {
            // Arrange
            Activity activity = Builder.Build();
            
            Repository.AddAsync(activity)
                .ReturnsForAnyArgs(activity);

            // Act
            Activity activityResponse = await Service.CreateActivityAsync(activity);

            // Assert
            Assert.IsNotNull(activityResponse);
            Assert.AreEqual(activity, activityResponse);
            await Repository.ReceivedWithAnyArgs(1)
                .AddAsync(
                    Arg.Any<Activity>()
                );
        }
    }
}
