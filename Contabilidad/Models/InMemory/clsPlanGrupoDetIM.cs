using Contabilidad.Models.Modules;
using Contabilidad.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contabilidad.Models.InMemory
{
    public class clsPlanGrupoDetIM
    {
        const string SessionKey = "clsPlanGrupoDetVM";
        public long PlanGrupoId { get; set; }

        public ICollection<clsPlanGrupoDetVM> PlanGrupoDetList
        {
            get
            {
                var session = HttpContext.Current.Session;
                if (session[SessionKey] == null)
                    session[SessionKey] = ComboBox.PlanGrupoDetList(PlanGrupoId);

                return (ICollection<clsPlanGrupoDetVM>)session[SessionKey];
            }
        }

        public void SaveChanges()
        {
            foreach (var oPlanGrupoDet in PlanGrupoDetList.Where(a => a.PlanGrupoDetId == 0))
            {
                oPlanGrupoDet.PlanGrupoDetId = PlanGrupoDetList.Max(a => a.PlanGrupoDetId) + 1;
            }
        }

    }
}