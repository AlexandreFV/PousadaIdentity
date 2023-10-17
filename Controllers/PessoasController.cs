using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using PousadaIdentity.Context;
using PousadaIdentity.Entities;
using static System.Net.WebRequestMethods;

namespace PousadaIdentity.Controllers
{
    public class PessoasController : Controller
    {
        private readonly AppDbContext _context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;

        public PessoasController(AppDbContext context, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _context = context;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        // GET: Pessoas
        public async Task<IActionResult> Index()
        {
              return _context.Pessoa != null ? 
                          View(await _context.Pessoa.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Pessoa'  is null.");
        }

        // GET: Pessoas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var roles = roleManager.Roles.ToList();
            var rolesSelectList = new SelectList(roles, "Id", "Name");

            if (id == null || _context.Pessoa == null)
            {
                return NotFound();
            }

            var pessoa = await _context.Pessoa
                .FirstOrDefaultAsync(m => m.PessoaId == id);
            if (pessoa == null)
            {
                return NotFound();
            }
            ViewData["Roles"] = rolesSelectList;

            return View(pessoa);
        }

        // GET: Pessoas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pessoas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PessoaId,Nome,CPF,Senha,Email,Usuario")] Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pessoa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pessoa);
        }

        // GET: Pessoas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pessoa == null)
            {
                return NotFound();
            }

            var pessoa = await _context.Pessoa.FindAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }
            return View(pessoa);
        }

        // POST: Pessoas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PessoaId,Nome,CPF,Senha,Email,Usuario")] Pessoa pessoa)
        {
            if (id != pessoa.PessoaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pessoa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PessoaExists(pessoa.PessoaId))
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
            return View(pessoa);
        }

        // GET: Pessoas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pessoa == null)
            {
                return NotFound();
            }

            var pessoa = await _context.Pessoa
                .FirstOrDefaultAsync(m => m.PessoaId == id);
            if (pessoa == null)
            {
                return NotFound();
            }

            return View(pessoa);
        }

        // POST: Pessoas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pessoa == null)
            {
                return Problem("Entity set 'AppDbContext.Pessoa'  is null.");
            }
            var pessoa = await _context.Pessoa.FindAsync(id);
            if (pessoa != null)
            {
                _context.Pessoa.Remove(pessoa);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PessoaExists(int id)
        {
          return (_context.Pessoa?.Any(e => e.PessoaId == id)).GetValueOrDefault();
        }

            [HttpGet]
            public async Task<IActionResult> CheckForDuplicateTokens(int roleSelecionada)
            {
                var duplicateTokens = await (from pessoa in _context.Pessoa
                                             join userRole in _context.UserRoles on pessoa.Token equals userRole.UserId
                                             select new
                                             {
                                                 pessoa.Token,
                                                 userRole.UserId,
                                                 userRole.RoleId
                                             }).ToListAsync();

                foreach (var tokenInfo in duplicateTokens)
                {
                    Console.WriteLine($"Token: {tokenInfo.Token} corresponde a UserId: {tokenInfo.UserId}");

                    // Verifique se o usuário existe
                    var user = await userManager.FindByIdAsync(tokenInfo.UserId);
                    if (user != null)
                    {
                        // Remova todas as roles existentes do usuário
                        var existingRoles = await userManager.GetRolesAsync(user);
                        await userManager.RemoveFromRolesAsync(user, existingRoles);
                        var oldUserRole = _context.UserRoles.FirstOrDefault(ur => ur.UserId == tokenInfo.UserId);

                    if (roleSelecionada == 1)
                        {
                        _context.UserRoles.Remove(oldUserRole);
                        await userManager.AddToRoleAsync(user, "CLIENT");

                        }
                        else if (roleSelecionada == 2)
                        {
                        _context.UserRoles.Remove(oldUserRole);
                        await userManager.AddToRoleAsync(user, "FUNCI");
                        }
                    }
                }

                return Ok("Verificação concluída.");
            }






    }
}
