using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contabilidad.Models.VM
{
    public class clsTipoPersonaVM
    {
        [Key]
        public long TipoPersonaId { get; set; }

        [Display(Name = "Código")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [StringLength(50)]
        public string TipoPersonaCod { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [StringLength(255)]
        public string TipoPersonaDes { get; set; }

        [Display(Name = "Tipo Relación")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [Range(1, long.MaxValue, ErrorMessage = "{0} es Requerido")]
        public long TipoRelacionId { get; set; }

        [Display(Name = "Tipo Relación")]
        public string TipoRelacionDes { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [Range(1, long.MaxValue, ErrorMessage = "{0} es Requerido")]
        public long EstadoId { get; set; }

        [Display(Name = "Estado")]
        public string EstadoDes { get; set; }

        public static string _TipoPersonaId = nameof(TipoPersonaId);
        public static string _TipoPersonaCod = nameof(TipoPersonaCod);
        public static string _TipoPersonaDes = nameof(TipoPersonaDes);
        public static string _TipoRelacionId = nameof(TipoRelacionId);
        public static string _TipoRelacionDes = nameof(TipoRelacionDes);
        public static string _EstadoId = nameof(EstadoId);
        public static string _EstadoDes = nameof(EstadoDes);
    }
}