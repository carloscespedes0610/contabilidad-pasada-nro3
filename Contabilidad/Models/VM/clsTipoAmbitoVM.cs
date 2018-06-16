using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contabilidad.Models.VM
{
    public class clsTipoAmbitoVM
    {
        [Key]
        public long TipoAmbitoId { get; set; }

        [Display(Name = "Ambito")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [StringLength(255)]
        public string TipoAmbitoDes { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [Range(1, long.MaxValue, ErrorMessage = "{0} es Requerido")]
        public long EstadoId { get; set; }

        [NotMapped]
        [Display(Name = "Estado")]
        public string EstadoDes { get; set; }

        public static string _TipoAmbitoId = nameof(TipoAmbitoId);
        public static string _TipoAmbitoDes = nameofTipoAmbitoDes();
        public static string _EstadoId = nameof(EstadoId);
        public static string _EstadoDes = nameof(EstadoDes);
    }
}