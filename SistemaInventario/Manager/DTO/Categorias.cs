using Manager.Datos;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaInventario.DTO
{
    [Table("Categorias")]
    public class Categorias : BaseEntity
    {
       
        public string Nombre_Categoria { get; set; }
        public string Descripcion { get; set; }
    }
}
