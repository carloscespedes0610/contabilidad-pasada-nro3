using System;
using System.Data.SqlClient;
using System.Data;
using Contabilidad.Models.VM;

namespace Contabilidad.Models.DAC
{
    public class clsCapitulo : clsBase, IDisposable
    {
        public clsCapituloVM VM;

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
            CapituloDes = 2,
            Grid = 3,
            GridCheck = 4,
            CapituloCod = 5,
            Grid_EstadoId = 6
        }

        public enum OrderByFilters : byte
        {
            None = 0,
            CapituloId = 1,
            CapituloDes = 2,
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
        public clsCapitulo()
        {
            mstrTableName = "ctbCapitulo";
            mstrClassName = "clsCapitulo";

            PropertyInit();
            FilterInit();
        }

        public clsCapitulo(string ConnectString) : this()
        {
            moConnection = new SqlConnection();

            mstrConnectionString = ConnectString;
        }

        public clsCapitulo(SqlConnection oConnection) : this()
        {
            moConnection = oConnection;
        }

        public clsCapitulo(SqlConnection oConnection, SelectFilters bytSelectFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
        }

        public clsCapitulo(SqlConnection oConnection, SelectFilters bytSelectFilter, WhereFilters bytWhereFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
            mintWhereFilter = bytWhereFilter;
        }

        public clsCapitulo(SqlConnection oConnection, SelectFilters bytSelectFilter, WhereFilters bytWhereFilter, OrderByFilters bytOrderByFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
            mintWhereFilter = bytWhereFilter;
            mintOrderByFilter = bytOrderByFilter;
        }

        public void PropertyInit()
        {
            VM = new clsCapituloVM();

            VM.CapituloId = 0;
            VM.TipoCapituloId = 0;
            VM.Orden = 0;
            VM.CapituloCod = "";
            VM.CapituloDes = "";
            VM.EstadoId = 0;
        }

        protected override void SetPrimaryKey()
        {
            VM.CapituloId = mlngId;
        }

        protected override void SelectParameter()
        {
            string strSQL = null;

            mstrStoreProcName = "ctbCapituloSelect";

            switch (mintSelectFilter)
            {
                case SelectFilters.All:
                    strSQL = " SELECT  " +
                           "    ctbCapitulo.CapituloId, " +
                           "    ctbCapitulo.TipoCapituloId, " +
                           "    ctbCapitulo.Orden, " +
                           "    ctbCapitulo.CapituloCod, " +
                           "    ctbCapitulo.CapituloDes, " +
                           "    ctbCapitulo.EstadoId " +
                           " FROM ctbCapitulo ";
                    break;

                case SelectFilters.ListBox:
                    strSQL = " SELECT  " +
                           "    ctbCapitulo.CapituloId, " +
                           "    ctbCapitulo.CapituloCod, " +
                           "    ctbCapitulo.CapituloDes " +
                           " FROM ctbCapitulo ";
                    break;

                case SelectFilters.Grid:
                    strSQL = " SELECT  " +
                            "    ctbCapitulo.CapituloId, " +
                            "    ctbCapitulo.TipoCapituloId, " +
                            "    ctbCapitulo.Orden, " +
                            "    ctbCapitulo.CapituloCod, " +
                            "    ctbCapitulo.CapituloDes, " +
                            "    parEstado.EstadoId, " +
                            "    parEstado.EstadoDes  " +
                            " FROM ctbCenCos ";
                    break;

                case SelectFilters.GridCheck:
                    break;
            }

            strSQL += WhereFilterGet() + OrderByFilterGet();

            Array.Resize(ref moParameters, 1);
            moParameters[0] = new SqlParameter("SQL", strSQL);
        }

        private string WhereFilterGet()
        {
            string strSQL = null;

            switch (mintWhereFilter)
            {
                case WhereFilters.PrimaryKey:
                    strSQL = " WHERE CapituloId = " + SysData.NumberToField(VM.CapituloId);
                    break;

                case WhereFilters.CapituloDes:
                    break;

                case WhereFilters.Grid:
                    strSQL = " LEFT JOIN parEstado ON ctbCapitulo.EstadoId = parEstado.EstadoId ";
                    break;

                case WhereFilters.CapituloCod:
                    strSQL = " WHERE CapituloCod = " + SysData.StringToField(VM.CapituloCod);
                    break;

                case WhereFilters.GridCheck:
                    break;

                case WhereFilters.Grid_EstadoId:
                    strSQL = " LEFT JOIN parEstado ON ctbCapitulo.EstadoId = parEstado.EstadoId " +
                             " WHERE ctbCapitulo.EstadoId = " + SysData.NumberToField(VM.EstadoId);
                    break;
            }

            return strSQL;
        }

