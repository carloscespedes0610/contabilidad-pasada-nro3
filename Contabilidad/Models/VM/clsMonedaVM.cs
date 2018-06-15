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
        [StringLength(50)]
        public string MonedaCod { get; set; }

        [Display(Name = "Moneda")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [StringLength(255)]
        public string MonedaDes { get; set; }

        public static string _MonedaId = nameof(MonedaId);
        public static string _MonedaCod = nameof(MonedaCod);
        public static string _MonedaDes = nameof(MonedaDes);

    }
}