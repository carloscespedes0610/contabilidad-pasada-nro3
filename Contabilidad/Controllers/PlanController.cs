using Contabilidad.Models.DAC;
using Contabilidad.Models.Modules;
using Contabilidad.Models.VM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Contabilidad.Controllers
{
    [SessionExpireFilter]
    public class PlanController : Controller
    {
        List<clsPlanVM> moPlanVM;

        // GET: Plan
        public ActionResult Index(string MessageErr)
        {
            try
            {
                this.GetDefaultData();

                var lstPlan = PlanGrid();
                ViewBag.MessageErr = MessageErr;
                ViewBag.PlanIdDef = -1;
                return View(lstPlan);
            }

            catch (Exception exp)
            {
                return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = exp.Message });
            }
        }

        // GET: Plan/Create
        public ActionResult Create(int? id)
        {
            string strMsg = string.Empty;
            clsPlanVM oPlanVM = new clsPlanVM();
            long lngPlanPadreId = SysData.ToLong(id);

            try
            {
                this.GetDefaultData();

                if (lngPlanPadreId > 0) 
                {
                    clsPlan oPlanPadre = new clsPlan(clsAppInfo.Connection);
                    oPlanPadre.VM.PlanId = lngPlanPadreId;

                    if (oPlanPadre.FindByPK())
                    {
                        if (oPlanPadre.VM.Nivel >= 1)
                        {
                            PlanHijoNew(oPlanPadre, oPlanVM);

                            ViewBagLoad();
                            return View(oPlanVM);
                        }
                        else
                        {
                            strMsg += "Cuenta Padre Inválida" + Environment.NewLine;
                        }
                    }
                    else
                    {
                        strMsg += "Cuenta Padre no encontrada" + Environment.NewLine;
                    }
                }
                else 
                {
                    strMsg += "Cuenta Padre Inválida" + Environment.NewLine;
                }
            }

            catch (Exception exp)
            {
                return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = exp.Message });
            }

            if (strMsg.Trim() != string.Empty)
            {
                ViewBag.MessageErr = strMsg;
                return RedirectToAction("Index", new { MessageErr = strMsg });
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Plan/Create
        [HttpPost()]
        [ValidateAntiForgeryToken()]
        public ActionResult Create(clsPlanVM oPlanVM)
        {
            string strMsg = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    clsPlan oPlan = new clsPlan(clsAppInfo.Connection);
                    DataMove(oPlanVM, oPlan, false);

                    if (oPlan.VM.TipoPlanId > 1)
                    {
                        if ((oPlanVM.MonedaId > 0) && (oPlanVM.TipoAmbitoId > 0))
                        {
                            if (oPlan.Insert())
                            {
                                return RedirectToAction("Index");
                            }
                        }
                        else
                        {
                            strMsg += "Moneda es Requerido" + Environment.NewLine;
                            strMsg += "Ambito es Requerido" + Environment.NewLine;
                        }
                    }
                    else
                    {
                        oPlan.VM.MonedaId = 0;
                        oPlan.VM.TipoAmbitoId = 0;

                        if (oPlan.Insert())
                        {
                            return RedirectToAction("Index");
                        }
                    }
                }
            }

            catch (Exception exp)
            {
                ViewBagLoad();
                ViewBag.MessageErr = exp.Message;
                return View(oPlanVM);
            }

            if (strMsg.Trim() != string.Empty)
            {
                ViewBagLoad();
                ViewBag.MessageErr = strMsg;
                return View(oPlanVM);
            }
            else
            {
                ViewBagLoad();
                return View(oPlanVM);
            }
        }


        // GET: Plan/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                this.GetDefaultData();

                if (ReferenceEquals(id, null))
                {
                    return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = "Índice nulo o no encontrado" });
                }

                clsPlanVM oPlanVM = PlanFind(SysData.ToLong(id));

                if (ReferenceEquals(oPlanVM, null))
                {
                    return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = "Índice no encontrado" });
                }

                ViewBagLoad();
                return View(oPlanVM);
            }

            catch (Exception exp)
            {
                return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = exp.Message });
            }
        }

        // POST: Plan/Edit/5
        [HttpPost()]
        [ValidateAntiForgeryToken()]
        public ActionResult Edit(clsPlanVM oPlanVM)
        {
            string strMsg = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    clsPlan oPlan = new clsPlan(clsAppInfo.Connection);
                    DataMove(oPlanVM, oPlan, true);

                    if (oPlan.VM.TipoPlanId > 1)
                    {
                        if ((oPlanVM.MonedaId > 0) && (oPlanVM.TipoAmbitoId > 0))
                        {
                            if (oPlan.Update())
                            {
                                return RedirectToAction("Index");
                            }
                        }
                        else
                        {
                            strMsg += "Moneda es Requerido" + Environment.NewLine;
                            strMsg += "Ambito es Requerido" + Environment.NewLine;
                        }
                    }
                    else
                    {
                        oPlan.VM.MonedaId = 0;
                        oPlan.VM.TipoAmbitoId = 0;

                        if (oPlan.Update())
                        {
                            return RedirectToAction("Index");
                        }
                    }
                }
            }

            catch (Exception exp)
            {
                ViewBagLoad();
                ViewBag.MessageErr = exp.Message;
                return View(oPlanVM);
            }

            if (strMsg.Trim() != string.Empty)
            {
                ViewBagLoad();
                ViewBag.MessageErr = strMsg;
                return View(oPlanVM);
            }
            else
            {
                ViewBagLoad();
                return View(oPlanVM);
            }
        }

        // GET: Plan/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                this.GetDefaultData();

                if (ReferenceEquals(id, null))
                {
                    return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = "Índice nulo o no encontrado" });
                }

                clsPlanVM oPlanVM = PlanFind(SysData.ToLong(id));

                if (ReferenceEquals(oPlanVM, null))
                {
                    return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = "Índice no encontrado" });
                }

                ViewBagLoad();
                return View(oPlanVM);
            }

            catch (Exception exp)
            {
                return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = exp.Message });
            }
        }

        // POST: Plan/Delete/5
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

                clsPlan oPlan = new clsPlan(clsAppInfo.Connection);

                oPlan.WhereFilter = clsPlan.WhereFilters.PrimaryKey;
                oPlan.VM.PlanId = id;

                if (oPlan.Delete())
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

        // GET: Plan/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                this.GetDefaultData();

                if (ReferenceEquals(id, null))
                {
                    return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = "Índice nulo o no encontrado" });
                }

                clsPlanVM oPlanVM = PlanFind(SysData.ToLong(id));

                if (ReferenceEquals(oPlanVM, null))
                {
                    return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = "Índice no encontrado" });
                }

                ViewBagLoad();
                return View(oPlanVM);
            }

            catch (Exception exp)
            {
                return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = exp.Message });
            }
        }








        private void ViewBagLoad()
        {
            ViewBag.TipoPlanId = ComboBox.TipoPlanList();
            ViewBag.MonedaId = ComboBox.MonedaList();
            ViewBag.TipoAmbitoId = ComboBox.TipoAmbitoList();
            ViewBag.EstadoId = ComboBox.EstadoList();
        }

        private void DataMove(clsPlanVM oPlanVM, clsPlan oPlan, bool boolEditing)
        {
            if (boolEditing)
            {
                oPlan.VM.PlanId = SysData.ToLong(oPlanVM.PlanId);
            }

            oPlan.VM.PlanCod = SysData.ToStr(oPlanVM.PlanCod);
            oPlan.VM.PlanDes = SysData.ToStr(oPlanVM.PlanDes);
            oPlan.VM.PlanEsp = SysData.ToStr(oPlanVM.PlanEsp);
            oPlan.VM.TipoPlanId = SysData.ToLong(oPlanVM.TipoPlanId);
            oPlan.VM.Orden = SysData.ToLong(oPlanVM.Orden);
            oPlan.VM.Nivel = SysData.ToLong(oPlanVM.Nivel);
            oPlan.VM.MonedaId = SysData.ToLong(oPlanVM.MonedaId);
            oPlan.VM.TipoAmbitoId = SysData.ToLong(oPlanVM.TipoAmbitoId);
            oPlan.VM.PlanAjusteId = SysData.ToLong(oPlanVM.PlanAjusteId);
            oPlan.VM.CapituloId = SysData.ToLong(oPlanVM.CapituloId);
            oPlan.VM.PlanPadreId = SysData.ToLong(oPlanVM.PlanPadreId);
            oPlan.VM.EstadoId = SysData.ToLong(oPlanVM.EstadoId);
        }

        private List<clsPlanVM> PlanGrid()
        {
            moPlanVM = new List<clsPlanVM>();

            try
            {
                moPlanVM.Add(new clsPlanVM()
                {
                    PlanId = -1,
                    PlanCod = "0",
                    PlanDes = "PLAN DE CUENTAS",
                    TipoPlanId = 0,
                    TipoPlanDes = "",
                    Orden = 0,
                    Nivel = 0,
                    MonedaId = 0,
                    MonedaDes = "",
                    CapituloId = 0,
                    PlanPadreId = 0,
                    EstadoId = 0,
                    EstadoDes = ""
                });

                PlanHijoLoad(-1);
            }

            catch (Exception exp)
            {
                throw (exp);

            }

            return moPlanVM;
        }

        private void PlanHijoLoad(long lngPlanPadreId)
        {
            clsPlan oPlan = new clsPlan(clsAppInfo.Connection);

            try
            {
                oPlan.SelectFilter = clsPlan.SelectFilters.Grid;
                oPlan.WhereFilter = clsPlan.WhereFilters.PlanPadreId;
                oPlan.OrderByFilter = clsPlan.OrderByFilters.Grid;
                oPlan.VM.PlanPadreId = lngPlanPadreId;

                if (oPlan.Open())
                {
                    foreach (DataRow dr in oPlan.DataSet.Tables[oPlan.TableName].Rows)
                    {
                        moPlanVM.Add(new clsPlanVM()
                        {
                            PlanId = SysData.ToLong(dr["PlanId"]),
                            PlanCod = SysData.ToStr(dr["PlanCod"]),
                            PlanDes = SysData.ToStr(dr["PlanDes"]),
                            TipoPlanId = SysData.ToLong(dr["TipoPlanId"]),
                            TipoPlanDes = SysData.ToStr(dr["TipoPlanDes"]),
                            Orden = SysData.ToLong(dr["Orden"]),
                            Nivel = SysData.ToLong(dr["Nivel"]),
                            MonedaId = SysData.ToLong(dr["MonedaId"]),
                            MonedaDes = SysData.ToStr(dr["MonedaDes"]),
                            CapituloId = SysData.ToLong(dr["CapituloId"]),
                            PlanPadreId = SysData.ToLong(dr["PlanPadreId"]),
                            EstadoId = SysData.ToLong(dr["EstadoId"]),
                            EstadoDes = SysData.ToStr(dr["EstadoDes"])
                        });

                        if (TieneHijos(SysData.ToLong(dr["PlanId"])))
                        {
                            PlanHijoLoad(SysData.ToLong(dr["PlanId"]));
                        }
                    }
                }
            }

            catch (Exception exp)
            {
                throw (exp);
            }
            finally
            {
                oPlan.Dispose();
            }
        }

        private bool TieneHijos(long lngPlanPadreId)
        {
            clsPlan oPlan = new clsPlan(clsAppInfo.Connection);
            bool returnValue = false;

            try
            {
                oPlan.SelectFilter = clsPlan.SelectFilters.All;
                oPlan.WhereFilter = clsPlan.WhereFilters.PlanPadreId;
                oPlan.VM.PlanPadreId = lngPlanPadreId;

                if (oPlan.FindOnly())
                {
                    returnValue = true;
                }
            }

            catch (Exception exp)
            {
                throw (exp);
            }
            finally
            {
                oPlan.Dispose();
            }

            return returnValue;
        }

        private clsPlanVM PlanFind(long lngPlanId)
        {
            clsPlan oPlan = new clsPlan(clsAppInfo.Connection);
            clsPlanVM oPlanVM = new clsPlanVM();

            try
            {
                oPlan.VM.PlanId = lngPlanId;

                if (oPlan.FindByPK())
                {
                    oPlanVM.PlanId = oPlan.VM.PlanId;
                    oPlanVM.PlanCod = oPlan.VM.PlanCod;
                    oPlanVM.PlanDes = oPlan.VM.PlanDes;
                    oPlanVM.PlanEsp = oPlan.VM.PlanEsp;
                    oPlanVM.TipoPlanId = oPlan.VM.TipoPlanId;
                    oPlanVM.Orden = oPlan.VM.Orden;
                    oPlanVM.Nivel = oPlan.VM.Nivel;
                    oPlanVM.MonedaId = oPlan.VM.MonedaId;
                    oPlanVM.TipoAmbitoId = oPlan.VM.TipoAmbitoId;
                    oPlanVM.PlanAjusteId = oPlan.VM.PlanAjusteId;
                    oPlanVM.CapituloId = oPlan.VM.CapituloId;
                    oPlanVM.PlanPadreId = oPlan.VM.PlanPadreId;
                    oPlanVM.EstadoId = oPlan.VM.EstadoId;

                    return oPlanVM;
                }
            }

            catch (Exception exp)
            {
                throw (exp);
            }
            finally
            {
                oPlan.Dispose();
            }

            return null;
        }

        private void PlanHijoNew(clsPlan oPlanPadre, clsPlanVM oPlanVM)
        {
            clsPlan oPlan = new clsPlan(clsAppInfo.Connection);

            try
            {
                oPlan.SelectFilter = clsPlan.SelectFilters.All;
                oPlan.WhereFilter = clsPlan.WhereFilters.PlanHijoMAXorden;
                oPlan.VM.PlanPadreId = oPlanPadre.VM.PlanId;
                oPlan.VM.EstadoId = ConstEstado.Activo;

                if (oPlan.Find())
                {
                    oPlanVM.PlanCod = SysData.ToStr(SysData.ToLong(oPlan.VM.PlanCod) + 1);
                    oPlanVM.TipoPlanId = oPlan.VM.TipoPlanId;
                    oPlanVM.Nivel = oPlan.VM.Nivel;
                    oPlanVM.Orden = oPlan.VM.Orden + 1;
                    oPlanVM.CapituloId = oPlan.VM.CapituloId;
                    oPlanVM.PlanPadreId = oPlan.VM.PlanPadreId;
                    oPlanVM.EstadoId = ConstEstado.Activo;
                }
                else
                {
                    oPlanVM.PlanCod = oPlanPadre.VM.PlanCod;
                    oPlanVM.TipoPlanId = 0;
                    oPlanVM.Nivel = oPlanPadre.VM.Nivel + 1;
                    oPlanVM.Orden = 1;
                    oPlanVM.CapituloId = oPlanPadre.VM.CapituloId;
                    oPlanVM.PlanPadreId = oPlanPadre.VM.PlanId;
                    oPlanVM.EstadoId = ConstEstado.Activo;
                }
            }

            catch (Exception exp)
            {
                throw (exp);
            }
            finally
            {
                oPlan.Dispose();
            }
        }

    }
}