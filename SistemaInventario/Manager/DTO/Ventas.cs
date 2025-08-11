using Manager.Datos;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaInventario.Manager.DTO
{
    [Table("Ventas")]
    public class Ventas : BaseEntity
    {
        public DateTime FechaVenta { get; set; }
        public int IdUsuario { get; set; }
        public decimal TotalVenta { get; set; }

        public decimal IVA { get; set; }
        public decimal TotalFinal { get; set; }
    }
}
