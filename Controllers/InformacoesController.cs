using Microsoft.AspNetCore.Mvc;

namespace PousadaIdentity.Controllers
{
    public class InformacoesController : Controller
    {
        public IActionResult Informacoes_Pousada()
        {
            return View();
        }
    }
}
