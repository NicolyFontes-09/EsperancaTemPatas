using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using SitePet.DTOs;
using SitePet.Models;

namespace SitePet.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Buscar Pets da API
        public async Task<List<PetApiDTO>> BuscarPetsAsync()
        {
            var resposta = await _httpClient.GetAsync("api/Pet");

            if (!resposta.IsSuccessStatusCode)
            {
                return new List<PetApiDTO>();
            }

            var pets = await resposta.Content
                .ReadFromJsonAsync<List<PetApiDTO>>();

            return pets ?? new List<PetApiDTO>();
        }

        // Cadastrar Pet na API
        public async Task<HttpResponseMessage> CadastrarPetAsync(
            Pet pet,
            IFormFile? foto)
        {
            var dados = new MultipartFormDataContent();

            dados.Add(new StringContent(pet.Nome), "Nome");
            dados.Add(new StringContent(pet.Especie), "Especie");
            dados.Add(new StringContent(pet.Raca), "Raca");
            dados.Add(new StringContent(pet.Porte), "Porte");
            dados.Add(new StringContent(pet.Sexo), "Sexo");

            dados.Add(
                new StringContent(pet.Idade.ToString()),
                "Idade"
            );

            dados.Add(new StringContent(pet.Vacinas), "Vacina");

            dados.Add(
                new StringContent(pet.Castrado ? "Sim" : "Não"),
                "Castrado"
            );

            dados.Add(
                new StringContent(pet.Localizacao),
                "Localização"
            );

            dados.Add(
                new StringContent(pet.Descricao),
                "Descricao"
            );

            // Enviar foto, caso tenha sido selecionada
            if (foto != null && foto.Length > 0)
            {
                var conteudoFoto = new StreamContent(
                    foto.OpenReadStream()
                );

                dados.Add(
                    conteudoFoto,
                    "Foto",
                    foto.FileName
                );
            }

            return await _httpClient.PostAsync("api/Pet", dados);
        }

        // Buscar adoções da API
        public async Task<List<AdocaoDTO>> BuscarAdocoesAsync()
        {
            var resposta = await _httpClient.GetAsync("api/Adocao");

            if (!resposta.IsSuccessStatusCode)
            {
                return new List<AdocaoDTO>();
            }

            var adocoes = await resposta.Content
                .ReadFromJsonAsync<List<AdocaoDTO>>();

            return adocoes ?? new List<AdocaoDTO>();
        }

        // Testar conexão
        public async Task<bool> TestarConexaoAsync()
        {
            try
            {
                var resposta = await _httpClient.GetAsync("api/Adocao");

                return resposta.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}