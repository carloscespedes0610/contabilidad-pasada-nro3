using System;
using System.Data.SqlClient;
using System.Data;

namespace Contabilidad.Models.DAC
{
    public class clsPlanGrupo : clsBase, IDisposable
    {
        private long mlngPlanGrupoId;
        private string mstrPlanGrupoCod;
        private string mstrPlanGrupoDes;
        private string mstrPlanGrupoEsp;
        private long mlngPlanGrupoTipoId;
        private long mlngPlanGrupoTipoDetId;
        private long mlngNroCuentas;
        private long mlngMonedaId;
        private bool mboolVerificaMto;
        private long mlngEstadoId;

        //******************************************************
        // Private Data To Match the Table Definition
        //******************************************************
        public long PlanGrupoId
        {
            get
            {
                return mlngPlanGrupoId;
            }

            set
            {
                mlngPlanGrupoId = value;
            }
        }

        public string PlanGrupoCod
        {
            get
            {
                return mstrPlanGrupoCod;
            }

            set
            {
                mstrPlanGrupoCod = value;
            }
        }

        public string PlanGrupoDes
        {
            get
            {
                return mstrPlanGrupoDes;
            }

            set
            {
                mstrPlanGrupoDes = value;
            }
        }

        public string PlanGrupoEsp
        {
            get
            {
                return mstrPlanGrupoEsp;
            }

            set
            {
                mstrPlanGrupoEsp = value;
            }
        }

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

        public long PlanGrupoTipoDetId
        {
            get
            {
                return mlngPlanGrupoTipoDetId;
            }

            set
            {
                mlngPlanGrupoTipoDetId = value;
            }
        }

        public long NroCuentas
        {
            get
            {
                return mlngNroCuentas;
            }

            set
            {
                mlngNroCuentas = value;
            }
        }

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

