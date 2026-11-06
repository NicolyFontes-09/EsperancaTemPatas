
using Microsoft.AspNetCore.Mvc;
using SitePet.Data;
using SitePet.Models;

namespace SitePet.Controllers
{
    public class PetController : Controller
    {
        // Lista todos os pets para gerenciamento
        public IActionResult Index()
        {
            return View(PetMocados.Pets);
        }

        // Exibe os detalhes de um pet
        public IActionResult Detalhes(int id)
        {
            var pet = PetMocados.Pets
                .FirstOrDefault(p => p.Id == id);

            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        // Abre a página de cadastro
        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        // Recebe e salva o cadastro do pet
        [HttpPost]
        public IActionResult Cadastrar(Pet pet)
        {
            pet.Id = PetMocados.Pets.Any()
                ? PetMocados.Pets.Max(p => p.Id) + 1
                : 1;

            PetMocados.Pets.Add(pet);

            return RedirectToAction("Index");
        }

        // Abre a página de edição
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

        // Recebe e salva as alterações
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

        // Abre a página de confirmação de exclusão
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

        // Confirma e executa a exclusão
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

        // Lista somente os gatos
        public IActionResult Gatos()
        {
            var pets = PetMocados.Pets
                .Where(p => p.Especie == "Gato")
                .ToList();

            return View(pets);
        }

        // Lista somente os cachorros
        public IActionResult Cachorros()
        {
            var pets = PetMocados.Pets
                .Where(p => p.Especie == "Cachorro")
                .ToList();

            return View(pets);
        }
    }
}
    