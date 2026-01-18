using EmployeeMgt.Models;
using EmployeeMgt.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeMgt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
       private readonly IAuthService _authService;  
        public LoginController(IAuthService authService)
        {
            _authService = authService;
        }


        // POST: api/employee/login
        [HttpGet]
        [HttpPost("login")]
        public async Task<IActionResult> Login(string username,string password)
        {
            var result = await _authService.AuthenticateAsync(username, password);

            if (!result.Success)
                return Unauthorized(new { success = false, message = "Invalid credentials" });

            return Ok(new { success = true, token = result.Token });
        }
    }
}
 