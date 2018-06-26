using Contabilidad.Models.DAC;
using Contabilidad.Models.VM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Contabilidad.Models.Modules
{
    public static class ComboBox
    {
        public static IEnumerable<clsTipoPlanVM> TipoPlanList()
        {
            clsTipoPlan oTipoPlan = new clsTipoPlan(clsAppInfo.Connection);
            List<clsTipoPlanVM> oTipoPlanVM = new List<clsTipoPlanVM>();

            try
            {
                oTipoPlan.SelectFilter = clsTipoPlan.SelectFilters.ListBox;
                oTipoPlan.WhereFilter = clsTipoPlan.WhereFilters.EstadoId;
                oTipoPlan.OrderByFilter = clsTipoPlan.OrderByFilters.TipoPlanDes;
                oTipoPlan.VM.EstadoId = ConstEstado.Activo;

                if (oTipoPlan.Open())
                {
                    foreach (DataRow dr in oTipoPlan.DataSet.Tables[oTipoPlan.TableName].Rows)
                    {
                        oTipoPlanVM.Add(new clsTipoPlanVM()
                        {
                            TipoPlanId = SysData.ToLong(dr[clsTipoPlanVM._TipoPlanId]),
                            TipoPlanDes = SysData.ToStr(dr[clsTipoPlanVM._TipoPlanDes])
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
                oTipoPlan.Dispose();
            }

            return ((IEnumerable<clsTipoPlanVM>)oTipoPlanVM);
        }

        public static IEnumerable<clsMonedaVM> MonedaList()
        {
            clsMoneda oMoneda = new clsMoneda(clsAppInfo.Connection);
            List<clsMonedaVM> oMonedaVM = new List<clsMonedaVM>();

            try
            {
                oMoneda.SelectFilter = clsMoneda.SelectFilters.ListBox;
                oMoneda.WhereFilter = clsMoneda.WhereFilters.None;
                oMoneda.OrderByFilter = clsMoneda.OrderByFilters.MonedaDes;

                if (oMoneda.Open())
                {
                    foreach (DataRow dr in oMoneda.DataSet.Tables[oMoneda.TableName].Rows)
                    {
                        oMonedaVM.Add(new clsMonedaVM()
                        {
                            MonedaId = SysData.ToLong(dr[clsMonedaVM._MonedaId]),
                            MonedaDes = SysData.ToStr(dr[clsMonedaVM._MonedaDes])
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
                oMoneda.Dispose();
            }

            return ((IEnumerable<clsMonedaVM>)oMonedaVM);
        }

        public static IEnumerable<clsTipoAmbitoVM> TipoAmbitoList()
        {
            clsTipoAmbito oTipoAmbito = new clsTipoAmbito(clsAppInfo.Connection);
            List<clsTipoAmbitoVM> oTipoAmbitoVM = new List<clsTipoAmbitoVM>();

            try
            {
                oTipoAmbito.SelectFilter = clsTipoAmbito.SelectFilters.ListBox;
                oTipoAmbito.WhereFilter = clsTipoAmbito.WhereFilters.EstadoId;
                oTipoAmbito.OrderByFilter = clsTipoAmbito.OrderByFilters.TipoAmbitoDes;
                oTipoAmbito.VM.EstadoId = ConstEstado.Activo;

                if (oTipoAmbito.Open())
                {
                    foreach (DataRow dr in oTipoAmbito.DataSet.Tables[oTipoAmbito.TableName].Rows)
                    {
                        oTipoAmbitoVM.Add(new clsTipoAmbitoVM()
                        {
                            TipoAmbitoId = SysData.ToLong(dr[clsTipoAmbitoVM._TipoAmbitoId]),
                            TipoAmbitoDes = SysData.ToStr(dr[clsTipoAmbitoVM._TipoAmbitoDes])
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
                oTipoAmbito.Dispose();
            }

            return ((IEnumerable<clsTipoAmbitoVM>)oTipoAmbitoVM);
        }

        public static IEnumerable<clsEstadoVM> EstadoList()
        {
            clsEstado oEstado = new clsEstado(clsAppInfo.Connection);
            List<clsEstadoVM> oEstadoVM = new List<clsEstadoVM>();

            try
            {
                oEstado.SelectFilter = clsEstado.SelectFilters.ListBox;
                oEstado.WhereFilter = clsEstado.WhereFilters.AplicacionId;
                oEstado.OrderByFilter = clsEstado.OrderByFilters.EstadoDes;
                oEstado.VM.AplicacionId = 0;

                if (oEstado.Open())
                {
                    foreach (DataRow dr in oEstado.DataSet.Tables[oEstado.TableName].Rows)
                    {
                        oEstadoVM.Add(new clsEstadoVM()
                        {
                            EstadoId = SysData.ToLong(dr[clsEstadoVM._EstadoId]),
                            EstadoDes = SysData.ToStr(dr[clsEstadoVM._EstadoDes])
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
                oEstado.Dispose();
            }

            return ((IEnumerable<clsEstadoVM>)oEstadoVM);
        }

        public static IEnumerable<clsCenCosGrupoVM> CenCosGrupoList()
        {
            clsCenCosGrupo oCenCosGrupo = new clsCenCosGrupo(clsAppInfo.Connection);
            List<clsCenCosGrupoVM> oCenCosGrupoVM = new List<clsCenCosGrupoVM>();

            try
            {
                oCenCosGrupo.SelectFilter = clsCenCosGrupo.SelectFilters.ListBox;
                oCenCosGrupo.OrderByFilter = clsCenCosGrupo.OrderByFilters.CenCosGrupoDes;

                if (oCenCosGrupo.Open())
                {
                    foreach (DataRow dr in oCenCosGrupo.DataSet.Tables[oCenCosGrupo.TableName].Rows)
                    {
                        oCenCosGrupoVM.Add(new clsCenCosGrupoVM()
                        {
                            CenCosGrupoId = SysData.ToLong(dr[clsCenCosGrupoVM._CenCosGrupoId]),
                            CenCosGrupoCod = SysData.ToStr(dr[clsCenCosGrupoVM._CenCosGrupoCod]),
                            CenCosGrupoDes = SysData.ToStr(dr[clsCenCosGrupoVM._CenCosGrupoDes])
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

            return ((IEnumerable<clsCenCosGrupoVM>)oCenCosGrupoVM);
        }

        public static IEnumerable<clsPlanGrupoTipoVM> PlanGrupoTipoList()
        {
            clsPlanGrupoTipo oPlanGrupoTipo = new clsPlanGrupoTipo(clsAppInfo.Connection);
            List<clsPlanGrupoTipoVM> oPlanGrupoTipoVM = new List<clsPlanGrupoTipoVM>();

            try
            {
                oPlanGrupoTipo.SelectFilter = clsPlanGrupoTipo.SelectFilters.ListBox;
                oPlanGrupoTipo.OrderByFilter = clsPlanGrupoTipo.OrderByFilters.PlanGrupoTipoDes;

                if (oPlanGrupoTipo.Open())
                {
                    foreach (DataRow dr in oPlanGrupoTipo.DataSet.Tables[oPlanGrupoTipo.TableName].Rows)
                    {
                        oPlanGrupoTipoVM.Add(new clsPlanGrupoTipoVM()
                        {
                            PlanGrupoTipoId = SysData.ToLong(dr[clsPlanGrupoTipoVM._PlanGrupoTipoId]),
                            PlanGrupoTipoCod = SysData.ToStr(dr[clsPlanGrupoTipoVM._PlanGrupoTipoCod]),
                            PlanGrupoTipoDes = SysData.ToStr(dr[clsPlanGrupoTipoVM._PlanGrupoTipoDes])
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
                oPlanGrupoTipo.Dispose();
            }

            return ((IEnumerable<clsPlanGrupoTipoVM>)oPlanGrupoTipoVM);
        }

        public static IEnumerable<clsPlanGrupoTipoDetVM> PlanGrupoTipoDetList()
        {
            clsPlanGrupoTipoDet oPlanGrupoTipoDet = new clsPlanGrupoTipoDet(clsAppInfo.Connection);
            List<clsPlanGrupoTipoDetVM> oPlanGrupoTipoDetVM = new List<clsPlanGrupoTipoDetVM>();

            try
            {
                oPlanGrupoTipoDet.SelectFilter = clsPlanGrupoTipoDet.SelectFilters.ListBox;
                oPlanGrupoTipoDet.OrderByFilter = clsPlanGrupoTipoDet.OrderByFilters.PlanGrupoTipoDetDes;

                if (oPlanGrupoTipoDet.Open())
                {
                    foreach (DataRow dr in oPlanGrupoTipoDet.DataSet.Tables[oPlanGrupoTipoDet.TableName].Rows)
                    {
                        oPlanGrupoTipoDetVM.Add(new clsPlanGrupoTipoDetVM()
                        {
                            PlanGrupoTipoDetId = SysData.ToLong(dr["PlanGrupoTipoDetId"]),
                            PlanGrupoTipoDetCod = SysData.ToStr(dr["PlanGrupoTipoDetCod"]),
                            PlanGrupoTipoDetDes = SysData.ToStr(dr["PlanGrupoTipoDetDes"]),
                            PlanGrupoTipoId = SysData.ToLong(dr["PlanGrupoTipoId"])
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
                oPlanGrupoTipoDet.Dispose();
            }

            return ((IEnumerable<clsPlanGrupoTipoDetVM>)oPlanGrupoTipoDetVM);
        }

        public static IEnumerable<clsPlanVM> PlanList()
        {
            clsPlan oPlan = new clsPlan(clsAppInfo.Connection);
            List<clsPlanVM> oPlanVM = new List<clsPlanVM>();

            try
            {
                oPlan.SelectFilter = clsPlan.SelectFilters.ListBox;
                oPlan.WhereFilter = clsPlan.WhereFilters.TipoPlanId;
                oPlan.OrderByFilter = clsPlan.OrderByFilters.PlanDes;
                oPlan.VM.TipoPlanId = 2;
                oPlan.VM.EstadoId = ConstEstado.Activo;

                if (oPlan.Open())
                {
                    foreach (DataRow dr in oPlan.DataSet.Tables[oPlan.TableName].Rows)
                    {
                        oPlanVM.Add(new clsPlanVM()
                        {
                            PlanId = SysData.ToLong(dr[clsPlanVM._PlanId]),
                            PlanDes = SysData.ToStr(dr[clsPlanVM._PlanDes]) + " - " + SysData.ToStr(dr[clsPlanVM._PlanCod])
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
                oPlan.Dispose();
            }

            return ((IEnumerable<clsPlanVM>)oPlanVM);
        }

        public static IEnumerable<clsSucursalVM> SucursalList()
        {
            clsSucursal oSucursal = new clsSucursal(clsAppInfo.Connection);
            List<clsSucursalVM> oSucursalVM = new List<clsSucursalVM>();

            try
            {
                oSucursal.SelectFilter = clsSucursal.SelectFilters.ListBox;
                oSucursal.OrderByFilter = clsSucursal.OrderByFilters.SucursalDes;

                if (oSucursal.Open())
                {
                    foreach (DataRow dr in oSucursal.DataSet.Tables[oSucursal.TableName].Rows)
                    {
                        oSucursalVM.Add(new clsSucursalVM()
                        {
                            SucursalId = SysData.ToLong(dr[clsSucursalVM._SucursalId]),
                            SucursalCod = SysData.ToStr(dr[clsSucursalVM._SucursalCod]),
                            SucursalDes = SysData.ToStr(dr[clsSucursalVM._SucursalDes])
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
                oSucursal.Dispose();
            }

            return ((IEnumerable<clsSucursalVM>)oSucursalVM);
        }

        public static IEnumerable<clsCenCosVM> CenCosList()
        {
            clsCenCos oCenCos = new clsCenCos(clsAppInfo.Connection);
            List<clsCenCosVM> oCenCosVM = new List<clsCenCosVM>();

            try
            {
                oCenCos.SelectFilter = clsCenCos.SelectFilters.ListBox;
                oCenCos.OrderByFilter = clsCenCos.OrderByFilters.CenCosDes;

                if (oCenCos.Open())
                {
                    foreach (DataRow dr in oCenCos.DataSet.Tables[oCenCos.TableName].Rows)
                    {
                        oCenCosVM.Add(new clsCenCosVM()
                        {
                            CenCosId = SysData.ToLong(dr[clsCenCosVM._CenCosId]),
                            CenCosCod = SysData.ToStr(dr[clsCenCosVM._CenCosCod]),
                            CenCosDes = SysData.ToStr(dr[clsCenCosVM._CenCosDes])
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
                oCenCos.Dispose();
            }

            return ((IEnumerable<clsCenCosVM>)oCenCosVM);
        }

        public static List<clsPlanGrupoDetVM> PlanGrupoDetList()
        {
            clsPlanGrupoDet oPlanGrupoDet = new clsPlanGrupoDet(clsAppInfo.Connection);
            List<clsPlanGrupoDetVM> oPlanGrupoDetVM = new List<clsPlanGrupoDetVM>();

            try
            {
                oPlanGrupoDet.SelectFilter = clsPlanGrupoDet.SelectFilters.All;
                oPlanGrupoDet.WhereFilter = clsPlanGrupoDet.WhereFilters.None;
                oPlanGrupoDet.OrderByFilter = clsPlanGrupoDet.OrderByFilters.Orden;

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
                            SucursalId = SysData.ToLong(dr[clsPlanGrupoDetVM._SucursalId]),
                            CenCosId = SysData.ToLong(dr[clsPlanGrupoDetVM._CenCosId]),
                            Orden = SysData.ToLong(dr[clsPlanGrupoDetVM._Orden]),
                            EstadoId = SysData.ToLong(dr[clsPlanGrupoDetVM._EstadoId])
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
                oPlanGrupoDet.Dispose();
            }

            return (oPlanGrupoDetVM);
        }

        public static List<clsPlanGrupoDetVM> PlanGrupoDetList(long lngPlanGrupoId)
        {
            clsPlanGrupoDet oPlanGrupoDet = new clsPlanGrupoDet(clsAppInfo.Connection);
            List<clsPlanGrupoDetVM> oPlanGrupoDetVM = new List<clsPlanGrupoDetVM>();

            try
            {
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
                            SucursalId = SysData.ToLong(dr[clsPlanGrupoDetVM._SucursalId]),
                            CenCosId = SysData.ToLong(dr[clsPlanGrupoDetVM._CenCosId]),
                            Orden = SysData.ToLong(dr[clsPlanGrupoDetVM._Orden]),
                            EstadoId = SysData.ToLong(dr[clsPlanGrupoDetVM._EstadoId])
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
                oPlanGrupoDet.Dispose();
            }

            return (oPlanGrupoDetVM);
        }

        public static IEnumerable<clsTipoPersonaVM> TipoPersonaList()
        {
            clsTipoPersona oTipoPersona = new clsTipoPersona(clsAppInfo.Connection);
            List<clsTipoPersonaVM> oTipoPersonaVM = new List<clsTipoPersonaVM>();

            try
            {
                oTipoPersona.SelectFilter = clsTipoPersona.SelectFilters.ListBox;
                oTipoPersona.OrderByFilter = clsTipoPersona.OrderByFilters.TipoPersonaDes;

                if (oTipoPersona.Open())
                {
                    foreach (DataRow dr in oTipoPersona.DataSet.Tables[oTipoPersona.TableName].Rows)
                    {
                        oTipoPersonaVM.Add(new clsTipoPersonaVM()
                        {
                            TipoPersonaId = SysData.ToLong(dr[clsTipoPersonaVM._TipoPersonaId]),
                            TipoPersonaCod = SysData.ToStr(dr[clsTipoPersonaVM._TipoPersonaCod]),
                            TipoPersonaDes = SysData.ToStr(dr[clsTipoPersonaVM._TipoPersonaDes])
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

            return ((IEnumerable<clsTipoPersonaVM>)oTipoPersonaVM);
        }

    }
}