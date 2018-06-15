using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contabilidad.Models.VM
{
    public class clsTipoAmbitoVM
    {
        [Key]
        public long TipoAmbitoId { get; set; }

        [Display(Name = "Ambito")]
        [Required(ErrorMessage = "{0} es Requerido")]
        public string TipoAmbitoDes { get; set; }

        [Display(Name = "Estado")]
        public long EstadoId { get; set; }

        [NotMapped]
        [Display(Name = "Estado")]
        public string EstadoDes { get; set; }
    }
}