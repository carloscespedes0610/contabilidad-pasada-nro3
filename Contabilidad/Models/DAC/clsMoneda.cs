using System;
using System.Data.SqlClient;
using System.Data;

namespace Contabilidad.Models.DAC
{
    public class clsMoneda : clsBase, IDisposable
    {
        private long mlngMonedaId;
        private string mstrMonedaCod;
        private string mstrMonedaDes;

        //******************************************************
        // Private Data To Match the Table Definition
        //******************************************************
        public long MonedaId
        {
            get
            {
                return mlngMonedaId;
            }

            set
            {
                mlngMonedaId = value;
            }
        }

        public string MonedaCod
        {
            get
            {
                return mstrMonedaCod;
            }

            set
            {
                mstrMonedaCod = value;
            }
        }

        public string MonedaDes
        {
            get
            {
                return mstrMonedaDes;
            }

            set
            {
                mstrMonedaDes = value;
            }
        }

        public long Id
        {
            get
            {
                return mlngId;
            }

            set
            {
                mlngId = value;
            }
        }

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
            mlngMonedaId = 0;
            mstrMonedaCod = "";
            mstrMonedaDes = "";
        }

        protected override void SetPrimaryKey()
        {
            mlngMonedaId = mlngId;
        }

        protected override void SelectParameter()
        {
            Array.Resize(ref moParameters, 3);
            moParameters[0] = new SqlParameter("@SelectFilter", mintSelectFilter);
            moParameters[1] = new SqlParameter("@WhereFilter", mintWhereFilter);
            moParameters[2] = new SqlParameter("@OrderByFilter", mintOrderByFilter);

            switch (mintSelectFilter)
            {
                case SelectFilters.All:
                    mstrStoreProcName = "parMonedaSelect";
                    break;

                case SelectFilters.RowCount:
                    mstrStoreProcName = "parMonedaSelect";
                    break;

                case SelectFilters.ListBox:
                    mstrStoreProcName = "parMonedaSelect";
                    break;

                case SelectFilters.Grid:
                    mstrStoreProcName = "parMonedaSelect";
                    break;

                case SelectFilters.GridCheck:
                    break;
            }

            WhereParameter();

            //Call OrderByParameter()
        }

        private void WhereParameter()
        {
            switch (mintWhereFilter)
            {
                case WhereFilters.None:
                    Array.Resize(ref moParameters, moParameters.Length + 1);
                    moParameters[3] = new SqlParameter("@MonedaId", Convert.ToInt32(0));
                    break;

                case WhereFilters.PrimaryKey:
                    Array.Resize(ref moParameters, moParameters.Length + 1);
                    moParameters[3] = new SqlParameter("@MonedaId", mlngMonedaId);
                    break;

                case WhereFilters.MonedaDes:
                    break;
                //strSQL = " WHERE  parMoneda.MonedaDes = " & StringToField(mstrMonedaDes)

                case WhereFilters.Grid:
                    Array.Resize(ref moParameters, moParameters.Length + 1);
                    moParameters[3] = new SqlParameter("@MonedaId", Convert.ToInt32(0));
                    break;

                case WhereFilters.MonedaCod:
                    break;

                case WhereFilters.GridCheck:
                    break;
            }
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
                        new SqlParameter("@MonedaCod", mstrMonedaCod),
                        new SqlParameter("@MonedaDes", mstrMonedaDes)};
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
                        new SqlParameter("@MonedaId", mlngMonedaId),
                        new SqlParameter("@MonedaCod", mstrMonedaCod),
                        new SqlParameter("@MonedaDes", mstrMonedaDes)};
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
                        new SqlParameter("@MonedaId", mlngMonedaId)};
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
                        mlngMonedaId = SysData.ToLong(oDataRow["MonedaId"]);
                        mstrMonedaCod = SysData.ToStr(oDataRow["MonedaCod"]);
                        mstrMonedaDes = SysData.ToStr(oDataRow["MonedaDes"]);
                        break;

                    case SelectFilters.ListBox:
                        mlngMonedaId = SysData.ToLong(oDataRow["MonedaId"]);
                        mstrMonedaCod = SysData.ToStr(oDataRow["MonedaCod"]);
                        mstrMonedaDes = SysData.ToStr(oDataRow["MonedaDes"]);
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

            if (mstrMonedaCod.Length == 0)
            {
                strMsg += "Código es Requerido" + Environment.NewLine;
            }

            if (mstrMonedaDes.Length == 0)
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