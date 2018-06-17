using Contabilidad.Models.Modules;
using Contabilidad.Models.VM;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using System.Web.Mvc;

namespace Contabilidad.Controllers
{
    public class ComboBoxController : Controller
    {
        [HttpGet]
        public ActionResult EstadoList(DataSourceLoadOptions loadOptions)
        {
            loadOptions.Sort = new[] { new SortingInfo { Selector = clsEstadoVM._EstadoDes } };

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(ComboBox.EstadoList(), loadOptions)), "application/json");
        }

        [HttpGet]
        public ActionResult CenCosGrupoList(DataSourceLoadOptions loadOptions)
        {
            loadOptions.Sort = new[] { new SortingInfo { Selector = clsCenCosGrupoVM._CenCosGrupoDes } };

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(ComboBox.CenCosGrupoList(), loadOptions)), "application/json");
        }

    }
}