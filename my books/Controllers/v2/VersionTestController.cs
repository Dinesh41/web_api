using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace my_books.Controllers.v2
{
    [ApiVersion("2.0")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    [ApiController]
    public class VersionTestController : ControllerBase
    {
        [HttpGet("get-data")]
        public IActionResult GetData()
        {
            return Ok("Getting data form version v2");
        }
    }
}
