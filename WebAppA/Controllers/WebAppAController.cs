using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAppA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebAppAController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return new OkObjectResult("WebAppA");
        }
    }
}
