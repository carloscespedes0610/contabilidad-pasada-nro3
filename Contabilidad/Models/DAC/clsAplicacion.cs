using System;
using System.Data.SqlClient;
using System.Data;
using Contabilidad.Models.VM;

namespace Contabilidad.Models.DAC
{
    public class clsAplicacion : clsBase, IDisposable
    {
        public clsAplicacionVM VM;

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
            AplicacionDes = 2,
            Grid = 3,
            GridCheck = 4,
            AplicacionCod = 5,
            GridAplicacionId = 6
        }

        public enum OrderByFilters : byte
        {
            None = 0,
            AplicacionId = 1,
            AplicacionDes = 2,
            Grid = 3,
            GridCheck = 4
        }

        public enum InsertFilters : byte
        {
            All = 0
        }

        public enum UpdateFilters : byte
        {
            All = 0
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
        public clsAplicacion()
        {
            mstrTableName = "segAplicacion";
            mstrClassName = "clsAplicacion";

            PropertyInit();
            FilterInit();
        }

        public clsAplicacion(string ConnectString) : this()
        {
            moConnection = new SqlConnection();

            mstrConnectionString = ConnectString;
        }

        public clsAplicacion(SqlConnection oConnection) : this()
        {
            moConnection = oConnection;
        }

        public clsAplicacion(SqlConnection oConnection, SelectFilters bytSelectFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
        }

        public clsAplicacion(SqlConnection oConnection, SelectFilters bytSelectFilter, WhereFilters bytWhereFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
            mintWhereFilter = bytWhereFilter;
        }

        public clsAplicacion(SqlConnection oConnection, SelectFilters bytSelectFilter, WhereFilters bytWhereFilter, OrderByFilters bytOrderByFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
            mintWhereFilter = bytWhereFilter;
            mintOrderByFilter = bytOrderByFilter;
        }

        public void PropertyInit()
        {
            VM = new clsAplicacionVM();
            VM.AplicacionId = 0;
            VM.AplicacionCod = "";
            VM.AplicacionDes = "";
            VM.AplicacionEsp = "";
            VM.ModuloId = 0;
            VM.EstadoId = 0;
        }

        protected override void SetPrimaryKey()
        {
            VM.AplicacionId = mlngId;
        }

        protected override void SelectParameter()
        {
            string strSQL = null;

            mstrStoreProcName = "segAplicacionSelect";

            switch (mintSelectFilter)
            {
                case SelectFilters.All:
                    strSQL = " SELECT  " +
                             "    segAplicacion.AplicacionId, " +
                             "    segAplicacion.AplicacionCod, " +
                             "    segAplicacion.AplicacionDes, " +
                             "    segAplicacion.AplicacionEsp, " +
                             "    segAplicacion.ModuloId, " +
                             "    segAplicacion.EstadoId " +
                             " FROM  segAplicacion ";
                    break;

                case SelectFilters.Grid:
                    strSQL = " SELECT  " +
                             "    segAplicacion.AplicacionId, " +
                             "    segAplicacion.AplicacionCod, " +
                             "    segAplicacion.AplicacionDes, " +
                             "    segAplicacion.AplicacionEsp, " +
                             "    segModulo.ModuloId,  " +
                             "    segModulo.ModuloDes, " +
                             "    parEstado.EstadoId,  " +
                             "    parEstado.EstadoDes " +
                             " FROM segAplicacion " +
                             " LEFT JOIN segModulo ON segAplicacion.ModuloId = segModulo.ModuloId " +
                             " LEFT JOIN parEstado ON segAplicacion.EstadoId = parEstado.EstadoId ";
                    break;

                case SelectFilters.ListBox:
                    strSQL = " SELECT  " +
                             "    segAplicacion.AplicacionId, " +
                             "    segAplicacion.AplicacionCod, " +
                             "    segAplicacion.AplicacionDes " +
                             " FROM segAplicacion ";

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
                    strSQL = " WHERE segAplicacion.AplicacionId = " + SysData.NumberToField(VM.AplicacionId);
                    break;

                case WhereFilters.AplicacionCod:
                    strSQL = " WHERE AplicacionCod = " + SysData.StringToField(VM.AplicacionCod);
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
                case OrderByFilters.AplicacionId:
                    strSQL = " ORDER BY segAplicacion.AplicacionId ";
                    break;

                case OrderByFilters.Grid:
                    strSQL = " ORDER BY segModulo.ModuloDes, segAplicacion.AplicacionDes ";
                    break;

                case OrderByFilters.AplicacionDes:
                    strSQL = " ORDER BY segAplicacion.AplicacionDes ";
                    break;
            }

            return strSQL;
        }

