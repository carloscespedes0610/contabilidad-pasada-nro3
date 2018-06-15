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
    }
}