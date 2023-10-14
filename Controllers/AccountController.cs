using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PousadaIdentity.Context;
using PousadaIdentity.Entities;
using PousadaIdentity.Models;
using System.Security.Claims;

namespace PousadaIdentity.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly AppDbContext appDbContext;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, AppDbContext appDbContext)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.appDbContext = appDbContext;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([Bind("Nome,CPF,Senha,Email,Usuario")] Pessoa pessoa, RegisterViewModel model)
        {


            if (ModelState.IsValid)
            {

                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                };
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "CLIENT");

                    await signInManager.SignInAsync(user, isPersistent: false);
                    appDbContext.Add(pessoa);
                    await appDbContext.SaveChangesAsync();

                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);

        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {


            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(
                    model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {

                    var user = await userManager.FindByNameAsync(model.Email);

                    /*Caso precise apagar algum usuario do banco deve ser logar dps com isso ativado
                    var rEesult = await userManager.DeleteAsync(user);
                    await signInManager.SignOutAsync();
                    */
                    var claims = new List<Claim>
                {

                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier , user.Id) // Adiciona o ID do usuário como uma claim

                };

                    var userIdentity = new ClaimsIdentity(claims, "login");
                    var userPrincipal = new ClaimsPrincipal(userIdentity);

                    // Faça login com o principal que contém as claims
                    await HttpContext.SignInAsync(userPrincipal);


                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Login Invalido");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


    }
}
