
using Microsoft.AspNetCore.Mvc;
using SitePet.Models;

namespace SitePet.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult QueroAdotar()
        {
            return View();
        }

        public IActionResult QuemSomos()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Contato()
        {
            return View(new MensagemContato());
        }

        [HttpPost]
        public IActionResult Contato(MensagemContato mensagem)
        {
            if (!ModelState.IsValid)
            {
                return View(mensagem);
            }

            // Futuramente, enviar mensagem para a API.

            TempData["MensagemSucesso"] = "Mensagem recebida!";

            return RedirectToAction("Contato");
        }
    }
}
