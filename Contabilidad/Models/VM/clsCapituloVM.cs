using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contabilidad.Models.VM
{
    public class clsCapituloVM
    {
        [Key]
        public long CapituloId { get; set; }

        [Display(Name = "Tipo Capítulo")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [Range(1, long.MaxValue, ErrorMessage = "{0} es Requerido")]
        public long TipoCapituloId { get; set; }

        [Display(Name = "Tipo Capítulo")]
        public string TipoCapituloDes { get; set; }

        [Display(Name = "Orden")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [Range(1, long.MaxValue, ErrorMessage = "{0} es Requerido")]
        public long Orden { get; set; }

        [Display(Name = "Código")]
        [Required(ErrorMessage = "{0} es Requerido")]
        public string CapituloCod { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "{0} es Requerido")]
        public string CapituloDes { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [Range(1, long.MaxValue, ErrorMessage = "{0} es Requerido")]
        public long EstadoId { get; set; }

        [Display(Name = "Estado")]
        public string EstadoDes { get; set; }

        public static string _CapituloId = nameof(CapituloId);
        public static string _TipoCapituloId = nameof(TipoCapituloId);
        public static string _TipoCapituloDes = nameof(TipoCapituloDes);
        public static string _Orden = nameof(Orden);
        public static string _CapituloCod = nameof(CapituloCod);
        public static string _CapituloDes = nameof(CapituloDes);
        public static string _EstadoId = nameof(EstadoId);
        public static string _EstadoDes = nameof(EstadoDes);
    }
}