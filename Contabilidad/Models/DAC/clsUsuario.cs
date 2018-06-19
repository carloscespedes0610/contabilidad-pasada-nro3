using System;
using System.Data.SqlClient;
using System.Data;
using Contabilidad.Models.Modules;
using Contabilidad.Models.VM;

namespace Contabilidad.Models.DAC
{
    public class clsUsuario : clsBase, IDisposable
    {
        public clsUsuarioVM VM;

        //******************************************************
        //* The following enumerations will change for each
        //* data access class
        //******************************************************
        public enum SelectFilters : byte
        {
            All = 0,
            RowCount = 1,
            ListBox = 2,
            Grid = 3,
            GridCheck = 4
        }

        public enum WhereFilters : byte
        {
            None = 0,
            PrimaryKey = 1,
            UsuarioDes = 2,
            Grid = 3,
            GridCheck = 4,
            UsuarioCod = 5,
            GridUsuarioId = 6,
            TipoUsuarioId = 7
        }

        public enum OrderByFilters : byte
        {
            None = 0,
            UsuarioId = 1,
            UsuarioDes = 2,
            Grid = 3,
            GridCheck = 4
        }

        public enum InsertFilters : byte
        {
            All = 0
        }

        public enum UpdateFilters : byte
        {
            All = 0,
            ActualizarEstado = 1
        }

        public enum DeleteFilters : byte
        {
            All = 0
        }

        public enum RowCountFilters : byte
        {
            All = 0
        }

        //*********************************************************
        //* The following filters will change for each
        //* data access class
        //*********************************************************
        private SelectFilters mintSelectFilter;
        private WhereFilters mintWhereFilter;
        private OrderByFilters mintOrderByFilter;
        private InsertFilters mintInsertFilter;
        private UpdateFilters mintUpdateFilter;
        private DeleteFilters mintDeleteFilter;
        private RowCountFilters mintRowCountFilter;

        public SelectFilters SelectFilter
        {
            get
            {
                return mintSelectFilter;
            }

            set
            {
                mintSelectFilter = value;
            }
        }

        public WhereFilters WhereFilter
        {
            get
            {
                return mintWhereFilter;
            }

            set
            {
                mintWhereFilter = value;
            }
        }

        public OrderByFilters OrderByFilter
        {
            get
            {
                return mintOrderByFilter;
            }

            set
            {
                mintOrderByFilter = value;
            }
        }

        public InsertFilters InsertFilter
        {
            get
            {
                return mintInsertFilter;
            }

            set
            {
                mintInsertFilter = value;
            }
        }

        public UpdateFilters UpdateFilter
        {
            get
            {
                return mintUpdateFilter;
            }

            set
            {
                mintUpdateFilter = value;
            }
        }

        public DeleteFilters DeleteFilter
        {
            get
            {
                return mintDeleteFilter;
            }

            set
            {
                mintDeleteFilter = value;
            }
        }

        public RowCountFilters RowCountFilter
        {
            get
            {
                return mintRowCountFilter;
            }

            set
            {
                mintRowCountFilter = value;
            }
        }

        //************************************************************
        //* Method Name  : New()
        //* Syntax       : Constructor
        //* Parameters   : None
        //*
        //* Description  : This event is called when the object is created.
        //* It can be used to initialize private data variables.
        //*
        //************************************************************
        public clsUsuario()
        {
            mstrTableName = "segUsuario";
            mstrClassName = "clsUsuario";

            PropertyInit();
            FilterInit();
        }

        public clsUsuario(string ConnectString) : this()
        {
            moConnection = new SqlConnection();

            mstrConnectionString = ConnectString;
        }

        public clsUsuario(SqlConnection oConnection) : this()
        {
            moConnection = oConnection;
        }

        public clsUsuario(SqlConnection oConnection, SelectFilters bytSelectFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
        }

        public clsUsuario(SqlConnection oConnection, SelectFilters bytSelectFilter, WhereFilters bytWhereFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
            mintWhereFilter = bytWhereFilter;
        }

        public clsUsuario(SqlConnection oConnection, SelectFilters bytSelectFilter, WhereFilters bytWhereFilter, OrderByFilters bytOrderByFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
            mintWhereFilter = bytWhereFilter;
            mintOrderByFilter = bytOrderByFilter;
        }

        public void PropertyInit()
        {
            VM = new clsUsuarioVM();
            VM.UsuarioId = 0;
            VM.UsuarioCod = "";
            VM.UsuarioDes = "";
            VM.TipoUsuarioId = 0;
            VM.UsuarioDocPath = "";
            VM.UsuarioFotoPath = "";
            VM.UsuarioMaxSes = 0;
            VM.EstadoId = 0;
        }

        protected override void SetPrimaryKey()
        {
            VM.UsuarioId = mlngId;
        }

