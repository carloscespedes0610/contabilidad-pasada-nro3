using Contabilidad.Models.DAC;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Contabilidad.Models.VM
{
    public class clsPlanGrupoFormVM
    {
        public ICollection<clsPlanGrupoDetVM> PlanGrupoDetVM { get; set; }

        public clsPlanGrupoVM VM { get; set; }

        public clsPlanGrupoFormVM()
        {
            PlanGrupoDetVM = new HashSet<clsPlanGrupoDetVM>();
            VM = new clsPlanGrupoVM();
        }

        public List<clsPlanGrupoDetVM> getListPlanGrupoDetVM() {
            return (List<clsPlanGrupoDetVM>)PlanGrupoDetVM;
        }


    }
}