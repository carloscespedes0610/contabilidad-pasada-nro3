using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contabilidad.Models.VM
{
    public class clsMonedaVM
    {
        [Key]
        public long MonedaId { get; set; }

        [Display(Name = "Código")]
        [Required(ErrorMessage = "{0} es Requerido")]
        public string MonedaCod { get; set; }

        [Display(Name = "Moneda")]
        [Required(ErrorMessage = "{0} es Requerido")]
        public string MonedaDes { get; set; }
    }
}