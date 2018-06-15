using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contabilidad.Models.VM
{
    public class clsCenCosGrupoVM
    {
        [Key]
        public long CenCosGrupoId { get; set; }

        [Display(Name = "Código")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [StringLength(50)]
        public string CenCosGrupoCod { get; set; }

        [Display(Name = "Descripción")]
        [StringLength(255)]
        [Required(ErrorMessage = "{0} es Requerido")]
        public string CenCosGrupoDes { get; set; }

        [Display(Name = "Especificación")]
        [StringLength(255)]
        public string CenCosGrupoEsp { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [Range(1, long.MaxValue, ErrorMessage = "{0} es Requerido")]
        public long EstadoId { get; set; }

        [Display(Name = "Estado")]
        public string EstadoDes { get; set; }

        public static string _CenCosGrupoId = nameof(CenCosGrupoId);
        public static string _CenCosGrupoCod = nameof(CenCosGrupoCod);
        public static string _CenCosGrupoDes = nameof(CenCosGrupoDes);
        public static string _CenCosGrupoEsp = nameof(CenCosGrupoEsp);
        public static string _EstadoId = nameof(EstadoId);
        public static string _EstadoDes = nameof(EstadoDes);
    }
}