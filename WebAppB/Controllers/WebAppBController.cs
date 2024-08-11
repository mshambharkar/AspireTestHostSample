using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAppB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebAppBController : ControllerBase
    {
        private readonly IHttpClientFactory httpClientFactory;

        public WebAppBController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var httpClient = this.httpClientFactory.CreateClient(nameof(WebAppBController));
            var resp = await httpClient.GetAsync("api/WebAppA");
            resp.EnsureSuccessStatusCode();

            return new OkObjectResult(new
            {
                Current = "WebAppB",
                Reference = await resp.Content.ReadAsStringAsync(),
                BaseAddress=httpClient.BaseAddress
            });
        }
    }
}
