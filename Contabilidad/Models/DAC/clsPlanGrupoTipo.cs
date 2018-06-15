using System;
using System.Data.SqlClient;
using System.Data;

namespace Contabilidad.Models.DAC
{
    public class clsPlanGrupoTipo : clsBase, IDisposable
    {
        private long mlngPlanGrupoTipoId;
        private string mstrPlanGrupoTipoCod;
        private string mstrPlanGrupoTipoDes;
        private string mstrPlanGrupoTipoEsp;
        private long mlngEstadoId;

        //******************************************************
        // Private Data To Match the Table Definition
        //******************************************************
        public long PlanGrupoTipoId
        {
            get
            {
                return mlngPlanGrupoTipoId;
            }

            set
            {
                mlngPlanGrupoTipoId = value;
            }
        }

        public string PlanGrupoTipoCod
        {
            get
            {
                return mstrPlanGrupoTipoCod;
            }

            set
            {
                mstrPlanGrupoTipoCod = value;
            }
        }

        public string PlanGrupoTipoDes
        {
            get
            {
                return mstrPlanGrupoTipoDes;
            }

            set
            {
                mstrPlanGrupoTipoDes = value;
            }
        }

        public string PlanGrupoTipoEsp
        {
            get
            {
                return mstrPlanGrupoTipoEsp;
            }

            set
            {
                mstrPlanGrupoTipoEsp = value;
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
            PlanGrupoTipoDes = 2,
            Grid = 3,
            GridCheck = 4,
            PlanGrupoTipoCod = 5,
        }

        public enum OrderByFilters : byte
        {
            None = 0,
            PlanGrupoTipoId = 1,
            PlanGrupoTipoDes = 2,
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
        public clsPlanGrupoTipo()
        {
            mstrTableName = "ctbPlanGrupoTipo";
            mstrClassName = "clsPlanGrupoTipo";

            PropertyInit();
            FilterInit();
        }

        public clsPlanGrupoTipo(string ConnectString) : this()
        {
            moConnection = new SqlConnection();

            mstrConnectionString = ConnectString;
        }

        public clsPlanGrupoTipo(SqlConnection oConnection) : this()
        {
            moConnection = oConnection;
        }

        public clsPlanGrupoTipo(SqlConnection oConnection, SelectFilters bytSelectFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
        }

        public clsPlanGrupoTipo(SqlConnection oConnection, SelectFilters bytSelectFilter, WhereFilters bytWhereFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
            mintWhereFilter = bytWhereFilter;
        }

        public clsPlanGrupoTipo(SqlConnection oConnection, SelectFilters bytSelectFilter, WhereFilters bytWhereFilter, OrderByFilters bytOrderByFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
            mintWhereFilter = bytWhereFilter;
            mintOrderByFilter = bytOrderByFilter;
        }

        public void PropertyInit()
        {
            mlngPlanGrupoTipoId = 0;
            mstrPlanGrupoTipoCod = "";
            mstrPlanGrupoTipoDes = "";
            mstrPlanGrupoTipoEsp = "";
            mlngEstadoId = 0;
        }

        protected override void SetPrimaryKey()
        {
            mlngPlanGrupoTipoId = mlngId;
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
                    mstrStoreProcName = "ctbPlanGrupoTipoSelect";
                    break;

                case SelectFilters.RowCount:
                    mstrStoreProcName = "ctbPlanGrupoTipoSelect";
                    break;

                case SelectFilters.ListBox:
                    mstrStoreProcName = "ctbPlanGrupoTipoSelect";
                    break;

                case SelectFilters.Grid:
                    mstrStoreProcName = "ctbPlanGrupoTipoSelect";
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
                    moParameters[3] = new SqlParameter("@PlanGrupoTipoId", Convert.ToInt32(0));
                    moParameters[4] = new SqlParameter("@PlanGrupoTipoCod", Convert.ToString(""));
                    moParameters[5] = new SqlParameter("@EstadoId", Convert.ToInt32(0));
                    break;

                case WhereFilters.PrimaryKey:
                    Array.Resize(ref moParameters, moParameters.Length + 3);
                    moParameters[3] = new SqlParameter("@PlanGrupoTipoId", mlngPlanGrupoTipoId);
                    moParameters[4] = new SqlParameter("@PlanGrupoTipoCod", Convert.ToString(""));
                    moParameters[5] = new SqlParameter("@EstadoId", Convert.ToInt32(0));
                    break;

                case WhereFilters.PlanGrupoTipoDes:
                    break;
                //strSQL = " WHERE  ctbPlanGrupoTipo.PlanGrupoTipoDes = " & StringToField(mstrPlanGrupoTipoDes)

                case WhereFilters.Grid:
                    Array.Resize(ref moParameters, moParameters.Length + 3);
                    moParameters[3] = new SqlParameter("@PlanGrupoTipoId", Convert.ToInt32(0));
                    moParameters[4] = new SqlParameter("@PlanGrupoTipoCod", Convert.ToString(""));
                    moParameters[5] = new SqlParameter("@EstadoId", Convert.ToInt32(0));
                    break;

                case WhereFilters.PlanGrupoTipoCod:
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
                    mstrStoreProcName = "ctbPlanGrupoTipoInsert";
                    moParameters = new SqlParameter[6] {
                        new SqlParameter("@InsertFilter", mintInsertFilter),
                        new SqlParameter("@Id", SqlDbType.Int),
                        new SqlParameter("@PlanGrupoTipoCod", mstrPlanGrupoTipoCod),
                        new SqlParameter("@PlanGrupoTipoDes", mstrPlanGrupoTipoDes),
                        new SqlParameter("@PlanGrupoTipoEsp", mstrPlanGrupoTipoEsp),
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
                    mstrStoreProcName = "ctbPlanGrupoTipoUpdate";
                    moParameters = new SqlParameter[6] {
                        new SqlParameter("@UpdateFilter", mintUpdateFilter),
                        new SqlParameter("@PlanGrupoTipoId", mlngPlanGrupoTipoId),
                        new SqlParameter("@PlanGrupoTipoCod", mstrPlanGrupoTipoCod),
                        new SqlParameter("@PlanGrupoTipoDes", mstrPlanGrupoTipoDes),
                        new SqlParameter("@PlanGrupoTipoEsp", mstrPlanGrupoTipoEsp),
                        new SqlParameter("@EstadoId", mlngEstadoId)};
                    break;
            }
        }

        protected override void DeleteParameter()
        {
            switch (mintDeleteFilter)
            {
                case DeleteFilters.All:
                    mstrStoreProcName = "ctbPlanGrupoTipoDelete";
                    moParameters = new SqlParameter[2] {
                        new SqlParameter("@DeleteFilter", mintDeleteFilter),
                        new SqlParameter("@PlanGrupoTipoId", mlngPlanGrupoTipoId)};
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
                        mlngPlanGrupoTipoId = SysData.ToLong(oDataRow["PlanGrupoTipoId"]);
                        mstrPlanGrupoTipoCod = SysData.ToStr(oDataRow["PlanGrupoTipoCod"]);
                        mstrPlanGrupoTipoDes = SysData.ToStr(oDataRow["PlanGrupoTipoDes"]);
                        mstrPlanGrupoTipoEsp = SysData.ToStr(oDataRow["PlanGrupoTipoEsp"]);
                        mlngEstadoId = SysData.ToLong(oDataRow["EstadoId"]);
                        break;

                    case SelectFilters.ListBox:
                        mlngPlanGrupoTipoId = SysData.ToLong(oDataRow["PlanGrupoTipoId"]);
                        mstrPlanGrupoTipoCod = SysData.ToStr(oDataRow["PlanGrupoTipoCod"]);
                        mstrPlanGrupoTipoDes = SysData.ToStr(oDataRow["PlanGrupoTipoDes"]);
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

            if (mstrPlanGrupoTipoCod.Length == 0)
            {
                strMsg += "Código es Requerido" + Environment.NewLine;
            }

            if (mstrPlanGrupoTipoDes.Length == 0)
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