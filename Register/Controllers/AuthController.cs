using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Register.DTO.Request;
using Register.Service;

namespace Register.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }


        // register new user
        [HttpPost("register")]

        public async Task<IActionResult> Register(UserRequest request)
        {
            try
            {
                var data = await authService.Register(request);
                if (data == null)
                {
                    return NotFound();
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //log in user
        [HttpGet("Log-in-with-email")]

        public async Task<IActionResult> LogIn(string email, string password)
        {
            try
            {
                var data = await authService.LogIn(email, password);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
