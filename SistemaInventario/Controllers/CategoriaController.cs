using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaInventario.DTO;
using SistemaInventario.Manager.Servicios;

namespace SistemaInventario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        // Inyección de dependencias
        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Categorias>>> GetAll()
        {
            var categorias = await _categoriaService.GetAllAsync();
            return Ok(categorias);
        }

        [HttpGet("GetById")]
        public async Task<ActionResult<Categorias>> GetById(int id)
        {
            var categoria = await _categoriaService.GetByIdAsync(id);
            if (categoria == null) return NotFound();
            return Ok(categoria);
        }


        [HttpPost("Create")]
        public async Task<ActionResult<Categorias>> Create(Categorias categorias)
        {


            var created = await _categoriaService.CreateAsync(categorias);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }


        [HttpPut("Update")]
        public async Task<IActionResult> Update(int id, Categorias categorias)
        {
            await _categoriaService.UpdateAsync(categorias);
            return NoContent();
        }


        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var categoria = await _categoriaService.GetByIdAsync(id);
            if (categoria == null) return NotFound();
            await _categoriaService.DeleteAsync(categoria);
            return NoContent();
        }
    }
}
