using System;
using System.Data.SqlClient;
using System.Data;

namespace Contabilidad.Models.DAC
{
    public class clsPlan : clsBase, IDisposable
    {
        private long mlngPlanId;
        private string mstrPlanCod;
        private string mstrPlanDes;
        private string mstrPlanEsp;
        private long mlngTipoPlanId;
        private long mlngOrden;
        private long mlngNivel;
        private long mlngMonedaId;
        private long mlngTipoAmbitoId;
        private long mlngPlanAjusteId;
        private long mlngCapituloId;
        private long mlngPlanPadreId;
        private long mlngEstadoId;

        //******************************************************
        // Private Data To Match the Table Definition
        //******************************************************
        public long PlanId
        {
            get
            {
                return mlngPlanId;
            }

            set
            {
                mlngPlanId = value;
            }
        }

        public string PlanCod
        {
            get
            {
                return mstrPlanCod;
            }

            set
            {
                mstrPlanCod = value;
            }
        }

        public string PlanDes
        {
            get
            {
                return mstrPlanDes;
            }

            set
            {
                mstrPlanDes = value;
            }
        }

        public string PlanEsp
        {
            get
            {
                return mstrPlanEsp;
            }

            set
            {
                mstrPlanEsp = value;
            }
        }

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

        public long Orden
        {
            get
            {
                return mlngOrden;
            }

            set
            {
                mlngOrden = value;
            }
        }

