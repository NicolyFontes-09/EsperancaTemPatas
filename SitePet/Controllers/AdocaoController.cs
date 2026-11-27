
using Microsoft.AspNetCore.Mvc;
using SitePet.Models;
using SitePet.Data;

namespace SitePet.Controllers
{
    public class AdocaoController : Controller
    {
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
            if (!adocao.AceitouTermos)
            {
                ModelState.AddModelError(
                    nameof(adocao.AceitouTermos),
                    "É necessário aceitar os termos de adoção."
                );
            }

            if (!ModelState.IsValid)
            {
                return View("~/Views/Adocao/Termos.cshtml", adocao);
            }

            adocao.Id = AdocaoMocados.Solicitacoes.Count == 0
                ? 1
                : AdocaoMocados.Solicitacoes.Max(a => a.Id) + 1;

            adocao.Status = "Pendente";
            adocao.UsuarioId = string.Empty;

            AdocaoMocados.Solicitacoes.Add(adocao);

            return View("~/Views/Adocao/Solicitacao.cshtml", adocao);
        }


        [HttpPost]
        public IActionResult Termos(Adocao adocao)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Adocao/Formulario.cshtml", adocao);
            }

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

            if (adocao == null)
                return NotFound();

            adocao.Status = "Aprovada";

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
                ModelState.AddModelError(
                    "Justificativa",
                    "Informe o motivo da rejeição."
                );

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
            return View(AdocaoMocados.Solicitacoes);
        }
    }
}
