
using System.ComponentModel.DataAnnotations;

namespace SitePet.Models
{
    public class Adocao
    {
        public int Id { get; set; }

        // Pet escolhido
        [Required]
        public int PetId { get; set; }

        [Required]
        public string NomePet { get; set; } = string.Empty;

        // Dados do adotante
        [Required(ErrorMessage = "Informe seu nome completo.")]
        public string NomeCompleto { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe seu e-mail.")]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe seu telefone.")]
        public string Telefone { get; set; } = string.Empty;

        [Range(18, 120, ErrorMessage = "A idade deve ser igual ou superior a 18 anos.")]
        public int Idade { get; set; }

        
        

        // Endereço completo
        [Required]
        public string CEP { get; set; } = string.Empty;

        [Required]
        public string Rua { get; set; } = string.Empty;

        [Required]
        public string Numero { get; set; } = string.Empty;

        public string? Complemento { get; set; }

        [Required]
        public string Bairro { get; set; } = string.Empty;

        [Required]
        public string Cidade { get; set; } = string.Empty;

        [Required]
        public string Estado { get; set; } = string.Empty;

        // Conexão com usuário
        public string UsuarioId { get; set; } = string.Empty;

        public string Status { get; set; } = "Pendente";

        public string? Justificativa { get; set; }

        [Required(ErrorMessage = "É necessário aceitar os termos de adoção.")]
        public bool AceitouTermos { get; set; }
    }
}
