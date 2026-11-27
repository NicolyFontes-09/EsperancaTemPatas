namespace SitePet.DTOs
{
    public class PetApiDTO
    {
        public Guid Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Especie { get; set; } = string.Empty;

        public string Raca { get; set; } = string.Empty;

        public string Porte { get; set; } = string.Empty;

        public string Sexo { get; set; } = string.Empty;

        public int Idade { get; set; }

        public string Vacina { get; set; } = string.Empty;

        public string Castrado { get; set; } = string.Empty;

        public string Localização { get; set; } = string.Empty;

        public string Descrição { get; set; } = string.Empty;
    }
}