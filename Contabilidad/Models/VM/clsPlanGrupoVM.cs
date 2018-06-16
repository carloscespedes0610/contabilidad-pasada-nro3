using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Contabilidad.Models.VM
{
    public class clsPlanGrupoVM
    {
        [Key]
        public long PlanGrupoId { get; set; }

        [Display(Name = "Código")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [StringLength(50)]
        public string PlanGrupoCod { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [StringLength(255)]
        public string PlanGrupoDes { get; set; }

        [Display(Name = "Especificación")]
        [StringLength(255)]
        public string PlanGrupoEsp { get; set; }

        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [Range(1, long.MaxValue, ErrorMessage = "{0} es Requerido")]
        public long PlanGrupoTipoId { get; set; }

        [Display(Name = "Tipo")]
        public string PlanGrupoTipoDes { get; set; }

        [Display(Name = "Rubro")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [Range(1, long.MaxValue, ErrorMessage = "{0} es Requerido")]
        public long PlanGrupoTipoDetId { get; set; }

        [Display(Name = "Rubro")]
        public string PlanGrupoTipoDetDes { get; set; }

        [Display(Name = "Cant. Cuentas")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [Range(1, long.MaxValue, ErrorMessage = "{0} es Requerido")]
        public long NroCuentas { get; set; }

        [Display(Name = "Moneda")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [Range(1, long.MaxValue, ErrorMessage = "{0} es Requerido")]
        public long MonedaId { get; set; }

        [Display(Name = "Moneda")]
        public string MonedaDes { get; set; }

        [Display(Name = "Verificar Mto.")]
        [Required(ErrorMessage = "{0} es Requerido")]
        public bool VerificaMto { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [Range(1, long.MaxValue, ErrorMessage = "{0} es Requerido")]
        public long EstadoId { get; set; }

        [Display(Name = "Estado")]
        public string EstadoDes { get; set; }

        public static string _PlanGrupoId = nameof(PlanGrupoId);
        public static string _PlanGrupoCod = nameof(PlanGrupoCod);
        public static string _PlanGrupoDes = nameof(PlanGrupoDes);
        public static string _PlanGrupoEsp = nameof(PlanGrupoEsp);
        public static string _PlanGrupoTipoId = nameof(PlanGrupoTipoId);
        public static string _PlanGrupoTipoDetId = nameof(PlanGrupoTipoDetId);
        public static string _PlanGrupoTipoDetDes = nameof(PlanGrupoTipoDetDes);
        public static string _NroCuentas = nameof(NroCuentas);
        public static string _MonedaId = nameof(MonedaId);
        public static string _MonedaDes = nameof(MonedaDes);
        public static string _VerificaMto = nameof(VerificaMto);
        public static string _EstadoId = nameof(EstadoId);
        public static string _EstadoDes = nameof(EstadoDes);
    }
}