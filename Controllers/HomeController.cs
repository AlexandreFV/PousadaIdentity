
using PousadaIdentity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PousadaIdentity.Models;
using PousadaIdentity.Entities;
using Microsoft.AspNetCore.Identity;
using PousadaIdentity.Context;

namespace PousadaIdentity.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
      
            int pessoaId = HttpContext.Session.GetInt32("SessionPessoaId") ?? 0; // 0 é o valor padrão se a sessão estiver vazia ou nula
            return View();
        }

    }
}