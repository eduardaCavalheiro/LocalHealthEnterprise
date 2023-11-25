using System.Diagnostics.CodeAnalysis;

namespace LocalHealth1.Models
{
    public class Localizacao
    {

        public int Id { get; set; }
        public int Cep { get; set; }
        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }


        public string? Logradouro { get; set; }

        public ICollection<Diagnostico> diagnostico { get; set; }

    }
}
