using Contabilidad.Models.DAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contabilidad.Models.Modules
{
    public static class AppCode
    {
        public static bool AplicacionFind(long lngAplicacionId)
        {
            bool returnValue = false;
            clsAplicacion oAplicacion = new clsAplicacion(clsAppInfo.Connection);

            clsAppInfo.ModuloId = 0;
            clsAppInfo.AplicacionId = 0;
            clsAppInfo.AplicacionDes = "";

            try
            {
                oAplicacion.VM.AplicacionId = lngAplicacionId;

                if (oAplicacion.FindByPK())
                {
                    if (oAplicacion.VM.EstadoId == ConstEstado.Activo)
                    {
                        clsAppInfo.ModuloId = oAplicacion.VM.ModuloId;
                        clsAppInfo.AplicacionId = oAplicacion.VM.AplicacionId;
                        clsAppInfo.AplicacionDes = oAplicacion.VM.AplicacionDes;

                        returnValue = true;
                    }
                }
            }

            catch (Exception exp)
            {
                throw (exp);

            }
            finally
            {
                oAplicacion.Dispose();
            }

            return returnValue;
        }

        public static bool AutorizaFind(long lngTipoUsuarioId, long lngAutorizaItemId, long lngRegistroId)
        {
            bool returnValue = false;
            clsAutoriza oAutoriza = new clsAutoriza(clsAppInfo.Connection);

            clsAppInfo.AutorizaId = 0;

            try
            {
                oAutoriza.SelectFilter = clsAutoriza.SelectFilters.All;
                oAutoriza.WhereFilter = clsAutoriza.WhereFilters.TipoUsuarioIdAppId;
                oAutoriza.VM.TipoUsuarioId = lngTipoUsuarioId;
                oAutoriza.VM.AutorizaItemId = lngAutorizaItemId;
                oAutoriza.VM.RegistroId = lngRegistroId;

                if (oAutoriza.Find())
                {
                    clsAppInfo.AutorizaId = oAutoriza.VM.AutorizaId;

                    returnValue = true;
                }
            }

            catch (Exception exp)
            {
                throw (exp);

            }
            finally
            {
                oAutoriza.Dispose();
            }

            return returnValue;
        }

        public static bool UsuarioCodFind(string strUsuarioCod)
        {
            bool returnValue = false;
            clsUsuario oUsuario = new clsUsuario(clsAppInfo.Connection);

            clsAppInfo.TipoUsuarioId = 0;
            clsAppInfo.UsuarioId = 0;
            clsAppInfo.UsuarioCod = "";
            clsAppInfo.UsuarioDes = "";

            try
            {
                oUsuario.SelectFilter = clsUsuario.SelectFilters.All;
                oUsuario.WhereFilter = clsUsuario.WhereFilters.UsuarioCod;
                oUsuario.VM.UsuarioCod = strUsuarioCod;

                if (oUsuario.Find())
                {
                    if (oUsuario.VM.EstadoId == ConstEstado.Activo)
                    {
                        clsAppInfo.TipoUsuarioId = oUsuario.VM.TipoUsuarioId;
                        clsAppInfo.UsuarioId = oUsuario.VM.UsuarioId;
                        clsAppInfo.UsuarioCod = oUsuario.VM.UsuarioCod;
                        clsAppInfo.UsuarioDes = oUsuario.VM.UsuarioDes;
                        clsAppInfo.UsuarioFotoPath = oUsuario.VM.UsuarioFotoPath;

                        returnValue = true;
                    }
                }
            }

            catch (Exception exp)
            {
                throw (exp);

            }
            finally
            {
                oUsuario.Dispose();
            }

            return returnValue;
        }

    }
}