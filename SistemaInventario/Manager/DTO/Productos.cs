using Manager.Datos;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTO
{
    [Table("Productos")]
    public class Productos : BaseEntity
    {
       
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public decimal Precio_Compra { get; set; }

        public decimal Precio_Venta { get; set; }

        public int Stock_Actual { get; set; }

        public int Stock_Minimo { get; set; }

        public int Id_Categoria { get; set; }
        public int Id_Proveedor { get; set; }
    }
}
