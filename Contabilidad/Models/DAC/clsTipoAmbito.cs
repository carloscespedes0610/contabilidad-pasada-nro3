using System;
using System.Data.SqlClient;
using System.Data;

namespace Contabilidad.Models.DAC
{
    public class clsTipoAmbito : clsBase, IDisposable
    {
        private long mlngTipoAmbitoId;
        private string mstrTipoAmbitoDes;
        private long mlngEstadoId;

        //******************************************************
        // Private Data To Match the Table Definition
        //******************************************************
        public long TipoAmbitoId
        {
            get
            {
                return mlngTipoAmbitoId;
            }

            set
            {
                mlngTipoAmbitoId = value;
            }
        }

        public string TipoAmbitoDes
        {
            get
            {
                return mstrTipoAmbitoDes;
            }

            set
            {
                mstrTipoAmbitoDes = value;
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
            TipoAmbitoDes = 2,
            Grid = 3,
            GridCheck = 4,
            EstadoId = 5
        }

        public enum OrderByFilters : byte
        {
            None = 0,
            TipoAmbitoId = 1,
            TipoAmbitoDes = 2,
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
        public clsTipoAmbito()
        {
            mstrTableName = "ctbTipoAmbito";
            mstrClassName = "clsTipoAmbito";

            PropertyInit();
            FilterInit();
        }

        public clsTipoAmbito(string ConnectString) : this()
        {
            moConnection = new SqlConnection();

            mstrConnectionString = ConnectString;
        }

        public clsTipoAmbito(SqlConnection oConnection) : this()
        {
            moConnection = oConnection;
        }

        public clsTipoAmbito(SqlConnection oConnection, SelectFilters bytSelectFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
        }

        public clsTipoAmbito(SqlConnection oConnection, SelectFilters bytSelectFilter, WhereFilters bytWhereFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
            mintWhereFilter = bytWhereFilter;
        }

        public clsTipoAmbito(SqlConnection oConnection, SelectFilters bytSelectFilter, WhereFilters bytWhereFilter, OrderByFilters bytOrderByFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
            mintWhereFilter = bytWhereFilter;
            mintOrderByFilter = bytOrderByFilter;
        }

        public void PropertyInit()
        {
            mlngTipoAmbitoId = 0;
            mstrTipoAmbitoDes = "";
            mlngEstadoId = 0;
        }

        protected override void SetPrimaryKey()
        {
            mlngTipoAmbitoId = mlngId;
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
                    mstrStoreProcName = "ctbTipoAmbitoSelect";
                    break;

                case SelectFilters.RowCount:
                    mstrStoreProcName = "ctbTipoAmbitoSelect";
                    break;

                case SelectFilters.ListBox:
                    mstrStoreProcName = "ctbTipoAmbitoSelect";
                    break;

                case SelectFilters.Grid:
                    mstrStoreProcName = "ctbTipoAmbitoSelect";
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
                    moParameters[3] = new SqlParameter("@TipoAmbitoId", mlngTipoAmbitoId);
                    moParameters[4] = new SqlParameter("@EstadoId", Convert.ToInt32(0));
                    break;

                case WhereFilters.TipoAmbitoDes:
                    break;
                //strSQL = " WHERE  ctbTipoAmbito.TipoAmbitoDes = " & StringToField(mstrTipoAmbitoDes)

                case WhereFilters.Grid:
                    Array.Resize(ref moParameters, moParameters.Length + 2);
                    moParameters[3] = new SqlParameter("@TipoAmbitoId", Convert.ToInt32(0));
                    moParameters[4] = new SqlParameter("@EstadoId", Convert.ToInt32(0));
                    break;

                case WhereFilters.GridCheck:
                    break;

                case WhereFilters.EstadoId:
                    Array.Resize(ref moParameters, moParameters.Length + 2);
                    moParameters[3] = new SqlParameter("@TipoAmbitoId", Convert.ToInt32(0));
                    moParameters[4] = new SqlParameter("@EstadoId", mlngEstadoId);
                    break;
            }
        }

        protected override void InsertParameter()
        {
            switch (mintInsertFilter)
            {
                case InsertFilters.All:
                    mstrStoreProcName = "ctbTipoAmbitoInsert";
                    moParameters = new SqlParameter[4] {
                        new SqlParameter("@InsertFilter", mintInsertFilter),
                        new SqlParameter("@Id", SqlDbType.Int),
                        new SqlParameter("@TipoAmbitoDes", mstrTipoAmbitoDes),
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
                    mstrStoreProcName = "ctbTipoAmbitoUpdate";
                    moParameters = new SqlParameter[4] {
                        new SqlParameter("@UpdateFilter", mintUpdateFilter),
                        new SqlParameter("@TipoAmbitoId", mlngTipoAmbitoId),
                        new SqlParameter("@TipoAmbitoDes", mstrTipoAmbitoDes),
                        new SqlParameter("@EstadoId", mlngEstadoId)};
                    break;
            }
        }

        protected override void DeleteParameter()
        {
            switch (mintDeleteFilter)
            {
                case DeleteFilters.All:
                    mstrStoreProcName = "ctbTipoAmbitoDelete";
                    moParameters = new SqlParameter[2] {
                        new SqlParameter("@DeleteFilter", mintDeleteFilter),
                        new SqlParameter("@TipoAmbitoId", mlngTipoAmbitoId)};
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
                        mlngTipoAmbitoId = SysData.ToLong(oDataRow["TipoAmbitoId"]);
                        mstrTipoAmbitoDes = SysData.ToStr(oDataRow["TipoAmbitoDes"]);
                        mlngEstadoId = SysData.ToLong(oDataRow["EstadoId"]);
                        break;

                    case SelectFilters.ListBox:
                        mlngTipoAmbitoId = SysData.ToLong(oDataRow["TipoAmbitoId"]);
                        mstrTipoAmbitoDes = SysData.ToStr(oDataRow["TipoAmbitoDes"]);
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

            if (mstrTipoAmbitoDes.Length == 0)
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