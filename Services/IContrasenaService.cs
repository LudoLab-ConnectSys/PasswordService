namespace PasswordService.Services
{
    public interface IContrasenaService
    {
        Task<bool> HaIniciadoSesionAntesAsync(int idUsuario);
        Task CambiarContrasenaAsync(int idUsuario, string nuevaContrasena);
        Task<int?> ObtenerIdUsuarioPorCedulaAsync(string cedulaUsuario);
    }
}