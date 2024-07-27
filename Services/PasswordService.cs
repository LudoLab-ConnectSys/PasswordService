using PasswordService.Data;
using System.Text;

namespace PasswordService.Services
{
    public class PasswordService
    {
        private readonly ApplicationDbContext _context;

        public PasswordService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DateTime?> GetLastLoginAsync(int userId)
        {
            var user = await _context.Usuario.FindAsync(userId);
            if (user == null)
            {
                throw new Exception("Usuario no encontrado.");
            }

            return user.UltimoLogin;
        }

        public async Task ChangePasswordAsync(int userId, string newPassword)
        {
            ValidatePasswordPolicy(newPassword);

            var user = await _context.Usuario.FindAsync(userId);
            if (user == null)
            {
                throw new Exception("Usuario no encontrado.");
            }

            user.HashContrasena = BCrypt.Net.BCrypt.HashPassword(newPassword);
            user.UltimoLogin = DateTime.Now;

            _context.Usuario.Update(user);
            await _context.SaveChangesAsync();
        }

        private void ValidatePasswordPolicy(string password)
        {
            var errors = new List<string>();

            if (password.Length < 8)
            {
                errors.Add("La contraseña debe tener al menos 8 caracteres.");
            }

            if (password.Length > 50)
            {
                errors.Add("La contraseña debe tener menos de 50 caracteres.");
            }

            if (!password.Any(char.IsUpper))
            {
                errors.Add("La contraseña debe contener al menos una letra mayúscula.");
            }

            if (!password.Any(char.IsLower))
            {
                errors.Add("La contraseña debe contener al menos una letra minúscula.");
            }

            if (!password.Any(char.IsDigit))
            {
                errors.Add("La contraseña debe contener al menos un número.");
            }

            if (!password.Any(ch => "!@#$%^&*()_+[]{}|;:,.<>?".Contains(ch)))
            {
                errors.Add("La contraseña debe contener al menos un carácter especial.");
            }

            if (errors.Any())
            {
                throw new Exception(string.Join(" ", errors));
            }
        }
    }
}