        public bool VerificaMto
        {
            get
            {
                return mboolVerificaMto;
            }

            set
            {
                mboolVerificaMto = value;
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
            PlanGrupoDes = 2,
            Grid = 3,
            GridCheck = 4,
            PlanGrupoCod = 5,
        }

        public enum OrderByFilters : byte
        {
            None = 0,
            PlanGrupoId = 1,
            PlanGrupoDes = 2,
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
        public clsPlanGrupo()
        {
            mstrTableName = "ctbPlanGrupo";
            mstrClassName = "clsPlanGrupo";

            PropertyInit();
            FilterInit();
        }

        public clsPlanGrupo(string ConnectString) : this()
        {
            moConnection = new SqlConnection();

            mstrConnectionString = ConnectString;
        }

        public clsPlanGrupo(SqlConnection oConnection) : this()
        {
            moConnection = oConnection;
        }

        public clsPlanGrupo(SqlConnection oConnection, SelectFilters bytSelectFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
        }

        public clsPlanGrupo(SqlConnection oConnection, SelectFilters bytSelectFilter, WhereFilters bytWhereFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
            mintWhereFilter = bytWhereFilter;
        }

        public clsPlanGrupo(SqlConnection oConnection, SelectFilters bytSelectFilter, WhereFilters bytWhereFilter, OrderByFilters bytOrderByFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
            mintWhereFilter = bytWhereFilter;
            mintOrderByFilter = bytOrderByFilter;
        }

        public void PropertyInit()
        {
            mlngPlanGrupoId = 0;
            mstrPlanGrupoCod = "";
            mstrPlanGrupoDes = "";
            mstrPlanGrupoEsp = "";
            mlngPlanGrupoTipoId = 0;
            mlngPlanGrupoTipoDetId = 0;
            mlngNroCuentas = 0;
            mlngMonedaId = 0;
            mboolVerificaMto = false;
            mlngEstadoId = 0;
        }

        protected override void SetPrimaryKey()
        {
            mlngPlanGrupoId = mlngId;
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
                    mstrStoreProcName = "ctbPlanGrupoSelect";
                    break;

                case SelectFilters.RowCount:
                    mstrStoreProcName = "ctbPlanGrupoSelect";
                    break;

                case SelectFilters.ListBox:
                    mstrStoreProcName = "ctbPlanGrupoSelect";
                    break;

                case SelectFilters.Grid:
                    mstrStoreProcName = "ctbPlanGrupoSelect";
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
                    Array.Resize(ref moParameters, moParameters.Length + 3);
                    moParameters[3] = new SqlParameter("@PlanGrupoId", mlngPlanGrupoId);
                    moParameters[4] = new SqlParameter("@PlanGrupoCod", Convert.ToString(""));
                    moParameters[5] = new SqlParameter("@EstadoId", Convert.ToInt32(0));
                    break;

                case WhereFilters.PlanGrupoDes:
                    break;
                //strSQL = " WHERE  ctbPlanGrupo.PlanGrupoDes = " & StringToField(mstrPlanGrupoDes)

                case WhereFilters.Grid:
                    Array.Resize(ref moParameters, moParameters.Length + 3);
                    moParameters[3] = new SqlParameter("@PlanGrupoId", Convert.ToInt32(0));
                    moParameters[4] = new SqlParameter("@PlanGrupoCod", Convert.ToString(""));
                    moParameters[5] = new SqlParameter("@EstadoId", mlngEstadoId);
                    break;

                case WhereFilters.PlanGrupoCod:
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
                    mstrStoreProcName = "ctbPlanGrupoInsert";
                    moParameters = new SqlParameter[11] {
                        new SqlParameter("@InsertFilter", mintInsertFilter),
                        new SqlParameter("@Id", SqlDbType.Int),
                        new SqlParameter("@PlanGrupoCod", mstrPlanGrupoCod),
                        new SqlParameter("@PlanGrupoDes", mstrPlanGrupoDes),
                        new SqlParameter("@PlanGrupoEsp", mstrPlanGrupoEsp),
                        new SqlParameter("@PlanGrupoTipoId", mlngPlanGrupoTipoId),
                        new SqlParameter("@PlanGrupoTipoDetId", mlngPlanGrupoTipoDetId),
                        new SqlParameter("@NroCuentas", mlngNroCuentas),
                        new SqlParameter("@MonedaId", mlngMonedaId),
                        new SqlParameter("@VerificaMto", mboolVerificaMto),
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
                    mstrStoreProcName = "ctbPlanGrupoUpdate";
                    moParameters = new SqlParameter[11] {
                        new SqlParameter("@UpdateFilter", mintUpdateFilter),
                        new SqlParameter("@PlanGrupoId", mlngPlanGrupoId),
                        new SqlParameter("@PlanGrupoCod", mstrPlanGrupoCod),
                        new SqlParameter("@PlanGrupoDes", mstrPlanGrupoDes),
                        new SqlParameter("@PlanGrupoEsp", mstrPlanGrupoEsp),
                        new SqlParameter("@PlanGrupoTipoId", mlngPlanGrupoTipoId),
                        new SqlParameter("@PlanGrupoTipoDetId", mlngPlanGrupoTipoDetId),
                        new SqlParameter("@NroCuentas", mlngNroCuentas),
                        new SqlParameter("@MonedaId", mlngMonedaId),
                        new SqlParameter("@VerificaMto", mboolVerificaMto),
                        new SqlParameter("@EstadoId", mlngEstadoId)};
                    break;
            }
        }

        protected override void DeleteParameter()
        {
            switch (mintDeleteFilter)
            {
                case DeleteFilters.All:
                    mstrStoreProcName = "ctbPlanGrupoDelete";
                    moParameters = new SqlParameter[2] {
                        new SqlParameter("@DeleteFilter", mintDeleteFilter),
                        new SqlParameter("@PlanGrupoId", mlngPlanGrupoId)};
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
                        mlngPlanGrupoId = SysData.ToLong(oDataRow["PlanGrupoId"]);
                        mstrPlanGrupoCod = SysData.ToStr(oDataRow["PlanGrupoCod"]);
                        mstrPlanGrupoDes = SysData.ToStr(oDataRow["PlanGrupoDes"]);
                        mstrPlanGrupoEsp = SysData.ToStr(oDataRow["PlanGrupoEsp"]);
                        mlngPlanGrupoTipoId = SysData.ToLong(oDataRow["PlanGrupoTipoId"]);
                        mlngPlanGrupoTipoDetId = SysData.ToLong(oDataRow["PlanGrupoTipoDetId"]);
                        mlngNroCuentas = SysData.ToLong(oDataRow["NroCuentas"]);
                        mlngMonedaId = SysData.ToLong(oDataRow["MonedaId"]);
                        mboolVerificaMto = SysData.ToBoolean(oDataRow["VerificaMto"]);
                        mlngEstadoId = SysData.ToLong(oDataRow["EstadoId"]);
                        break;

                    case SelectFilters.ListBox:
                        mlngPlanGrupoId = SysData.ToLong(oDataRow["PlanGrupoId"]);
                        mstrPlanGrupoCod = SysData.ToStr(oDataRow["PlanGrupoCod"]);
                        mstrPlanGrupoDes = SysData.ToStr(oDataRow["PlanGrupoDes"]);
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

            if (mstrPlanGrupoCod.Length == 0)
            {
                strMsg += "Código es Requerido" + Environment.NewLine;
            }

            if (mstrPlanGrupoDes.Length == 0)
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