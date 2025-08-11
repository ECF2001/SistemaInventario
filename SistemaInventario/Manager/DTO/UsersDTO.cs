using Manager.Datos;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamInCode.Manager.DTO
{
    [Table("Users")]
    public class UsersDTO : BaseEntity
    {
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public int? Edad { get; set; }
        public string Direccion { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public string Password { get; set; }
        public int TipoUsuario { get; set; } 
    }
}
