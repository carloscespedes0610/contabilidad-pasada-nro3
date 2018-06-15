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
        public string PlanCod { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "{0} es Requerido")]
        public string PlanDes { get; set; }

        [Display(Name = "Especificación")]
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
        public long Nivel { get; set; }

        [Display(Name = "Moneda")]
        public long MonedaId { get; set; }

        [Display(Name = "Moneda")]
        public string MonedaDes { get; set; }

        [Display(Name = "Ambito")]
        public long TipoAmbitoId { get; set; }

        [Display(Name = "Cuenta Ajuste")]
        public long PlanAjusteId { get; set; }

        [Display(Name = "Capitulo")]
        public long CapituloId { get; set; }

        [Display(Name = "Cuenta Padre")]
        public long PlanPadreId { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [Range(1, long.MaxValue, ErrorMessage = "{0} es Requerido")]
        public long EstadoId { get; set; }

        [Display(Name = "Estado")]
        public string EstadoDes { get; set; }
    }
}