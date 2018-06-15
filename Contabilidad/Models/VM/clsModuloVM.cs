using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contabilidad.Models.VM
{
    public class clsModuloVM
    {
        [Key]
        public long ModuloId { get; set; }

        [Display(Name = "Código")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [StringLength(3)]
        public string ModuloCod { get; set; }

        [Display(Name = "Modulo")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [StringLength(255)]
        public string ModuloDes { get; set; }

        [Display(Name = "Especificación")]
        [StringLength(255)]
        public string ModuloEsp { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [Range(1, long.MaxValue, ErrorMessage = "{0} es Requerido")]
        public long EstadoId { get; set; }

        [NotMapped]
        [Display(Name = "Estado")]
        public string EstadoDes { get; set; }

        public static string _ModuloId = nameof(ModuloId);
        public static string _ModuloCod = nameof(ModuloCod);
        public static string _ModuloDes = nameof(ModuloDes);
        public static string _ModuloEsp = nameof(ModuloEsp);
        public static string _EstadoId = nameof(EstadoId);
    }
}