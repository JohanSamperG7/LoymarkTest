using Loymark.Back.Application.DTOs;
using Loymark.Back.Application.Feature.Users.Commands;
using Loymark.Back.Domain.Entities;
using Loymark.Back.WebApi.Tests.Helpers;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using System.Net;
using Xunit;

namespace Loymark.Back.WebApi.Tests.Users
{
    [Collection("TestCollection")]
    public class UserControllerTest
    {
        private const string PARAMETER_PATH = "api/User";

        private readonly IntegrationTestBuilder<Program> factory;
        private readonly HttpClient httpClient;
        private readonly UserBuilder builder;

        public UserControllerTest(
            IntegrationTestBuilder<Program> factory
        )
        {
            this.factory = factory;

            httpClient = this.factory.CreateClient();
            httpClient.Timeout = TimeSpan.FromMinutes(5);

            builder = new();
        }

        [Fact]
        public async Task GetAllUsersAsync_Ok()
        {
            // Arrange
            HttpRequestMessage requestMessageGet = new(
                HttpMethod.Get,
                new Uri(PARAMETER_PATH, UriKind.Relative)
            );

            await this.CreateUserAsync_Ok();

            // Act
            HttpResponseMessage resultGet = await httpClient
                .SendAsync(requestMessageGet);

            resultGet.EnsureSuccessStatusCode();

            string data = await resultGet.Content
                .ReadAsStringAsync();

            IEnumerable<UserDto> userList = JsonConvert
                .DeserializeObject<IEnumerable<UserDto>>(data)!;

            // Assert
            Assert.Equal(HttpStatusCode.OK, resultGet.StatusCode);
            Assert.NotNull(userList);
            Assert.True(userList.Any());
        }

        [Fact]
        public async Task CreateUserAsync_Ok()
        {
            // Arrange
            UserCreateCommand createCommand = builder
                .WithName("Johan")
                .BuildCreateCommand();

            HttpRequestMessage requestMessagePost = new(HttpMethod.Post, new Uri(PARAMETER_PATH, UriKind.Relative))
            {
                Content = StringContentHelper.GetStringContent(
                    System.Text.Json.JsonSerializer.Serialize(createCommand)
                )
            };

            // Act
            HttpResponseMessage resultPost = await httpClient
                .SendAsync(requestMessagePost);

            resultPost.EnsureSuccessStatusCode();

            string dataPost = await resultPost.Content
                .ReadAsStringAsync();

            UserDto userPost = JsonConvert
                .DeserializeObject<UserDto>(dataPost)!;

            HttpRequestMessage requestMessageGetById = new(
                HttpMethod.Get,
                new Uri($"{PARAMETER_PATH}/byId/{userPost.Id}", UriKind.Relative)
            );

            HttpResponseMessage resultGetById = await httpClient
                .SendAsync(requestMessageGetById);

            resultGetById.EnsureSuccessStatusCode();

            string dataGet = await resultGetById.Content
                .ReadAsStringAsync();

            UserDto userGet = JsonConvert
                .DeserializeObject<UserDto>(dataGet)!;

            // Assert
            Assert.Equal(HttpStatusCode.OK, resultPost.StatusCode);
            Assert.Equal(HttpStatusCode.OK, resultGetById.StatusCode);
            Assert.NotNull(userPost);
            Assert.NotNull(userGet);
            Assert.Equal(userPost.Id, userGet.Id);
            Assert.Equal(userPost.Name, userGet.Name);
        }

        [Fact]
        public async Task UpdateUserAsync_Ok()
        {
            // Arrange
            UserUpdateCommand updateCommand = builder
                .WithId(1)
                .WithName("prueba")
                .BuildUpdateCommand();

            HttpRequestMessage requestMessageGet = new(
                HttpMethod.Get,
                new Uri($"{PARAMETER_PATH}/byId/{updateCommand.Id}", UriKind.Relative)
            );

            // Act
            HttpResponseMessage resultGet = await httpClient
                .SendAsync(requestMessageGet);

            resultGet.EnsureSuccessStatusCode();

            string dataGet = await resultGet.Content
                .ReadAsStringAsync();

            UserDto userGet = JsonConvert
                .DeserializeObject<UserDto>(dataGet)!;

            HttpRequestMessage requestMessagePatch = new(HttpMethod.Patch, new Uri(PARAMETER_PATH, UriKind.Relative))
            {
                Content = StringContentHelper.GetStringContent(
                    System.Text.Json.JsonSerializer.Serialize(updateCommand)
                )
            };

            HttpResponseMessage resultPatch = await httpClient
                .SendAsync(requestMessagePatch);

            resultPatch.EnsureSuccessStatusCode();

            string dataPatch = await resultPatch.Content
                .ReadAsStringAsync();

            UserDto userPatch = JsonConvert
                .DeserializeObject<UserDto>(dataPatch)!;

            // Assert
            Assert.Equal(HttpStatusCode.OK, resultPatch.StatusCode);
            Assert.Equal(HttpStatusCode.OK, resultGet.StatusCode);
            Assert.NotNull(userPatch);
            Assert.NotNull(userGet);
            Assert.Equal(userPatch.Id, userGet.Id);
            Assert.NotEqual(userPatch.Name, userGet.Name);
        }

        [Fact]
        public async Task DeleteUserByIdAsync_Ok()
        {
            // Arrange
            int id = 1;

            UserDeleteCommand deleteCommand = builder
                .WithId(id)
                .BuildDeleteCommand();

            HttpRequestMessage requestMessageGet = new(
                HttpMethod.Get,
                new Uri($"{PARAMETER_PATH}/byId/{id}", UriKind.Relative)
            );

            // Act
            HttpResponseMessage resultGet = await httpClient
                .SendAsync(requestMessageGet);

            resultGet.EnsureSuccessStatusCode();

            string dataGet = await resultGet.Content
                .ReadAsStringAsync();

            UserDto userGet = JsonConvert
                .DeserializeObject<UserDto>(dataGet)!;

            HttpRequestMessage requestMessageDelete = new(HttpMethod.Delete, new Uri($"{PARAMETER_PATH}/{userGet.Id}", UriKind.Relative))
            {
                Content = StringContentHelper.GetStringContent(
                    System.Text.Json.JsonSerializer.Serialize(deleteCommand)
                )
            };

            HttpResponseMessage resultDelete = await httpClient
                .SendAsync(requestMessageDelete);

            resultDelete.EnsureSuccessStatusCode();

            HttpRequestMessage requestMessageGetAfterDelete = new(
                HttpMethod.Get,
                new Uri($"{PARAMETER_PATH}/byId/{id}", UriKind.Relative)
            );

            HttpResponseMessage resultGetAfterDelete = await httpClient
                .SendAsync(requestMessageGetAfterDelete);

            resultGetAfterDelete.EnsureSuccessStatusCode();

            string responseGetAfter = await resultGetAfterDelete.Content
                .ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, resultGet.StatusCode);
            Assert.Equal(HttpStatusCode.OK, resultDelete.StatusCode);
            Assert.Equal(HttpStatusCode.NoContent, resultGetAfterDelete.StatusCode);
            Assert.Equal(id, userGet.Id);
            Assert.True(string.IsNullOrEmpty(responseGetAfter));
        }
    }
}
