using System.Net.Http;
using System.Net.Http.Json;
using SitePet.DTOs;

namespace SitePet.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Buscar adoções na API
        public async Task<List<AdocaoDTO>> BuscarAdocoesAsync()
        {
            var resposta = await _httpClient.GetAsync("api/Adocao");

            if (!resposta.IsSuccessStatusCode)
            {
                return new List<AdocaoDTO>();
            }

            var adocoes = await resposta.Content.ReadFromJsonAsync<List<AdocaoDTO>>();

            return adocoes ?? new List<AdocaoDTO>();
        }

        // Testar conexão com a API
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