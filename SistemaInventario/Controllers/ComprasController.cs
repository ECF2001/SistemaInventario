using Microsoft.AspNetCore.Mvc;
using SistemaInventario.Manager.Servicios;
using DTO;

namespace SistemaInventario.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComprasController : ControllerBase
    {
        private readonly ICompraService _svc;

        public ComprasController(ICompraService svc)
        {
            _svc = svc;
        }

        [HttpPost("Registrar")]
        public async Task<ActionResult<int>> Registrar([FromBody] CompraRequest request)
        {
            if (request is null || request.Items.Count == 0) return BadRequest("Sin items.");
            var id = await _svc.RegistrarCompraAsync(request);
            return Ok(id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Compras>>> Listar([FromQuery] DateTime? desde = null, [FromQuery] DateTime? hasta = null, [FromQuery] int? idProveedor = null)
        {
            var data = await _svc.ListarComprasAsync(desde, hasta, idProveedor);
            return Ok(data);
        }

        [HttpGet("Detalle/{id:int}")]
        public async Task<ActionResult> Detalle(int id)
        {
            var (compra, detalle) = await _svc.ObtenerDetalleCompraAsync(id);
            if (compra is null) return NotFound();
            return Ok(new { compra, detalle });
        }
    }
}
