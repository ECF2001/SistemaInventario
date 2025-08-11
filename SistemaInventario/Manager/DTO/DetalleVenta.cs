using Manager.Datos;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaInventario.Manager.DTO
{
    [Table("DetalleVenta")]
    public class DetalleVenta : BaseEntity
    {
        public int IdVenta { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }

    }
}