        protected override void SelectParameter()
        {
            string strSQL = null;

            mstrStoreProcName = "segUsuarioSelect";

            switch (mintSelectFilter)
            {
                case SelectFilters.All:
                    strSQL = " SELECT  " +
                             "    segUsuario.UsuarioId, " +
                             "    segUsuario.UsuarioCod, " +
                             "    segUsuario.UsuarioDes, " +
                             "    segUsuario.TipoUsuarioId, " +
                             "    segUsuario.UsuarioDocPath, " +
                             "    segUsuario.UsuarioFotoPath, " +
                             "    segUsuario.UsuarioMaxSes, " +
                             "    segUsuario.EstadoId " +
                             " FROM  segUsuario ";
                    break;

                case SelectFilters.ListBox:
                    strSQL = " SELECT  " +
                             "    segUsuario.UsuarioId, " +
                             "    segUsuario.UsuarioCod, " +
                             "    segUsuario.UsuarioDes " +
                             " FROM  segUsuario ";
                    break;

                case SelectFilters.Grid:
                    strSQL = " SELECT  " +
                             "    segUsuario.UsuarioId, " +
                             "    segUsuario.UsuarioCod, " +
                             "    segUsuario.UsuarioDes, " +
                             "    segUsuario.TipoUsuarioId, " +
                             "    segTipoUsuario.TipoUsuarioDes, " +
                             "    segUsuario.UsuarioDocPath, " +
                             "    segUsuario.UsuarioFotoPath, " +
                             "    segUsuario.UsuarioMaxSes, " +
                             "    segUsuario.EstadoId, " +
                             "    parEstado.EstadoDes" +
                             " FROM  segUsuario" +
                             " LEFT JOIN segTipoUsuario	ON segUsuario.TipoUsuarioId = segTipoUsuario.TipoUsuarioId " +
                             " LEFT JOIN parEstado ON segUsuario.EstadoId = parEstado.EstadoId ";

                    break;

                case SelectFilters.GridCheck:
                    break;
            }

            strSQL += WhereFilterGet() + OrderByFilterGet();

            Array.Resize(ref moParameters, 1);
            moParameters[0] = new SqlParameter("@SQL", strSQL);
        }

        private string WhereFilterGet()
        {
            string strSQL = null;

            switch (mintWhereFilter)
            {
                case WhereFilters.PrimaryKey:
                    strSQL = " WHERE UsuarioId = " + SysData.NumberToField(VM.UsuarioId);
                    break;

                case WhereFilters.TipoUsuarioId:
                    strSQL = " WHERE segUsuario.TipoUsuarioId = " + SysData.NumberToField(VM.TipoUsuarioId);
                    break;

                case WhereFilters.UsuarioCod:
                    strSQL = " WHERE  segUsuario.UsuarioCod = " + SysData.StringToField(VM.UsuarioCod);
                    break;

                case WhereFilters.Grid:
                    break;
            }

            return strSQL;
        }

        private string OrderByFilterGet()
        {
            string strSQL = null;

            switch (mintOrderByFilter)
            {
                case OrderByFilters.UsuarioId:
                    strSQL = " ORDER BY segUsuario.UsuarioId ";
                    break;

                case OrderByFilters.Grid:
                    strSQL = " ORDER BY segTipoUsuario.TipoUsuarioDes, segUsuario.UsuarioDes ";
                    break;

                case OrderByFilters.UsuarioDes:
                    strSQL = " ORDER BY segUsuario.UsuarioDes ";
                    break;
            }

            return strSQL;
        }

        protected override void InsertParameter()
        {
            switch (mintInsertFilter)
            {
                case InsertFilters.All:
                    mstrStoreProcName = "segUsuarioInsert";
                    moParameters = new SqlParameter[9] {
                        new SqlParameter("@InsertFilter", mintInsertFilter),
                        new SqlParameter("@Id", SqlDbType.Int),
                        new SqlParameter(clsUsuarioVM._UsuarioCod, VM.UsuarioCod),
                        new SqlParameter(clsUsuarioVM._UsuarioDes, VM.UsuarioDes),
                        new SqlParameter(clsUsuarioVM._TipoUsuarioId, VM.TipoUsuarioId),
                        new SqlParameter(clsUsuarioVM._UsuarioDocPath, VM.UsuarioDocPath),
                        new SqlParameter(clsUsuarioVM._UsuarioFotoPath, VM.UsuarioFotoPath),
                        new SqlParameter(clsUsuarioVM._UsuarioMaxSes, VM.UsuarioMaxSes),
                        new SqlParameter(clsUsuarioVM._EstadoId, VM.EstadoId)};
                    moParameters[1].Direction = ParameterDirection.Output;
                    break;
            }
        }

