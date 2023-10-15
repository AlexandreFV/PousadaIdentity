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
using Microsoft.AspNetCore.Authorization;
using System.Data;

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
        public async Task<IActionResult> Create([Bind("ReservaId,CheckIn,CheckOut,DataReservada,Estado,ValorTotalReserva,QuartoID,PessoaId")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                int numeroDias = (reserva.CheckOut - reserva.CheckIn).Days;

                bool quartoDisponivel = VerificarDisponibilidadeQuarto(reserva.QuartoID, reserva.CheckIn, reserva.CheckOut);
                if (quartoDisponivel)
                {
                    decimal precoQuarto = ObterPrecoQuarto(reserva.QuartoID);
                    decimal valorTotal = numeroDias * precoQuarto;
                    reserva.ValorTotalReserva = valorTotal;
                    ViewBag.ValorTotalReserva = valorTotal; // onde "valorTotal" é o valor calculado

                    // O quarto está disponível, pode criar a reserva
                    _context.Add(reserva);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "O quarto escolhido não está disponível para as datas selecionadas.");

                }

            }

            ViewData["PessoaId"] = new SelectList(_context.Pessoa, "PessoaId", "PessoaId", reserva.PessoaId);
            ViewData["QuartoID"] = new SelectList(_context.Quarto, "QuartoId", "QuartoId", reserva.QuartoID);
            return View(reserva);
        }


        private decimal ObterPrecoQuarto(int quartoId)
        {
            // Consulte o banco de dados ou outra fonte de dados para obter o preço do quarto com base no ID do quarto
            var quarto = _context.Quarto.FirstOrDefault(q => q.QuartoId == quartoId);
            if (quarto != null)
            {
                return quarto.Preco;
            }

            ModelState.AddModelError(string.Empty, "Selecione um quarto valido.");
            return 0.0M;
        }

        public IActionResult ObterPrecoQuartoExibicao(int quartoId)
        {
            // Consulte o banco de dados ou outra fonte de dados para obter o preço do quarto com base no ID do quarto
            var quarto = _context.Quarto.FirstOrDefault(q => q.QuartoId == quartoId);

            if (quarto != null)
            {
          
                return Json(new { preco = quarto.Preco });
            }

            return Json(new { error = "Quarto não encontrado" });
        }

        [Authorize(Roles = "Funcionario")] // Certifique-se de que apenas usuários com a role FUNCI podem acessar essa ação.
        public async Task<IActionResult> MarcarComoPago(int id)
        {
            var reserva = _context.Reserva.FirstOrDefault(q => q.ReservaId == id);
            if (reserva != null)
            {
                reserva.Estado = "Pago"; // Altera o estado para "Pago".
                _context.Update(reserva);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Details", new { id });
        }


        [Authorize(Roles = "Funcionario")] // Certifique-se de que apenas usuários com a role FUNCI podem acessar essa ação.
        public async Task<IActionResult> MarcarComoNãoPago(int id)
        {
            var reserva = _context.Reserva.FirstOrDefault(q => q.ReservaId == id);
            if (reserva != null)
            {
                reserva.Estado = "Não Pago"; // Altera o estado para "Pago".
                _context.Update(reserva);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Details", new { id });
        }


        private bool VerificarDisponibilidadeQuarto(int quartoId, DateTime checkIn, DateTime checkOut)
        {
            // Verifique no banco de dados se o quarto está disponível para as datas escolhidas
            // Implemente a lógica de consulta ao banco de dados de acordo com a sua estrutura de dados

            bool quartoDisponivel = !_context.Reserva.Any(r =>
                r.QuartoID == quartoId &&
                (checkIn >= r.CheckIn && checkIn <= r.CheckOut) ||
                (checkOut >= r.CheckIn && checkOut <= r.CheckOut));

            return quartoDisponivel;
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
        public async Task<IActionResult> Edit(int id, [Bind("ReservaId,CheckIn,CheckOut,DataReservada,Estado,ValorTotalReserva,QuartoID,PessoaId")] Reserva reserva)
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
