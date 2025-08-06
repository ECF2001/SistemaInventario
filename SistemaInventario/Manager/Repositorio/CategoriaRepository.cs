using Manager.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Repositorio
{
    public interface ICategoriaRepository : IRepository
    {

    }

    public class CategoriaRepository(AppDbContext.AppDbContext dbContext) : EfRepository(dbContext), ICategoriaRepository;
}
