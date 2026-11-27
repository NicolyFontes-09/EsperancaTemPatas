
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using SitePet.DTOs;
using SitePet.Models;
using Microsoft.AspNetCore.Http;

namespace SitePet.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApiService(
            HttpClient httpClient,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        // Login na API
        public async Task<HttpResponseMessage> LoginAsync(
            string email,
            string senha)
        {
            var dados = new
            {
                Email = email,
                Senha = senha
            };

            return await _httpClient.PostAsJsonAsync(
                "api/Auth/login",
                dados
            );
        }

        // Criar requisição autenticada
        private async Task<HttpResponseMessage> EnviarComTokenAsync(
            HttpMethod metodo,
            string endereco,
            HttpContent? conteudo = null)
        {
            var token = _httpContextAccessor.HttpContext?
                .Session.GetString("Token");

            var requisicao = new HttpRequestMessage(metodo, endereco);

            if (conteudo != null)
            {
                requisicao.Content = conteudo;
            }

            if (!string.IsNullOrWhiteSpace(token))
            {
                requisicao.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            return await _httpClient.SendAsync(requisicao);
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

            return await EnviarComTokenAsync(
                HttpMethod.Post,
                "api/Pet",
                dados
            );
        }

        // Buscar adoções da API
        public async Task<List<AdocaoDTO>> BuscarAdocoesAsync()
        {
            var resposta = await EnviarComTokenAsync(
                HttpMethod.Get,
                "api/Adocao"
            );

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
                var resposta = await EnviarComTokenAsync(
                    HttpMethod.Get,
                    "api/Adocao"
                );

                return resposta.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
