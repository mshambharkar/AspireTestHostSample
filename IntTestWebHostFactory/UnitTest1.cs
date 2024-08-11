using Microsoft.AspNetCore.Mvc.Testing;
using System.Collections.Concurrent;
using System.Net.Http;
using WebAppB.Controllers;

namespace IntTestWebHostFactory
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test1()
        {
            using var factory = new WebApplicationFactory<WebAppA.Program>();
            var httpclient = factory.CreateClient();
             ConcurrentDictionary<string, HttpClient> HttpClients  = new();
            HttpClients.TryAdd(nameof(WebAppBController),httpclient);

            using var factoryB = new WebApplicationFactory<WebAppB.Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.AddSingleton<IHttpClientFactory>(new CustomHttpClientFactory(HttpClients));
                    //services.AddHttpClient(nameof(WebAppBController),(client)=> client=httpclient);
                    });

                });
            

            var resp = await httpclient.GetStringAsync("api/WebAppA");
            Assert.IsNotNull(resp);
            Assert.That("WebAppA".Equals(resp));

            var httpBclient = factoryB.CreateClient();

            var respB = await httpBclient.GetStringAsync("api/WebAppB");
            Assert.IsNotNull(resp);

        }
    }

    internal sealed class CustomHttpClientFactory : IHttpClientFactory
    {
        // Takes dictionary storing named HTTP clients in constructor
        public CustomHttpClientFactory(
            IReadOnlyDictionary<string, HttpClient> httpClients)
        {
            HttpClients = httpClients;
        }

        private IReadOnlyDictionary<string, HttpClient> HttpClients { get; }

        // Provides named HTTP client from dictionary
        public HttpClient CreateClient(string name) =>
            HttpClients.GetValueOrDefault(name)
            ?? throw new InvalidOperationException(
                $"HTTP client is not found for client with name {name}");
    }
}