using Microsoft.AspNetCore.Mvc;
using SistemaInventario.Manager.Servicios;
using DTO;

namespace SistemaInventario.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventarioController : ControllerBase
    {
        private readonly IInventarioService _inventario;

        public InventarioController(IInventarioService inventario)
        {
            _inventario = inventario;
        }

        [HttpGet("Productos")]
        public async Task<ActionResult<IEnumerable<Productos>>> GetProductos()
        {
            var data = await _inventario.GetProductosAsync();
            return Ok(data);
        }

        [HttpGet("StockBajoMinimo")]
        public async Task<ActionResult<IEnumerable<Productos>>> GetStockBajoMinimo()
        {
            var data = await _inventario.GetStockBajoMinimoAsync();
            return Ok(data);
        }
    }
}
