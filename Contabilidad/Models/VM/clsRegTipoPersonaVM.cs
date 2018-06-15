using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contabilidad.Models.VM
{
    public class clsRegTipoPersonaVM
    {
        [Key]
        public long RegTipoPersonaId { get; set; }

        [Required(ErrorMessage = "{0} es Requerido")]
        public long TipoPersonaId { get; set; }

        [Required(ErrorMessage = "{0} es Requerido")]
        public long PlanGrupoId { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [Range(1, long.MaxValue, ErrorMessage = "{0} es Requerido")]
        public long EstadoId { get; set; }

        [Display(Name = "Estado")]
        public string EstadoDes { get; set; }
    }
}