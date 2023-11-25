using System.ComponentModel.DataAnnotations;

namespace LocalHealth1.Models
{
    public class Doenca
    {
        [Key]
        [Display(Name = "Número Cid")]
        public string NrCid { get; set; }

        public string Nome { get; set; }

        public string Sintomas { get; set; }

        public ICollection<Diagnostico> diagnostico { get; set; }


    }
}
