using DreamInCode.Manager.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaInventario.Manager.Servicios;

namespace DreamInCode.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _registerService;

        public UserController(IUserService registerService)
        {
            _registerService = registerService;
        }

        [HttpPost ("Register")]
        public async Task<IActionResult> Register([FromBody] UsersDTO request)
        {

            request.FechaRegistro = DateTime.Now;
            var created = await _registerService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpGet("GetById")]
        public async Task<ActionResult<UsersDTO>> GetById(int id)
        {
            var producto = await _registerService.GetByIdAsync(id);
            if (producto == null) return NotFound();
            return Ok(producto);
        }
    }
}

