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
    public class PlanGrupoController : Controller
    {
        string SessionKey = "clsPlanGrupoDetVM";

        // GET: PlanGrupo
        public ActionResult Index()
        {
            try
            {
                this.GetDefaultData();

                var lstPlanGrupo = PlanGrupoGrid();
                return View(lstPlanGrupo);
            }

            catch (Exception exp)
            {
                return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = exp.Message });
            }
        }

        public ActionResult CreateMain(int? id)
        {
            long lngPlanGrupoTipoDetId = SysData.ToLong(id);

            switch (lngPlanGrupoTipoDetId)
            {
                case 1:
                    return RedirectToAction("Create");

                case 4:
                    return RedirectToAction("CreateCtaCteDeudor");

                case 5:
                    return RedirectToAction("CreateCtaCobrar");

                case 6:
                    break;

                case 7:
                    break;
            }

            return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = "Índice nulo o no encontrado" });
        }

        public ActionResult EditMain(int? id, int? TipoDetId)
        {
            if (ReferenceEquals(id, null) && ReferenceEquals(TipoDetId, null))
            {
                return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = "Índice nulo o no encontrado" });
            }

            long lngPlanGrupoId = SysData.ToLong(id);
            long lngPlanGrupoTipoDetId = SysData.ToLong(TipoDetId);

            switch (lngPlanGrupoTipoDetId)
            {
                case 1:
                    return RedirectToAction("Edit", new { id = lngPlanGrupoId });

                case 4:
                    return RedirectToAction("EditCtaCteDeudor", new { id = lngPlanGrupoId });

                case 5:
                    return RedirectToAction("EditCtaCobrar", new { id = lngPlanGrupoId });

                case 6:
                    break;

                case 7:
                    break;
            }


            return RedirectToAction("Edit", "PlanGrupo", new { id = lngPlanGrupoId });
        }

        // GET: PlanGrupo/Create
        public ActionResult Create()
        {
            clsPlanGrupoFormVM oPlanGrupoFormVM = new clsPlanGrupoFormVM();

            try
            {
                this.GetDefaultData();

                Session[SessionKey] = null;

                DataInit(oPlanGrupoFormVM);

                ViewBagLoad();
                return View(oPlanGrupoFormVM);
            }

            catch (Exception exp)
            {
                return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = exp.Message });
            }
        }

        // POST: PlanGrupo/Create
        [HttpPost()]
        [ValidateAntiForgeryToken()]
        public ActionResult Create(clsPlanGrupoFormVM oPlanGrupoFormVM)
        {
            clsPlanGrupo oPlanGrupo = new clsPlanGrupo(clsAppInfo.Connection);
            clsPlanGrupoDet oPlanGrupoDet = new clsPlanGrupoDet(clsAppInfo.Connection);
            long lngRowCount = 0;

            try
            {
                if (ModelState.IsValid)
                {
                    DataMove(oPlanGrupoFormVM, oPlanGrupo, false);
                    oPlanGrupo.BeginTransaction();

                    if (oPlanGrupo.Insert())
                    {
                        var oPlanGrupoDetVMList = (List<clsPlanGrupoDetVM>)Session[SessionKey];
                        //oPlanGrupo.PlanGrupoId = oPlanGrupo.Id;
                        oPlanGrupoDet.Transaction = oPlanGrupo.Transaction;

                        foreach (clsPlanGrupoDetVM oPlanGrupoDetVM in oPlanGrupoDetVMList)
                        {
                            DataMoveDet(oPlanGrupo, oPlanGrupoDetVM, oPlanGrupoDet, false);

                            if (oPlanGrupoDet.Insert())
                            {
                                lngRowCount += 1;
                            }
                        }

                        if (oPlanGrupoDetVMList.Count == lngRowCount)
                        {
                            oPlanGrupo.Commit();
                            Session[SessionKey] = null;
                            return RedirectToAction("Index");
                        }
                    }

                    oPlanGrupo.Rollback();
                }

                ViewBagLoad();
                return View(oPlanGrupoFormVM);
            }

            catch (Exception exp)
            {
                oPlanGrupo.Rollback();

                ViewBagLoad();
                ViewBag.MessageErr = exp.Message;
                return View(oPlanGrupoFormVM);
            }
        }

        // GET: PlanGrupo/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                this.GetDefaultData();

                if (ReferenceEquals(id, null))
                {
                    return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = "Índice nulo o no encontrado" });
                }

                Session[SessionKey] = null;

                clsPlanGrupoFormVM oPlanGrupoFormVM = PlanGrupoFormFind(SysData.ToLong(id));

                if (ReferenceEquals(oPlanGrupoFormVM, null))
                {
                    return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = "Índice no encontrado" });
                }

                ViewBagLoad();
                return View(oPlanGrupoFormVM);
            }

            catch (Exception exp)
            {
                return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = exp.Message });
            }
        }

        // POST: PlanGrupo/Edit/5
        [HttpPost()]
        [ValidateAntiForgeryToken()]
        public ActionResult Edit(clsPlanGrupoFormVM oPlanGrupoFormVM)
        {
            clsPlanGrupo oPlanGrupo = new clsPlanGrupo(clsAppInfo.Connection);
            clsPlanGrupoDet oPlanGrupoDet = new clsPlanGrupoDet(clsAppInfo.Connection);
            long lngRowCount = 0;

            try
            {
                if (ModelState.IsValid)
                {
                    DataMove(oPlanGrupoFormVM, oPlanGrupo, true);
                    oPlanGrupo.BeginTransaction();

                    if (oPlanGrupo.Update())
                    {
                        var oPlanGrupoDetVMList = (List<clsPlanGrupoDetVM>)Session[SessionKey];

                        oPlanGrupoDet.Transaction = oPlanGrupo.Transaction;

                        foreach (clsPlanGrupoDetVM oPlanGrupoDetVM in oPlanGrupoDetVMList)
                        {
                            if (oPlanGrupoDetVM.PlanGrupoDetId == 0)
                            {
                                DataMoveDet(oPlanGrupo, oPlanGrupoDetVM, oPlanGrupoDet, false);

                                if (oPlanGrupoDet.Insert())
                                    lngRowCount += 1;
                            }
                            else
                            {
                                DataMoveDet(oPlanGrupo, oPlanGrupoDetVM, oPlanGrupoDet, true);

                                if (oPlanGrupoDet.Update())
                                    lngRowCount += 1;
                            }
                        }

                        if (oPlanGrupoDetVMList.Count == lngRowCount)
                        {
                            oPlanGrupo.Commit();
                            Session[SessionKey] = null;
                            return RedirectToAction("Index");
                        }
                    }

                    oPlanGrupo.Rollback();
                }

                ViewBagLoad();
                return View(oPlanGrupoFormVM);
            }

            catch (Exception exp)
            {
                oPlanGrupo.Rollback();

                ViewBagLoad();
                ViewBag.MessageErr = exp.Message;
                return View(oPlanGrupoFormVM);
            }
        }

        // GET: PlanGrupo/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                this.GetDefaultData();

                if (ReferenceEquals(id, null))
                {
                    return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = "Índice nulo o no encontrado" });
                }

                clsPlanGrupoFormVM oPlanGrupoFormVM = PlanGrupoFormFind(SysData.ToLong(id));

                if (ReferenceEquals(oPlanGrupoFormVM, null))
                {
                    return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = "Índice no encontrado" });
                }

                ViewBagLoad();
                return View(oPlanGrupoFormVM);
            }

            catch (Exception exp)
            {
                return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = exp.Message });
            }
        }

        // POST: CenCos/Delete/5
        [HttpPost()]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteConfirmed(int id)
        {
            clsPlanGrupo oPlanGrupo = new clsPlanGrupo(clsAppInfo.Connection);
            clsPlanGrupoDet oPlanGrupoDet = new clsPlanGrupoDet(clsAppInfo.Connection);

            try
            {
                if (ReferenceEquals(id, null))
                {
                    return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = "Índice nulo o no encontrado" });
                }

                oPlanGrupo.PlanGrupoId = SysData.ToLong(id);
                oPlanGrupo.Transaction = oPlanGrupo.Connection.BeginTransaction(IsolationLevel.ReadCommitted);

                if (oPlanGrupo.Delete())
                {
                    oPlanGrupoDet.DeleteFilter = clsPlanGrupoDet.DeleteFilters.PlanGrupoId;
                    oPlanGrupoDet.PlanGrupoId = SysData.ToLong(id);
                    oPlanGrupoDet.Transaction = oPlanGrupo.Transaction;

                    if (oPlanGrupoDet.Delete())
                    {
                        oPlanGrupo.Transaction.Commit();
                        return RedirectToAction("Index");
                    }
                }

                oPlanGrupo.Transaction.Rollback();

                return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = "Error al Eliminar el Registro" });
            }

            catch (Exception exp)
            {
                try { oPlanGrupo.Transaction.Rollback(); } catch { }
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

                clsPlanGrupoFormVM oPlanGrupoFormVM = PlanGrupoFormFind(SysData.ToLong(id));

                if (ReferenceEquals(oPlanGrupoFormVM, null))
                {
                    return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = "Índice no encontrado" });
                }

                ViewBagLoad();
                return View(oPlanGrupoFormVM);
            }

            catch (Exception exp)
            {
                return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = exp.Message });
            }
        }




        // GET: PlanGrupo/CreateCtaCteDeudor
        public ActionResult CreateCtaCteDeudor()
        {
            clsPlanGrupoFormVM oPlanGrupoFormVM = new clsPlanGrupoFormVM();

            try
            {
                this.GetDefaultData();

                Session[SessionKey] = null;

                DataInit(oPlanGrupoFormVM);

                ViewBagLoad();
                return View(oPlanGrupoFormVM);
            }

            catch (Exception exp)
            {
                return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = exp.Message });
            }
        }

        // POST: PlanGrupo/CreateCtaCteDeudor
        [HttpPost()]
        [ValidateAntiForgeryToken()]
        public ActionResult CreateCtaCteDeudor(clsPlanGrupoFormVM oPlanGrupoFormVM)
        {
            clsPlanGrupo oPlanGrupo = new clsPlanGrupo(clsAppInfo.Connection);
            clsPlanGrupoDet oPlanGrupoDet = new clsPlanGrupoDet(clsAppInfo.Connection);
            long lngRowCount = 0;

            try
            {
                if (ModelState.IsValid)
                {
                    DataMove(oPlanGrupoFormVM, oPlanGrupo, false);
                    oPlanGrupo.BeginTransaction();

                    if (oPlanGrupo.Insert())
                    {
                        var oPlanGrupoDetVMList = (List<clsPlanGrupoDetVM>)Session[SessionKey];
                        oPlanGrupo.PlanGrupoId = oPlanGrupo.Id;
                        oPlanGrupoDet.Transaction = oPlanGrupo.Transaction;

                        foreach (clsPlanGrupoDetVM oPlanGrupoDetVM in oPlanGrupoDetVMList)
                        {
                            DataMoveDet(oPlanGrupo, oPlanGrupoDetVM, oPlanGrupoDet, false);

                            if (oPlanGrupoDet.Insert())
                            {
                                lngRowCount += 1;
                            }
                        }

                        if (oPlanGrupoDetVMList.Count == lngRowCount)
                        {
                            oPlanGrupo.Commit();
                            Session[SessionKey] = null;
                            return RedirectToAction("Index");
                        }
                    }

                    oPlanGrupo.Rollback();
                }

                ViewBagLoad();
                return View(oPlanGrupoFormVM);
            }

            catch (Exception exp)
            {
                oPlanGrupo.Rollback();

                ViewBagLoad();
                ViewBag.MessageErr = exp.Message;
                return View(oPlanGrupoFormVM);
            }
        }

        // GET: PlanGrupo/EditCtaCteDeudor/5
        public ActionResult EditCtaCteDeudor(int? id)
        {
            try
            {
                this.GetDefaultData();

                if (ReferenceEquals(id, null))
                {
                    return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = "Índice nulo o no encontrado" });
                }

                Session[SessionKey] = null;

                clsPlanGrupoFormVM oPlanGrupoFormVM = PlanGrupoFormFind(SysData.ToLong(id));

                if (ReferenceEquals(oPlanGrupoFormVM, null))
                {
                    return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = "Índice no encontrado" });
                }

                ViewBagLoad();
                return View(oPlanGrupoFormVM);
            }

            catch (Exception exp)
            {
                return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = exp.Message });
            }
        }

        // POST: PlanGrupo/EditCtaCteDeudor/5
        [HttpPost()]
        [ValidateAntiForgeryToken()]
        public ActionResult EditCtaCteDeudor(clsPlanGrupoFormVM oPlanGrupoFormVM)
        {
            clsPlanGrupo oPlanGrupo = new clsPlanGrupo(clsAppInfo.Connection);
            clsPlanGrupoDet oPlanGrupoDet = new clsPlanGrupoDet(clsAppInfo.Connection);
            long lngRowCount = 0;

            try
            {
                if (ModelState.IsValid)
                {
                    DataMove(oPlanGrupoFormVM, oPlanGrupo, true);
                    oPlanGrupo.BeginTransaction();

                    if (oPlanGrupo.Update())
                    {
                        var oPlanGrupoDetVMList = (List<clsPlanGrupoDetVM>)Session[SessionKey];

                        oPlanGrupoDet.Transaction = oPlanGrupo.Transaction;

                        foreach (clsPlanGrupoDetVM oPlanGrupoDetVM in oPlanGrupoDetVMList)
                        {
                            if (oPlanGrupoDetVM.PlanGrupoDetId == 0)
                            {
                                DataMoveDet(oPlanGrupo, oPlanGrupoDetVM, oPlanGrupoDet, false);

                                if (oPlanGrupoDet.Insert())
                                    lngRowCount += 1;
                            }
                            else
                            { 
                                DataMoveDet(oPlanGrupo, oPlanGrupoDetVM, oPlanGrupoDet, true);

                                if (oPlanGrupoDet.Update())
                                    lngRowCount += 1;
                            }
                        }

                        if (oPlanGrupoDetVMList.Count == lngRowCount)
                        {
                            oPlanGrupo.Commit();
                            Session[SessionKey] = null;
                            return RedirectToAction("Index");
                        }
                    }

                    oPlanGrupo.Rollback();
                }

                ViewBagLoad();
                return View(oPlanGrupoFormVM);
            }

            catch (Exception exp)
            {
                oPlanGrupo.Rollback();

                ViewBagLoad();
                ViewBag.MessageErr = exp.Message;
                return View(oPlanGrupoFormVM);
            }
        }




        // GET: PlanGrupo/CreateCtaCobrar
        public ActionResult CreateCtaCobrar()
        {
            clsPlanGrupoFormVM oPlanGrupoFormVM = new clsPlanGrupoFormVM();

            try
            {
                this.GetDefaultData();

                Session[SessionKey] = null;

                DataInit(oPlanGrupoFormVM);

                ViewBagLoad();
                return View(oPlanGrupoFormVM);
            }

            catch (Exception exp)
            {
                return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = exp.Message });
            }
        }

        // POST: PlanGrupo/CreateCtaCobrar
        [HttpPost()]
        [ValidateAntiForgeryToken()]
        public ActionResult CreateCtaCobrar(clsPlanGrupoFormVM oPlanGrupoFormVM)
        {
            clsPlanGrupo oPlanGrupo = new clsPlanGrupo(clsAppInfo.Connection);
            clsPlanGrupoDet oPlanGrupoDet = new clsPlanGrupoDet(clsAppInfo.Connection);
            long lngRowCount = 0;

            try
            {
                if (ModelState.IsValid)
                {
                    DataMove(oPlanGrupoFormVM, oPlanGrupo, false);
                    oPlanGrupo.BeginTransaction();

                    if (oPlanGrupo.Insert())
                    {
                        var oPlanGrupoDetVMList = (List<clsPlanGrupoDetVM>)Session[SessionKey];
                        oPlanGrupo.PlanGrupoId = oPlanGrupo.Id;
                        oPlanGrupoDet.Transaction = oPlanGrupo.Transaction;

                        foreach (clsPlanGrupoDetVM oPlanGrupoDetVM in oPlanGrupoDetVMList)
                        {
                            DataMoveDet(oPlanGrupo, oPlanGrupoDetVM, oPlanGrupoDet, false);

                            if (oPlanGrupoDet.Insert())
                            {
                                lngRowCount += 1;
                            }
                        }

                        if (oPlanGrupoDetVMList.Count == lngRowCount)
                        {
                            oPlanGrupo.Commit();
                            Session[SessionKey] = null;
                            return RedirectToAction("Index");
                        }
                    }

                    oPlanGrupo.Rollback();
                }

                ViewBagLoad();
                return View(oPlanGrupoFormVM);
            }

            catch (Exception exp)
            {
                oPlanGrupo.Rollback();

                ViewBagLoad();
                ViewBag.MessageErr = exp.Message;
                return View(oPlanGrupoFormVM);
            }
        }




        private void ViewBagLoad()
        {
            ViewBag.PlanGrupoTipoId = ComboBox.PlanGrupoTipoList();
            ViewBag.PlanGrupoTipoDetId = ComboBox.PlanGrupoTipoDetList();
            ViewBag.MonedaId = ComboBox.MonedaList();
            ViewBag.EstadoId = ComboBox.EstadoList();

            ViewBag.PlanId = ComboBox.PlanList();
            ViewBag.CenCosId = ComboBox.CenCosList();
            ViewBag.SucursalId = ComboBox.SucursalList();
        }

        private void DataInit(clsPlanGrupoFormVM oPlanGrupoFormVM)
        {
            oPlanGrupoFormVM.PlanGrupoId = 0;
            oPlanGrupoFormVM.MonedaId = 1;
            oPlanGrupoFormVM.NroCuentas = 1;
            oPlanGrupoFormVM.EstadoId = ConstEstado.Activo;
        }

        private void DataMove(clsPlanGrupoFormVM oPlanGrupoFormVM, clsPlanGrupo oPlanGrupo, bool boolEditing)
        {
            if (boolEditing)
            {
                oPlanGrupo.PlanGrupoId = SysData.ToLong(oPlanGrupoFormVM.PlanGrupoId);
            }

            oPlanGrupo.PlanGrupoCod = SysData.ToStr(oPlanGrupoFormVM.PlanGrupoCod);
            oPlanGrupo.PlanGrupoDes = SysData.ToStr(oPlanGrupoFormVM.PlanGrupoDes);
            oPlanGrupo.PlanGrupoEsp = SysData.ToStr(oPlanGrupoFormVM.PlanGrupoEsp);
            oPlanGrupo.PlanGrupoTipoId = SysData.ToLong(oPlanGrupoFormVM.PlanGrupoTipoId);
            oPlanGrupo.PlanGrupoTipoDetId = SysData.ToLong(oPlanGrupoFormVM.PlanGrupoTipoDetId);
            oPlanGrupo.NroCuentas = SysData.ToLong(oPlanGrupoFormVM.NroCuentas);
            oPlanGrupo.MonedaId = SysData.ToLong(oPlanGrupoFormVM.MonedaId);
            oPlanGrupo.VerificaMto = SysData.ToBoolean(oPlanGrupoFormVM.VerificaMto);
            oPlanGrupo.EstadoId = SysData.ToLong(oPlanGrupoFormVM.EstadoId);
        }

        private void DataMoveDet(clsPlanGrupo oPlanGrupo, clsPlanGrupoDetVM oPlanGrupoDetVM, clsPlanGrupoDet oPlanGrupoDet, bool boolEditing)
        {
            if (boolEditing)
            {
                oPlanGrupoDet.PlanGrupoDetId = SysData.ToLong(oPlanGrupoDetVM.PlanGrupoDetId);
            }

            oPlanGrupoDet.PlanGrupoId = SysData.ToLong(oPlanGrupo.PlanGrupoId);
            oPlanGrupoDet.PlanGrupoDetDes = SysData.ToStr(oPlanGrupoDetVM.PlanGrupoDetDes);
            oPlanGrupoDet.PlanId = SysData.ToLong(oPlanGrupoDetVM.PlanId);
            oPlanGrupoDet.PlanFlujoId = SysData.ToLong(oPlanGrupoDetVM.PlanFlujoId);
            oPlanGrupoDet.SucursalId = SysData.ToLong(oPlanGrupoDetVM.SucursalId);
            oPlanGrupoDet.CenCosId = SysData.ToLong(oPlanGrupoDetVM.CenCosId);
            oPlanGrupoDet.Orden = SysData.ToLong(oPlanGrupoDetVM.Orden);
            oPlanGrupoDet.EstadoId = SysData.ToLong(oPlanGrupo.EstadoId);
        }

        public List<clsPlanGrupoVM> PlanGrupoGrid()
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

        private clsPlanGrupoFormVM PlanGrupoFormFind(long lngPlanGrupoId)
        {
            clsPlanGrupo oPlanGrupo = new clsPlanGrupo(clsAppInfo.Connection);
            clsPlanGrupoDet oPlanGrupoDet = new clsPlanGrupoDet(clsAppInfo.Connection);
            List<clsPlanGrupoDetVM> oPlanGrupoDetVM = new List<clsPlanGrupoDetVM>();
            clsPlanGrupoFormVM oPlanGrupoFormVM = new clsPlanGrupoFormVM();

            try
            {
                oPlanGrupo.PlanGrupoId = lngPlanGrupoId;

                if (oPlanGrupo.FindByPK())
                {
                    oPlanGrupoFormVM.PlanGrupoId = oPlanGrupo.PlanGrupoId;
                    oPlanGrupoFormVM.PlanGrupoCod = oPlanGrupo.PlanGrupoCod;
                    oPlanGrupoFormVM.PlanGrupoDes = oPlanGrupo.PlanGrupoDes;
                    oPlanGrupoFormVM.PlanGrupoEsp = oPlanGrupo.PlanGrupoEsp;
                    oPlanGrupoFormVM.PlanGrupoTipoId = oPlanGrupo.PlanGrupoTipoId;
                    oPlanGrupoFormVM.PlanGrupoTipoDetId = oPlanGrupo.PlanGrupoTipoDetId;
                    oPlanGrupoFormVM.NroCuentas = oPlanGrupo.NroCuentas;
                    oPlanGrupoFormVM.MonedaId = oPlanGrupo.MonedaId;
                    oPlanGrupoFormVM.VerificaMto = oPlanGrupo.VerificaMto;
                    oPlanGrupoFormVM.EstadoId = oPlanGrupo.EstadoId;

                    oPlanGrupoDet.SelectFilter = clsPlanGrupoDet.SelectFilters.All;
                    oPlanGrupoDet.WhereFilter = clsPlanGrupoDet.WhereFilters.PlanGrupoId;
                    oPlanGrupoDet.OrderByFilter = clsPlanGrupoDet.OrderByFilters.Orden;
                    oPlanGrupoDet.PlanGrupoId = lngPlanGrupoId;

                    if (oPlanGrupoDet.Open())
                    {
                        foreach (DataRow dr in oPlanGrupoDet.DataSet.Tables[oPlanGrupoDet.TableName].Rows)
                        {
                            oPlanGrupoDetVM.Add(new clsPlanGrupoDetVM()
                            {
                                PlanGrupoDetId = SysData.ToLong(dr["PlanGrupoDetId"]),
                                PlanGrupoId = SysData.ToLong(dr["PlanGrupoId"]),
                                PlanGrupoDetDes = SysData.ToStr(dr["PlanGrupoDetDes"]),
                                PlanId = SysData.ToLong(dr["PlanId"]),
                                PlanFlujoId = SysData.ToLong(dr["PlanFlujoId"]),
                                SucursalId = SysData.ToLong(dr["SucursalId"]),
                                CenCosId = SysData.ToLong(dr["CenCosId"]),
                                Orden = SysData.ToLong(dr["Orden"]),
                                EstadoId = SysData.ToLong(dr["EstadoId"])
                            });
                        }

                        oPlanGrupoFormVM.PlanGrupoDetVM = (ICollection<clsPlanGrupoDetVM>)oPlanGrupoDetVM;
                        return oPlanGrupoFormVM;
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

            return null;
        }

    }
}