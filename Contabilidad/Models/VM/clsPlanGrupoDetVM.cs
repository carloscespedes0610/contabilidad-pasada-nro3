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
        public long PlanGrupoId { get; set; }

        [Display(Name = "Descripción")]
        public string PlanGrupoDetDes { get; set; }

        [Display(Name = "Cuenta")]
        public long PlanId { get; set; }

        [Display(Name = "Cuenta Flujo")]
        public long PlanFlujoId { get; set; }

        [Display(Name = "Sucursal")]
        public long SucursalId { get; set; }

        [Display(Name = "Centro Costo")]
        public long CenCosId { get; set; }

        [Display(Name = "Orden")]
        public long Orden { get; set; }

        [Display(Name = "Estado")]
        public long EstadoId { get; set; }

        [Display(Name = "Estado")]
        public string EstadoDes { get; set; }
    }
}