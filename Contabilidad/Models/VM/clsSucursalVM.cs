using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contabilidad.Models.VM
{
    public class clsSucursalVM
    {
        [Key]
        public long SucursalId { get; set; }

        [Display(Name = "Código")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [StringLength(50)]
        public string SucursalCod { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [StringLength(255)]
        public string SucursalDes { get; set; }

        [Display(Name = "Especificación")]
        [StringLength(255)]
        [Required(ErrorMessage = "{0} es Requerido")]
        public string SucursalEsp { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [Range(1, long.MaxValue, ErrorMessage = "{0} es Requerido")]
        public long EstadoId { get; set; }

        [Display(Name = "Estado")]
        public string EstadoDes { get; set; }

        public static string _SucursalId = nameof(SucursalId);
        public static string _SucursalCod = nameof(SucursalCod);
        public static string _SucursalDes = nameof(SucursalDes);
        public static string _SucursalEsp = nameof(SucursalEsp);
        public static string _EstadoId = nameof(EstadoId);
        public static string _EstadoDes = nameof(EstadoDes);
    }
}