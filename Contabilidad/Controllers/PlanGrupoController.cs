using Contabilidad.Models.DAC;
using Contabilidad.Models.Modules;
using Contabilidad.Models.VM;
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
    public class PlanGrupoController : Controller
    {
        string SessionKey = "clsPlanGrupoDetVM";

        // GET: PlanGrupo
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
                    //return RedirectToAction("EditCtaCobrar", new { id = lngPlanGrupoId }); todavia no esta implementado este controlador
                    return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = "todavia no esta implementado esta Opcion en Controller; Carlos" });
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

                return View(oPlanGrupoFormVM);
            }

            catch (Exception exp)
            {
                oPlanGrupo.Rollback();

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

               // ViewBagLoad();
                return View(oPlanGrupoFormVM);
            }

            catch (Exception exp)
            {
                oPlanGrupo.Rollback();

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

                oPlanGrupo.VM.PlanGrupoId = SysData.ToLong(id);
                oPlanGrupo.Transaction = oPlanGrupo.Connection.BeginTransaction(IsolationLevel.ReadCommitted);

                if (oPlanGrupo.Delete())
                {
                    oPlanGrupoDet.DeleteFilter = clsPlanGrupoDet.DeleteFilters.PlanGrupoId;
                    oPlanGrupoDet.VM.PlanGrupoId = SysData.ToLong(id);
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

               // ViewBagLoad();
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

                return View(oPlanGrupoFormVM);
            }

            catch (Exception exp)
            {
                oPlanGrupo.Rollback();

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

                return View(oPlanGrupoFormVM);
            }

            catch (Exception exp)
            {
                oPlanGrupo.Rollback();

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

                return View(oPlanGrupoFormVM);
            }

            catch (Exception exp)
            {
                oPlanGrupo.Rollback();

                ViewBag.MessageErr = exp.Message;
                return View(oPlanGrupoFormVM);
            }
        }



        private void DataInit(clsPlanGrupoFormVM oPlanGrupoFormVM)
        {
            oPlanGrupoFormVM.VM.PlanGrupoId = 0;
            oPlanGrupoFormVM.VM.MonedaId = 1;
            oPlanGrupoFormVM.VM.NroCuentas = 1;
            oPlanGrupoFormVM.VM.EstadoId = ConstEstado.Activo;
        }

        private void DataMove(clsPlanGrupoFormVM oPlanGrupoFormVM, clsPlanGrupo oPlanGrupo, bool boolEditing)
        {
            if (boolEditing)
            {
                oPlanGrupo.VM.PlanGrupoId = SysData.ToLong(oPlanGrupoFormVM.VM.PlanGrupoId);
            }

            oPlanGrupo.VM.PlanGrupoCod = SysData.ToStr(oPlanGrupoFormVM.VM.PlanGrupoCod);
            oPlanGrupo.VM.PlanGrupoDes = SysData.ToStr(oPlanGrupoFormVM.VM.PlanGrupoDes);
            oPlanGrupo.VM.PlanGrupoEsp = SysData.ToStr(oPlanGrupoFormVM.VM.PlanGrupoEsp);
            oPlanGrupo.VM.PlanGrupoTipoId = SysData.ToLong(oPlanGrupoFormVM.VM.PlanGrupoTipoId);
            oPlanGrupo.VM.PlanGrupoTipoDetId = SysData.ToLong(oPlanGrupoFormVM.VM.PlanGrupoTipoDetId);
            oPlanGrupo.VM.NroCuentas = SysData.ToLong(oPlanGrupoFormVM.VM.NroCuentas);
            oPlanGrupo.VM.MonedaId = SysData.ToLong(oPlanGrupoFormVM.VM.MonedaId);
            oPlanGrupo.VM.VerificaMto = SysData.ToBoolean(oPlanGrupoFormVM.VM.VerificaMto);
            oPlanGrupo.VM.EstadoId = SysData.ToLong(oPlanGrupoFormVM.VM.EstadoId);
        }

        private void DataMoveDet(clsPlanGrupo oPlanGrupo, clsPlanGrupoDetVM oPlanGrupoDetVM, clsPlanGrupoDet oPlanGrupoDet, bool boolEditing)
        {
            if (boolEditing)
            {
                oPlanGrupoDet.VM.PlanGrupoDetId = SysData.ToLong(oPlanGrupoDetVM.PlanGrupoDetId);
            }

            oPlanGrupoDet.VM.PlanGrupoId = SysData.ToLong(oPlanGrupo.VM.PlanGrupoId);
            oPlanGrupoDet.VM.PlanGrupoDetDes = SysData.ToStr(oPlanGrupoDetVM.PlanGrupoDetDes);
            oPlanGrupoDet.VM.PlanId = SysData.ToLong(oPlanGrupoDetVM.PlanId);
            //oPlanGrupoDet.VM.PlanFlujoId = SysData.ToLong(oPlanGrupoDetVM.PlanFlujoId);
            oPlanGrupoDet.VM.SucursalId = SysData.ToLong(oPlanGrupoDetVM.SucursalId);
            oPlanGrupoDet.VM.CenCosId = SysData.ToLong(oPlanGrupoDetVM.CenCosId);
            oPlanGrupoDet.VM.Orden = SysData.ToLong(oPlanGrupoDetVM.Orden);
            oPlanGrupoDet.VM.EstadoId = SysData.ToLong(oPlanGrupo.VM.EstadoId);
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

        private clsPlanGrupoFormVM PlanGrupoFormFind(long lngPlanGrupoId)
        {
            clsPlanGrupo oPlanGrupo = new clsPlanGrupo(clsAppInfo.Connection);
            clsPlanGrupoDet oPlanGrupoDet = new clsPlanGrupoDet(clsAppInfo.Connection);
            List<clsPlanGrupoDetVM> oPlanGrupoDetVM = new List<clsPlanGrupoDetVM>();
            clsPlanGrupoFormVM oPlanGrupoFormVM = new clsPlanGrupoFormVM();

            try
            {
                oPlanGrupo.VM.PlanGrupoId = lngPlanGrupoId;

                if (oPlanGrupo.FindByPK())
                {
                    oPlanGrupoFormVM.VM.PlanGrupoId = oPlanGrupo.VM.PlanGrupoId;
                    oPlanGrupoFormVM.VM.PlanGrupoCod = oPlanGrupo.VM.PlanGrupoCod;
                    oPlanGrupoFormVM.VM.PlanGrupoDes = oPlanGrupo.VM.PlanGrupoDes;
                    oPlanGrupoFormVM.VM.PlanGrupoEsp = oPlanGrupo.VM.PlanGrupoEsp;
                    oPlanGrupoFormVM.VM.PlanGrupoTipoId = oPlanGrupo.VM.PlanGrupoTipoId;
                    oPlanGrupoFormVM.VM.PlanGrupoTipoDetId = oPlanGrupo.VM.PlanGrupoTipoDetId;
                    oPlanGrupoFormVM.VM.NroCuentas = oPlanGrupo.VM.NroCuentas;
                    oPlanGrupoFormVM.VM.MonedaId = oPlanGrupo.VM.MonedaId;
                    oPlanGrupoFormVM.VM.VerificaMto = oPlanGrupo.VM.VerificaMto;
                    oPlanGrupoFormVM.VM.EstadoId = oPlanGrupo.VM.EstadoId;

                    oPlanGrupoDet.SelectFilter = clsPlanGrupoDet.SelectFilters.All;
                    oPlanGrupoDet.WhereFilter = clsPlanGrupoDet.WhereFilters.PlanGrupoId;
                    oPlanGrupoDet.OrderByFilter = clsPlanGrupoDet.OrderByFilters.Orden;
                    oPlanGrupoDet.VM.PlanGrupoId = lngPlanGrupoId;

                    if (oPlanGrupoDet.Open())
                    {
                        foreach (DataRow dr in oPlanGrupoDet.DataSet.Tables[oPlanGrupoDet.TableName].Rows)
                        {
                            oPlanGrupoDetVM.Add(new clsPlanGrupoDetVM()
                            {
                                PlanGrupoDetId = SysData.ToLong(dr[clsPlanGrupoDetVM._PlanGrupoDetId]),
                                PlanGrupoId = SysData.ToLong(dr[clsPlanGrupoDetVM._PlanGrupoId]),
                                PlanGrupoDetDes = SysData.ToStr(dr[clsPlanGrupoDetVM._PlanGrupoDetDes]),
                                PlanId = SysData.ToLong(dr[clsPlanGrupoDetVM._PlanId]),
                                //PlanFlujoId = SysData.ToLong(dr[clsPlanGrupoDetVM._PlanFlujoId]),
                                SucursalId = SysData.ToLong(dr[clsPlanGrupoDetVM._SucursalId]),
                                CenCosId = SysData.ToLong(dr[clsPlanGrupoDetVM._CenCosId]),
                                Orden = SysData.ToLong(dr[clsPlanGrupoDetVM._Orden]),
                                EstadoId = SysData.ToLong(dr[clsPlanGrupoDetVM._EstadoId])
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

        [HttpGet]
        public ActionResult PlanGrupoGrid(DataSourceLoadOptions loadOptions)
        {
            return Content(JsonConvert.SerializeObject(PlanGrupoGrid()), "application/json");
        }
    }
}