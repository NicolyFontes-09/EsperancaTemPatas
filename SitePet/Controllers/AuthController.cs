
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SitePet.Services;

namespace SitePet.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApiService _apiService;

        public AuthController(ApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("Token") != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string senha)
        {
            if (string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(senha))
            {
                ModelState.AddModelError("", "Preencha o e-mail e a senha.");
                return View();
            }

            try
            {
                using var resposta = await _apiService.LoginAsync(email, senha);

                if (!resposta.IsSuccessStatusCode)
                {
                    ModelState.AddModelError("", "E-mail ou senha incorretos.");
                    return View();
                }

                using var documento = await JsonDocument.ParseAsync(
                    await resposta.Content.ReadAsStreamAsync());

                var dados = documento.RootElement;

                string? token = dados.GetProperty("token").GetString();

                var usuario = dados.GetProperty("usuario");

                string? nome = usuario.GetProperty("nome").GetString();
                string? tipo = usuario.GetProperty("tipo").GetString();
                string id = usuario.GetProperty("id").ToString();

                if (string.IsNullOrWhiteSpace(token))
                {
                    ModelState.AddModelError("", "Não foi possível acessar sua conta.");
                    return View();
                }

                HttpContext.Session.SetString("Token", token);
                HttpContext.Session.SetString("NomeUsuario", nome ?? "");
                HttpContext.Session.SetString("TipoUsuario", tipo ?? "Usuário");
                HttpContext.Session.SetString("UsuarioId", id);

                if (tipo == "Adm")
                {
                    return RedirectToAction("Index", "Pet");
                }

                return RedirectToAction("Index", "Home");
            }
            catch (HttpRequestException)
            {
                ModelState.AddModelError("", "Não foi possível conectar à API.");
                return View();
            }
            catch (JsonException)
            {
                ModelState.AddModelError("", "A resposta da API está inválida.");
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }
    }
}
