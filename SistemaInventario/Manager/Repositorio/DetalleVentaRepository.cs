using Manager.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Repositorio
{
    public interface IDetalleVentaRepository : IRepository
    {

    }

    public class DetalleVentaRepository(AppDbContext.AppDbContext dbContext) : EfRepository(dbContext), IDetalleVentaRepository;
}
