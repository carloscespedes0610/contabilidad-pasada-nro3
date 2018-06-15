using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Contabilidad.Models.VM
{
    public class clsPlanGrupoFormVM
    {
        public ICollection<clsPlanGrupoDetVM> PlanGrupoDetVM { get; set; }

        public clsPlanGrupoFormVM()
        {
            PlanGrupoDetVM = new HashSet<clsPlanGrupoDetVM>();
        }

        [Key]
        public long PlanGrupoId { get; set; }

        [Display(Name = "Código")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [StringLength(50)]
        public string PlanGrupoCod { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [StringLength(255)]
        public string PlanGrupoDes { get; set; }

        [Display(Name = "Especificación")]
        [StringLength(255)]
        public string PlanGrupoEsp { get; set; }

        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [Range(1, long.MaxValue, ErrorMessage = "{0} es Requerido")]
        public long PlanGrupoTipoId { get; set; }

        [Display(Name = "Tipo")]
        public string PlanGrupoTipoDes { get; set; }

        [Display(Name = "Rubro")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [Range(1, long.MaxValue, ErrorMessage = "{0} es Requerido")]
        public long PlanGrupoTipoDetId { get; set; }

        [Display(Name = "Rubro")]
        public string PlanGrupoTipoDetDes { get; set; }

        [Display(Name = "Cant. Cuentas")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [Range(1, long.MaxValue, ErrorMessage = "{0} es Requerido")]
        public long NroCuentas { get; set; }

        [Display(Name = "Moneda")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [Range(1, long.MaxValue, ErrorMessage = "{0} es Requerido")]
        public long MonedaId { get; set; }

        [Display(Name = "Moneda")]
        public string MonedaDes { get; set; }

        [Display(Name = "Verificar Mto.")]
        [Required(ErrorMessage = "{0} es Requerido")]
        public bool VerificaMto { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [Range(1, long.MaxValue, ErrorMessage = "{0} es Requerido")]
        public long EstadoId { get; set; }

        [Display(Name = "Estado")]
        public string EstadoDes { get; set; }

        
    }
}