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

        [HttpGet]
        public ActionResult TipoPlanList(DataSourceLoadOptions loadOptions)
        {
            loadOptions.Sort = new[] { new SortingInfo { Selector = clsTipoPlanVM._TipoPlanDes } };

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(ComboBox.TipoPlanList(), loadOptions)), "application/json");
        }

        [HttpGet]
        public ActionResult MonedaList(DataSourceLoadOptions loadOptions)
        {
            //loadOptions.Sort = new[] { new SortingInfo { Selector = clsMonedaVM._MonedaDes } };

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(ComboBox.MonedaList(), loadOptions)), "application/json");
        }

        [HttpGet]
        public ActionResult TipoAmbitoList(DataSourceLoadOptions loadOptions)
        {
            loadOptions.Sort = new[] { new SortingInfo { Selector = clsTipoAmbitoVM._TipoAmbitoDes } };

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(ComboBox.TipoAmbitoList(), loadOptions)), "application/json");
        }

        [HttpGet]
        public ActionResult PlanGrupoTipoList(DataSourceLoadOptions loadOptions)
        {
            loadOptions.Sort = new[] { new SortingInfo { Selector = clsPlanGrupoTipoVM._PlanGrupoTipoDes } };

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(ComboBox.PlanGrupoTipoList(), loadOptions)), "application/json");
        }

        [HttpGet]
        public ActionResult PlanGrupoTipoDetList(DataSourceLoadOptions loadOptions)
        {
            loadOptions.Sort = new[] { new SortingInfo { Selector = clsPlanGrupoTipoDetVM._PlanGrupoTipoDetDes } };

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(ComboBox.PlanGrupoTipoDetList(), loadOptions)), "application/json");
        }

        [HttpGet]
        public ActionResult PlanList(DataSourceLoadOptions loadOptions)
        {
            loadOptions.Sort = new[] { new SortingInfo { Selector = clsPlanVM._TipoPlanDes } };

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(ComboBox.PlanList(), loadOptions)), "application/json");
        }

        [HttpGet]
        public ActionResult SucursalList(DataSourceLoadOptions loadOptions)
        {
            loadOptions.Sort = new[] { new SortingInfo { Selector = clsSucursalVM._SucursalDes } };

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(ComboBox.SucursalList(), loadOptions)), "application/json");
        }

        [HttpGet]
        public ActionResult CenCosList(DataSourceLoadOptions loadOptions)
        {
            loadOptions.Sort = new[] { new SortingInfo { Selector = clsCenCosVM._CenCosDes } };

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(ComboBox.CenCosList(), loadOptions)), "application/json");
        }

        [HttpGet]
        public ActionResult PlanGrupoDetList(DataSourceLoadOptions loadOptions)
        {
            loadOptions.Sort = new[] { new SortingInfo { Selector = clsPlanGrupoDetVM._PlanGrupoDetDes } };

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(ComboBox.PlanGrupoDetList(), loadOptions)), "application/json");
        }

        [HttpGet]
        public ActionResult PlanGrupoDetList(DataSourceLoadOptions loadOptions, int PlangrupoId)
        {
            loadOptions.Sort = new[] { new SortingInfo { Selector = clsPlanGrupoDetVM._PlanGrupoDetDes } };

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(ComboBox.PlanGrupoDetList(PlangrupoId), loadOptions)), "application/json");
        }

        [HttpGet]
        public ActionResult TipoPersonaList(DataSourceLoadOptions loadOptions)
        {
            loadOptions.Sort = new[] { new SortingInfo { Selector = clsTipoPersonaVM._TipoPersonaDes } };

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(ComboBox.TipoPersonaList(), loadOptions)), "application/json");
        }

       
    }
}