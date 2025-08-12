using Manager.Datos;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTO
{
    [Table("Detalle_Compras")]
    public class Detalle_Compras : BaseEntity
    {
        [Column("id_compra")]
        public int Id_Compra { get; set; }

        [Column("id_producto")]
        public int Id_Producto { get; set; }

        [Column("cantidad")]
        public int Cantidad { get; set; }

        [Column("precio_unitario")]
        public decimal Precio_Unitario { get; set; }
    }
}
