using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Contabilidad.Models.VM
{
    public class clsPlanGrupoAbmVM
    {
        public clsPlanGrupoVM PlanGrupoVM { get; set; }
        public clsPlanGrupoDetVM PlanGrupoDetVM { get; set; }

        public clsPlanGrupoAbmVM() {
            PlanGrupoVM = new clsPlanGrupoVM();
            PlanGrupoDetVM = new clsPlanGrupoDetVM();
        }

    }
}