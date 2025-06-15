using Microsoft.AspNetCore.Mvc;

namespace CPC_PROJECT.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("/Error")]
        [HttpGet, HttpPost, HttpPut, HttpDelete]
        public IActionResult HandleError()
        {
            return Problem(
                title: "An error occurred",
                statusCode: 500
            );
        }
    }
}
