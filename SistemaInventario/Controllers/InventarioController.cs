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

        [HttpGet("Kardex/{idProducto:int}")]
        public async Task<ActionResult<IEnumerable<Movimientos>>> GetKardex(int idProducto, [FromQuery] DateTime? desde = null, [FromQuery] DateTime? hasta = null)
        {
            var data = await _inventario.GetKardexAsync(idProducto, desde, hasta);
            return Ok(data);
        }

        public class AjusteRequest
        {
            public int IdProducto { get; set; }
            public int Cantidad { get; set; }
            public string Tipo { get; set; } = "ENTRADA"; // ENTRADA | SALIDA
            public string? Observaciones { get; set; }
        }

        [HttpPost("AjusteStock")]
        public async Task<ActionResult<Movimientos>> AjusteStock([FromBody] AjusteRequest request)
        {
            if (request.Cantidad <= 0) return BadRequest("La cantidad debe ser mayor que cero");
            var mov = await _inventario.AjusteStockAsync(request.IdProducto, request.Cantidad, request.Tipo, request.Observaciones);
            return Ok(mov);
        }
    }
}
