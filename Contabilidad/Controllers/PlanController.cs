﻿using Contabilidad.Models.DAC;
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
    public class PlanController : Controller
    {
        List<clsPlanVM> moPlanVM = new List<clsPlanVM>();

        // GET: Plan
        public ActionResult Index(string MessageErr)
        {
            try
            {
                this.GetDefaultData();

                ViewBag.MessageErr = MessageErr;
                ViewBag.PlanIdDef = -1;
                return View();
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
                    // el nivel minimo para crear nuevo planes es 2
                    if (oPlanVM.Nivel > 1)
                    {
                        clsPlan oPlan = new clsPlan(clsAppInfo.Connection);
                        DataMove(oPlanVM, oPlan, false);


                        //preguntamos por el tipo de plan a insertar
                        if (oPlanVM.TipoPlanId == 1)
                        {  // TipoPlan = Grupo

                            // preguntamos si el PlanPadre tiene hijos
                            if (CantidadHijos(oPlanVM.PlanPadreId) > 0)
                            {
                                //preguntamos si tiene hijo de tipo analitico
                                clsPlanVM hijo = get_un_Hijo(oPlanVM.PlanPadreId);
                                if (hijo.TipoPlanId == 2)
                                {
                                    strMsg += "Solo puede Insertar Planes de Tipo Analíticos" + Environment.NewLine;
                                }

                            }
                        }
                        else
                        { // TipoPlan = Analiticos

                            //Moneda y TipoAmbito son necesarios para los Analiticos
                            if ((oPlanVM.MonedaId > 0) && (oPlanVM.TipoAmbitoId > 0))
                            {
                                // preguntamos si el PlanPadre tiene hijos
                                if (CantidadHijos(oPlanVM.PlanPadreId) > 0)
                                {
                                    //preguntamos si tiene hijo de tipo Grupo
                                    clsPlanVM hijo = get_un_Hijo(oPlanVM.PlanPadreId);
                                    if (hijo.TipoPlanId == 1)
                                    {
                                        strMsg += "Solo puede Insertar Planes de Tipo Grupo" + Environment.NewLine;
                                    }
                                }

                            }
                            else
                            {
                                strMsg += "Moneda es Requerido" + Environment.NewLine;
                                strMsg += "Ambito es Requerido" + Environment.NewLine;
                            }
                        }


                        //verificamos que no exista ningun error
                        if (strMsg.Trim() == string.Empty)
                        {
                            //preguntamos por el tipo de plan a insertar, si es Grupo, modificamos ciertas propiedades
                            if (oPlanVM.TipoPlanId == 1)
                            {

                                oPlan.VM.MonedaId = 0;
                                oPlan.VM.TipoAmbitoId = 0;
                            }

                            if (oPlan.Insert())
                            {
                                return RedirectToAction("Index");
                            }

                        }
                    }
                    else {
                        strMsg += "Nivel Invalido para Crear Nuevos Plan, seleccione un Nivel mas profundo" + Environment.NewLine;
                    }

                }
                else {
                    strMsg += "Datos Incorrectos Revise y Vuelva a Intentar" + Environment.NewLine;
                }
            }

            catch (Exception exp)
            {
                ViewBag.MessageErr = exp.Message;
                return View(oPlanVM);
            }

            if (strMsg.Trim() != string.Empty)
            {
                ViewBag.MessageErr = strMsg;
                return View(oPlanVM);
            }
            else
            {
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
            clsPlan oPlan = new clsPlan(clsAppInfo.Connection);

            try
            {
                if (ModelState.IsValid)
                {
                    
                    DataMove(oPlanVM, oPlan, true);

                    // obtenemos el TipoPlan original del Plan
                    if (getTipoPlan(oPlanVM.PlanId) == 1)
                    {    //TipoPlan = Grupo

                        strMsg = Validar_Edit_TipoPlan_Grupo(oPlanVM);
                    }
                    else
                    {    //TipoPlan = Analitica

                        strMsg = Validar_Edit_TipoPlan_Analitica(oPlanVM);

                    }

                }
                else
                {
                    if (oPlanVM.PlanPadreId == -1) {
                        strMsg += "Operacion Invalida: Nivel de Plan Inaccesible para realizar Cambios, porfavor elija otro Plan" + Environment.NewLine;
                    }
                }

                if (strMsg.Trim() != string.Empty)
                {
                    ViewBag.MessageErr = strMsg;
                }
                else
                {
                    if (oPlan.Update())
                    {
                        return RedirectToAction("Index");
                    }

                }

                return View(oPlanVM);

            }
            catch (Exception exp)
            {
                ViewBag.MessageErr = exp.Message;
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
            string strMsg = string.Empty;
            clsPlan oPlan = new clsPlan(clsAppInfo.Connection);
            oPlan.WhereFilter = clsPlan.WhereFilters.PrimaryKey;
            oPlan.VM.PlanId = id;

            try
            {
                if (ReferenceEquals(id, null))
                {
                    return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = "Índice nulo o no encontrado" });
                }

                // obtenemos el TipoPlan original del Plan
                if (getTipoPlan(oPlan.VM.PlanId) == 1)
                {    //TipoPlan = Grupo

                    //preguntamos si tiene hijos
                    if (CantidadHijos(oPlan.VM.PlanId) > 0)
                    {

                        strMsg += "Operacion Invalida: No puede cambiar el Tipo de Cuenta, ya que existen otros planes dependientes de este Grupo" + Environment.NewLine;
                    }
                }
                else
                {    //TipoPlan = Analitica

                    // Debe agregarse la condicion que no tengan movimiento contable

                }


                if (strMsg.Trim() != string.Empty)
                {
                    ViewBag.MessageErr = strMsg;
                }
                else
                {
                    if (oPlan.Delete())
                    {
                        return RedirectToAction("Index");
                    }

                }

                return View(oPlan.VM.PlanId);


            }

            catch (Exception exp)
            {
                ViewBag.MessageErr = exp.Message;
                return View(oPlan.VM.PlanId);
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


                return View(oPlanVM);
            }

            catch (Exception exp)
            {
                return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = exp.Message });
            }
        }


        /// <summary>
        /// Valida condiciones del plan que tengan almacenado TipoPlan = Grupo
        /// </summary>
        /// <param name="oPlanVM"></param>
        /// <returns></returns>
        private string Validar_Edit_TipoPlan_Grupo(clsPlanVM oPlanVM)
        {
            string strMsg = "";
            clsPlan oPlan = new clsPlan(clsAppInfo.Connection);

            long lgnTipoPlanBD = getTipoPlan(oPlanVM.PlanId);    // obtenemos el tipo plan de ese plan, guardado en la BD

            // preguntamos si quiere cambiar el TipoPlan(Tipo Cuenta)
            if (oPlanVM.TipoPlanId != lgnTipoPlanBD)
            {
                //preguntamos si tiene hijos
                if (CantidadHijos(oPlanVM.PlanId) > 0)
                {

                    strMsg += "Operacion Invalida: No puede cambiar el Tipo de Cuenta, ya que existen otros planes dependientes de este Grupo" + Environment.NewLine;
                }

            }

            return strMsg;
        }



        /// <summary>
        ///Valida condiciones del plan que tengan almacenado TipoPlan = Analitica
        /// </summary>
        /// <param name="oPlanVM"></param>
        /// <returns></returns>
        private string Validar_Edit_TipoPlan_Analitica(clsPlanVM oPlanVM)
        {
            string strMsg = "";
            clsPlan oPlan = new clsPlan(clsAppInfo.Connection);

            long lgnTipoPlanBD = getTipoPlan(oPlanVM.PlanId);    // obtenemos el tipo plan de ese plan, guardado en la BD

            // preguntamos si quiere cambiar el TipoPlan(Tipo Cuenta)
            if (oPlanVM.TipoPlanId != lgnTipoPlanBD)
            {
                //preguntamos si quiere cambiar a TipoPlan = Grupo
                if (oPlanVM.TipoPlanId == 1)
                {
                    //preguntamos si tiene hermanos analicos
                    if (CantidadHijos(oPlanVM.PlanPadreId) > 1)
                    {
                        strMsg += "Operacion Invalida: No puede cambiar el Tipo de Cuenta, ya que existen otros planes del mismo Tipo(Analitica) dentro del Grupo" + Environment.NewLine;
                    }
                }

            }

            return strMsg;
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
                            PlanId = SysData.ToLong(dr[clsPlanVM._PlanId]),
                            PlanCod = SysData.ToStr(dr[clsPlanVM._PlanCod]),
                            PlanDes = SysData.ToStr(dr[clsPlanVM._PlanDes]),
                            TipoPlanId = SysData.ToLong(dr[clsPlanVM._TipoPlanId]),
                            TipoPlanDes = SysData.ToStr(dr[clsPlanVM._TipoPlanDes]),
                            Orden = SysData.ToLong(dr[clsPlanVM._Orden]),
                            Nivel = SysData.ToLong(dr[clsPlanVM._Nivel]),
                            MonedaId = SysData.ToLong(dr[clsPlanVM._MonedaId]),
                            MonedaDes = SysData.ToStr(dr[clsPlanVM._MonedaDes]),
                            CapituloId = SysData.ToLong(dr[clsPlanVM._CapituloId]),
                            PlanPadreId = SysData.ToLong(dr[clsPlanVM._PlanPadreId]),
                            EstadoId = SysData.ToLong(dr[clsPlanVM._EstadoId]),
                            EstadoDes = SysData.ToStr(dr[clsPlanVM._EstadoDes])
                        });

                        if (TieneHijos(SysData.ToLong(dr[clsPlanVM._PlanId])))
                        {
                            PlanHijoLoad(SysData.ToLong(dr[clsPlanVM._PlanId]));
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

        /// <summary>
        /// Retorna el TipoPlanId actual de dicho Plan
        /// </summary>
        /// <param name="lngPlanId"></param>
        /// <returns></returns>
        private long getTipoPlan(long lngPlanId) {

            clsPlan oPlan = new clsPlan(clsAppInfo.Connection);
            long returnValue = 0;

            try
            {
                oPlan.VM.PlanId = lngPlanId;

                if (oPlan.FindByPK())
                {
                    returnValue = oPlan.VM.TipoPlanId;
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

        /// <summary>
        /// Retorna la cantidad de Planes que contiene dicho Plan Padre
        /// </summary>
        /// <param name="lngPlanPadreId"></param>
        /// <returns></returns>
        private int CantidadHijos(long lngPlanPadreId) {
            clsPlan oPlan = new clsPlan(clsAppInfo.Connection);
            int returnValue = 0;

            try
            {
                oPlan.SelectFilter = clsPlan.SelectFilters.All;
                oPlan.WhereFilter = clsPlan.WhereFilters.PlanPadreId;
                oPlan.VM.PlanPadreId = lngPlanPadreId;

                if (oPlan.FindOnly())
                {
                    returnValue = oPlan.getMintRowsCount();
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

        private clsPlanVM get_un_Hijo(long PlanPadreId) {

            clsPlan oPlan = new clsPlan(clsAppInfo.Connection);
            clsPlanVM oPlanVM = new clsPlanVM();

            try
            {
                oPlan.SelectFilter = clsPlan.SelectFilters.All;
                oPlan.WhereFilter = clsPlan.WhereFilters.PlanPadreId;
                oPlan.VM.PlanPadreId = PlanPadreId;

                if (oPlan.Find()) {
                    oPlanVM = oPlan.VM;

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


        [HttpGet]
        public ActionResult PlanGrid_View(DataSourceLoadOptions loadOptions)
        {
            return Content(JsonConvert.SerializeObject(PlanGrid()), "application/json");
        }
    }
}