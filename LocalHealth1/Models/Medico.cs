using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LocalHealth1.Models
{
    public class Medico
    {
        public string Nome { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Crm")]
        public string CrmId { get; set; }
        public string Especialidade { get; set; }
        public string Senha { get; set; }
        public ICollection<Diagnostico> diagnostico { get; set; }




    }
}
