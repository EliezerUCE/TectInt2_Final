using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Noticias.API.Data;
using Noticias.API.Models;

namespace Noticias.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoticiasController : ControllerBase
    {
        private readonly NoticiasContext _context;

        public NoticiasController(NoticiasContext context)
        {
            _context = context;
        }

        // GET: api/Noticias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Noticia>>> GetNoticias()
        {
            return await _context.Noticias
                .Include(n => n.Categoria)
                .Include(n => n.Pais)
                .Include(n => n.Usuario)
                .OrderByDescending(n => n.FechaPublicacion)
                .ToListAsync();
        }

        // GET: api/Noticias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Noticia>> GetNoticia(int id)
        {
            var noticia = await _context.Noticias
                .Include(n => n.Categoria)
                .Include(n => n.Pais)
                .Include(n => n.Usuario)
                .FirstOrDefaultAsync(n => n.Id == id);

            if (noticia == null)
                return NotFound();

            return noticia;
        }

        // GET: api/Noticias/pais/1
        [HttpGet("pais/{paisId}")]
        public async Task<ActionResult<IEnumerable<Noticia>>> GetNoticiasPorPais(int paisId)
        {
            return await _context.Noticias
                .Include(n => n.Categoria)
                .Include(n => n.Pais)
                .Include(n => n.Usuario)
                .Where(n => n.PaisId == paisId)
                .OrderByDescending(n => n.FechaPublicacion)
                .ToListAsync();
        }

        // GET: api/Noticias/categoria/2
        [HttpGet("categoria/{categoriaId}")]
        public async Task<ActionResult<IEnumerable<Noticia>>> GetNoticiasPorCategoria(int categoriaId)
        {
            return await _context.Noticias
                .Include(n => n.Categoria)
                .Include(n => n.Pais)
                .Include(n => n.Usuario)
                .Where(n => n.CategoriaId == categoriaId)
                .OrderByDescending(n => n.FechaPublicacion)
                .ToListAsync();
        }

        // GET: api/Noticias/buscar?termino=tecnologia
        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<Noticia>>> BuscarNoticias([FromQuery] string termino)
        {
            if (string.IsNullOrWhiteSpace(termino))
                return BadRequest("El término de búsqueda no puede estar vacío");

            return await _context.Noticias
                .Include(n => n.Categoria)
                .Include(n => n.Pais)
                .Include(n => n.Usuario)
                .Where(n => n.Titulo.Contains(termino) || n.Contenido.Contains(termino))
                .OrderByDescending(n => n.FechaPublicacion)
                .ToListAsync();
        }

        // GET: api/Noticias/usuario/2
        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<Noticia>>> GetNoticiasPorUsuario(int usuarioId)
        {
            return await _context.Noticias
                .Include(n => n.Categoria)
                .Include(n => n.Pais)
                .Include(n => n.Usuario)
                .Where(n => n.UsuarioId == usuarioId)
                .OrderByDescending(n => n.FechaPublicacion)
                .ToListAsync();
        }

        // POST: api/Noticias
        [HttpPost]
        public async Task<ActionResult<Noticia>> PostNoticia(Noticia noticia)
        {
            _context.Noticias.Add(noticia);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNoticia), new { id = noticia.Id }, noticia);
        }

        // PUT: api/Noticias/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNoticia(int id, Noticia noticia)
        {
            if (id != noticia.Id)
                return BadRequest();

            _context.Entry(noticia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoticiaExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Noticias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNoticia(int id)
        {
            var noticia = await _context.Noticias.FindAsync(id);
            if (noticia == null)
                return NotFound();

            _context.Noticias.Remove(noticia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NoticiaExists(int id)
        {
            return _context.Noticias.Any(e => e.Id == id);
        }
    }
}