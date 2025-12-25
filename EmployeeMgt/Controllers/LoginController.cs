using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeMgt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        // POST: api/employee/login
        [HttpGet]
        public IActionResult Login()
        {
            try
            {
                // Placeholder implementation: always return success
                return Ok(new { success = true, message = "Login successful" });
            }
            catch (Exception ex)
            {
                // Return generic server error with minimal details
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { success = false, message = "Login failed", error = ex.Message });
            }
        }
    }
}
 