        protected override void InsertParameter()
        {
            switch (mintInsertFilter)
            {
                case InsertFilters.All:
                    mstrStoreProcName = "segAplicacionInsert";
                    moParameters = new SqlParameter[7] {
                        new SqlParameter("@InsertFilter", mintInsertFilter),
                        new SqlParameter("@Id", SqlDbType.Int),
                        new SqlParameter(clsAplicacionVM._AplicacionCod, VM.AplicacionCod),
                        new SqlParameter(clsAplicacionVM._AplicacionDes, VM.AplicacionDes),
                        new SqlParameter(clsAplicacionVM._AplicacionEsp, VM.AplicacionEsp),
                        new SqlParameter(clsAplicacionVM._ModuloId, VM.ModuloId),
                        new SqlParameter(clsAplicacionVM._EstadoId, VM.EstadoId)};
                    moParameters[1].Direction = ParameterDirection.Output;
                    break;
            }
        }

        protected override void UpdateParameter()
        {
            switch (mintUpdateFilter)
            {
                case UpdateFilters.All:
                    mstrStoreProcName = "segAplicacionUpdate";
                    moParameters = new SqlParameter[7] {
                        new SqlParameter("@UpdateFilter", mintUpdateFilter),
                        new SqlParameter(clsAplicacionVM._AplicacionId, VM.AplicacionId),
                        new SqlParameter(clsAplicacionVM._AplicacionCod, VM.AplicacionCod),
                        new SqlParameter(clsAplicacionVM._AplicacionDes, VM.AplicacionDes),
                        new SqlParameter(clsAplicacionVM._AplicacionEsp, VM.AplicacionEsp),
                        new SqlParameter(clsAplicacionVM._ModuloId, VM.ModuloId),
                        new SqlParameter(clsAplicacionVM._EstadoId, VM.EstadoId)};
                    break;
            }
        }

        protected override void DeleteParameter()
        {
            switch (mintDeleteFilter)
            {
                case DeleteFilters.All:
                    mstrStoreProcName = "segAplicacionDelete";
                    moParameters = new SqlParameter[2] {
                        new SqlParameter("@DeleteFilter", mintDeleteFilter),
                        new SqlParameter(clsAplicacionVM._AplicacionId, VM.AplicacionId)};
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
                        VM.AplicacionId = SysData.ToLong(oDataRow[clsAplicacionVM._AplicacionId]);
                        VM.AplicacionCod = SysData.ToStr(oDataRow[clsAplicacionVM._AplicacionCod]);
                        VM.AplicacionDes = SysData.ToStr(oDataRow[clsAplicacionVM._AplicacionDes]);
                        VM.AplicacionEsp = SysData.ToStr(oDataRow[clsAplicacionVM._AplicacionEsp]);
                        VM.ModuloId = SysData.ToLong(oDataRow[clsAplicacionVM._ModuloId]);
                        VM.EstadoId = SysData.ToLong(oDataRow[clsAplicacionVM._EstadoId]);
                        break;

                    case SelectFilters.ListBox:
                        VM.AplicacionId = SysData.ToLong(oDataRow[clsAplicacionVM._AplicacionId]);
                        VM.AplicacionCod = SysData.ToStr(oDataRow[clsAplicacionVM._AplicacionCod]);
                        VM.AplicacionDes = SysData.ToStr(oDataRow[clsAplicacionVM._AplicacionDes]);
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

            if (VM.AplicacionCod.Length == 0)
            {
                strMsg += "Código de la Aplicación es Requerido" + Environment.NewLine;
            }

            if (VM.AplicacionDes.Length == 0)
            {
                strMsg += "Descripción de la Aplicación es requerido" + Environment.NewLine;
            }

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