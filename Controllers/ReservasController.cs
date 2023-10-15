using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PousadaIdentity.Context;
using PousadaIdentity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Components;

namespace PousadaIdentity.Controllers
{
    public class ReservasController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ReservasController(AppDbContext context, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;


        }

        // GET: Reservas
        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);
            int pessoaId = httpContextAccessor.HttpContext.Session.GetInt32("SessionPessoaId") ?? 0;

            if (await userManager.IsInRoleAsync(user, "CLIENT"))
            {

                // Se o usuário tem a função "CLIENT", consulte apenas as reservas relacionadas ao seu ID

                var reservas = _context.Reserva
                    .Where(r => r.PessoaId == pessoaId)
                    .Include(r => r.Pessoa);
                return View(await reservas.ToListAsync());
            }
            else if (await userManager.IsInRoleAsync(user, "FUNCI"))
            {
                // Se o usuário tem a função "FUNCI", ele pode ver todas as reservas
                var DbContext = _context.Reserva.Include(r => r.Pessoa);
                return View(await DbContext.ToListAsync());
            }
            else
            {
                // Trate outras funções ou usuários sem função de acordo com sua lógica
                return View(new List<Reserva>());
            }
        }

        // GET: Reservas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reserva == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva
                .Include(r => r.Pessoa)
                .Include(r => r.Quarto)
                .FirstOrDefaultAsync(m => m.ReservaId == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // GET: Reservas/Create
        public IActionResult Create()
        {
            ViewData["PessoaId"] = new SelectList(_context.Pessoa, "PessoaId", "PessoaId");
            ViewData["QuartoID"] = new SelectList(_context.Quarto, "QuartoId", "QuartoId");
            return View();
        }

        // POST: Reservas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReservaId,CheckIn,CheckUp,DataReservada,Estado,ValorTotalReserva,QuartoID,PessoaId")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reserva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["PessoaId"] = new SelectList(_context.Pessoa, "PessoaId", "PessoaId", reserva.PessoaId);
            ViewData["QuartoID"] = new SelectList(_context.Quarto, "QuartoId", "QuartoId", reserva.QuartoID);
            return View(reserva);
        }

        // GET: Reservas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Reserva == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }
            ViewData["PessoaId"] = new SelectList(_context.Pessoa, "PessoaId", "PessoaId", reserva.PessoaId);
            ViewData["QuartoID"] = new SelectList(_context.Quarto, "QuartoId", "QuartoId", reserva.QuartoID);
            return View(reserva);
        }

        // POST: Reservas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReservaId,CheckIn,CheckUp,DataReservada,Estado,ValorTotalReserva,QuartoID,PessoaId")] Reserva reserva)
        {
            if (id != reserva.ReservaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reserva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaExists(reserva.ReservaId))
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
            ViewData["PessoaId"] = new SelectList(_context.Pessoa, "PessoaId", "PessoaId", reserva.PessoaId);
            ViewData["QuartoID"] = new SelectList(_context.Quarto, "QuartoId", "QuartoId", reserva.QuartoID);
            return View(reserva);
        }

        // GET: Reservas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reserva == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva
                .Include(r => r.Pessoa)
                .Include(r => r.Quarto)
                .FirstOrDefaultAsync(m => m.ReservaId == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Reserva == null)
            {
                return Problem("Entity set 'AppDbContext.Reserva'  is null.");
            }
            var reserva = await _context.Reserva.FindAsync(id);
            if (reserva != null)
            {
                _context.Reserva.Remove(reserva);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservaExists(int id)
        {
          return (_context.Reserva?.Any(e => e.ReservaId == id)).GetValueOrDefault();
        }
    }
}
