using Microsoft.AspNetCore.Mvc;
using SitePet.Data;
using SitePet.Models;
using SitePet.Services;

namespace SitePet.Controllers
{
    public class PetController : Controller
    {
        private readonly ApiService _apiService;

        public PetController(ApiService apiService)
        {
            _apiService = apiService;
        }

        // Busca os Pets da API
        private async Task<List<Pet>> BuscarPets()
        {
            var petsApi = await _apiService.BuscarPetsAsync();

            var pets = new List<Pet>();

            int contador = 1;

            foreach (var petApi in petsApi)
            {
                pets.Add(new Pet
                {
                    Id = contador,
                    ApiId = petApi.Id,
                    Nome = petApi.Nome,
                    Especie = petApi.Especie,
                    Raca = petApi.Raca,
                    Sexo = petApi.Sexo,
                    Idade = petApi.Idade,
                    Porte = petApi.Porte,
                    Vacinas = petApi.Vacina,
                    Castrado = petApi.Castrado == "Sim",
                    Localizacao = petApi.Localização,
                    Descricao = petApi.Descrição,
                    Foto = string.Empty
                });

                contador++;
            }

            return pets;
        }

        // Lista todos os Pets
        public async Task<IActionResult> Index()
        {
            var pets = await BuscarPets();

            return View(pets);
        }

        // Detalhes do Pet
        public async Task<IActionResult> Detalhes(int id)
        {
            var pets = await BuscarPets();

            var pet = pets.FirstOrDefault(p => p.Id == id);

            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        // Abre cadastro
        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        // Cadastra Pet na API
        [HttpPost]
        public async Task<IActionResult> Cadastrar(
            Pet pet,
            IFormFile? Foto)
        {
            if (!ModelState.IsValid)
            {
                return View(pet);
            }

            try
            {
                var resposta = await _apiService.CadastrarPetAsync(
                    pet,
                    Foto
                );

                if (resposta.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                var erro = await resposta.Content.ReadAsStringAsync();

                ViewBag.ErroCadastro =
                    "Não foi possível cadastrar o Pet na API. " + erro;

                return View(pet);
            }
            catch (Exception ex)
            {
                ViewBag.ErroCadastro =
                    "Erro ao conectar com a API: " + ex.Message;

                return View(pet);
            }
        }

        // Abre edição
        [HttpGet]
        public IActionResult Editar(int id)
        {
            var pet = PetMocados.Pets
                .FirstOrDefault(p => p.Id == id);

            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        // Salva edição (ainda usando mocados)
        [HttpPost]
        public IActionResult Editar(Pet pet)
        {
            var petExistente = PetMocados.Pets
                .FirstOrDefault(p => p.Id == pet.Id);

            if (petExistente == null)
            {
                return NotFound();
            }

            petExistente.Nome = pet.Nome;
            petExistente.Especie = pet.Especie;
            petExistente.Raca = pet.Raca;
            petExistente.Sexo = pet.Sexo;
            petExistente.Idade = pet.Idade;
            petExistente.Porte = pet.Porte;
            petExistente.Vacinas = pet.Vacinas;
            petExistente.Castrado = pet.Castrado;
            petExistente.Localizacao = pet.Localizacao;
            petExistente.Foto = pet.Foto;
            petExistente.Descricao = pet.Descricao;

            return RedirectToAction("Index");
        }

        // Abre exclusão
        [HttpGet]
        public IActionResult Excluir(int id)
        {
            var pet = PetMocados.Pets
                .FirstOrDefault(p => p.Id == id);

            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        // Exclui (ainda usando mocados)
        [HttpPost]
        public IActionResult ConfirmarExclusao(int id)
        {
            var pet = PetMocados.Pets
                .FirstOrDefault(p => p.Id == id);

            if (pet == null)
            {
                return NotFound();
            }

            PetMocados.Pets.Remove(pet);

            return RedirectToAction("Index");
        }

        // Lista gatos
        public async Task<IActionResult> Gatos()
        {
            var pets = await BuscarPets();

            var gatos = pets
                .Where(p => p.Especie == "Gato")
                .ToList();

            return View(gatos);
        }

        // Lista cachorros
        public async Task<IActionResult> Cachorros()
        {
            var pets = await BuscarPets();

            var cachorros = pets
                .Where(p => p.Especie == "Cachorro")
                .ToList();

            return View(cachorros);
        }
    }
}