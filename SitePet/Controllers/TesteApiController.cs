using Microsoft.AspNetCore.Mvc;
using SitePet.Services;

namespace SitePet.Controllers
{
    public class TesteApiController : Controller
    {
        private readonly ApiService _apiService;

        public TesteApiController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            bool conectado = await _apiService.TestarConexaoAsync();

            if (conectado)
            {
                return Content("CONEXÃO COM A API FUNCIONOU!");
            }

            return Content("NÃO FOI POSSÍVEL CONECTAR COM A API.");
        }
    }
}