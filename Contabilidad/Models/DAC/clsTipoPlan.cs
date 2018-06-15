using System;
using System.Data.SqlClient;
using System.Data;

namespace Contabilidad.Models.DAC
{
    public class clsTipoPlan : clsBase, IDisposable
    {
        private long mlngTipoPlanId;
        private string mstrTipoPlanDes;
        private long mlngEstadoId;

        //******************************************************
        // Private Data To Match the Table Definition
        //******************************************************
        public long TipoPlanId
        {
            get
            {
                return mlngTipoPlanId;
            }

            set
            {
                mlngTipoPlanId = value;
            }
        }

        public string TipoPlanDes
        {
            get
            {
                return mstrTipoPlanDes;
            }

            set
            {
                mstrTipoPlanDes = value;
            }
        }

        public long EstadoId
        {
            get
            {
                return mlngEstadoId;
            }

            set
            {
                mlngEstadoId = value;
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
            TipoPlanDes = 2,
            Grid = 3,
            GridCheck = 4,
            EstadoId = 5
        }

        public enum OrderByFilters : byte
        {
            None = 0,
            TipoPlanId = 1,
            TipoPlanDes = 2,
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
        public clsTipoPlan()
        {
            mstrTableName = "ctbTipoPlan";
            mstrClassName = "clsTipoPlan";

            PropertyInit();
            FilterInit();
        }

        public clsTipoPlan(string ConnectString) : this()
        {
            moConnection = new SqlConnection();

            mstrConnectionString = ConnectString;
        }

        public clsTipoPlan(SqlConnection oConnection) : this()
        {
            moConnection = oConnection;
        }

        public clsTipoPlan(SqlConnection oConnection, SelectFilters bytSelectFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
        }

        public clsTipoPlan(SqlConnection oConnection, SelectFilters bytSelectFilter, WhereFilters bytWhereFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
            mintWhereFilter = bytWhereFilter;
        }

        public clsTipoPlan(SqlConnection oConnection, SelectFilters bytSelectFilter, WhereFilters bytWhereFilter, OrderByFilters bytOrderByFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
            mintWhereFilter = bytWhereFilter;
            mintOrderByFilter = bytOrderByFilter;
        }

        public void PropertyInit()
        {
            mlngTipoPlanId = 0;
            mstrTipoPlanDes = "";
            mlngEstadoId = 0;
        }

        protected override void SetPrimaryKey()
        {
            mlngTipoPlanId = mlngId;
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
                    mstrStoreProcName = "ctbTipoPlanSelect";
                    break;

                case SelectFilters.RowCount:
                    mstrStoreProcName = "ctbTipoPlanSelect";
                    break;

                case SelectFilters.ListBox:
                    mstrStoreProcName = "ctbTipoPlanSelect";
                    break;

                case SelectFilters.Grid:
                    mstrStoreProcName = "ctbTipoPlanSelect";
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
                case WhereFilters.PrimaryKey:
                    Array.Resize(ref moParameters, moParameters.Length + 2);
                    moParameters[3] = new SqlParameter("@TipoPlanId", mlngTipoPlanId);
                    moParameters[4] = new SqlParameter("@EstadoId", Convert.ToInt32(0));
                    break;

                case WhereFilters.TipoPlanDes:
                    break;
                //strSQL = " WHERE  ctbTipoPlan.TipoPlanDes = " & StringToField(mstrTipoPlanDes)

                case WhereFilters.Grid:
                    Array.Resize(ref moParameters, moParameters.Length + 2);
                    moParameters[3] = new SqlParameter("@TipoPlanId", Convert.ToInt32(0));
                    moParameters[4] = new SqlParameter("@EstadoId", Convert.ToInt32(0));
                    break;

                case WhereFilters.GridCheck:
                    break;

                case WhereFilters.EstadoId:
                    Array.Resize(ref moParameters, moParameters.Length + 2);
                    moParameters[3] = new SqlParameter("@TipoPlanId", Convert.ToInt32(0));
                    moParameters[4] = new SqlParameter("@EstadoId", mlngEstadoId);
                    break;
            }
        }

        protected override void InsertParameter()
        {
            switch (mintInsertFilter)
            {
                case InsertFilters.All:
                    mstrStoreProcName = "ctbTipoPlanInsert";
                    moParameters = new SqlParameter[4] {
                        new SqlParameter("@InsertFilter", mintInsertFilter),
                        new SqlParameter("@Id", SqlDbType.Int),
                        new SqlParameter("@TipoPlanDes", mstrTipoPlanDes),
                        new SqlParameter("@EstadoId", mlngEstadoId)};
                    moParameters[1].Direction = ParameterDirection.Output;
                    break;
            }
        }

        protected override void UpdateParameter()
        {
            switch (mintUpdateFilter)
            {
                case UpdateFilters.All:
                    mstrStoreProcName = "ctbTipoPlanUpdate";
                    moParameters = new SqlParameter[4] {
                        new SqlParameter("@UpdateFilter", mintUpdateFilter),
                        new SqlParameter("@TipoPlanId", mlngTipoPlanId),
                        new SqlParameter("@TipoPlanDes", mstrTipoPlanDes),
                        new SqlParameter("@EstadoId", mlngEstadoId)};
                    break;
            }
        }

        protected override void DeleteParameter()
        {
            switch (mintDeleteFilter)
            {
                case DeleteFilters.All:
                    mstrStoreProcName = "ctbTipoPlanDelete";
                    moParameters = new SqlParameter[2] {
                        new SqlParameter("@DeleteFilter", mintDeleteFilter),
                        new SqlParameter("@TipoPlanId", mlngTipoPlanId)};
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
                        mlngTipoPlanId = SysData.ToLong(oDataRow["TipoPlanId"]);
                        mstrTipoPlanDes = SysData.ToStr(oDataRow["TipoPlanDes"]);
                        mlngEstadoId = SysData.ToLong(oDataRow["EstadoId"]);
                        break;

                    case SelectFilters.ListBox:
                        mlngTipoPlanId = SysData.ToLong(oDataRow["TipoPlanId"]);
                        mstrTipoPlanDes = SysData.ToStr(oDataRow["TipoPlanDes"]);
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

            if (mstrTipoPlanDes.Length == 0)
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