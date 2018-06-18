using Contabilidad.Models.DAC;
using Contabilidad.Models.VM;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Contabilidad.Controllers
{
    [SessionExpireFilter]
    public class RegTipoPersonaController : Controller
    {
        // GET: RegTipoPersona
        public ActionResult Index()
        {
            try
            {
                this.GetDefaultData();

                var lstPlanGrupo = TipoPersonaGrid();

                return View(lstPlanGrupo);
            }

            catch (Exception exp)
            {
                return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = exp.Message });
            }
        }

        // GET: RegTipoPersona/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                this.GetDefaultData();

                if (ReferenceEquals(id, null))
                {
                    return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = "Índice nulo o no encontrado" });
                }

                var lstPlanGrupo = PlanGrupoGrid();

                if (ReferenceEquals(lstPlanGrupo, null))
                {
                    return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = "Índice no encontrado" });
                }                

                var strPlanGrupoId = PlanGrupoIdLoad(SysData.ToLong(id));

                ViewBag.TipoPersonaId = SysData.ToInteger(id);
                ViewBag.TipoPersonaDes = "Clientes Locales";
                ViewBag.strPlanGrupoId = strPlanGrupoId;

                //ViewBagLoad();
                return View(lstPlanGrupo);
            }

            catch (Exception exp)
            {
                return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = exp.Message });
            }
        }

        public ActionResult EditPost(int? id, string SelectedKeys)
        {
            clsRegTipoPersona oRegTipoPersona = new clsRegTipoPersona(clsAppInfo.Connection);
            long lngTipoPersonaId = SysData.ToLong(id);
            long lngRowCount = 0;

            try
            {
                JArray ArrayId = JArray.Parse(SelectedKeys);

                if (ModelState.IsValid)
                {
                    oRegTipoPersona.BeginTransaction();

                    oRegTipoPersona.DeleteFilter = clsRegTipoPersona.DeleteFilters.TipoPersonaId;
                    oRegTipoPersona.Delete();

                    foreach (var lngId in ArrayId)
                    {
                        oRegTipoPersona.VM.TipoPersonaId = lngTipoPersonaId;
                        oRegTipoPersona.VM.PlanGrupoId = SysData.ToLong(lngId);
                        oRegTipoPersona.VM.EstadoId = ConstEstado.Activo;

                        if (oRegTipoPersona.Insert())
                        {
                            lngRowCount += 1;
                        }
                    }

                    if (ArrayId.Count == lngRowCount)
                    {
                        oRegTipoPersona.Commit();
                        return RedirectToAction("Index");
                    }

                    oRegTipoPersona.Rollback();
                }

                return RedirectToAction("Edit", new { id = lngTipoPersonaId });
            }

            catch (Exception exp)
            {
                oRegTipoPersona.Rollback();

                ViewBag.MessageErr = exp.Message;
                return RedirectToAction("Edit", new { id = lngTipoPersonaId });
            }
        }







        private List<clsTipoPersonaVM> TipoPersonaGrid()
        {
            clsTipoPersona oTipoPersona = new clsTipoPersona(clsAppInfo.Connection);
            List<clsTipoPersonaVM> oTipoPersonaVM = new List<clsTipoPersonaVM>();

            try
            {
                oTipoPersona.SelectFilter = clsTipoPersona.SelectFilters.Grid;
                oTipoPersona.WhereFilter = clsTipoPersona.WhereFilters.Grid;
                oTipoPersona.OrderByFilter = clsTipoPersona.OrderByFilters.Grid;

                if (oTipoPersona.Open())
                {
                    foreach (DataRow dr in oTipoPersona.DataSet.Tables[oTipoPersona.TableName].Rows)
                    {
                        oTipoPersonaVM.Add(new clsTipoPersonaVM()
                        {
                            TipoPersonaId = SysData.ToLong(dr[clsTipoPersonaVM._TipoPersonaId]),
                            TipoPersonaCod = SysData.ToStr(dr[clsTipoPersonaVM._TipoPersonaCod]),
                            TipoPersonaDes = SysData.ToStr(dr[clsTipoPersonaVM._TipoPersonaDes]),
                            TipoRelacionId = SysData.ToLong(dr[clsTipoPersonaVM._TipoRelacionId]),
                            TipoRelacionDes = SysData.ToStr(dr[clsTipoPersonaVM._TipoRelacionDes]),
                            EstadoId = SysData.ToLong(dr[clsTipoPersonaVM._EstadoId]),
                            EstadoDes = SysData.ToStr(dr[clsTipoPersonaVM._EstadoDes])
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
                oTipoPersona.Dispose();
            }

            return oTipoPersonaVM;
        }

        private List<clsPlanGrupoVM> PlanGrupoGrid()
        {
            clsPlanGrupo oPlanGrupo = new clsPlanGrupo(clsAppInfo.Connection);
            List<clsPlanGrupoVM> oPlanGrupoVM = new List<clsPlanGrupoVM>();

            try
            {
                oPlanGrupo.SelectFilter = clsPlanGrupo.SelectFilters.Grid;
                oPlanGrupo.WhereFilter = clsPlanGrupo.WhereFilters.Grid;
                oPlanGrupo.OrderByFilter = clsPlanGrupo.OrderByFilters.Grid;

                if (oPlanGrupo.Open())
                {
                    foreach (DataRow dr in oPlanGrupo.DataSet.Tables[oPlanGrupo.TableName].Rows)
                    {
                        oPlanGrupoVM.Add(new clsPlanGrupoVM()
                        {
                            PlanGrupoId = SysData.ToLong(dr[clsPlanGrupoVM._PlanGrupoId]),
                            PlanGrupoCod = SysData.ToStr(dr[clsPlanGrupoVM._PlanGrupoCod]),
                            PlanGrupoDes = SysData.ToStr(dr[clsPlanGrupoVM._PlanGrupoDes]),
                            PlanGrupoEsp = SysData.ToStr(dr[clsPlanGrupoVM._PlanGrupoEsp]),
                            PlanGrupoTipoId = SysData.ToLong(dr[clsPlanGrupoVM._PlanGrupoTipoId]),
                            PlanGrupoTipoDes = SysData.ToStr(dr[clsPlanGrupoVM._PlanGrupoTipoDes]),
                            PlanGrupoTipoDetId = SysData.ToLong(dr[clsPlanGrupoVM._PlanGrupoTipoDetId]),
                            PlanGrupoTipoDetDes = SysData.ToStr(dr[clsPlanGrupoVM._PlanGrupoTipoDetDes]),
                            NroCuentas = SysData.ToLong(dr[clsPlanGrupoVM._NroCuentas]),
                            MonedaId = SysData.ToLong(dr[clsPlanGrupoVM._MonedaId]),
                            MonedaDes = SysData.ToStr(dr[clsPlanGrupoVM._MonedaDes]),
                            VerificaMto = SysData.ToBoolean(dr[clsPlanGrupoVM._VerificaMto]),
                            EstadoId = SysData.ToLong(dr[clsPlanGrupoVM._EstadoId]),
                            EstadoDes = SysData.ToStr(dr[clsPlanGrupoVM._EstadoDes])
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
                oPlanGrupo.Dispose();
            }

            return oPlanGrupoVM;
        }

        private string PlanGrupoIdLoad(long lngTipoPersonaId)
        {
            clsRegTipoPersona oRegTipoPersona = new clsRegTipoPersona(clsAppInfo.Connection);
            List<clsRegTipoPersonaVM> oRegTipoPersonaVM = new List<clsRegTipoPersonaVM>();
            long lngRowCount = 0;
            string strPlanGrupoId = "";

            try
            {
                oRegTipoPersona.SelectFilter = clsRegTipoPersona.SelectFilters.All;
                oRegTipoPersona.WhereFilter = clsRegTipoPersona.WhereFilters.TipoPersonaId;
                oRegTipoPersona.OrderByFilter = clsRegTipoPersona.OrderByFilters.RegTipoPersonaId;
                oRegTipoPersona.VM.TipoPersonaId = lngTipoPersonaId;

                if (oRegTipoPersona.Open())
                {
                    foreach (DataRow dr in oRegTipoPersona.DataSet.Tables[oRegTipoPersona.TableName].Rows)
                    {
                        if (lngRowCount == 0)
                        {
                            strPlanGrupoId = "[" + SysData.ToStr(dr[clsRegTipoPersonaVM._PlanGrupoId]);
                        }
                        else
                        {
                            strPlanGrupoId += "," + SysData.ToStr(dr[clsRegTipoPersonaVM._PlanGrupoId]);
                        }

                        lngRowCount++;
                    }

                    if (lngRowCount > 0)
                        strPlanGrupoId += "]";
                }
            }

            catch (Exception exp)
            {
                throw (exp);

            }
            finally
            {
                oRegTipoPersona.Dispose();
            }

            return strPlanGrupoId;
        }

        [HttpGet]
        public ActionResult RegTipoPersonaGrid(DataSourceLoadOptions loadOptions)
        {
            return Content(JsonConvert.SerializeObject(TipoPersonaGrid()), "application/json");
        }
    }
}