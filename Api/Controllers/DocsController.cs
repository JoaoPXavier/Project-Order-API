using Microsoft.AspNetCore.Mvc;

namespace OrdersApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocsController : ControllerBase
    {
        [HttpGet("swagger-ui")]
        public IActionResult SwaggerUi()
        {
            var html = System.IO.File.ReadAllText("swagger-orders.html");
            return Content(html, "text/html");
        }
    }
}
