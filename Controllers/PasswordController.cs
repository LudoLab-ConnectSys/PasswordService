using Microsoft.AspNetCore.Mvc;

namespace PasswordService.Controllers
{
    [Route("password")]
    [ApiController]
    public class PasswordController : ControllerBase
    {
        private readonly Services.PasswordService _passwordService;

        public PasswordController(Services.PasswordService passwordService)
        {
            _passwordService = passwordService;
        }

        [HttpGet("lastlogin/{userId}")]
        public async Task<IActionResult> GetLastLogin(int userId)
        {
            try
            {
                var lastLogin = await _passwordService.GetLastLoginAsync(userId);
                if (lastLogin == null)
                {
                    return Ok(new { Message = "El usuario nunca ha iniciado sesión." });
                }

                return Ok(new { LastLogin = lastLogin });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("changepassword")]
        public async Task<IActionResult> ChangePassword(int userId, [FromBody] string newPassword)
        {
            try
            {
                await _passwordService.ChangePasswordAsync(userId, newPassword);
                return Ok(new { Message = "Contraseña cambiada exitosamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}