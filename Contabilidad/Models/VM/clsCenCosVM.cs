using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contabilidad.Models.VM
{
    public class clsCenCosVM
    {
        [Key]
        public long CenCosId { get; set; }

        [Display(Name = "Código")]
        [Required(ErrorMessage = "{0} es Requerido")]
        public string CenCosCod { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "{0} es Requerido")]
        public string CenCosDes { get; set; }

        [Display(Name = "Especificación")]
        public string CenCosEsp { get; set; }

        [Display(Name = "Grupo")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [Range(1, long.MaxValue, ErrorMessage = "{0} es Requerido")]
        public long CenCosGrupoId { get; set; }

        [Display(Name = "Grupo")]
        public string CenCosGrupoDes { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [Range(1, long.MaxValue, ErrorMessage = "{0} es Requerido")]
        public long EstadoId { get; set; }

        [Display(Name = "Estado")]
        public string EstadoDes { get; set; }

        public static string _CenCosId = nameof(CenCosId);
        public static string _CenCosCod = nameof(CenCosCod);
        public static string _CenCosDes = nameof(CenCosDes);
        public static string _CenCosEsp = nameof(CenCosEsp);
        public static string _CenCosGrupoId = nameof(CenCosGrupoId);
        public static string _CenCosGrupoDes = nameof(CenCosGrupoDes);
        public static string _EstadoId = nameof(EstadoId);
        public static string _EstadoDes = nameof(EstadoDes);
    }
}