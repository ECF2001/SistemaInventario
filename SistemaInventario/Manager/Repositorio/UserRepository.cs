using Manager.Datos;
using Manager.Repositorio;

namespace DreamInCode.Manager.Repository
{
    public interface IUserRepository : IRepository
    {

    }

    public class UserRepository : EfRepository, IUserRepository
    {
        public UserRepository(AppDbContext.AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
