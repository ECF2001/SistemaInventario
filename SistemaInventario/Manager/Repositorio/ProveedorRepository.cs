using Manager.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Repositorio
{
    public interface IProveedorRepository : IRepository
    {

    }

    public class ProveedorRepository(AppDbContext.AppDbContext dbContext) : EfRepository(dbContext), IProveedorRepository;
}
