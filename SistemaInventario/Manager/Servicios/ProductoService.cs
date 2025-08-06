using DTO;
using Manager.Repositorio;
using Manager.Servicios;
using SistemaInventario.DTO;

namespace SistemaInventario.Manager.Servicios
{
    public interface IProductoService : ICrud<Productos>
    {

    }
    // Implementación correcta de la interfaz IProveedorService
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;

        // Constructor que recibe IProveedorRepository
        public ProductoService(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        // Implementación de GetAllAsync
        public async Task<IEnumerable<Productos>> GetAllAsync()
        {
            return await _productoRepository.ListAsync<Productos>();
        }

        // Implementación de GetByIdAsync
        public async Task<Productos?> GetByIdAsync(int id)
        {
            return await _productoRepository.GetByIdAsync<Productos>(id);
        }

        // Implementación de CreateAsync
        public async Task<Productos> CreateAsync(Productos entity)
        {

            return await _productoRepository.AddAsync(entity);
        }

        // Implementación de UpdateAsync
        public async Task UpdateAsync(Productos entity)
        {
            await _productoRepository.UpdateAsync(entity);
        }

        // Implementación de DeleteAsync
        public async Task DeleteAsync(Productos entity)
        {
            await _productoRepository.DeleteAsync(entity);
        }
    }
}
