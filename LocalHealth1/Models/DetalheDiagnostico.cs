using System.ComponentModel.DataAnnotations;

namespace LocalHealth1.Models
    {
        public class DetalheDiagnostico
        {
            public int Id { get; set; }
            public DateTime Data { get; set; }

            [Display(Name= "Exames")]
            public string ExamesSolicitados { get; set; }

            [Display(Name = "Nome Paciente")]
            public string NomePaciente { get; set; }
            public Diagnostico Diagnostico { get; set; }
            public int DiagnosticoId { get; set; }
        }
    }
