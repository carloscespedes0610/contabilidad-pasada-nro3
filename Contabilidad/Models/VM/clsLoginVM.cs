using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contabilidad.Models.VM
{
    public class clsLoginVM
    {
        [Key]
        public long UsuarioId { get; set; }

        [Display(Name = "Cuenta de Usuario")]
        [Required(ErrorMessage = "{0} es Requerido")]
        public string UsuarioCod { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [DataType(DataType.Password)]
        public string UsuarioPass { get; set; }

        [NotMapped]
        public long EmpresaId { get; set; }

        public static string _UsuarioId = nameof(UsuarioId);
        public static string _UsuarioPass = nameof(UsuarioPass);
        public static string _EmpresaId = nameof(EmpresaId);
    }
}