        private string OrderByFilterGet()
        {
            string strSQL = null;

            switch (mintOrderByFilter)
            {
                case OrderByFilters.CapituloId:
                    strSQL = " ORDER BY ctbCapitulo.CapituloId ";
                    break;

                case OrderByFilters.CapituloDes:
                    strSQL = " ORDER BY ctbCapitulo.CapituloDes ";
                    break;

                case OrderByFilters.Grid:
                    strSQL = " ORDER BY Orden ";
                    break;

                case OrderByFilters.GridCheck:
                    break;
            }

            return strSQL;
        }

        protected override void InsertParameter()
        {
            switch (mintInsertFilter)
            {
                case InsertFilters.All:
                    mstrStoreProcName = "ctbCapituloInsert";
                    moParameters = new SqlParameter[7] {
                        new SqlParameter("InsertFilter", mintInsertFilter),
                        new SqlParameter("Id", SqlDbType.Int),
                        new SqlParameter(clsCapituloVM._TipoCapituloId, VM.TipoCapituloId),
                        new SqlParameter(clsCapituloVM._Orden, VM.Orden),
                        new SqlParameter(clsCapituloVM._CapituloCod, VM.CapituloCod),
                        new SqlParameter(clsCapituloVM._CapituloDes, VM.CapituloDes),
                        new SqlParameter(clsCapituloVM._EstadoId, VM.EstadoId)};
                    moParameters[1].Direction = ParameterDirection.Output;
                    break;
            }
        }

        protected override void UpdateParameter()
        {
            switch (mintUpdateFilter)
            {
                case UpdateFilters.All:
                    mstrStoreProcName = "ctbCapituloUpdate";
                    moParameters = new SqlParameter[7] {
                        new SqlParameter("UpdateFilter", mintUpdateFilter),
                        new SqlParameter(clsCapituloVM._CapituloId, VM.CapituloId),
                        new SqlParameter(clsCapituloVM._TipoCapituloId, VM.TipoCapituloId),
                        new SqlParameter(clsCapituloVM._Orden, VM.Orden),
                        new SqlParameter(clsCapituloVM._CapituloCod, VM.CapituloCod),
                        new SqlParameter(clsCapituloVM._CapituloDes, VM.CapituloDes),
                        new SqlParameter(clsCapituloVM._EstadoId, VM.EstadoId)};
                    break;
            }
        }

        protected override void DeleteParameter()
        {
            switch (mintDeleteFilter)
            {
                case DeleteFilters.All:
                    mstrStoreProcName = "ctbCapituloDelete";
                    moParameters = new SqlParameter[2] {
                        new SqlParameter("DeleteFilter", mintDeleteFilter),
                        new SqlParameter(clsCapituloVM._CapituloId, VM.CapituloId)};
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
                        VM.CapituloId = SysData.ToLong(oDataRow[clsCapituloVM._CapituloId]);
                        VM.TipoCapituloId = SysData.ToLong(oDataRow[clsCapituloVM._TipoCapituloId]);
                        VM.Orden = SysData.ToLong(oDataRow[clsCapituloVM._Orden]);
                        VM.CapituloCod = SysData.ToStr(oDataRow[clsCapituloVM._CapituloCod]);
                        VM.CapituloDes = SysData.ToStr(oDataRow[clsCapituloVM._CapituloDes]);
                        VM.EstadoId = SysData.ToLong(oDataRow[clsCapituloVM._EstadoId]);
                        break;

                    case SelectFilters.ListBox:
                        VM.CapituloId = SysData.ToLong(oDataRow[clsCapituloVM._CapituloId]);
                        VM.CapituloCod = SysData.ToStr(oDataRow[clsCapituloVM._CapituloCod]);
                        VM.CapituloDes = SysData.ToStr(oDataRow[clsCapituloVM._CapituloDes]);
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

            if (VM.CapituloCod.Length == 0)
            {
                strMsg += "Código es Requerido" + Environment.NewLine;
            }

            if (VM.CapituloDes.Length == 0)
            {
                strMsg += "Descipción del Tipo Usuario es Requerido" + Environment.NewLine;
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