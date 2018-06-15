using Contabilidad.Models.DAC;
using Contabilidad.Models.VM;
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
                    oRegTipoPersona.TipoPersonaId = lngTipoPersonaId;
                    oRegTipoPersona.Delete();

                    foreach (var lngId in ArrayId)
                    {
                        oRegTipoPersona.TipoPersonaId = lngTipoPersonaId;
                        oRegTipoPersona.PlanGrupoId = SysData.ToLong(lngId);
                        oRegTipoPersona.EstadoId = ConstEstado.Activo;

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
                            TipoPersonaId = SysData.ToLong(dr["TipoPersonaId"]),
                            TipoPersonaCod = SysData.ToStr(dr["TipoPersonaCod"]),
                            TipoPersonaDes = SysData.ToStr(dr["TipoPersonaDes"]),
                            TipoRelacionId = SysData.ToLong(dr["TipoRelacionId"]),
                            TipoRelacionDes = SysData.ToStr(dr["TipoRelacionDes"]),
                            EstadoId = SysData.ToLong(dr["EstadoId"]),
                            EstadoDes = SysData.ToStr(dr["EstadoDes"])
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
                            PlanGrupoId = SysData.ToLong(dr["PlanGrupoId"]),
                            PlanGrupoCod = SysData.ToStr(dr["PlanGrupoCod"]),
                            PlanGrupoDes = SysData.ToStr(dr["PlanGrupoDes"]),
                            PlanGrupoEsp = SysData.ToStr(dr["PlanGrupoEsp"]),
                            PlanGrupoTipoId = SysData.ToLong(dr["PlanGrupoTipoId"]),
                            PlanGrupoTipoDes = SysData.ToStr(dr["PlanGrupoTipoDes"]),
                            PlanGrupoTipoDetId = SysData.ToLong(dr["PlanGrupoTipoDetId"]),
                            PlanGrupoTipoDetDes = SysData.ToStr(dr["PlanGrupoTipoDetDes"]),
                            NroCuentas = SysData.ToLong(dr["NroCuentas"]),
                            MonedaId = SysData.ToLong(dr["MonedaId"]),
                            MonedaDes = SysData.ToStr(dr["MonedaDes"]),
                            VerificaMto = SysData.ToBoolean(dr["VerificaMto"]),
                            EstadoId = SysData.ToLong(dr["EstadoId"]),
                            EstadoDes = SysData.ToStr(dr["EstadoDes"])
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
                oRegTipoPersona.TipoPersonaId = lngTipoPersonaId;

                if (oRegTipoPersona.Open())
                {
                    foreach (DataRow dr in oRegTipoPersona.DataSet.Tables[oRegTipoPersona.TableName].Rows)
                    {
                        if (lngRowCount == 0)
                        {
                            strPlanGrupoId = "[" + SysData.ToStr(dr["PlanGrupoId"]);
                        }
                        else
                        {
                            strPlanGrupoId += "," + SysData.ToStr(dr["PlanGrupoId"]);
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

    }
}