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
    public class UserServiceTest
    {
        private IGenericRepository<User> Repository { get; set; } = default!;
        private IGenericRepository<Activity> ActivityRepository { get; set; } = default!;
        private UserService Service { get; set; } = default!;
        private ActivityService ActivityService { get; set; } = default!;
        private UserBuilder Builder { get; set; } = default!;

        [TestInitialize]
        public void Initialize()
        {
            Repository = Substitute.For<IGenericRepository<User>>();
            ActivityRepository = Substitute.For<IGenericRepository<Activity>>();

            ActivityService = new(ActivityRepository);
            Service = new(
                Repository,
                ActivityService
            );

            Builder = new();
        }

        [TestMethod]
        public async Task GetUsersAsync_Empty()
        {
            // Arrange
            IEnumerable<User> users = Array.Empty<User>();
            
            Repository.GetAsync()
                .ReturnsForAnyArgs(users);

            // Act
            IEnumerable<User> usersResponse = await Service.GetUsersAsync();

            // Assert
            Assert.AreEqual(users, usersResponse);
            Assert.AreEqual(users.Count(), usersResponse.Count());
            await Repository.ReceivedWithAnyArgs(1)
                .GetAsync();
        }

        [TestMethod]
        public async Task GetUsersAsync_Ok()
        {
            // Arrange
            IEnumerable<User> users = Builder.BuildList();

            Repository.GetAsync()
                .ReturnsForAnyArgs(users);

            // Act
            IEnumerable<User> usersResponse = await Service.GetUsersAsync();

            // Assert
            Assert.AreEqual(users, usersResponse);
            Assert.AreEqual(users.Count(), usersResponse.Count());
            await Repository.ReceivedWithAnyArgs(1)
                .GetAsync();
        }

        [TestMethod]
        public async Task GetUserByIdAsync_Ok()
        {
            // Arrange
            User user = Builder.Build();

            Repository.GetByIdAsync(user.Id)
                .ReturnsForAnyArgs(user);

            // Act
            User userResponse = await Service.GetUserByIdAsync(user.Id);

            // Assert
            Assert.AreEqual(user, userResponse);
            await Repository.ReceivedWithAnyArgs(1)
                .GetByIdAsync(Arg.Any<int>());
        }

        [TestMethod]
        public async Task CreateUserAsync_Ok()
        {
            // Arrange
            User user = Builder
                .WithId(0)
                .Build();

            Repository.AddAsync(user)
                .ReturnsForAnyArgs(user);

            ActivityRepository.AddAsync(Arg.Any<Activity>())
                .ReturnsNullForAnyArgs();

            // Act
            User userResponse = await Service.CreateUserAsync(user);

            // Assert
            Assert.AreEqual(user, userResponse);
            await Repository.ReceivedWithAnyArgs(1)
                .AddAsync(Arg.Any<User>());
            await ActivityRepository.ReceivedWithAnyArgs(1)
                .AddAsync(Arg.Any<Activity>());
        }

        [TestMethod]
        public async Task UpdateUserAsync_Ok()
        {
            // Arrange
            User user = Builder
                .WithLastName("Suarez")
                .Build();

            Repository.UpdateAsync(user)
                .ReturnsForAnyArgs(user);

            ActivityRepository.AddAsync(Arg.Any<Activity>())
                .ReturnsNullForAnyArgs();

            // Act
            User userResponse = await Service.UpdateUserAsync(user);

            // Assert
            Assert.AreEqual(user, userResponse);
            await Repository.ReceivedWithAnyArgs(1)
                .UpdateAsync(Arg.Any<User>());
            await ActivityRepository.ReceivedWithAnyArgs(1)
                .AddAsync(Arg.Any<Activity>());
        }

        [TestMethod]
        public async Task DeleteUserAsync_Ok()
        {
            // Arrange
            User user = Builder.Build();

            Repository.DeleteAsync(user => user.Id == user.Id)
                .ReturnsForAnyArgs(Task.CompletedTask);

            ActivityRepository.AddAsync(Arg.Any<Activity>())
                .ReturnsNullForAnyArgs();

            // Act
            Task task = Service.DeleteUserAsync(user.Id);
            task.Wait();

            // Assert
            Assert.IsTrue(task.IsCompleted);
            await Repository.ReceivedWithAnyArgs(1)
                .DeleteAsync(Arg.Any<Expression<Func<User, bool>>>());
            await ActivityRepository.ReceivedWithAnyArgs(1)
                .AddAsync(Arg.Any<Activity>());
        }
    }
}
