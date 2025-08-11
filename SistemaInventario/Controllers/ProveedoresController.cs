
using DTO;
using Manager.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Laboratorio_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedoresController : ControllerBase
    {
        private readonly IProveedorService _proveedorService;

        // Inyección de dependencias
        public ProveedoresController(IProveedorService proveedorService)
        {
            _proveedorService = proveedorService;
        }

        // Obtener todos los proveedores
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Proveedores>>> GetAll()
        {
            var proveedores = await _proveedorService.GetAllAsync();
            return Ok(proveedores);
        }

        // Obtener proveedor por ID
        [HttpGet("GetById")]
        public async Task<ActionResult<Proveedores>> GetById(int id)
        {
            var proveedor = await _proveedorService.GetByIdAsync(id);
            if (proveedor == null) return NotFound();
            return Ok(proveedor);
        }

        // Crear un nuevo proveedor
        [HttpPost("Create")]
        public async Task<ActionResult<Proveedores>> Create(Proveedores proveedor)
        {
            await _proveedorService.CreateAsync(proveedor);
            return CreatedAtAction(nameof(GetById), new { id = proveedor.Id }, proveedor);
        }

        // Actualizar un proveedor
        [HttpPut("Update")]
        public async Task<IActionResult> Update(int id, Proveedores proveedor)
        {
            await _proveedorService.UpdateAsync(proveedor);
            return NoContent();
        }

        // Eliminar un proveedor
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var proveedor = await _proveedorService.GetByIdAsync(id);
            if (proveedor == null) return NotFound();
            await _proveedorService.DeleteAsync(proveedor);
            return NoContent();
        }
    }
}
