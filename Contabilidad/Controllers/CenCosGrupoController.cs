using Contabilidad.Models.DAC;
using Contabilidad.Models.VM;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Contabilidad.Controllers
{
    [SessionExpireFilter]
    public class CenCosGrupoController : Controller
    {
        // GET: CenCosGrupo
        public ActionResult Index()
        {
            try
            {
                this.GetDefaultData();

                return View();
            }

            catch (Exception exp)
            {
                return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = exp.Message });
            }
        }

        // GET: CenCosGrupo/Create
        public ActionResult Create()
        {
            clsCenCosGrupoVM oCenCosGrupoVM = new clsCenCosGrupoVM();

            try
            {
                this.GetDefaultData();

                oCenCosGrupoVM.EstadoId = ConstEstado.Activo;
                return View(oCenCosGrupoVM);
            }

            catch (Exception exp)
            {
                return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = exp.Message });
            }
        }

        // POST: CenCosGrupo/Create
        [HttpPost()]
        [ValidateAntiForgeryToken()]
        public ActionResult Create(clsCenCosGrupoVM oCenCosGrupoVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    clsCenCosGrupo oCenCosGrupo = new clsCenCosGrupo(clsAppInfo.Connection);
                    DataMove(oCenCosGrupoVM, oCenCosGrupo, false);

                    if (oCenCosGrupo.Insert())
                    {
                        return RedirectToAction("Index");
                    }
                }

                return View(oCenCosGrupoVM);
            }

            catch (Exception exp)
            {
                ViewBag.MessageErr = exp.Message;
                return View(oCenCosGrupoVM);
            }
        }

        // GET: CenCosGrupo/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                this.GetDefaultData();

                if (ReferenceEquals(id, null))
                {
                    return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = "Índice nulo o no encontrado" });
                }

                clsCenCosGrupoVM oCenCosGrupoVM = CenCosGrupoFind(SysData.ToLong(id));

                if (ReferenceEquals(oCenCosGrupoVM, null))
                {
                    return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = "Índice no encontrado" });
                }

                return View(oCenCosGrupoVM);
            }

            catch (Exception exp)
            {
                return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = exp.Message });
            }
        }

        // POST: CenCosGrupo/Edit/5
        [HttpPost()]
        [ValidateAntiForgeryToken()]
        public ActionResult Edit(clsCenCosGrupoVM oCenCosGrupoVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    clsCenCosGrupo oCenCosGrupo = new clsCenCosGrupo(clsAppInfo.Connection);
                    DataMove(oCenCosGrupoVM, oCenCosGrupo, true);

                    if (oCenCosGrupo.Update())
                    {
                        return RedirectToAction("Index");
                    }
                }

                return View(oCenCosGrupoVM);
            }

            catch (Exception exp)
            {
                ViewBag.MessageErr = exp.Message;
                return View(oCenCosGrupoVM);
            }
        }

        // GET: CenCosGrupo/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                this.GetDefaultData();

                if (ReferenceEquals(id, null))
                {
                    return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = "Índice nulo o no encontrado" });
                }

                clsCenCosGrupoVM oCenCosGrupoVM = CenCosGrupoFind(SysData.ToLong(id));

                if (ReferenceEquals(oCenCosGrupoVM, null))
                {
                    return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = "Índice no encontrado" });
                }

                return View(oCenCosGrupoVM);
            }

            catch (Exception exp)
            {
                return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = exp.Message });
            }
        }

        // POST: CenCosGrupo/Delete/5
        [HttpPost()]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                if (ReferenceEquals(id, null))
                {
                    return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = "Índice nulo o no encontrado" });
                }

                clsCenCosGrupo oCenCosGrupo = new clsCenCosGrupo(clsAppInfo.Connection);

                oCenCosGrupo.VM.CenCosGrupoId = SysData.ToLong(id);

                if (oCenCosGrupo.Delete())
                {
                    return RedirectToAction("Index");
                }

                return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = "Error al Eliminar el Registro" });
            }

            catch (Exception exp)
            {
                return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = exp.Message });
            }
        }

        // GET: CenCos/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                this.GetDefaultData();

                if (ReferenceEquals(id, null))
                {
                    return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = "Índice nulo o no encontrado" });
                }

                clsCenCosGrupoVM oCenCosGrupoVM = CenCosGrupoFind(SysData.ToLong(id));

                if (ReferenceEquals(oCenCosGrupoVM, null))
                {
                    return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = "Índice no encontrado" });
                }

                return View(oCenCosGrupoVM);
            }

            catch (Exception exp)
            {
                return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = exp.Message });
            }
        }

        [HttpPost()]
        public ActionResult CreatePopup(clsCenCosGrupoVM oCenCosGrupoVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    clsCenCosGrupo oCenCosGrupo = new clsCenCosGrupo(clsAppInfo.Connection);
                    DataMove(oCenCosGrupoVM, oCenCosGrupo, false);

                    if (oCenCosGrupo.Insert())
                    {
                        return Content("OK");
                    }
                }

                return Content("Datos Imcompletos, Por favor Revise");
            }

            catch (Exception exp)
            {
                return Content("Error: " + exp.Message);
            }
        }

        [HttpGet]
        public ActionResult CenCosGrupoGrid(DataSourceLoadOptions loadOptions)
        {
            return Content(JsonConvert.SerializeObject(CenCosGrupoGrid()), "application/json");
        }






        private void DataMove(clsCenCosGrupoVM oCenCosGrupoVM, clsCenCosGrupo oCenCosGrupo, bool boolEditing)
        {
            if (boolEditing)
            {
                oCenCosGrupo.VM.CenCosGrupoId = SysData.ToLong(oCenCosGrupoVM.CenCosGrupoId);
            }

            oCenCosGrupo.VM.CenCosGrupoCod = SysData.ToStr(oCenCosGrupoVM.CenCosGrupoCod);
            oCenCosGrupo.VM.CenCosGrupoDes = SysData.ToStr(oCenCosGrupoVM.CenCosGrupoDes);
            oCenCosGrupo.VM.CenCosGrupoEsp = SysData.ToStr(oCenCosGrupoVM.CenCosGrupoEsp);
            oCenCosGrupo.VM.EstadoId = SysData.ToLong(oCenCosGrupoVM.EstadoId);
        }

        private List<clsCenCosGrupoVM> CenCosGrupoGrid()
        {
            clsCenCosGrupo oCenCosGrupo = new clsCenCosGrupo(clsAppInfo.Connection);
            List<clsCenCosGrupoVM> oCenCosGrupoVM = new List<clsCenCosGrupoVM>();

            try
            {
                oCenCosGrupo.SelectFilter = clsCenCosGrupo.SelectFilters.Grid;
                oCenCosGrupo.WhereFilter = clsCenCosGrupo.WhereFilters.Grid;
                oCenCosGrupo.OrderByFilter = clsCenCosGrupo.OrderByFilters.Grid;

                if (oCenCosGrupo.Open())
                {
                    foreach (DataRow dr in oCenCosGrupo.DataSet.Tables[oCenCosGrupo.TableName].Rows)
                    {
                        oCenCosGrupoVM.Add(new clsCenCosGrupoVM()
                        {
                            CenCosGrupoId = SysData.ToLong(dr[clsCenCosGrupoVM._CenCosGrupoId]),
                            CenCosGrupoCod = SysData.ToStr(dr[clsCenCosGrupoVM._CenCosGrupoCod]),
                            CenCosGrupoDes = SysData.ToStr(dr[clsCenCosGrupoVM._CenCosGrupoDes]),
                            CenCosGrupoEsp = SysData.ToStr(dr[clsCenCosGrupoVM._CenCosGrupoEsp]),
                            EstadoId = SysData.ToLong(dr[clsCenCosGrupoVM._EstadoId]),
                            EstadoDes = SysData.ToStr(dr[clsCenCosGrupoVM._EstadoDes])
                        });
                    }
                }
            }

            catch (Exception exp)
            {
                throw (exp);

            }
            finally
            {
                oCenCosGrupo.Dispose();
            }

            return oCenCosGrupoVM;
        }

        private clsCenCosGrupoVM CenCosGrupoFind(long lngCenCosGrupoId)
        {
            clsCenCosGrupo oCenCosGrupo = new clsCenCosGrupo(clsAppInfo.Connection);
            clsCenCosGrupoVM oCenCosGrupoVM = new clsCenCosGrupoVM();

            try
            {
                oCenCosGrupo.VM.CenCosGrupoId = lngCenCosGrupoId;

                if (oCenCosGrupo.FindByPK())
                {
                    oCenCosGrupoVM.CenCosGrupoId = oCenCosGrupo.VM.CenCosGrupoId;
                    oCenCosGrupoVM.CenCosGrupoCod = oCenCosGrupo.VM.CenCosGrupoCod;
                    oCenCosGrupoVM.CenCosGrupoDes = oCenCosGrupo.VM.CenCosGrupoDes;
                    oCenCosGrupoVM.CenCosGrupoEsp = oCenCosGrupo.VM.CenCosGrupoEsp;
                    oCenCosGrupoVM.EstadoId = oCenCosGrupo.VM.EstadoId;

                    return oCenCosGrupoVM;
                }
            }

            catch (Exception exp)
            {
                throw (exp);
            }
            finally
            {
                oCenCosGrupo.Dispose();
            }

            return null;
        }
    }
}