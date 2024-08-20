using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PasswordService.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key] [Column("id_usuario")] public int IdUsuario { get; set; }

        [Column("cedula_usuario")] public string CedulaUsuario { get; set; }

        [Column("contrasena")] public string? Contrasena { get; set; }

        [Column("UltimoLogin")] public DateTime? UltimoLogin { get; set; }

        [Column("estadoActivo")] public bool EstadoActivo { get; set; }
    }
}