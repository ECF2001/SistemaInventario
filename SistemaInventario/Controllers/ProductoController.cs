using DTO;
using Manager.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaInventario.DTO;
using SistemaInventario.Manager.Servicios;

namespace SistemaInventario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;

        // Inyección de dependencias
        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Productos>>> GetAll()
        {
            var productos = await _productoService.GetAllAsync();
            return Ok(productos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Productos>> GetById(int id)
        {
            var producto = await _productoService.GetByIdAsync(id);
            if (producto == null) return NotFound();
            return Ok(producto);
        }

        
        [HttpPost]
        public async Task<ActionResult<Productos>> Create(Productos producto)
        {
            var created = await _productoService.CreateAsync(producto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Productos producto)
        {
            await _productoService.UpdateAsync(producto);
            return NoContent();
        }

      
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var producto = await _productoService.GetByIdAsync(id);
            if (producto == null) return NotFound();
            await _productoService.DeleteAsync(producto);
            return NoContent();
        }
    }
}
