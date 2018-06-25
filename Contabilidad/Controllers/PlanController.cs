using Contabilidad.Models.DAC;
using Contabilidad.Models.Modules;
using Contabilidad.Models.VM;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        public ActionResult Index(string MessageErr, long? idPlan)
        {
            try
            {
                this.GetDefaultData();

                ViewBag.MessageErr = MessageErr;
                ViewBag.PlanIdDef = -1;

                long idTreeSelect = -1;
                if (!ReferenceEquals(idPlan, null))
                {
                    idTreeSelect = SysData.ToLong(idPlan);
                }

                ViewBag.SelectId = idTreeSelect;
                ViewBag.rutaListaId = CalcularCaminoListaId(idTreeSelect);
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

            try
            {

                this.GetDefaultData();

                if (ReferenceEquals(id, null))
                {
                    return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = "Índice nulo o no encontrado" });
                }

                clsPlan oPlanPadre = new clsPlan(clsAppInfo.Connection);
                oPlanPadre.VM.PlanId = SysData.ToLong(id);

                if (oPlanPadre.FindByPK())
                {
                    strMsg += CheckPlanGetCreate(oPlanPadre);   // funcion que valida al plan

                    if (String.IsNullOrEmpty(strMsg))
                    {

                        clsPlanVM oPlanVM = new clsPlanVM();
                        PlanHijoNew(oPlanPadre, oPlanVM);

                        return View(oPlanVM);
                    }

                }
                else
                {
                    strMsg += "Cuenta Padre Inválida" + Environment.NewLine;
                }

                // mostramos mensaje de error
                if (strMsg.Trim() != string.Empty)
                {
                    ViewBag.MessageErr = strMsg;
                    return RedirectToAction("Index", new { MessageErr = strMsg, idPlan = SysData.ToLong(id) });

                }
                else
                {
                    return RedirectToAction("Index", new { idPlan = SysData.ToLong(id) });
                }

            }
            catch (Exception exp)
            {
                return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = exp.Message });
            }


        }


        private string CheckPlanGetCreate(clsPlan oPlanPadre)
        {

            string strMsg = string.Empty;
            clsPlanVM oPlanVM = new clsPlanVM();

            if (oPlanPadre.VM.Nivel >= 1)
            {
                if (oPlanPadre.VM.Nivel >= 10)
                {
                    strMsg += "Nivel Maximo(10) Superado, seleccione un Nivel anterior y vuelva a Intentarlo" + Environment.NewLine;
                }

                if (oPlanPadre.VM.TipoPlanId > 1) // el padre no puede ser analitico
                {
                    strMsg += "Un Plan de Tipo Analítico no puede contener otros Planes";
                }
            }
            else
            {
                strMsg += "Cuenta Padre Inválida" + Environment.NewLine;
            }

            return strMsg;
        }

        // POST: Plan/Create
        [HttpPost()]
        [ValidateAntiForgeryToken()]
        public ActionResult Create(clsPlanVM oPlanVM)
        {
            string strMsg = string.Empty;
            clsPlan oPlan = new clsPlan(clsAppInfo.Connection);

            try
            {
                if (ModelState.IsValid)
                {
                    
                    DataMove(oPlanVM, oPlan, false);
                    strMsg += CheckPlanCreatePost(ref oPlan);

                    if (String.IsNullOrEmpty(strMsg))              // verificar si existe error
                    {
                        List<clsPlanVM> hijos = get_Hijos(oPlan);
                        oPlan.BeginTransaction();

                        if (oPlan.Insert())
                        {
                            if (ActualizarOrden(oPlan, hijos))
                            {
                                oPlan.Commit();
                                strMsg += "Plan Creado Correctamente";
                                return RedirectToAction("Index", new { MessageErr = strMsg, idPlan = SysData.ToLong(oPlan.VM.PlanId) });
                            }
                            else
                            {
                                oPlan.Rollback();
                                strMsg += "Ocurrio un Problema a Crear(Problema a Actualizar los Orden) Plan Vuelva a Intentarlo";
                                return RedirectToAction("Index", new { MessageErr = strMsg, idPlan = SysData.ToLong(oPlanVM.PlanPadreId) });
                            }
                        }
                        else
                        {
                            oPlan.Rollback();
                            strMsg += "Ocurrio un Problema a Crear Plan Vuelva a Intentarlo";
                            return RedirectToAction("Index", new { MessageErr = strMsg, idPlan = SysData.ToLong(oPlanVM.PlanPadreId) });
                        }
                    }
                }
                else
                {
                    strMsg += "Datos Incorrectos Revise y Vuelva a Intentar" + Environment.NewLine;
                }
            }

            catch (Exception exp)
            {
                oPlan.Rollback();
                ViewBag.MessageErr = exp.Message;
                return View(oPlanVM);
            }

            if (strMsg.Trim() != string.Empty)
            {
                ViewBag.MessageErr = strMsg;
            }

            return View(oPlanVM);
        }

        private string CheckPlanCreatePost(ref clsPlan oPlan)
        {
            string strMsg = string.Empty;

            if (oPlan.VM.TipoPlanId == 1)
            {  // TipoPlan = Grupo

                strMsg += CheckPlanCreatePostGrupo(oPlan.VM);

                if (String.IsNullOrEmpty(strMsg))              // verificar si existe error
                {
                    oPlan.VM.MonedaId = 0;
                    oPlan.VM.TipoAmbitoId = 0;
                }
            }
            else
            { // TipoPlan = Analiticos

                strMsg += CheckPlanCreatePostAnalitica(oPlan.VM);

            }

            return strMsg;
        }

        private string CheckPlanCreatePostGrupo(clsPlanVM oPlanVM)
        {
            string strMsg = string.Empty;

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

            return strMsg;
        }

        private string CheckPlanCreatePostAnalitica(clsPlanVM oPlanVM)
        {
            string strMsg = string.Empty;

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

            return strMsg;
        }


        // GET: Plan/Edit/5
        public ActionResult Edit(int? id)
        {
            string strMsg = string.Empty;
            clsPlanVM oPlanVM = new clsPlanVM();

            try
            {
                this.GetDefaultData();

                if (ReferenceEquals(id, null))
                {
                    return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = "Índice nulo o no encontrado" });
                }

                oPlanVM = PlanFind(SysData.ToLong(id));

                if (ReferenceEquals(oPlanVM, null))
                {
                    return RedirectToAction("Index", new { MessageErr = "Nivel Inválido", idPlan = -1});
                }

                // se pueden editar apartir del nivel 2
                if (oPlanVM.Nivel > 1)
                {
                    return View(oPlanVM);
                }
                else
                {
                    strMsg += "El Plan no puede ser Editado, se encuentra en un nivel protegido, porfavor seleccione un Plan con Nivel mas profundo." + Environment.NewLine;
                    return RedirectToAction("Index", new { MessageErr = strMsg, idPlan = SysData.ToLong(id) });
                }

            }
            catch (Exception exp)
            {
                ViewBag.MessageErr = exp.Message;
                return View(oPlanVM);
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

                    strMsg += CheckPlanEditPost(oPlan);

                }
                else
                {
                    if (oPlanVM.PlanPadreId == -1)
                    {
                        strMsg += "Operacion Invalida: Nivel de Plan Inaccesible para realizar Cambios, porfavor elija otro Plan" + Environment.NewLine;
                    }
                    else
                    {
                        strMsg += "Modelo Invalido" + Environment.NewLine;
                    }
                }

                if (strMsg.Trim() != string.Empty)
                {
                    ViewBag.MessageErr = strMsg;
                }
                else
                {
                    // obtenemos el orden guardado en la BD, por si lo quiere modificar
                    long ordenBD = getOrden(oPlan.VM.PlanId);
                    List<clsPlanVM> hijos = new List<clsPlanVM>();

                    if (ordenBD != oPlan.VM.Orden)
                    {  // si quiere modificar el orden
                        hijos = get_Hijos(oPlan);

                        hijos.RemoveAll((x) => x.PlanId == oPlan.VM.PlanId); // eliminamos el plan que se esta actualizando
                    }


                    oPlan.BeginTransaction();
                    if (oPlan.Update())
                    {
                        if (hijos.Count > 0)
                        {  // si quiere modificar el orden

                            if (!ActualizarOrden(oPlan, hijos))
                            {
                                oPlan.Rollback();
                                ViewBag.MessageErr = "Error al Actualizar Orden de los demas Planes del mismo Grupo";
                                return View(oPlanVM);
                            }
                        }

                        oPlan.Commit();
                        return RedirectToAction("Index", new { idPlan = SysData.ToLong(oPlanVM.PlanId) });
                    }

                    oPlan.Rollback();

                }

                return View(oPlanVM);

            }
            catch (Exception exp)
            {
                oPlan.Rollback();
                ViewBag.MessageErr = exp.Message;
                return View(oPlanVM);
            }

        }



        // GET: Plan/Delete/5
        public ActionResult Delete(int? id, string mensajeError)
        {
            string strMsg = string.Empty;
            clsPlanVM oPlanVM = new clsPlanVM();

            try
            {
                this.GetDefaultData();

                if (ReferenceEquals(id, null))
                {
                    return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = "Índice nulo o no encontrado" });
                }

                oPlanVM = PlanFind(SysData.ToLong(id));

                if (ReferenceEquals(oPlanVM, null))
                {
                    //return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = "Índice no encontrado" });
                    return RedirectToAction("Index", new { MessageErr = "Nivel Inválido", idPlan = -1 });
                }

                //preguntamos si proviene un mensaje de error desde el post delete
                if (String.IsNullOrEmpty(mensajeError))
                {
                    strMsg += CheckPlanDeleteGet(oPlanVM);
                    if (String.IsNullOrEmpty(strMsg))
                    {
                        return View(oPlanVM);
                    }
                    else
                    {
                        return RedirectToAction("Index",new { MessageErr = strMsg, idPlan = oPlanVM.PlanId});
                    }
                }
                else
                {
                    strMsg = mensajeError;
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

                oPlan.FindByPK();

                long idPadre = oPlan.VM.PlanPadreId;
                strMsg += CheckPlanDeletePost(oPlan.VM);

                if (String.IsNullOrEmpty(strMsg))
                {
                    if (oPlan.Delete())
                    {

                        return RedirectToAction("Index", new { idPlan = SysData.ToLong(idPadre) });
                    }
                }

                return RedirectToAction("Delete", new { id = oPlan.VM.PlanId, mensajeError = " Error al eliminar Plan" });

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
                    return RedirectToAction("Index", new { MessageErr = "Nivel Invalido", idPlan = -1 });
                }

                //ViewBag.Hijos = get_Hijos(oPlanVM.PlanId).Count;

                return View(oPlanVM);
            }

            catch (Exception exp)
            {
                return RedirectToAction("httpErrorMsg", "Error", new { MessageErr = exp.Message });
            }
        }


        private string CheckPlanEditPost(clsPlan oPlan)
        {
            string strMsg = string.Empty;

            // obtenemos el TipoPlan original del Plan
            if (getTipoPlan(oPlan.VM.PlanId) == 1)
            {    //TipoPlan = Grupo

                strMsg = _CheckPlanEditPostGrupo(oPlan.VM);
            }
            else
            {    //TipoPlan = Analitica

                strMsg = _CheckPlanEditPostAnalitica(oPlan.VM);

            }

            return strMsg;
        }

        private string _CheckPlanEditPostGrupo(clsPlanVM oPlanVM)
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
                else
                {
                    //obtenemos la cantidad de hijos del planPadre
                    if (CantidadHijos(oPlanVM.PlanPadreId) > 1)
                    {       // sino tiene hermanos, puede cambiar a analitica tranquilamente
                        strMsg += "No puede cambiar de Tipo de Cuenta a Analitica, ya que existen planes de Tipo Grupo, dentro del mismo nivel" + Environment.NewLine;
                    }
                }
            }

            return strMsg;
        }

        private string _CheckPlanEditPostAnalitica(clsPlanVM oPlanVM)
        {
            string strMsg = "";
            clsPlan oPlan = new clsPlan(clsAppInfo.Connection);

            long lgnTipoPlanBD = getTipoPlan(oPlanVM.PlanId);    // obtenemos el tipo plan de ese plan, guardado en la BD

            // preguntamos si quiere cambiar el TipoPlan(Tipo Cuenta)
            if (oPlanVM.TipoPlanId != lgnTipoPlanBD)
            {
                //preguntamos si tiene hermanos analiticos
                if (CantidadHijos(oPlanVM.PlanPadreId) > 1)
                {
                    strMsg += "Operacion Invalida: No puede cambiar el Tipo de Cuenta, ya que existen otros planes del mismo Tipo(Analitica) dentro del Grupo" + Environment.NewLine;
                }

            }

            return strMsg;
        }

        private string CheckPlanDeleteGet(clsPlanVM oPlanVM)
        {
            string strMsg = string.Empty;

            // se pueden eliminar apartir del nivel 2
            if (oPlanVM.Nivel < 2)
            {
                strMsg += "El Plan no puede ser Eliminado, se encuentra en un nivel protegido, porfavor seleccione un Plan con Nivel mas profundo." + Environment.NewLine;
            }

            // verficamos que no tenga hijos
            if (CantidadHijos(oPlanVM.PlanId) > 0)
            {
                strMsg += "El Plan no puede ser elminado por que tiene Planes dependiente de el";
            }

            return strMsg;
        }

        private string CheckPlanDeletePost(clsPlanVM oPlanVM)
        {
            //hay que agregar condiciones como que no tenga movimientos

            return string.Empty;
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
                //oPlan.OrderByFilter = clsPlan.OrderByFilters.Grid;
                oPlan.OrderByFilter = clsPlan.OrderByFilters.Orden;
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

                        if (CantidadHijos(SysData.ToLong(dr[clsPlanVM._PlanId])) > 0)
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


        private long getTipoPlan(long lngPlanId)
        {
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

        private long getOrden(long lngPlanId)
        {
            clsPlan oPlan = new clsPlan(clsAppInfo.Connection);
            long returnValue = 0;

            try
            {
                oPlan.VM.PlanId = lngPlanId;

                if (oPlan.FindByPK())
                {
                    returnValue = oPlan.VM.Orden;
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


        private int CantidadHijos(long lngPlanPadreId)
        {
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

        /*private bool TieneHijos(long lngPlanPadreId)
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
        }*/

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

        private clsPlanVM get_un_Hijo(long PlanPadreId)
        {   // devuelve el primer hijo de ese PlanPadre

            clsPlan oPlan = new clsPlan(clsAppInfo.Connection);
            clsPlanVM oPlanVM = new clsPlanVM();

            try
            {
                oPlan.SelectFilter = clsPlan.SelectFilters.All;
                oPlan.WhereFilter = clsPlan.WhereFilters.PlanPadreId;
                oPlan.VM.PlanPadreId = PlanPadreId;

                if (oPlan.Find())
                {
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



        /*
          * la lista debe contener los hijos menos el oplan a insertar o modificar
          * oplan, es el nuevo hijo a insertar o modificar
        */
        private bool ActualizarOrden(clsPlan oplan, List<clsPlanVM> hijos)   
        {
            int bandera = 0;

            if (hijos.Count > 0)
            {
                try
                {
                    for (int i = 0; i < hijos.Count; i++)
                    {
                        clsPlanVM hijo = hijos[i];

                        if ( oplan.VM.Orden == i+1)
                        {
                            bandera = 1;
                        }    

                        if( !(hijo.Orden == i + bandera + 1))
                        {
                            hijo.Orden = i + bandera + 1;

                            clsPlan auxplan = new clsPlan(clsAppInfo.Connection);
                            auxplan.VM = hijos[i];
                            auxplan.UpdateFilter = clsPlan.UpdateFilters.Orden;
                            auxplan.Transaction = oplan.Transaction;
                            

                            if (!auxplan.Update())
                            {  // error al actualizar
                                return false;
                            }

                            auxplan.Dispose();  
                        }

                    }
                }
                catch (Exception exp) {
                    throw (exp);
                }

            }

            // preguntamos si el nuevo plan quiere ir al final
            if (bandera == 0)
            {
                clsPlan auxplan = new clsPlan(clsAppInfo.Connection);
                
                //actualizamos el orden del nuevo plan
                oplan.VM.Orden = hijos.Count + 1;
                auxplan.VM = oplan.VM;

                auxplan.UpdateFilter = clsPlan.UpdateFilters.Orden;
                auxplan.Transaction = oplan.Transaction;


                if (!auxplan.Update())
                {  // error al actualizar
                    return false;
                }

                auxplan.Dispose();
            }

            return true;

        }

        //private bool ActualizarOrdenEditar(clsPlan oplan, List<clsPlanVM> hijos)
        //{
        //    int bandera = 0;
        //    long incrementadorOrden = 0;

        //    if (hijos.Count > 0)
        //    {
        //        try
        //        {
        //            for (int i = 0; i < hijos.Count; i++)
        //            {
        //                incrementadorOrden++;

        //                clsPlanVM hijo = hijos[i];

        //                if (oplan.VM.Orden == i + 1)
        //                {
        //                    bandera = 1;

        //                    hijo.Orden = i + bandera + 1;

        //                    clsPlan auxplan = new clsPlan(clsAppInfo.Connection);
        //                    auxplan.VM = hijos[i];
        //                    auxplan.UpdateFilter = clsPlan.UpdateFilters.Orden;
        //                    auxplan.Transaction = oplan.Transaction;


        //                    if (!auxplan.Update())
        //                    {  // error al actualizar
        //                        return false;
        //                    }

        //                    auxplan.Dispose();
        //                }
        //                else
        //                {
        //                    if (!(hijo.Orden == i + bandera + 1))
        //                    {
        //                        hijo.Orden = i + bandera + 1;

        //                        clsPlan auxplan = new clsPlan(clsAppInfo.Connection);
        //                        auxplan.VM = hijos[i];
        //                        auxplan.UpdateFilter = clsPlan.UpdateFilters.Orden;
        //                        auxplan.Transaction = oplan.Transaction;


        //                        if (!auxplan.Update())
        //                        {  // error al actualizar
        //                            return false;
        //                        }

        //                        auxplan.Dispose();
        //                    }

        //                }

        //            }
        //        }
        //        catch (Exception exp)
        //        {
        //            throw (exp);
        //        }

        //    }

        //    return true;

        //}


        private List<clsPlanVM> get_Hijos(clsPlan oPlan )
        {   // devuelve todos hijos de ese PlanPadre

            List<clsPlanVM> lista = new List<clsPlanVM>();

            try
            {
                oPlan.SelectFilter = clsPlan.SelectFilters.All;
                oPlan.WhereFilter = clsPlan.WhereFilters.PlanPadreId;
                oPlan.OrderByFilter = clsPlan.OrderByFilters.Orden;

                if (oPlan.Open())
                {
                    foreach (DataRow dr in oPlan.DataSet.Tables[oPlan.TableName].Rows)
                    {
                        lista.Add(new clsPlanVM()
                        {
                            PlanId = SysData.ToLong(dr[clsPlanVM._PlanId]),
                            PlanCod = SysData.ToStr(dr[clsPlanVM._PlanCod]),
                            PlanDes = SysData.ToStr(dr[clsPlanVM._PlanDes]),
                            PlanEsp = SysData.ToStr(dr[clsPlanVM._PlanEsp]),
                            TipoPlanId = SysData.ToLong(dr[clsPlanVM._TipoPlanId]),
                            Orden = SysData.ToLong(dr[clsPlanVM._Orden]),
                            Nivel = SysData.ToLong(dr[clsPlanVM._Nivel]),
                            MonedaId = SysData.ToLong(dr[clsPlanVM._MonedaId]),
                            TipoAmbitoId = SysData.ToLong(dr[clsPlanVM._TipoAmbitoId]),
                            PlanAjusteId = SysData.ToLong(dr[clsPlanVM._PlanAjusteId]),
                            CapituloId = SysData.ToLong(dr[clsPlanVM._CapituloId]),
                            PlanPadreId = SysData.ToLong(dr[clsPlanVM._PlanPadreId]),
                            EstadoId = SysData.ToLong(dr[clsPlanVM._EstadoId])

                        });

                    }
                    return lista;

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
                    //oPlanVM.TipoPlanId = oPlan.VM.TipoPlanId;
                    oPlanVM.Nivel = oPlan.VM.Nivel;
                    oPlanVM.Orden = oPlan.VM.Orden + 1;
                    oPlanVM.CapituloId = oPlan.VM.CapituloId;
                    oPlanVM.PlanPadreId = oPlan.VM.PlanPadreId;
                    //oPlanVM.EstadoId = ConstEstado.Activo;
                }
                else
                {
                    oPlanVM.PlanCod = oPlanPadre.VM.PlanCod;
                    // oPlanVM.TipoPlanId = 0;
                    oPlanVM.Nivel = oPlanPadre.VM.Nivel + 1;
                    oPlanVM.Orden = 1;
                    oPlanVM.CapituloId = oPlanPadre.VM.CapituloId;
                    oPlanVM.PlanPadreId = oPlanPadre.VM.PlanId;
                    //oPlanVM.EstadoId = ConstEstado.Activo;
                }
                oPlanVM.TipoPlanId = 0;
                oPlanVM.EstadoId = oPlanPadre.VM.EstadoId;
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

        [HttpGet]
        public List<long> CalcularCaminoListaId(long id)
        {
            List<long> ruta = new List<long>();
            clsPlan clsPlan = new clsPlan(clsAppInfo.Connection);

            clsPlan.VM.PlanId = id;
            clsPlan.FindByPK();

            while (clsPlan.VM.PlanId > 0)
            {
                ruta.Add(SysData.ToLong(clsPlan.VM.PlanId));
                clsPlan.VM.PlanId = clsPlan.VM.PlanPadreId;
                clsPlan.FindByPK();
            }

            ruta.Add(-1); // agregamos el nodo 0

            ruta.Reverse();

            return ruta;
        }
    }
}