        public long Nivel
        {
            get
            {
                return mlngNivel;
            }

            set
            {
                mlngNivel = value;
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

        public long PlanAjusteId
        {
            get
            {
                return mlngPlanAjusteId;
            }

            set
            {
                mlngPlanAjusteId = value;
            }
        }

        public long CapituloId
        {
            get
            {
                return mlngCapituloId;
            }

            set
            {
                mlngCapituloId = value;
            }
        }

        public long PlanPadreId
        {
            get
            {
                return mlngPlanPadreId;
            }

            set
            {
                mlngPlanPadreId = value;
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
            PlanDes = 2,
            Grid = 3,
            GridCheck = 4,
            PlanCod = 5,
            Grid_PlanPadreId = 6,
            PlanPadreId = 7,
            PlanHijo_MaxOrden = 8,
            EstadoId = 9,
            TipoPlanId = 10
        }

        public enum OrderByFilters : byte
        {
            None = 0,
            PlanId = 1,
            PlanDes = 2,
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
        public clsPlan()
        {
            mstrTableName = "ctbPlan";
            mstrClassName = "clsPlan";

            PropertyInit();
            FilterInit();
        }

        public clsPlan(string ConnectString) : this()
        {
            moConnection = new SqlConnection();

            mstrConnectionString = ConnectString;
        }

        public clsPlan(SqlConnection oConnection) : this()
        {
            moConnection = oConnection;
        }

        public clsPlan(SqlConnection oConnection, SelectFilters bytSelectFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
        }

        public clsPlan(SqlConnection oConnection, SelectFilters bytSelectFilter, WhereFilters bytWhereFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
            mintWhereFilter = bytWhereFilter;
        }

        public clsPlan(SqlConnection oConnection, SelectFilters bytSelectFilter, WhereFilters bytWhereFilter, OrderByFilters bytOrderByFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
            mintWhereFilter = bytWhereFilter;
            mintOrderByFilter = bytOrderByFilter;
        }

        public void PropertyInit()
        {
            mlngPlanId = 0;
            mstrPlanCod = "";
            mstrPlanDes = "";
            mstrPlanEsp = "";
            mlngTipoPlanId = 0;
            mlngOrden = 0;
            mlngNivel = 0;
            mlngMonedaId = 0;
            mlngTipoAmbitoId = 0;
            mlngPlanAjusteId = 0;
            mlngCapituloId = 0;
            mlngPlanPadreId = 0;
            mlngEstadoId = 0;
        }

        protected override void SetPrimaryKey()
        {
            mlngPlanId = mlngId;
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
                    mstrStoreProcName = "ctbPlanSelect";
                    break;

                case SelectFilters.RowCount:
                    mstrStoreProcName = "ctbPlanSelect";
                    break;

                case SelectFilters.ListBox:
                    mstrStoreProcName = "ctbPlanSelect";
                    break;

                case SelectFilters.Grid:
                    mstrStoreProcName = "ctbPlanSelect";
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
                    Array.Resize(ref moParameters, moParameters.Length + 5);
                    moParameters[3] = new SqlParameter("@PlanId", mlngPlanId);
                    moParameters[4] = new SqlParameter("@PlanCod", Convert.ToString(""));
                    moParameters[5] = new SqlParameter("@PlanPadreId", Convert.ToInt32(0));
                    moParameters[6] = new SqlParameter("@TipoPlanId", Convert.ToInt32(0));
                    moParameters[7] = new SqlParameter("@EstadoId", Convert.ToInt32(0));
                    break;

                case WhereFilters.PlanDes:
                    break;
                //strSQL = " WHERE  ctbPlan.PlanDes = " & StringToField(mstrPlanDes)

                case WhereFilters.Grid:
                    Array.Resize(ref moParameters, moParameters.Length + 5);
                    moParameters[3] = new SqlParameter("@PlanId", Convert.ToInt32(0));
                    moParameters[4] = new SqlParameter("@PlanCod", Convert.ToString(""));
                    moParameters[5] = new SqlParameter("@PlanPadreId", mlngPlanPadreId);
                    moParameters[6] = new SqlParameter("@TipoPlanId", Convert.ToInt32(0));
                    moParameters[7] = new SqlParameter("@EstadoId", mlngEstadoId);
                    break;

                case WhereFilters.PlanCod:
                    break;

                case WhereFilters.GridCheck:
                    break;

                case WhereFilters.Grid_PlanPadreId:
                    Array.Resize(ref moParameters, moParameters.Length + 5);
                    moParameters[3] = new SqlParameter("@PlanId", Convert.ToInt32(0));
                    moParameters[4] = new SqlParameter("@PlanCod", Convert.ToString(""));
                    moParameters[5] = new SqlParameter("@PlanPadreId", mlngPlanPadreId);
                    moParameters[6] = new SqlParameter("@TipoPlanId", Convert.ToInt32(0));
                    moParameters[7] = new SqlParameter("@EstadoId", Convert.ToInt32(0));
                    break;

                case WhereFilters.PlanPadreId:
                    Array.Resize(ref moParameters, moParameters.Length + 5);
                    moParameters[3] = new SqlParameter("@PlanId", Convert.ToInt32(0));
                    moParameters[4] = new SqlParameter("@PlanCod", Convert.ToString(""));
                    moParameters[5] = new SqlParameter("@PlanPadreId", mlngPlanPadreId);
                    moParameters[6] = new SqlParameter("@TipoPlanId", Convert.ToInt32(0));
                    moParameters[7] = new SqlParameter("@EstadoId", Convert.ToInt32(0));
                    break;

                case WhereFilters.PlanHijo_MaxOrden:
                    Array.Resize(ref moParameters, moParameters.Length + 5);
                    moParameters[3] = new SqlParameter("@PlanId", Convert.ToInt32(0));
                    moParameters[4] = new SqlParameter("@PlanCod", Convert.ToString(""));
                    moParameters[5] = new SqlParameter("@PlanPadreId", mlngPlanPadreId);
                    moParameters[6] = new SqlParameter("@TipoPlanId", Convert.ToInt32(0));
                    moParameters[7] = new SqlParameter("@EstadoId", mlngEstadoId);
                    break;

                case WhereFilters.EstadoId:
                    Array.Resize(ref moParameters, moParameters.Length + 5);
                    moParameters[3] = new SqlParameter("@PlanId", Convert.ToInt32(0));
                    moParameters[4] = new SqlParameter("@PlanCod", Convert.ToString(""));
                    moParameters[5] = new SqlParameter("@PlanPadreId", Convert.ToInt32(0));
                    moParameters[6] = new SqlParameter("@TipoPlanId", Convert.ToInt32(0));
                    moParameters[7] = new SqlParameter("@EstadoId", mlngEstadoId);
                    break;

                case WhereFilters.TipoPlanId:
                    Array.Resize(ref moParameters, moParameters.Length + 5);
                    moParameters[3] = new SqlParameter("@PlanId", Convert.ToInt32(0));
                    moParameters[4] = new SqlParameter("@PlanCod", Convert.ToString(""));
                    moParameters[5] = new SqlParameter("@PlanPadreId", Convert.ToInt32(0));
                    moParameters[6] = new SqlParameter("@TipoPlanId", mlngTipoPlanId);
                    moParameters[7] = new SqlParameter("@EstadoId", mlngEstadoId);
                    break;
            }
        }

        protected override void InsertParameter()
        {
            switch (mintInsertFilter)
            {
                case InsertFilters.All:
                    mstrStoreProcName = "ctbPlanInsert";
                    moParameters = new SqlParameter[14] {
                        new SqlParameter("@InsertFilter", mintInsertFilter),
                        new SqlParameter("@Id", SqlDbType.Int),
                        new SqlParameter("@PlanCod", mstrPlanCod),
                        new SqlParameter("@PlanDes", mstrPlanDes),
                        new SqlParameter("@PlanEsp", mstrPlanEsp),
                        new SqlParameter("@TipoPlanId", mlngTipoPlanId),
                        new SqlParameter("@Orden", mlngOrden),
                        new SqlParameter("@Nivel", mlngNivel),
                        new SqlParameter("@MonedaId", mlngMonedaId),
                        new SqlParameter("@TipoAmbitoId", mlngTipoAmbitoId),
                        new SqlParameter("@PlanAjusteId", mlngPlanAjusteId),
                        new SqlParameter("@CapituloId", mlngCapituloId),
                        new SqlParameter("@PlanPadreId", mlngPlanPadreId),
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
                    mstrStoreProcName = "ctbPlanUpdate";
                    moParameters = new SqlParameter[14] {
                        new SqlParameter("@UpdateFilter", mintUpdateFilter),
                        new SqlParameter("@PlanId", mlngPlanId),
                        new SqlParameter("@PlanCod", mstrPlanCod),
                        new SqlParameter("@PlanDes", mstrPlanDes),
                        new SqlParameter("@PlanEsp", mstrPlanEsp),
                        new SqlParameter("@TipoPlanId", mlngTipoPlanId),
                        new SqlParameter("@Orden", mlngOrden),
                        new SqlParameter("@Nivel", mlngNivel),
                        new SqlParameter("@MonedaId", mlngMonedaId),
                        new SqlParameter("@TipoAmbitoId", mlngTipoAmbitoId),
                        new SqlParameter("@PlanAjusteId", mlngPlanAjusteId),
                        new SqlParameter("@CapituloId", mlngCapituloId),
                        new SqlParameter("@PlanPadreId", mlngPlanPadreId),
                        new SqlParameter("@EstadoId", mlngEstadoId)};
                    break;
            }
        }

        protected override void DeleteParameter()
        {
            switch (mintDeleteFilter)
            {
                case DeleteFilters.All:
                    mstrStoreProcName = "ctbPlanDelete";
                    moParameters = new SqlParameter[2] {
                        new SqlParameter("@DeleteFilter", mintDeleteFilter),
                        new SqlParameter("@PlanId", mlngPlanId)};
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
                        mlngPlanId = SysData.ToLong(oDataRow["PlanId"]);
                        mstrPlanCod = SysData.ToStr(oDataRow["PlanCod"]);
                        mstrPlanDes = SysData.ToStr(oDataRow["PlanDes"]);
                        mstrPlanEsp = SysData.ToStr(oDataRow["PlanEsp"]);
                        mlngTipoPlanId = SysData.ToLong(oDataRow["TipoPlanId"]);
                        mlngOrden = SysData.ToLong(oDataRow["Orden"]);
                        mlngNivel = SysData.ToLong(oDataRow["Nivel"]);
                        mlngMonedaId = SysData.ToLong(oDataRow["MonedaId"]);
                        mlngTipoAmbitoId = SysData.ToLong(oDataRow["TipoAmbitoId"]);
                        mlngPlanAjusteId = SysData.ToLong(oDataRow["PlanAjusteId"]);
                        mlngCapituloId = SysData.ToLong(oDataRow["CapituloId"]);
                        mlngPlanPadreId = SysData.ToLong(oDataRow["PlanPadreId"]);
                        mlngEstadoId = SysData.ToLong(oDataRow["EstadoId"]);
                        break;

                    case SelectFilters.ListBox:
                        mlngPlanId = SysData.ToLong(oDataRow["PlanId"]);
                        mstrPlanCod = SysData.ToStr(oDataRow["PlanCod"]);
                        mstrPlanDes = SysData.ToStr(oDataRow["PlanDes"]);
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

            if (mstrPlanCod.Length == 0)
            {
                strMsg += "Código es Requerido" + Environment.NewLine;
            }

            if (mstrPlanDes.Length == 0)
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