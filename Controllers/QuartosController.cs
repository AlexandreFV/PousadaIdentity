using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PousadaIdentity.Context;
using PousadaIdentity.Entities;

namespace PousadaIdentity.Controllers
{
    public class QuartosController : Controller
    {
        private readonly AppDbContext _context;

        public QuartosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Quartos
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Quartos.Include(q => q.Reserva);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Quartos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Quartos == null)
            {
                return NotFound();
            }

            var quarto = await _context.Quartos
                .Include(q => q.Reserva)
                .FirstOrDefaultAsync(m => m.QuartoId == id);
            if (quarto == null)
            {
                return NotFound();
            }

            return View(quarto);
        }

        // GET: Quartos/Create
        public IActionResult Create()
        {
            ViewData["ReservaID"] = new SelectList(_context.Reservas, "ReservaId", "ReservaId");
            return View();
        }

        // POST: Quartos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuartoId,Disponibilidade,Numero,Tipo,ArCondicionado,ValorTotalQuarto,ReservaID")] Quarto quarto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quarto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReservaID"] = new SelectList(_context.Reservas, "ReservaId", "ReservaId", quarto.ReservaID);
            return View(quarto);
        }

        // GET: Quartos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Quartos == null)
            {
                return NotFound();
            }

            var quarto = await _context.Quartos.FindAsync(id);
            if (quarto == null)
            {
                return NotFound();
            }
            ViewData["ReservaID"] = new SelectList(_context.Reservas, "ReservaId", "ReservaId", quarto.ReservaID);
            return View(quarto);
        }

        // POST: Quartos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QuartoId,Disponibilidade,Numero,Tipo,ArCondicionado,ValorTotalQuarto,ReservaID")] Quarto quarto)
        {
            if (id != quarto.QuartoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quarto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuartoExists(quarto.QuartoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReservaID"] = new SelectList(_context.Reservas, "ReservaId", "ReservaId", quarto.ReservaID);
            return View(quarto);
        }

        // GET: Quartos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Quartos == null)
            {
                return NotFound();
            }

            var quarto = await _context.Quartos
                .Include(q => q.Reserva)
                .FirstOrDefaultAsync(m => m.QuartoId == id);
            if (quarto == null)
            {
                return NotFound();
            }

            return View(quarto);
        }

        // POST: Quartos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Quartos == null)
            {
                return Problem("Entity set 'AppDbContext.Quartos'  is null.");
            }
            var quarto = await _context.Quartos.FindAsync(id);
            if (quarto != null)
            {
                _context.Quartos.Remove(quarto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuartoExists(int id)
        {
          return (_context.Quartos?.Any(e => e.QuartoId == id)).GetValueOrDefault();
        }
    }
}
