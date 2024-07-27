using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PasswordService.Models
{
    [Table("Usuario")]
    public class User
    {
        [Key] [Column("id_usuario")] public int IdUsuario { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("cedula_usuario")]
        public string CedulaUsuario { get; set; }

        [Required]
        [MaxLength(150)]
        [Column("HashContrasena")]
        public string HashContrasena { get; set; }

        [Column("UltimoLogin")] public DateTime? UltimoLogin { get; set; }
    }
}