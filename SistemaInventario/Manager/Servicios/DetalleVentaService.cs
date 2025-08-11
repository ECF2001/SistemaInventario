using Manager.Repositorio;
using Manager.Servicios;
using SistemaInventario.DTO;
using SistemaInventario.Manager.DTO;

namespace SistemaInventario.Manager.Servicios
{
    public interface IDetalleVentaService : ICrud<DetalleVenta>
    {

    }
    // Implementación correcta de la interfaz IProveedorService
    public class DetalleVentaService : IDetalleVentaService
    {
        private readonly IDetalleVentaRepository _detalleVentaRepository;

        // Constructor que recibe IProveedorRepository
        public DetalleVentaService(IDetalleVentaRepository detalleVentaRepository)
        {
            _detalleVentaRepository = detalleVentaRepository;
        }

        // Implementación de GetAllAsync
        public async Task<IEnumerable<DetalleVenta>> GetAllAsync()
        {
            return await _detalleVentaRepository.ListAsync<DetalleVenta>();
        }

        // Implementación de GetByIdAsync
        public async Task<DetalleVenta?> GetByIdAsync(int id)
        {
            return await _detalleVentaRepository.GetByIdAsync<DetalleVenta>(id);
        }

        // Implementación de CreateAsync
        public async Task<DetalleVenta> CreateAsync(DetalleVenta entity)
        {

            return await _detalleVentaRepository.AddAsync(entity);
        }

        // Implementación de UpdateAsync
        public async Task UpdateAsync(DetalleVenta entity)
        {
            await _detalleVentaRepository.UpdateAsync(entity);
        }

        // Implementación de DeleteAsync
        public async Task DeleteAsync(DetalleVenta entity)
        {
            await _detalleVentaRepository.DeleteAsync(entity);
        }
    }
}
