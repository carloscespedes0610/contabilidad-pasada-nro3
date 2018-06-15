using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contabilidad.Models.VM
{
   public class clsAplicacionVM
   {
      [Key]
      public long AplicacionId { get; set; }

      [Display(Name = "Código")]
      [Required(ErrorMessage = "{0} es Requerido")]
      public string AplicacionCod { get; set; }

      [Display(Name = "Descripción")]
      [Required(ErrorMessage = "{0} es Requerido")]
      public string AplicacionDes { get; set; }

      [Display(Name = "Especificación")]
      public string AplicacionEsp { get; set; }

      [Display(Name = "Módulo")]
      public long ModuloId { get; set; }

      [NotMapped]
      [Display(Name = "Módulo")]
      public string ModuloDes { get; set; }

      [Display(Name = "Estado")]
      public long EstadoId { get; set; }

      [NotMapped]
      [Display(Name = "Estado")]
      public string EstadoDes { get; set; }

      public static string _AplicacionId = nameof(AplicacionId);
      public static string _AplicacionCod = nameof(AplicacionCod);
      public static string _AplicacionDes = nameof(AplicacionDes);
      public static string _AplicacionEsp = nameof(AplicacionEsp);
      public static string _ModuloId = nameof(ModuloId);
      public static string _EstadoId = nameof(EstadoId);
   }
}