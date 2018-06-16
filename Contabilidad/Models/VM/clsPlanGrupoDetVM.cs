using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Contabilidad.Models.VM
{
    public class clsPlanGrupoDetVM
    {
        [Key]
        public long PlanGrupoDetId { get; set; }

        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [Range(1, long.MaxValue, ErrorMessage = "{0} es Requerido")]
        public long PlanGrupoId { get; set; }

        [Display(Name = "Descripción")]
        [StringLength(255)]
        public string PlanGrupoDetDes { get; set; }

        [Display(Name = "Cuenta")]
        [Range(1, long.MaxValue, ErrorMessage = "{0} es Requerido")]
        [Required(ErrorMessage = "{0} es Requerido")]
        public long PlanId { get; set; }

        [Display(Name = "Cuenta Flujo")]
        [Range(1, long.MaxValue, ErrorMessage = "{0} es Requerido")]
        [Required(ErrorMessage = "{0} es Requerido")]
        public long PlanFlujoId { get; set; }

        [Display(Name = "Sucursal")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [Range(1, long.MaxValue, ErrorMessage = "{0} es Requerido")]
        public long SucursalId { get; set; }

        [Display(Name = "Centro Costo")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [Range(1, long.MaxValue, ErrorMessage = "{0} es Requerido")]
        public long CenCosId { get; set; }

        [Display(Name = "Orden")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [Range(1, long.MaxValue, ErrorMessage = "{0} es Requerido")]
        public long Orden { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [Range(1, long.MaxValue, ErrorMessage = "{0} es Requerido")]
        public long EstadoId { get; set; }

        [Display(Name = "Estado")]
        public string EstadoDes { get; set; }

        public static string _PlanGrupoDetId = nameof(PlanGrupoDetId);
        public static string _PlanGrupoId = nameof(PlanGrupoId);
        public static string _PlanGrupoDetDes = nameof(PlanGrupoDetDes);
        public static string _PlanId = nameof(PlanId);
        public static string _PlanFlujoId = nameof(PlanFlujoId);
        public static string _SucursalId = nameof(SucursalId);
        public static string _CenCosId = nameof(CenCosId);
        public static string _Orden = nameof(Orden);
        public static string _EstadoId = nameof(EstadoId);
        public static string _EstadoDes = nameof(EstadoDes);
    }
}