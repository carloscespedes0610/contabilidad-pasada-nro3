using System;
using System.Data.SqlClient;
using System.Data;

namespace Contabilidad.Models.DAC
{
    public class clsSucursal : clsBase, IDisposable
    {
        private long mlngSucursalId;
        private string mstrSucursalCod;
        private string mstrSucursalDes;
        private string mstrSucursalEsp;
        private long mlngEstadoId;

        //******************************************************
        // Private Data To Match the Table Definition
        //******************************************************
        public long SucursalId
        {
            get
            {
                return mlngSucursalId;
            }

            set
            {
                mlngSucursalId = value;
            }
        }

        public string SucursalCod
        {
            get
            {
                return mstrSucursalCod;
            }

            set
            {
                mstrSucursalCod = value;
            }
        }

        public string SucursalDes
        {
            get
            {
                return mstrSucursalDes;
            }

            set
            {
                mstrSucursalDes = value;
            }
        }

        public string SucursalEsp
        {
            get
            {
                return mstrSucursalEsp;
            }

            set
            {
                mstrSucursalEsp = value;
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
            mlngSucursalId = 0;
            mstrSucursalCod = "";
            mstrSucursalDes = "";
            mstrSucursalEsp = "";
            mlngEstadoId = 0;
        }

        protected override void SetPrimaryKey()
        {
            mlngSucursalId = mlngId;
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
                    mstrStoreProcName = "ctbSucursalSelect";
                    break;

                case SelectFilters.RowCount:
                    mstrStoreProcName = "ctbSucursalSelect";
                    break;

                case SelectFilters.ListBox:
                    mstrStoreProcName = "ctbSucursalSelect";
                    break;

                case SelectFilters.Grid:
                    mstrStoreProcName = "ctbSucursalSelect";
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
                    Array.Resize(ref moParameters, moParameters.Length + 3);
                    moParameters[3] = new SqlParameter("@SucursalId", Convert.ToInt32(0));
                    moParameters[4] = new SqlParameter("@SucursalCod", Convert.ToString(""));
                    moParameters[5] = new SqlParameter("@EstadoId", Convert.ToInt32(0));
                    break;

                case WhereFilters.PrimaryKey:
                    Array.Resize(ref moParameters, moParameters.Length + 3);
                    moParameters[3] = new SqlParameter("@SucursalId", mlngSucursalId);
                    moParameters[4] = new SqlParameter("@SucursalCod", Convert.ToString(""));
                    moParameters[5] = new SqlParameter("@EstadoId", Convert.ToInt32(0));
                    break;

                case WhereFilters.SucursalDes:
                    break;
                //strSQL = " WHERE  ctbSucursal.SucursalDes = " & StringToField(mstrSucursalDes)

                case WhereFilters.Grid:
                    Array.Resize(ref moParameters, moParameters.Length + 3);
                    moParameters[3] = new SqlParameter("@SucursalId", Convert.ToInt32(0));
                    moParameters[4] = new SqlParameter("@SucursalCod", Convert.ToString(""));
                    moParameters[5] = new SqlParameter("@EstadoId", Convert.ToInt32(0));
                    break;

                case WhereFilters.SucursalCod:
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
                    mstrStoreProcName = "ctbSucursalInsert";
                    moParameters = new SqlParameter[6] {
                        new SqlParameter("@InsertFilter", mintInsertFilter),
                        new SqlParameter("@Id", SqlDbType.Int),
                        new SqlParameter("@SucursalCod", mstrSucursalCod),
                        new SqlParameter("@SucursalDes", mstrSucursalDes),
                        new SqlParameter("@SucursalEsp", mstrSucursalEsp),
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
                    mstrStoreProcName = "ctbSucursalUpdate";
                    moParameters = new SqlParameter[6] {
                        new SqlParameter("@UpdateFilter", mintUpdateFilter),
                        new SqlParameter("@SucursalId", mlngSucursalId),
                        new SqlParameter("@SucursalCod", mstrSucursalCod),
                        new SqlParameter("@SucursalDes", mstrSucursalDes),
                        new SqlParameter("@SucursalEsp", mstrSucursalEsp),
                        new SqlParameter("@EstadoId", mlngEstadoId)};
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
                        new SqlParameter("@SucursalId", mlngSucursalId)};
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
                        mlngSucursalId = SysData.ToLong(oDataRow["SucursalId"]);
                        mstrSucursalCod = SysData.ToStr(oDataRow["SucursalCod"]);
                        mstrSucursalDes = SysData.ToStr(oDataRow["SucursalDes"]);
                        mstrSucursalEsp = SysData.ToStr(oDataRow["SucursalEsp"]);
                        mlngEstadoId = SysData.ToLong(oDataRow["EstadoId"]);
                        break;

                    case SelectFilters.ListBox:
                        mlngSucursalId = SysData.ToLong(oDataRow["SucursalId"]);
                        mstrSucursalCod = SysData.ToStr(oDataRow["SucursalCod"]);
                        mstrSucursalDes = SysData.ToStr(oDataRow["SucursalDes"]);
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

            if (mstrSucursalCod.Length == 0)
            {
                strMsg += "Código es Requerido" + Environment.NewLine;
            }

            if (mstrSucursalDes.Length == 0)
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