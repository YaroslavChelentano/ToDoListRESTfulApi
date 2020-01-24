using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using TasksListAPI.Contracts.V1;
using TasksListAPI.Contracts.V1.Requests;
using TasksListAPI.Contracts.V1.Responses;
using TasksListAPI.Services;
using Xunit;
namespace XUnitTestProject.Tests
{
    public class CustomTasksControllerTests : IntegrationTest
    {
        [Fact]
        public async Task GetAll_WithoutAnyCustomTasks_ReturnsEmptyResponse()
        {
            // Arrange
            await AuthenticateAsync();


            // Act
            var response = await TestClient.GetAsync(ApiRoutes.CustomTask.GetAll);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            (await response.Content.ReadAsAsync<List<CustomTaskResponse>>()).Should().BeEmpty();
        }

        [Fact]
        public async Task Get_ReturnsCustomTask_WhenCustomTaskExistsInTheDatabase()
        {
            // Arrange
            await AuthenticateAsync();
            var createdTask = await TestClient.PostAsJsonAsync(ApiRoutes.CustomTask.Create, new CreateCustomTaskRequest
            {
                Title="Hello",
                Description="World"
            });

            // Act
            var response = await TestClient.GetAsync(ApiRoutes.CustomTask.Get.Replace("{customTaskTitle}", createdTask.Content.ReadAsAsync<CustomTaskResponse>().Result.Title));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.ReadAsAsync<CustomTaskResponse>().Result.Title.Should().Be("Hello");
        }
    }
}
