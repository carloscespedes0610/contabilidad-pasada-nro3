using System;
using System.Data.SqlClient;
using System.Data;

namespace Contabilidad.Models.DAC
{
    public class clsPlanGrupoDet : clsBase, IDisposable
    {
        private long mlngPlanGrupoDetId;
        private long mlngPlanGrupoId;
        private string mstrPlanGrupoDetDes;
        private long mlngPlanId;
        private long mlngPlanFlujoId;
        private long mlngSucursalId;
        private long mlngCenCosId;
        private long mlngOrden;
        private long mlngEstadoId;

        //******************************************************
        // Private Data To Match the Table Definition
        //******************************************************
        public long PlanGrupoDetId
        {
            get
            {
                return mlngPlanGrupoDetId;
            }

            set
            {
                mlngPlanGrupoDetId = value;
            }
        }

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

        public string PlanGrupoDetDes
        {
            get
            {
                return mstrPlanGrupoDetDes;
            }

            set
            {
                mstrPlanGrupoDetDes = value;
            }
        }

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

        public long PlanFlujoId
        {
            get
            {
                return mlngPlanFlujoId;
            }

            set
            {
                mlngPlanFlujoId = value;
            }
        }

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

        public long CenCosId
        {
            get
            {
                return mlngCenCosId;
            }

            set
            {
                mlngCenCosId = value;
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
            PlanGrupoDetDes = 2,
            Grid = 3,
            GridCheck = 4,
            PlanGrupoId = 5
        }

        public enum OrderByFilters : byte
        {
            None = 0,
            PlanGrupoDetId = 1,
            PlanGrupoDetDes = 2,
            Grid = 3,
            GridCheck = 4,
            Orden = 5
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
            All = 0,
            PlanGrupoId = 1
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
        public clsPlanGrupoDet()
        {
            mstrTableName = "ctbPlanGrupoDet";
            mstrClassName = "clsPlanGrupoDet";

            PropertyInit();
            FilterInit();
        }

        public clsPlanGrupoDet(string ConnectString) : this()
        {
            moConnection = new SqlConnection();

            mstrConnectionString = ConnectString;
        }

        public clsPlanGrupoDet(SqlConnection oConnection) : this()
        {
            moConnection = oConnection;
        }

        public clsPlanGrupoDet(SqlConnection oConnection, SelectFilters bytSelectFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
        }

        public clsPlanGrupoDet(SqlConnection oConnection, SelectFilters bytSelectFilter, WhereFilters bytWhereFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
            mintWhereFilter = bytWhereFilter;
        }

        public clsPlanGrupoDet(SqlConnection oConnection, SelectFilters bytSelectFilter, WhereFilters bytWhereFilter, OrderByFilters bytOrderByFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
            mintWhereFilter = bytWhereFilter;
            mintOrderByFilter = bytOrderByFilter;
        }

        public void PropertyInit()
        {
            mlngPlanGrupoDetId = 0;
            mlngPlanGrupoId = 0;
            mstrPlanGrupoDetDes = "";
            mlngPlanId = 0;
            mlngPlanFlujoId = 0;
            mlngSucursalId = 0;
            mlngCenCosId = 0;
            mlngOrden = 0;
            mlngEstadoId = 0;
        }

        protected override void SetPrimaryKey()
        {
            mlngPlanGrupoDetId = mlngId;
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
                    mstrStoreProcName = "ctbPlanGrupoDetSelect";
                    break;

                case SelectFilters.RowCount:
                    mstrStoreProcName = "ctbPlanGrupoDetSelect";
                    break;

                case SelectFilters.ListBox:
                    mstrStoreProcName = "ctbPlanGrupoDetSelect";
                    break;

                case SelectFilters.Grid:
                    mstrStoreProcName = "ctbPlanGrupoDetSelect";
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
                    moParameters[3] = new SqlParameter("@PlanGrupoDetId", mlngPlanGrupoDetId);
                    moParameters[4] = new SqlParameter("@PlanGrupoId", Convert.ToInt32(0));
                    moParameters[5] = new SqlParameter("@EstadoId", Convert.ToInt32(0));
                    break;

                case WhereFilters.PlanGrupoDetDes:
                    break;
                //strSQL = " WHERE  ctbPlanGrupoDet.PlanGrupoDetDes = " & StringToField(mstrPlanGrupoDetDes)

                case WhereFilters.Grid:
                    Array.Resize(ref moParameters, moParameters.Length + 3);
                    moParameters[3] = new SqlParameter("@PlanGrupoDetId", Convert.ToInt32(0));
                    moParameters[4] = new SqlParameter("@PlanGrupoId", mlngPlanGrupoId);
                    moParameters[5] = new SqlParameter("@EstadoId", Convert.ToInt32(0));
                    break;

                case WhereFilters.GridCheck:
                    break;

                case WhereFilters.PlanGrupoId:
                    Array.Resize(ref moParameters, moParameters.Length + 3);
                    moParameters[3] = new SqlParameter("@PlanGrupoDetId", Convert.ToInt32(0));
                    moParameters[4] = new SqlParameter("@PlanGrupoId", mlngPlanGrupoId);
                    moParameters[5] = new SqlParameter("@EstadoId", Convert.ToInt32(0));
                    break;
            }
        }

        protected override void InsertParameter()
        {
            switch (mintInsertFilter)
            {
                case InsertFilters.All:
                    mstrStoreProcName = "ctbPlanGrupoDetInsert";
                    moParameters = new SqlParameter[10] {
                        new SqlParameter("@InsertFilter", mintInsertFilter),
                        new SqlParameter("@Id", SqlDbType.Int),
                        new SqlParameter("@PlanGrupoId", mlngPlanGrupoId),
                        new SqlParameter("@PlanGrupoDetDes", mstrPlanGrupoDetDes),
                        new SqlParameter("@PlanId", mlngPlanId),
                        new SqlParameter("@PlanFlujoId", mlngPlanFlujoId),
                        new SqlParameter("@SucursalId", mlngSucursalId),
                        new SqlParameter("@CenCosId", mlngCenCosId),
                        new SqlParameter("@Orden", mlngOrden),
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
                    mstrStoreProcName = "ctbPlanGrupoDetUpdate";
                    moParameters = new SqlParameter[10] {
                        new SqlParameter("@UpdateFilter", mintUpdateFilter),
                        new SqlParameter("@PlanGrupoDetId", mlngPlanGrupoDetId),
                        new SqlParameter("@PlanGrupoId", mlngPlanGrupoId),
                        new SqlParameter("@PlanGrupoDetDes", mstrPlanGrupoDetDes),
                        new SqlParameter("@PlanId", mlngPlanId),
                        new SqlParameter("@PlanFlujoId", mlngPlanFlujoId),
                        new SqlParameter("@SucursalId", mlngSucursalId),
                        new SqlParameter("@CenCosId", mlngCenCosId),
                        new SqlParameter("@Orden", mlngOrden),
                        new SqlParameter("@EstadoId", mlngEstadoId)};
                    break;
            }
        }

        protected override void DeleteParameter()
        {
            switch (mintDeleteFilter)
            {
                case DeleteFilters.All:
                    mstrStoreProcName = "ctbPlanGrupoDetDelete";
                    moParameters = new SqlParameter[3] {
                        new SqlParameter("@DeleteFilter", mintDeleteFilter),
                        new SqlParameter("@PlanGrupoDetId", mlngPlanGrupoDetId),
                        new SqlParameter("@PlanGrupoId", Convert.ToInt32(0))};
                    break;

                case DeleteFilters.PlanGrupoId:
                    mstrStoreProcName = "ctbPlanGrupoDetDelete";
                    moParameters = new SqlParameter[3] {
                        new SqlParameter("@DeleteFilter", mintDeleteFilter),
                        new SqlParameter("@PlanGrupoDetId", Convert.ToInt32(0)),
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
                        mlngPlanGrupoDetId = SysData.ToLong(oDataRow["PlanGrupoDetId"]);
                        mlngPlanGrupoId = SysData.ToLong(oDataRow["PlanGrupoId"]);
                        mstrPlanGrupoDetDes = SysData.ToStr(oDataRow["PlanGrupoDetDes"]);
                        mlngPlanId = SysData.ToLong(oDataRow["PlanId"]);
                        mlngPlanFlujoId = SysData.ToLong(oDataRow["PlanFlujoId"]);
                        mlngSucursalId = SysData.ToLong(oDataRow["SucursalId"]);
                        mlngCenCosId = SysData.ToLong(oDataRow["CenCosId"]);
                        mlngOrden = SysData.ToLong(oDataRow["Orden"]);
                        mlngEstadoId = SysData.ToLong(oDataRow["EstadoId"]);
                        break;

                    case SelectFilters.ListBox:
                        mlngPlanGrupoDetId = SysData.ToLong(oDataRow["PlanGrupoDetId"]);
                        mstrPlanGrupoDetDes = SysData.ToStr(oDataRow["PlanGrupoDetDes"]);
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

            //if (mstrPlanGrupoDetDes.Length == 0)
            //{
            //    strMsg += "Descipción del Tipo Usuario es Requerido" + Environment.NewLine;
            //}

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