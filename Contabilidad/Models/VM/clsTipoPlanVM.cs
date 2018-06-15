using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contabilidad.Models.VM
{
    public class clsTipoPlanVM
    {
        [Key]
        public long TipoPlanId { get; set; }

        [Display(Name = "Tipo Cuenta")]
        [Required(ErrorMessage = "{0} es Requerido")]
        public string TipoPlanDes { get; set; }

        [Display(Name = "Estado")]
        public long EstadoId { get; set; }

        [NotMapped]
        [Display(Name = "Estado")]
        public string EstadoDes { get; set; }
    }
}