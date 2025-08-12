using AppDbContext;
using Manager.Datos;

namespace Manager.Repositorio
{
    public interface IMovimientoRepository : IRepository { }

    // Primary constructor syntax (C# 12) consistent with existing repos in the project
    public class MovimientoRepository(AppDbContext.AppDbContext dbContext) : EfRepository(dbContext), IMovimientoRepository;
}
