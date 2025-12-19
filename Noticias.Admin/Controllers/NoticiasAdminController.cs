using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Noticias.Admin.Data;
using Noticias.Admin.Models;
using System.Security.Claims;

namespace Noticias.Admin.Controllers
{
    [Authorize]
    public class NoticiasAdminController : Controller
    {
        private readonly NoticiasContext _context;

        public NoticiasAdminController(NoticiasContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var rol = User.FindFirstValue(ClaimTypes.Role);

            IQueryable<Noticia> query = _context.Noticias
                .Include(n => n.Categoria)
                .Include(n => n.Pais)
                .Include(n => n.Usuario);

            if (rol == "Editor")
            {
                query = query.Where(n => n.UsuarioId == usuarioId);
            }

            var noticias = await query.OrderByDescending(n => n.FechaPublicacion).ToListAsync();
            return View(noticias);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categorias = new SelectList(await _context.Categorias.ToListAsync(), "Id", "Nombre");
            ViewBag.Paises = new SelectList(await _context.Paises.ToListAsync(), "Id", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Noticia noticia)
        {
            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            noticia.UsuarioId = usuarioId;
            noticia.FechaPublicacion = DateTime.Now;

            _context.Noticias.Add(noticia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var noticia = await _context.Noticias.FindAsync(id);
            if (noticia == null)
                return NotFound();

            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var rol = User.FindFirstValue(ClaimTypes.Role);

            if (rol == "Editor" && noticia.UsuarioId != usuarioId)
                return Forbid();

            ViewBag.Categorias = new SelectList(await _context.Categorias.ToListAsync(), "Id", "Nombre", noticia.CategoriaId);
            ViewBag.Paises = new SelectList(await _context.Paises.ToListAsync(), "Id", "Nombre", noticia.PaisId);
            return View(noticia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Noticia noticia)
        {
            if (id != noticia.Id)
                return NotFound();

            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var rol = User.FindFirstValue(ClaimTypes.Role);

            var noticiaOriginal = await _context.Noticias.AsNoTracking().FirstOrDefaultAsync(n => n.Id == id);
            if (rol == "Editor" && noticiaOriginal.UsuarioId != usuarioId)
                return Forbid();

            noticia.UsuarioId = noticiaOriginal.UsuarioId;
            noticia.FechaPublicacion = noticiaOriginal.FechaPublicacion;

            _context.Update(noticia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var noticia = await _context.Noticias
                .Include(n => n.Categoria)
                .Include(n => n.Pais)
                .FirstOrDefaultAsync(n => n.Id == id);

            if (noticia == null)
                return NotFound();

            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var rol = User.FindFirstValue(ClaimTypes.Role);

            if (rol == "Editor" && noticia.UsuarioId != usuarioId)
            return Forbid();

        return View(noticia);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var noticia = await _context.Noticias.FindAsync(id);
        _context.Noticias.Remove(noticia);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
}
