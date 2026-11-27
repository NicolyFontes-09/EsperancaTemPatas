namespace SitePet.Models
{
    public class Pet
    {
        public int Id { get; set; }

        // ID original do Pet na API
        public Guid ApiId { get; set; }

        public string Nome { get; set; } = string.Empty;
        public string Especie { get; set; } = string.Empty;
        public int Idade { get; set; }
        public string Porte { get; set; } = string.Empty;
        public string Vacinas { get; set; } = string.Empty;
        public bool Castrado { get; set; }
        public string Localizacao { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Foto { get; set; } = string.Empty;
        public string Raca { get; set; } = string.Empty;
        public string Sexo { get; set; } = string.Empty;
    }
}