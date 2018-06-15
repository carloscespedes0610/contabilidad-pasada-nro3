using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contabilidad.Models.VM
{
    public class clsPlanGrupoTipoVM
    {
        [Key]
        public long PlanGrupoTipoId { get; set; }

        [Display(Name = "Código")]
        [Required(ErrorMessage = "{0} es Requerido")]
        public string PlanGrupoTipoCod { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "{0} es Requerido")]
        public string PlanGrupoTipoDes { get; set; }

        [Display(Name = "Especificación")]
        public string PlanGrupoTipoEsp { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [Range(1, long.MaxValue, ErrorMessage = "{0} es Requerido")]
        public long EstadoId { get; set; }

        [Display(Name = "Estado")]
        public string EstadoDes { get; set; }
    }
}