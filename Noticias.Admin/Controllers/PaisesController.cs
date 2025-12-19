using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Noticias.Admin.Data;
using Noticias.Admin.Models;

namespace Noticias.Admin.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class PaisesController : Controller
    {
        private readonly NoticiasContext _context;

        public PaisesController(NoticiasContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Paises.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pais pais)
        {
            _context.Add(pais);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var pais = await _context.Paises.FindAsync(id);
            if (pais == null)
                return NotFound();
            return View(pais);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Pais pais)
        {
            if (id != pais.Id)
                return NotFound();

            _context.Update(pais);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var pais = await _context.Paises.FindAsync(id);
            if (pais == null)
                return NotFound();
            return View(pais);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pais = await _context.Paises.FindAsync(id);
            _context.Paises.Remove(pais);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}