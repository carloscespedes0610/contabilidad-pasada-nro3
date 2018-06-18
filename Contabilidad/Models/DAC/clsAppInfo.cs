using System;
using System.Data.SqlClient;

namespace Contabilidad.Models.DAC
{
    public class clsAppInfo
    {
        private static string mstrConnectString;
        private static SqlConnection moConnection;

        private static long mlngModuloId;
        private static long mlngAplicacionId = 4;
        private static long mlngAutorizaItemId = 1;
        private static string mstrAplicacionDes;
        private static long mlngAutorizaId;

        private static long mlngTipoUsuarioId;
        private static long mlngUsuarioId;
        private static string mstrUsuarioCod;
        private static string mstrUsuarioDes;
        private static string mstrUsuarioPass;
        private static string mstrUsuarioFotoPath;

        private static string mstrAppPath;

        public static string ConnectString
        {
            get
            {
                return mstrConnectString;
            }

            set
            {
                mstrConnectString = value;
            }
        }

        public static SqlConnection Connection
        {
            get
            {
                return moConnection;
            }

            set
            {
                moConnection = value;
            }
        }

        public static long ModuloId
        {
            get
            {
                return mlngModuloId;
            }

            set
            {
                mlngModuloId = value;
            }
        }

        public static long AutorizaItemId
        {
            get
            {
                return mlngAutorizaItemId;
            }

            set
            {
                mlngAutorizaItemId = value;
            }
        }

        public static long AplicacionId
        {
            get
            {
                return mlngAplicacionId;
            }

            set
            {
                mlngAplicacionId = value;
            }
        }

        public static string AplicacionDes
        {
            get
            {
                return mstrAplicacionDes;
            }

            set
            {
                mstrAplicacionDes = value;
            }
        }

        public static long AutorizaId
        {
            get
            {
                return mlngAutorizaId;
            }

            set
            {
                mlngAutorizaId = value;
            }
        }

        public static long TipoUsuarioId
        {
            get
            {
                return mlngTipoUsuarioId;
            }

            set
            {
                mlngTipoUsuarioId = value;
            }
        }

        public static long UsuarioId
        {
            get
            {
                return mlngUsuarioId;
            }

            set
            {
                mlngUsuarioId = value;
            }
        }

        public static string UsuarioCod
        {
            get
            {
                return mstrUsuarioCod;
            }

            set
            {
                mstrUsuarioCod = value;
            }
        }

        public static string UsuarioDes
        {
            get
            {
                return mstrUsuarioDes;
            }

            set
            {
                mstrUsuarioDes = value;
            }
        }

        public static string UsuarioPass
        {
            get
            {
                return mstrUsuarioPass;
            }

            set
            {
                mstrUsuarioPass = value;
            }
        }

        public static string UsuarioFotoPath
        {
            get
            {
                return mstrUsuarioFotoPath;
            }

            set
            {
                mstrUsuarioFotoPath = value;
            }
        }

        public static string AppPath
        {
            get
            {
                return mstrAppPath;
            }

            set
            {
                mstrAppPath = value;
            }
        }

        public static void Init(string strDataSource, string strInitialCatalog, string strUsuarioCod, string mstrUsuarioPass)
        {
            mstrConnectString = "Data Source=" + strDataSource + "; Initial Catalog=" + strInitialCatalog +
                "; User ID=" + strUsuarioCod + "; Password=" + mstrUsuarioPass + "; Current Language=us_english";
        }

        public static bool OpenConection()
        {
            bool returnValue = false;

            try
            {
                moConnection = new SqlConnection();

                if (moConnection.State == System.Data.ConnectionState.Closed)
                {
                    moConnection = new SqlConnection(mstrConnectString);
                    moConnection.Open();

                    returnValue = true;
                }
            }

            catch (Exception exp)
            {
                throw (exp);
            }

            return returnValue;
        }

    }

    static class ConstEstado
    {
        public const long Inactivo = 2;
        public const long Activo = 1;
    }

}