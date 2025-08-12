using Manager.Datos;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTO
{
    [Table("Compras")]
    public class Compras : BaseEntity
    {
        [Column("fecha_compra")]
        public DateTime Fecha_Compra { get; set; }

        [Column("id_proveedor")]
        public int Id_Proveedor { get; set; }

        [Column("total")]
        public decimal Total { get; set; }
    }
}
