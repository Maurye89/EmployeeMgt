using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeMgt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {
        [HttpGet]
        [Route("Health")]
        public IActionResult Ping()
        {
            return Ok("Pong");
        }
    }
}
