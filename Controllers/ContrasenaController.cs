using Microsoft.AspNetCore.Mvc;
using PasswordService.Models;
using PasswordService.Services;

namespace PasswordService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContrasenaController : ControllerBase
    {
        private readonly IContrasenaService _contrasenaService;

        public ContrasenaController(IContrasenaService contrasenaService)
        {
            _contrasenaService = contrasenaService;
        }

        [HttpGet("haIniciadoSesion/{idUsuario}")]
        public async Task<IActionResult> HaIniciadoSesionAntes(int idUsuario)
        {
            try
            {
                var haIniciado = await _contrasenaService.HaIniciadoSesionAntesAsync(idUsuario);
                return Ok(new { HaIniciado = haIniciado });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("cambiarContrasena")]
        public async Task<IActionResult> CambiarContrasena(int idUsuario, [FromBody] ContrasenaRequest request)
        {
            try
            {
                await _contrasenaService.CambiarContrasenaAsync(idUsuario, request.NuevaContrasena);
                return Ok(new { Message = "Contraseña cambiada exitosamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("obtenerIdUsuarioPorCedula/{cedulaUsuario}")]
        public async Task<IActionResult> ObtenerIdUsuarioPorCedula(string cedulaUsuario)
        {
            try
            {
                var idUsuario = await _contrasenaService.ObtenerIdUsuarioPorCedulaAsync(cedulaUsuario);
                if (idUsuario == null)
                {
                    return NotFound(new { Message = "Usuario no encontrado." });
                }

                return Ok(new { IdUsuario = idUsuario });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}