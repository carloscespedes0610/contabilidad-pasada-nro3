using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contabilidad.Models.VM
{
    public class clsUsuarioVM
    {
        [Key]
        public long UsuarioId { get; set; }

        [Display(Name = "Cuenta de Usuario")]
        [Required(ErrorMessage = "{0} es Requerido")]
        public string UsuarioCod { get; set; }

        [Display(Name = "Usuario")]
        public string UsuarioDes { get; set; }

        [Display(Name = "Tipo Usuario")]
        public long TipoUsuarioId { get; set; }

        [NotMapped]
        [Display(Name = "Tipo Usuario")]
        public string TipoUsuarioDes { get; set; }

        [Display(Name = "Repositorio")]
        public string UsuarioDocPath { get; set; }

        [Display(Name = "Avatar")]
        public string UsuarioFotoPath { get; set; }

        [Display(Name = "Maximo Sesiones")]
        public long UsuarioMaxSes { get; set; }

        [Display(Name = "Estado")]
        public long EstadoId { get; set; }

        [NotMapped]
        [Display(Name = "Estado")]
        public string EstadoDes { get; set; }

        [Display(Name = "Contraseña")]
        //[Required(ErrorMessage = "{0} es Requerido")]
        [DataType(DataType.Password)]
        public string UsuarioPass { get; set; }

        [NotMapped]
        public long EmpresaId { get; set; }

        public static string _UsuarioId = nameof(UsuarioId);
        public static string _UsuarioCod = nameof(UsuarioCod);
        public static string _UsuarioDes = nameof(UsuarioDes);
        public static string _TipoUsuarioId = nameof(TipoUsuarioId);
        public static string _UsuarioDocPath = nameof(UsuarioDocPath);
        public static string _UsuarioFotoPath = nameof(UsuarioFotoPath);
        public static string _UsuarioMaxSes = nameof(UsuarioMaxSes);
        public static string _EstadoId = nameof(EstadoId);
    }
}