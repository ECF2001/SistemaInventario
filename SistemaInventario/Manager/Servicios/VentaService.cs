using Dapper;
using DreamInCode.Manager.DTO;
using DreamInCode.Manager.Repository;
using Manager.Repositorio;
using Manager.Servicios;
using SistemaInventario.DTO;
using SistemaInventario.Manager.DTO;
using System.Data.SqlClient;


namespace DreamInCode.Manager.Services
{
    public interface IVentaService : ICrud<Ventas>
    {

    }
    // Implementación correcta de la interfaz IProveedorService
    public class VentaService : IVentaService
    {
        private readonly IVentaRepository _ventaRepository;

        // Constructor que recibe IProveedorRepository
        public VentaService(IVentaRepository ventaRepository)
        {
            _ventaRepository = ventaRepository;
        }

        // Implementación de GetAllAsync
        public async Task<IEnumerable<Ventas>> GetAllAsync()
        {
            return await _ventaRepository.ListAsync<Ventas>();
        }

        // Implementación de GetByIdAsync
        public async Task<Ventas?> GetByIdAsync(int id)
        {
            return await _ventaRepository.GetByIdAsync<Ventas>(id);
        }

        // Implementación de CreateAsync
        public async Task<Ventas> CreateAsync(Ventas entity)
        {

            return await _ventaRepository.AddAsync(entity);
        }

        // Implementación de UpdateAsync
        public async Task UpdateAsync(Ventas entity)
        {
            await _ventaRepository.UpdateAsync(entity);
        }

        // Implementación de DeleteAsync
        public async Task DeleteAsync(Ventas entity)
        {
            await _ventaRepository.DeleteAsync(entity);
        }


    }
}


