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
        public string SucursalCod { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "{0} es Requerido")]
        public string SucursalDes { get; set; }

        [Display(Name = "Especificación")]
        public string SucursalEsp { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [Range(1, long.MaxValue, ErrorMessage = "{0} es Requerido")]
        public long EstadoId { get; set; }

        [Display(Name = "Estado")]
        public string EstadoDes { get; set; }
    }
}