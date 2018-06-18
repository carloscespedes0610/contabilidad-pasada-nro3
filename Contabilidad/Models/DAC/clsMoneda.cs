using System;
using System.Data.SqlClient;
using System.Data;
using Contabilidad.Models.VM;

namespace Contabilidad.Models.DAC
{
    public class clsMoneda : clsBase, IDisposable
    {
        public clsMonedaVM VM;

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
            MonedaDes = 2,
            Grid = 3,
            GridCheck = 4,
            MonedaCod = 5,
        }

        public enum OrderByFilters : byte
        {
            None = 0,
            MonedaId = 1,
            MonedaDes = 2,
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
        public clsMoneda()
        {
            mstrTableName = "parMoneda";
            mstrClassName = "clsMoneda";

            PropertyInit();
            FilterInit();
        }

        public clsMoneda(string ConnectString) : this()
        {
            moConnection = new SqlConnection();

            mstrConnectionString = ConnectString;
        }

        public clsMoneda(SqlConnection oConnection) : this()
        {
            moConnection = oConnection;
        }

        public clsMoneda(SqlConnection oConnection, SelectFilters bytSelectFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
        }

        public clsMoneda(SqlConnection oConnection, SelectFilters bytSelectFilter, WhereFilters bytWhereFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
            mintWhereFilter = bytWhereFilter;
        }

        public clsMoneda(SqlConnection oConnection, SelectFilters bytSelectFilter, WhereFilters bytWhereFilter, OrderByFilters bytOrderByFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
            mintWhereFilter = bytWhereFilter;
            mintOrderByFilter = bytOrderByFilter;
        }

        public void PropertyInit()
        {
            VM = new clsMonedaVM();
            VM.MonedaId = 0;
            VM.MonedaCod = "";
            VM.MonedaDes = "";
        }

        protected override void SetPrimaryKey()
        {
            VM.MonedaId = mlngId;
        }

        protected override void SelectParameter()
        {
            string strSQL = null;

            mstrStoreProcName = "parMonedaSelect";

            switch (mintSelectFilter)
            {

                case SelectFilters.All:
                    strSQL = " SELECT  " +
                            "    parMoneda.MonedaId, " +
                            "    parMoneda.MonedaCod, " +
                            "    parMoneda.MonedaDes " +
                            " FROM parMoneda ";
                    break;

                case SelectFilters.RowCount:
                    break;

                case SelectFilters.ListBox:
                    strSQL = " SELECT  " +
                           "    parMoneda.MonedaId, " +
                           "    parMoneda.MonedaCod, " +
                           "    parMoneda.MonedaDes " +
                           " FROM parMoneda ";
                    break;

                case SelectFilters.Grid:
                    strSQL = " SELECT  " +
                          "    parMoneda.MonedaId, " +
                          "    parMoneda.MonedaCod, " +
                          "    parMoneda.MonedaDes " +
                          " FROM parMoneda ";
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
                case WhereFilters.None:
                    break;

                case WhereFilters.PrimaryKey:
                    strSQL = " WHERE parMoneda.MonedaId = " + SysData.NumberToField(VM.MonedaId);
                    break;

                case WhereFilters.MonedaDes:
                    strSQL = " WHERE parMoneda.MonedaDes  = " + SysData.NumberToField(VM.MonedaDes);
                    break;

                case WhereFilters.Grid:
                    break;

                case WhereFilters.MonedaCod:
                    strSQL = " WHERE parMoneda.MonedaCod = " + SysData.NumberToField(VM.MonedaCod);
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
                case OrderByFilters.Grid:
                    strSQL = " ORDER BY parMoneda.MonedaDes ";
                    break;

                case OrderByFilters.GridCheck:
                    break;

                case OrderByFilters.MonedaDes:
                    strSQL = " ORDER BY parMoneda.MonedaDes  ";
                    break;

                case OrderByFilters.MonedaId:
                    strSQL = " ORDER BY parMoneda.MonedaId  ";
                    break;
            }

            return strSQL;
        }


        protected override void InsertParameter()
        {
            switch (mintInsertFilter)
            {
                case InsertFilters.All:
                    mstrStoreProcName = "parMonedaInsert";
                    moParameters = new SqlParameter[4] {
                        new SqlParameter("@InsertFilter", mintInsertFilter),
                        new SqlParameter("@Id", SqlDbType.Int),
                        new SqlParameter(clsMonedaVM._MonedaCod, VM.MonedaCod),
                        new SqlParameter(clsMonedaVM._MonedaDes, VM.MonedaDes)};
                    moParameters[1].Direction = ParameterDirection.Output;
                    break;
            }
        }

        protected override void UpdateParameter()
        {
            switch (mintUpdateFilter)
            {
                case UpdateFilters.All:
                    mstrStoreProcName = "parMonedaUpdate";
                    moParameters = new SqlParameter[4] {
                        new SqlParameter("@UpdateFilter", mintUpdateFilter),
                        new SqlParameter(clsMonedaVM._MonedaId, VM.MonedaId),
                        new SqlParameter(clsMonedaVM._MonedaCod, VM.MonedaCod),
                        new SqlParameter(clsMonedaVM._MonedaDes, VM.MonedaDes)};
                    break;
            }
        }

        protected override void DeleteParameter()
        {
            switch (mintDeleteFilter)
            {
                case DeleteFilters.All:
                    mstrStoreProcName = "parMonedaDelete";
                    moParameters = new SqlParameter[2] {
                        new SqlParameter("@DeleteFilter", mintDeleteFilter),
                        new SqlParameter(clsMonedaVM._MonedaId, VM.MonedaId)};
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
                        VM.MonedaId = SysData.ToLong(oDataRow[clsMonedaVM._MonedaId]);
                        VM.MonedaCod = SysData.ToStr(oDataRow[clsMonedaVM._MonedaCod]);
                        VM.MonedaDes = SysData.ToStr(oDataRow[clsMonedaVM._MonedaDes]);
                        break;

                    case SelectFilters.ListBox:
                        VM.MonedaId = SysData.ToLong(oDataRow[clsMonedaVM._MonedaId]);
                        VM.MonedaCod = SysData.ToStr(oDataRow[clsMonedaVM._MonedaCod]);
                        VM.MonedaDes = SysData.ToStr(oDataRow[clsMonedaVM._MonedaDes]);
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

            if (VM.MonedaCod.Length == 0)
            {
                strMsg += "Código es Requerido" + Environment.NewLine;
            }

            if (VM.MonedaDes.Length == 0)
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