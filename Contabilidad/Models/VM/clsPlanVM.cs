using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contabilidad.Models.VM
{
    public class clsPlanVM
    {
        [Key]
        public long PlanId { get; set; }

        [Display(Name = "Código")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [StringLength(50)]
        public string PlanCod { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [StringLength(255)]
        public string PlanDes { get; set; }

        [Display(Name = "Especificación")]
        [StringLength(255)]
        public string PlanEsp { get; set; }

        [Display(Name = "Tipo Cuenta")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [Range(1, long.MaxValue, ErrorMessage = "{0} es Requerido")]
        public long TipoPlanId { get; set; }

        [Display(Name = "Tipo Cuenta")]
        public string TipoPlanDes { get; set; }

        [Display(Name = "Orden")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [Range(1, long.MaxValue, ErrorMessage = "{0} es Requerido")]
        public long Orden { get; set; }

        [Display(Name = "Nivel")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [Range(1, long.MaxValue, ErrorMessage = "{0} es Requerido")]
        public long Nivel { get; set; }

        [Display(Name = "Moneda")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [Range(0, long.MaxValue, ErrorMessage = "{0} es Requerido")]
        public long MonedaId { get; set; }

        [Display(Name = "Moneda")]
        public string MonedaDes { get; set; }

        [Display(Name = "Ambito")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [Range(0, long.MaxValue, ErrorMessage = "{0} es Requerido")]
        public long TipoAmbitoId { get; set; }

        [Display(Name = "Cuenta Ajuste")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [Range(0, long.MaxValue, ErrorMessage = "{0} es Requerido")]
        public long PlanAjusteId { get; set; }

        [Display(Name = "Capitulo")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [Range(1, long.MaxValue, ErrorMessage = "{0} es Requerido")]
        public long CapituloId { get; set; }

        [Display(Name = "Cuenta Padre")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [Range(1, long.MaxValue, ErrorMessage = "{0} es Requerido")]
        public long PlanPadreId { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [Range(1, long.MaxValue, ErrorMessage = "{0} es Requerido")]
        public long EstadoId { get; set; }

        [Display(Name = "Estado")]
        public string EstadoDes { get; set; }

        public static string _PlanId = nameof(PlanId);
        public static string _PlanCod = nameof(PlanCod);
        public static string _PlanDes = nameof(PlanDes);
        public static string _PlanEsp = nameof(PlanEsp);
        public static string _TipoPlanId = nameof(TipoPlanId);
        public static string _TipoPlanDes = nameof(TipoPlanDes);
        public static string _Orden = nameof(Orden);
        public static string _Nivel = nameof(Nivel);
        public static string _MonedaId = nameof(MonedaId);
        public static string _MonedaDes = nameof(MonedaDes);
        public static string _TipoAmbitoId = nameof(TipoAmbitoId);
        public static string _PlanAjusteId = nameof(PlanAjusteId);
        public static string _CapituloId = nameof(CapituloId);
        public static string _PlanPadreId = nameof(PlanPadreId);
        public static string _EstadoId = nameof(EstadoId);
        public static string _EstadoDes = nameof(EstadoDes);
    }
}