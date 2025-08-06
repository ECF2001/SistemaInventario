using DTO;
using Manager.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Servicios
{
    public interface IProveedorService : ICrud<Proveedores>
    {
        
    }
    // Implementación correcta de la interfaz IProveedorService
    public class ProveedorService : IProveedorService
    {
        private readonly IProveedorRepository _proveedorRepository;

        // Constructor que recibe IProveedorRepository
        public ProveedorService(IProveedorRepository proveedorRepository)
        {
            _proveedorRepository = proveedorRepository;
        }

        // Implementación de GetAllAsync
        public async Task<IEnumerable<Proveedores>> GetAllAsync()
        {
            return await _proveedorRepository.ListAsync<Proveedores>();
        }

        // Implementación de GetByIdAsync
        public async Task<Proveedores?> GetByIdAsync(int id)
        {
            return await _proveedorRepository.GetByIdAsync<Proveedores>(id);
        }

        // Implementación de CreateAsync
        public async Task<Proveedores> CreateAsync(Proveedores entity)
        {

            return await _proveedorRepository.AddAsync(entity);
        }

        // Implementación de UpdateAsync
        public async Task UpdateAsync(Proveedores entity)
        {
            await _proveedorRepository.UpdateAsync(entity);
        }

        // Implementación de DeleteAsync
        public async Task DeleteAsync(Proveedores entity)
        {
            await _proveedorRepository.DeleteAsync(entity);
        }

     
    }
}
