using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Noticias.API.Data;
using Noticias.API.Models;

namespace Noticias.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaisesController : ControllerBase
    {
        private readonly NoticiasContext _context;

        public PaisesController(NoticiasContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pais>>> GetPaises()
        {
            return await _context.Paises.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pais>> GetPais(int id)
        {
            var pais = await _context.Paises.FindAsync(id);
            if (pais == null)
                return NotFound();
            return pais;
        }

        [HttpPost]
        public async Task<ActionResult<Pais>> PostPais(Pais pais)
        {
            _context.Paises.Add(pais);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPais), new { id = pais.Id }, pais);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPais(int id, Pais pais)
        {
            if (id != pais.Id)
                return BadRequest();

            _context.Entry(pais).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePais(int id)
        {
            var pais = await _context.Paises.FindAsync(id);
            if (pais == null)
                return NotFound();

            _context.Paises.Remove(pais);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}