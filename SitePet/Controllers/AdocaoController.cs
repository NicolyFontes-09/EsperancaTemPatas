using Microsoft.AspNetCore.Mvc;
using SitePet.Models;
using SitePet.Data;

namespace SitePet.Controllers
{
    public class AdocaoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Cadastrar(int id)
        {
            var pet = PetMocados.Pets.FirstOrDefault(p => p.Id == id);

            if (pet == null)
                return NotFound();

            var adocao = new Adocao
            {
                PetId = pet.Id,
                NomePet = pet.Nome
            };

            return View("~/Views/Adocao/Formulario.cshtml", adocao);
        }

        [HttpPost]
        public IActionResult Cadastrar(Adocao adocao)
        {
            adocao.Id = AdocaoMocados.Solicitacoes.Count + 1;

            AdocaoMocados.Solicitacoes.Add(adocao);

            return View("~/Views/Adocao/Solicitacao.cshtml", adocao);
        }

        [HttpPost]
        public IActionResult Termos(Adocao adocao)
        {
            return View("Termos", adocao);
        }

        public IActionResult Pendencia()
        {
            return View(AdocaoMocados.Solicitacoes);
        }

        [HttpPost]
        public IActionResult Aprovar(int id)
        {
            var adocao = AdocaoMocados.Solicitacoes
                .FirstOrDefault(a => a.Id == id);

            if (adocao != null)
            {
                adocao.Status = "Aprovada";
            }

            return RedirectToAction("Pendencia");
        }


        [HttpGet]
        public IActionResult Rejeitar(int id)
        {
            var adocao = AdocaoMocados.Solicitacoes
                .FirstOrDefault(a => a.Id == id);

            if (adocao == null)
                return NotFound();

            return View(adocao);
        }

        [HttpPost]
        public IActionResult Rejeitar(Adocao adocao)
        {
            var solicitacao = AdocaoMocados.Solicitacoes
                .FirstOrDefault(a => a.Id == adocao.Id);

            if (solicitacao == null)
                return NotFound();

            if (string.IsNullOrWhiteSpace(adocao.Justificativa))
            {
                return View(solicitacao);
            }

            solicitacao.Status = "Rejeitada";
            solicitacao.Justificativa = adocao.Justificativa;

            return RedirectToAction("Pendencia");
        }



        public IActionResult Detalhes(int id)
        {
            var adocao = AdocaoMocados.Solicitacoes
                .FirstOrDefault(a => a.Id == id);

            if (adocao == null)
                return NotFound();

            return View(adocao);
        }

        public IActionResult MinhasAdocoes()
        {
            var emailUsuario = User.Identity?.Name;

            if (string.IsNullOrEmpty(emailUsuario))
                return Redirect("/Identity/Account/Login");

            var minhasAdocoes = AdocaoMocados.Solicitacoes
                .Where(a => a.Email == emailUsuario)
                .ToList();

            return View(minhasAdocoes);
        }


    }
}