        protected override void UpdateParameter()
        {
            switch (mintUpdateFilter)
            {
                case UpdateFilters.All:
                    mstrStoreProcName = "segUsuarioUpdate";
                    moParameters = new SqlParameter[9] {
                        new SqlParameter("@UpdateFilter", mintUpdateFilter),
                        new SqlParameter(clsUsuarioVM._UsuarioId, VM.UsuarioId),
                        new SqlParameter(clsUsuarioVM._UsuarioCod, VM.UsuarioCod),
                        new SqlParameter(clsUsuarioVM._UsuarioDes, VM.UsuarioDes),
                        new SqlParameter(clsUsuarioVM._TipoUsuarioId, VM.TipoUsuarioId),
                        new SqlParameter(clsUsuarioVM._UsuarioDocPath, VM.UsuarioDocPath),
                        new SqlParameter(clsUsuarioVM._UsuarioFotoPath, VM.UsuarioFotoPath),
                        new SqlParameter(clsUsuarioVM._UsuarioMaxSes, VM.UsuarioMaxSes),
                        new SqlParameter(clsUsuarioVM._EstadoId, VM.EstadoId)};
                    break;

                case UpdateFilters.ActualizarEstado:
                    mstrStoreProcName = "segUsuarioUpdate";
                    moParameters = new SqlParameter[9] {
                        new SqlParameter("@UpdateFilter", mintUpdateFilter),
                        new SqlParameter(clsUsuarioVM._UsuarioId, VM.UsuarioId),
                        new SqlParameter(clsUsuarioVM._UsuarioCod, VM.UsuarioCod),
                        new SqlParameter(clsUsuarioVM._UsuarioDes, VM.UsuarioDes),
                        new SqlParameter(clsUsuarioVM._TipoUsuarioId, VM.TipoUsuarioId),
                        new SqlParameter(clsUsuarioVM._UsuarioDocPath, VM.UsuarioDocPath),
                        new SqlParameter(clsUsuarioVM._UsuarioFotoPath, VM.UsuarioFotoPath),
                        new SqlParameter(clsUsuarioVM._UsuarioMaxSes, VM.UsuarioMaxSes),
                        new SqlParameter(clsUsuarioVM._EstadoId, VM.EstadoId)};
                    break;
            }
        }

        protected override void DeleteParameter()
        {
            switch (mintDeleteFilter)
            {
                case DeleteFilters.All:
                    mstrStoreProcName = "segUsuarioDelete";
                    moParameters = new SqlParameter[2] {
                        new SqlParameter("@DeleteFilter", mintDeleteFilter),
                        new SqlParameter(clsUsuarioVM._UsuarioId, VM.UsuarioId)};
                    break;
            }
        }

        protected override void Retrieve(DataRow oDataRow)
        {
            try
            {
                PropertyInit();

                switch (mintSelectFilter)
                {
                    case SelectFilters.All:
                        VM.UsuarioId = SysData.ToLong(oDataRow[clsUsuarioVM._UsuarioId]);
                        VM.UsuarioCod = SysData.ToStr(oDataRow[clsUsuarioVM._UsuarioCod]);
                        VM.UsuarioDes = SysData.ToStr(oDataRow[clsUsuarioVM._UsuarioDes]);
                        VM.TipoUsuarioId = SysData.ToLong(oDataRow[clsUsuarioVM._TipoUsuarioId]);
                        VM.UsuarioDocPath = SysData.ToStr(oDataRow[clsUsuarioVM._UsuarioDocPath]);
                        VM.UsuarioFotoPath = SysData.ToStr(oDataRow[clsUsuarioVM._UsuarioFotoPath]);
                        VM.UsuarioMaxSes = SysData.ToLong(oDataRow[clsUsuarioVM._UsuarioMaxSes]);
                        VM.EstadoId = SysData.ToLong(oDataRow[clsUsuarioVM._EstadoId]);
                        break;

                    case SelectFilters.ListBox:
                        VM.UsuarioId = SysData.ToLong(oDataRow[clsUsuarioVM._UsuarioId]);
                        VM.UsuarioCod = SysData.ToStr(oDataRow[clsUsuarioVM._UsuarioCod]);
                        VM.UsuarioDes = SysData.ToStr(oDataRow[clsUsuarioVM._UsuarioDes]);
                        break;
                }
            }

            catch (Exception exp)
            {
                throw (exp);
            }
        }

        public override bool Validate()
        {
            bool returnValue = false;
            string strMsg = string.Empty;

            if (VM.TipoUsuarioId == 0) { strMsg += "Tipo De Usuario es Requerido" + Environment.NewLine; }

            if (VM.UsuarioCod.Length == 0) { strMsg += "Código es Requerido" + Environment.NewLine; }

            if (VM.UsuarioDes.Length == 0) { strMsg += "Usuario es Requerido" + Environment.NewLine; }

            if (strMsg.Trim() != string.Empty)
            {
                returnValue = false;
                throw (new Exception(strMsg));
            }
            else
            {
                returnValue = true;
            }

            return returnValue;
        }

        public bool FindByPK()
        {
            bool returnValue = false;
            returnValue = false;

            try
            {
                mintSelectFilter = SelectFilters.All;
                mintWhereFilter = WhereFilters.PrimaryKey;
                mintOrderByFilter = OrderByFilters.None;

                if (Open())
                {
                    if (Read())
                    {
                        returnValue = true;
                    }
                }
            }

            catch (Exception exp)
            {
                throw (exp);
            }

            return returnValue;
        }

        public void FilterInit()
        {
            mintWhereFilter = 0;
            mintOrderByFilter = 0;
            mintSelectFilter = 0;
            mintInsertFilter = 0;
            mintUpdateFilter = 0;
            mintDeleteFilter = 0;
            mintRowCountFilter = 0;
        }

        virtual public void Dispose()
        {
            //Call CloseConection()
        }

    }
}