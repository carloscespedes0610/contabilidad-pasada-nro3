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
        [StringLength(255)]
        public string TipoPlanDes { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [Range(1, long.MaxValue, ErrorMessage = "{0} es Requerido")]
        public long EstadoId { get; set; }

        [NotMapped]
        [Display(Name = "Estado")]
        public string EstadoDes { get; set; }

        public static string _TipoPlanId = nameof(TipoPlanId);
        public static string _TipoPlanDes = nameof(TipoPlanDes);
        public static string _EstadoId = nameof(EstadoId);
        public static string _EstadoDes = nameof(EstadoDes);

    }
}