using System.ComponentModel.DataAnnotations;

namespace SitePet.Models
{
    public class Adocao
    {
        public int Id { get; set; }

        // Pet escolhido
        public int PetId { get; set; }
        public string NomePet { get; set; } = string.Empty;

        // Dados do adotante
        public string NomeCompleto { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public int Idade { get; set; }

        // Endereço completo
        public string CEP { get; set; } = string.Empty;
        public string Rua { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string? Complemento { get; set; }
        public string Bairro { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;

        //CONEXÃO
        public string UsuarioId { get; set; } = string.Empty;
        public string Status { get; set; } = "Pendente";
        public string? Justificativa { get; set; }

    }
}