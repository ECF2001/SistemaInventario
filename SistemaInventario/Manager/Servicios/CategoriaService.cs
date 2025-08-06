using Manager.Repositorio;
using Manager.Servicios;
using SistemaInventario.DTO;

namespace SistemaInventario.Manager.Servicios
{
    public interface ICategoriaService : ICrud<Categorias>
    {

    }
    // Implementación correcta de la interfaz IProveedorService
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        // Constructor que recibe IProveedorRepository
        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        // Implementación de GetAllAsync
        public async Task<IEnumerable<Categorias>> GetAllAsync()
        {
            return await _categoriaRepository.ListAsync<Categorias>();
        }

        // Implementación de GetByIdAsync
        public async Task<Categorias?> GetByIdAsync(int id)
        {
            return await _categoriaRepository.GetByIdAsync<Categorias>(id);
        }

        // Implementación de CreateAsync
        public async Task<Categorias> CreateAsync(Categorias entity)
        {

            return await _categoriaRepository.AddAsync(entity);
        }

        // Implementación de UpdateAsync
        public async Task UpdateAsync(Categorias entity)
        {
            await _categoriaRepository.UpdateAsync(entity);
        }

        // Implementación de DeleteAsync
        public async Task DeleteAsync(Categorias entity)
        {
            await _categoriaRepository.DeleteAsync(entity);
        }
    }
}
