using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocalHealth1.Models
{
    public class Diagnostico
    {
        public int Id { get; set; }
        [Display(Name = "Sintomas")]
        public string SintomasPaciente { get; set; }
        public string Doenca {  get; set; }
        public DetalheDiagnostico DetalheDiagnostico { get; set; }
        public Medico Medico { get; set; }
        public string MedicoCrmId { get; set; }

        public Localizacao Localizacao { get; set; }

        public int LocalizacaoId { get; set; }
        public ICollection<Doenca> doenca { get; }

    }
}