using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaInventario.DTO;
using SistemaInventario.Manager.DTO;
using SistemaInventario.Manager.Servicios;

namespace SistemaInventario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        private readonly IVentaService _ventaService;

        // Inyección de dependencias
        public VentaController(IVentaService ventaService)
        {
            _ventaService = ventaService;
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Ventas>>> GetAll()
        {
            var ventas = await _ventaService.GetAllAsync();
            return Ok(ventas);
        }

        [HttpGet("GetById")]
        public async Task<ActionResult<Ventas>> GetById(int id)
        {
            var venta = await _ventaService.GetByIdAsync(id);
            if (venta == null) return NotFound();
            return Ok(venta);
        }


        [HttpPost("Create")]
        public async Task<ActionResult<Ventas>> Create(Ventas ventas)
        {


            var created = await _ventaService.CreateAsync(ventas);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }


        [HttpPut("Update")]
        public async Task<IActionResult> Update(int id, Ventas ventas)
        {
            await _ventaService.UpdateAsync(ventas);
            return NoContent();
        }


        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var venta = await _ventaService.GetByIdAsync(id);
            if (venta == null) return NotFound();
            await _ventaService.DeleteAsync(venta);
            return NoContent();
        }
    }
}
