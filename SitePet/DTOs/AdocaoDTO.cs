using System;

namespace SitePet.DTOs
{
    public class AdocaoDTO
    {
        public Guid Id { get; set; }

        public Guid PetId { get; set; }

        public Guid PessoaId { get; set; }

        public int Idade { get; set; }

        public string CEP { get; set; } = string.Empty;

        public string Endereco { get; set; } = string.Empty;

        public string Bairro { get; set; } = string.Empty;

        public string NumeroDaCasa { get; set; } = string.Empty;

        public string Numero { get; set; } = string.Empty;

        public string CPF { get; set; } = string.Empty;

        public DateTime DataAdocao { get; set; }
    }
}