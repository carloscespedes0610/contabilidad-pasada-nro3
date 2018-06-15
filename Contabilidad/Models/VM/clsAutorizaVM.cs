using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contabilidad.Models.VM
{
    public class clsAutorizaVM
    {
        [Key]
        public long AutorizaId { get; set; }

        [Display(Name = "RegistroId")]
        public long RegistroId { get; set; }

        [Display(Name = "Descripción")]
        public string AutorizaDes { get; set; }

        [Display(Name = "Autoriza Item")]
        public long AutorizaItemId { get; set; }

        [NotMapped]
        [Display(Name = "Autoriza Item")]
        public string AutorizaItemDes { get; set; }

        [Display(Name = "Tipo Usuario")]
        public long TipoUsuarioId { get; set; }

        [NotMapped]
        [Display(Name = "Tipo Usuario")]
        public string TipoUsuarioDes { get; set; }

        [Display(Name = "Módulo")]
        public long ModuloId { get; set; }

        [NotMapped]
        [Display(Name = "Módulo")]
        public string ModuloDes { get; set; }

        public static string _AutorizaId = nameof(AutorizaId);
        public static string _TipoUsuarioId = nameof(TipoUsuarioId);
        public static string _AutorizaDes = nameof(AutorizaDes);
        public static string _RegistroId = nameof(RegistroId);
        public static string _AutorizaItemId = nameof(AutorizaItemId);
        public static string _ModuloId = nameof(ModuloId);
    }
}