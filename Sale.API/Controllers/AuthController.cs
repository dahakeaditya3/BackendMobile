using Microsoft.AspNetCore.Mvc;
using Sale.BAL.Services;
using Sale.Models.DTOs;

namespace Sale.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _service;
        public AuthController(AuthService service) => _service = service;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            try
            {
                var result = await _service.LoginAsync(dto);
                return result == null ? Unauthorized("Invalid credentials.") : Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
