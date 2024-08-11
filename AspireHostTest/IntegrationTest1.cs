using Microsoft.Extensions.DependencyInjection;
using Projects;
using System.Net;

namespace AspireHostTest.Tests
{
    public class IntegrationTest1
    {
        // Instructions:
        // 1. Add a project reference to the target AppHost project, e.g.:
        //
        //    <ItemGroup>
        //        <ProjectReference Include="../MyAspireApp.AppHost/MyAspireApp.AppHost.csproj" />
        //    </ItemGroup>
        //
        // 2. Uncomment the following example test and update 'Projects.MyAspireApp_AppHost' to match your AppHost project:
        // 
        [Fact]
        public async Task GetWebResourceRootReturnsOkStatusCode()
        {
            // Arrange
            var appHost = await DistributedApplicationTestingBuilder.CreateAsync<Projects.AspireAppHost>();
            await using var app = await appHost.BuildAsync();
            await app.StartAsync();

            // Act
            var httpClient = app.CreateHttpClient(nameof(WebAppB));
            var response = await httpClient.GetAsync("/api/webappb");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            
        }
    }
}