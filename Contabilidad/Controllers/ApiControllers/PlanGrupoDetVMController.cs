using Contabilidad.Models.InMemory;
using Contabilidad.Models.Modules;
using Contabilidad.Models.VM;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;

namespace Contabilidad.Controllers.ApiControllers
{
    public class PlanGrupoDetVMController : ApiController
    {
        clsPlanGrupoDetIM db = new clsPlanGrupoDetIM();
        clsPlanGrupoDetIM dbDel = new clsPlanGrupoDetIM();

        [HttpGet]
        public HttpResponseMessage Get(DataSourceLoadOptions loadOptions, int? id)
        {
            db.PlanGrupoId = SysData.ToInteger(id);
            return Request.CreateResponse(DataSourceLoader.Load(db.PlanGrupoDetList, loadOptions));
        }

        [HttpPost]
        public HttpResponseMessage Post(FormDataCollection form)
        {
            var values = form.Get("values");

            var oPlanGrupoDetVM = new clsPlanGrupoDetVM();
            JsonConvert.PopulateObject(values, oPlanGrupoDetVM);

            Validate(oPlanGrupoDetVM);
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error HttpResponseMessage Post");

            db.PlanGrupoDetList.Add(oPlanGrupoDetVM);
            //db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Created);
        }

        [HttpPut]
        public HttpResponseMessage Put(FormDataCollection form)
        {
            var key = Convert.ToInt32(form.Get("key"));
            var values = form.Get("values");
            var oPlanGrupoDetVM = db.PlanGrupoDetList.First(e => e.PlanGrupoDetId == key);

            JsonConvert.PopulateObject(values, oPlanGrupoDetVM);

            Validate(oPlanGrupoDetVM);
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error HttpResponseMessage Put");

            //db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        public void Delete(FormDataCollection form)
        {
            var key = Convert.ToInt32(form.Get("key"));
            var oPlanGrupoDetVM = db.PlanGrupoDetList.First(e => e.PlanGrupoDetId == key);

            dbDel.PlanGrupoDetList.Add(oPlanGrupoDetVM);

            db.PlanGrupoDetList.Remove(oPlanGrupoDetVM);
            db.SaveChanges();
        }

    }
}