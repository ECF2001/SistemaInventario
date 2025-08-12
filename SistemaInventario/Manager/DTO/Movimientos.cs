using Manager.Datos;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTO
{
    [Table("Movimientos")]
    public class Movimientos : BaseEntity
    {
        [Column("id_producto")]
        public int Id_Producto { get; set; }

        [Column("tipo_movimiento")]
        public string Tipo_Movimiento { get; set; }

        [Column("cantidad")]
        public int Cantidad { get; set; }

        [Column("fecha")]
        public DateTime Fecha { get; set; }

        [Column("observaciones")]
        public string? Observaciones { get; set; }
    }
}
