using AppDbContext;
using Manager.Datos;

namespace Manager.Repositorio
{
    public interface ICompraRepository : IRepository { }

    public class CompraRepository(AppDbContext.AppDbContext dbContext) : EfRepository(dbContext), ICompraRepository;
}
