using System;
using System.Data.SqlClient;
using System.Data;
using Contabilidad.Models.VM;

namespace Contabilidad.Models.DAC
{
    public class clsSucursal : clsBase, IDisposable
    {
        public clsSucursalVM VM;

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
            SucursalDes = 2,
            Grid = 3,
            GridCheck = 4,
            SucursalCod = 5,
        }

        public enum OrderByFilters : byte
        {
            None = 0,
            SucursalId = 1,
            SucursalDes = 2,
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
        public clsSucursal()
        {
            mstrTableName = "ctbSucursal";
            mstrClassName = "clsSucursal";

            PropertyInit();
            FilterInit();
        }

        public clsSucursal(string ConnectString) : this()
        {
            moConnection = new SqlConnection();

            mstrConnectionString = ConnectString;
        }

        public clsSucursal(SqlConnection oConnection) : this()
        {
            moConnection = oConnection;
        }

        public clsSucursal(SqlConnection oConnection, SelectFilters bytSelectFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
        }

        public clsSucursal(SqlConnection oConnection, SelectFilters bytSelectFilter, WhereFilters bytWhereFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
            mintWhereFilter = bytWhereFilter;
        }

        public clsSucursal(SqlConnection oConnection, SelectFilters bytSelectFilter, WhereFilters bytWhereFilter, OrderByFilters bytOrderByFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
            mintWhereFilter = bytWhereFilter;
            mintOrderByFilter = bytOrderByFilter;
        }

        public void PropertyInit()
        {
            VM = new clsSucursalVM();
            VM.SucursalId = 0;
            VM.SucursalCod = "";
            VM.SucursalDes = "";
            VM.SucursalEsp = "";
            VM.EstadoId = 0;
        }

        protected override void SetPrimaryKey()
        {
            VM.SucursalId = mlngId;
        }

        protected override void SelectParameter()
        {
            string strSQL = null;

            mstrStoreProcName = "ctbSucursalSelect";

            switch (mintSelectFilter)
            {
                case SelectFilters.All:
                    strSQL = " SELECT  " +
                           "    ctbSucursal.SucursalId, " +
                           "    ctbSucursal.SucursalCod, " +
                           "    ctbSucursal.SucursalDes, " +
                           "    ctbSucursal.SucursalEsp, " +
                           "    ctbSucursal.EstadoId " +
                           " FROM ctbSucursal ";
                    break;

                case SelectFilters.RowCount:
                    break;

                case SelectFilters.ListBox:
                    strSQL = " SELECT  " +
                           "    ctbSucursal.SucursalId, " +
                           "    ctbSucursal.SucursalCod, " +
                           "    ctbSucursal.SucursalDes " +
                           " FROM ctbSucursal ";
                    break;

                case SelectFilters.Grid:
                    strSQL = " SELECT  " +
                           "    ctbSucursal.SucursalId, " +
                           "    ctbSucursal.SucursalCod, " +
                           "    ctbSucursal.SucursalDes, " +
                           "    ctbSucursal.SucursalEsp, " +
                           "    ctbSucursal.EstadoId, " +
                           "    parEstado.EstadoDes " +
                           " FROM ctbSucursal " +
                           "    LEFT JOIN	parEstado ON ctbSucursal.EstadoId = parEstado.EstadoId   ";
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
                case WhereFilters.None:
                    break;

                case WhereFilters.PrimaryKey:
                    strSQL = " WHERE ctbSucursal.SucursalId = " + SysData.NumberToField(VM.SucursalId);
                    break;

                case WhereFilters.SucursalDes:
                    strSQL = " WHERE ctbSucursal.SucursalDes = " + SysData.StringToField(VM.SucursalDes);
                    break;

                case WhereFilters.Grid:
                   
                    break;

                case WhereFilters.SucursalCod:
                    strSQL = " WHERE ctbSucursal.SucursalCod = " + SysData.StringToField(VM.SucursalCod);
                    break;

                case WhereFilters.GridCheck:
                    break;
            }

            return strSQL;
        }


        private string OrderByFilterGet()
        {
            string strSQL = null;

            switch (mintOrderByFilter)
            {
                case OrderByFilters.None:
                    break;
                case OrderByFilters.SucursalId:
                    strSQL = " ORDER BY ctbSucursal.SucursalId ";
                    break;
                case OrderByFilters.SucursalDes:
                    strSQL = " ORDER BY ctbSucursal.SucursalDes ";
                    break;
                case OrderByFilters.Grid:
                    strSQL = " ORDER BY ctbSucursal.SucursalDes ";
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
                    mstrStoreProcName = "ctbSucursalInsert";
                    moParameters = new SqlParameter[6] {
                        new SqlParameter("@InsertFilter", mintInsertFilter),
                        new SqlParameter("@Id", SqlDbType.Int),
                        new SqlParameter(clsSucursalVM._SucursalCod, VM.SucursalCod),
                        new SqlParameter(clsSucursalVM._SucursalDes, VM.SucursalDes),
                        new SqlParameter(clsSucursalVM._SucursalEsp, VM.SucursalEsp),
                        new SqlParameter(clsSucursalVM._EstadoId, VM.EstadoId)};
                    moParameters[1].Direction = ParameterDirection.Output;
                    break;
            }
        }

        protected override void UpdateParameter()
        {
            switch (mintUpdateFilter)
            {
                case UpdateFilters.All:
                    mstrStoreProcName = "ctbSucursalUpdate";
                    moParameters = new SqlParameter[6] {
                        new SqlParameter("@UpdateFilter", mintUpdateFilter),
                        new SqlParameter(clsSucursalVM._SucursalId, VM.SucursalId),
                        new SqlParameter(clsSucursalVM._SucursalCod, VM.SucursalCod),
                        new SqlParameter(clsSucursalVM._SucursalDes, VM.SucursalDes),
                        new SqlParameter(clsSucursalVM._SucursalEsp, VM.SucursalEsp),
                        new SqlParameter(clsSucursalVM._EstadoId, VM.EstadoId)};
                    break;
            }
        }

        protected override void DeleteParameter()
        {
            switch (mintDeleteFilter)
            {
                case DeleteFilters.All:
                    mstrStoreProcName = "ctbSucursalDelete";
                    moParameters = new SqlParameter[2] {
                        new SqlParameter("@DeleteFilter", mintDeleteFilter),
                        new SqlParameter(clsSucursalVM._SucursalId, VM.SucursalId)};
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
                        VM.SucursalId = SysData.ToLong(oDataRow[clsSucursalVM._SucursalId]);
                        VM.SucursalCod = SysData.ToStr(oDataRow[clsSucursalVM._SucursalCod]);
                        VM.SucursalDes = SysData.ToStr(oDataRow[clsSucursalVM._SucursalDes]);
                        VM.SucursalEsp = SysData.ToStr(oDataRow[clsSucursalVM._SucursalEsp]);
                        VM.EstadoId = SysData.ToLong(oDataRow[clsSucursalVM._EstadoId]);
                        break;

                    case SelectFilters.ListBox:
                        VM.SucursalId = SysData.ToLong(oDataRow[clsSucursalVM._SucursalId]);
                        VM.SucursalCod = SysData.ToStr(oDataRow[clsSucursalVM._SucursalCod]);
                        VM.SucursalDes = SysData.ToStr(oDataRow[clsSucursalVM._SucursalDes]);
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

            if (VM.SucursalCod.Length == 0)
            {
                strMsg += "Código es Requerido" + Environment.NewLine;
            }

            if (VM.SucursalDes.Length == 0)
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