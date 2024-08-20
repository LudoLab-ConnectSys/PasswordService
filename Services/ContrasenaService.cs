using Microsoft.EntityFrameworkCore;
using PasswordService.Data;

namespace PasswordService.Services
{
    public class ContrasenaService : IContrasenaService
    {
        private readonly ApplicationDbContext _context;

        public ContrasenaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> HaIniciadoSesionAntesAsync(int idUsuario)
        {
            var usuario = await _context.Usuarios.FindAsync(idUsuario);
            if (usuario == null || !usuario.EstadoActivo)
            {
                throw new Exception("Usuario no encontrado o inactivo.");
            }

            return usuario.UltimoLogin.HasValue;
        }

        public async Task CambiarContrasenaAsync(int idUsuario, string nuevaContrasena)
        {
            var usuario = await _context.Usuarios.FindAsync(idUsuario);
            if (usuario == null || !usuario.EstadoActivo)
            {
                throw new Exception("Usuario no encontrado o inactivo.");
            }

            usuario.Contrasena = BCrypt.Net.BCrypt.HashPassword(nuevaContrasena);
            usuario.UltimoLogin = DateTime.Now;

            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<int?> ObtenerIdUsuarioPorCedulaAsync(string cedulaUsuario)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.CedulaUsuario == cedulaUsuario && u.EstadoActivo);

            return usuario?.IdUsuario;
        }
    }
}