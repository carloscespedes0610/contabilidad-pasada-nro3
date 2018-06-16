using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contabilidad.Models.VM
{
    public class clsPlanGrupoTipoDetVM
    {
        [Key]
        public long PlanGrupoTipoDetId { get; set; }

        [Display(Name = "Código")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [StringLength(50)]
        public string PlanGrupoTipoDetCod { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [StringLength(255)]
        public string PlanGrupoTipoDetDes { get; set; }

        [Display(Name = "Especificación")]
        [StringLength(255)]
        public string PlanGrupoTipoDetEsp { get; set; }

        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [Range(1, long.MaxValue, ErrorMessage = "{0} es Requerido")]
        public long PlanGrupoTipoId { get; set; }

        [Display(Name = "Tipo")]
        public string PlanGrupoTipoDes { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [Range(1, long.MaxValue, ErrorMessage = "{0} es Requerido")]
        public long EstadoId { get; set; }

        [Display(Name = "Estado")]
        public string EstadoDes { get; set; }

        public static string _PlanGrupoTipoDetId = nameof(PlanGrupoTipoDetId);
        public static string _PlanGrupoTipoDetCod = nameof(PlanGrupoTipoDetCod);
        public static string _PlanGrupoTipoDetDes = nameof(PlanGrupoTipoDetDes);
        public static string _PlanGrupoTipoDetEsp = nameof(PlanGrupoTipoDetEsp);
        public static string _PlanGrupoTipoId = nameof(PlanGrupoTipoId);
        public static string _PlanGrupoTipoDes = nameof(PlanGrupoTipoDes);
        public static string _EstadoId = nameof(EstadoId);
        public static string _EstadoDes = nameof(EstadoDes);
    }
}