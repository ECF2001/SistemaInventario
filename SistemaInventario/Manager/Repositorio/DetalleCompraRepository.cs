using AppDbContext;
using Manager.Datos;

namespace Manager.Repositorio
{
    public interface IDetalleCompraRepository : IRepository { }

    public class DetalleCompraRepository(AppDbContext.AppDbContext dbContext) : EfRepository(dbContext), IDetalleCompraRepository;
}
