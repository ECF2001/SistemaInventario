using Manager.Datos;

namespace SistemaInventario.DTO
{
    public class Categoria : BaseEntity
    {
       
        public string Nombre_Categoria { get; set; }
        public string Descripcion { get; set; }
    }
}
