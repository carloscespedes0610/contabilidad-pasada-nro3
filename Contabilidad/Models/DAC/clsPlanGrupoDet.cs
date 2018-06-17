using System;
using System.Data.SqlClient;
using System.Data;
using Contabilidad.Models.VM;

namespace Contabilidad.Models.DAC
{
    public class clsPlanGrupoDet : clsBase, IDisposable
    {
        public clsPlanGrupoDetVM VM;

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
            VM = new clsPlanGrupoDetVM();

            VM.PlanGrupoDetId = 0;
            VM.PlanGrupoId = 0;
            VM.PlanGrupoDetDes = "";
            VM.PlanId = 0;
            VM.PlanFlujoId = 0;
            VM.SucursalId = 0;
            VM.CenCosId = 0;
            VM.Orden = 0;
            VM.EstadoId = 0;
        }

        protected override void SetPrimaryKey()
        {
            VM.PlanGrupoDetId = mlngId;
        }

        protected override void SelectParameter()
        {
            string strSQL = null;

            mstrStoreProcName = "ctbPlanGrupoDetSelect";

            switch (mintSelectFilter)
            {
                case SelectFilters.All:
                    strSQL = " SELECT  " +
                           "    ctbPlanGrupoDet.PlanGrupoDetId, " +
                           "    ctbPlanGrupoDet.PlanGrupoId , " +
                           "    ctbPlanGrupoDet.PlanGrupoDetDes , " +
                           "    ctbPlanGrupoDet.PlanId , " +
                           "    ctbPlanGrupoDet.PlanFlujoId , " +
                           "    ctbPlanGrupoDet.SucursalId , " +
                           "    ctbPlanGrupoDet.CenCosId , " +
                           "    ctbPlanGrupoDet.Orden , " +
                           "    ctbPlanGrupoDet.EstadoId " +
                           " FROM ctbPlanGrupoDet ";
                    break;

                case SelectFilters.RowCount:
                    mstrStoreProcName = "ctbPlanGrupoDetSelect";
                    break;

                case SelectFilters.ListBox:
                    strSQL = " SELECT  " +
                           "    ctbPlanGrupoDet.PlanGrupoDetId, " +
                           "    ctbPlanGrupoDet.PlanGrupoId , " +
                           "    ctbPlanGrupoDet.PlanGrupoDetDes , " +
                           " FROM ctbPlanGrupoDet ";
                    break;

                case SelectFilters.Grid:
                    strSQL = " SELECT  " +
                           "    ctbPlanGrupoDet.PlanGrupoDetId, " +
                           "    ctbPlanGrupo.PlanGrupoId, " +
                           "    ctbPlanGrupo.PlanGrupoDes, " +
                           "    ctbPlanGrupoDet.PlanGrupoDetDes, " +
                           "    ctbPlan.PlanId,  " +
                           "    ctbPlan.PlanDes, " +
                           "    ctbPlanGrupoDet.PlanFlujoId, " +
                           "    ctbSucursal.SucursalId, " +
                           "    ctbSucursal.SucursalDes, " +
                           "    ctbCenCos.CenCosId, " +
                           "    ctbCenCos.CenCosDes, " +
                           "    ctbPlanGrupoDet.Orden, " +
                           "    parEstado.EstadoId, " +
                           "    parEstado.EstadoDes " +
                           " FROM ctbPlanGrupoDet  " +
                           "    LEFT JOIN	ctbPlanGrupo	ON ctbPlanGrupoDet.PlanGrupoId = ctbPlanGrupo.PlanGrupoId " +
                           "    LEFT JOIN	ctbPlan			ON ctbPlanGrupoDet.PlanId = ctbPlan.PlanId " +
                           "    LEFT JOIN	ctbSucursal		ON ctbPlanGrupoDet.SucursalId = ctbSucursal.SucursalId	 " +
                           "    LEFT JOIN	ctbCenCos		ON ctbPlanGrupoDet.CenCosId = ctbCenCos.CenCosId " +
                           "    LEFT JOIN	parEstado		ON ctbPlanGrupoDet.EstadoId = parEstado.EstadoId  ";
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
                case WhereFilters.PrimaryKey:
                    strSQL = " WHERE ctbPlanGrupoDet.PlanGrupoDetId = " + SysData.NumberToField(VM.PlanGrupoDetId);
                    break;

                case WhereFilters.PlanGrupoDetDes:
                    strSQL = " WHERE ctbPlanGrupoDet.PlanGrupoDes = " + SysData.StringToField(VM.PlanGrupoDetDes);
                    break;

                case WhereFilters.Grid:
                    strSQL = " WHERE ctbPlanGrupoDet.PlanGrupoId = " + SysData.NumberToField(VM.PlanGrupoId);
                    break;

                case WhereFilters.GridCheck:
                    break;

                case WhereFilters.PlanGrupoId:
                    strSQL = " WHERE ctbPlanGrupoDet.PlanGrupoId = " + SysData.NumberToField(VM.PlanGrupoId);
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
                case OrderByFilters.PlanGrupoDetId:
                    strSQL = " ORDER BY ctbPlanGrupoDet.PlanGrupoDetId ";
                    break;
                case OrderByFilters.PlanGrupoDetDes:
                    strSQL = " ORDER BY ctbPlanGrupoDet.PlanGrupoDetDes ";
                    break;
                case OrderByFilters.Grid:
                    strSQL = " ORDER BY ctbPlanGrupoDet.Orden ";
                    break;
                case OrderByFilters.GridCheck:
                    break;
                case OrderByFilters.Orden:
                    strSQL = " ORDER BY ctbPlanGrupoDet.Orden ";
                    break;
                
            }
            return strSQL;

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
                        new SqlParameter(clsPlanGrupoDetVM._PlanGrupoId, VM.PlanGrupoId),
                        new SqlParameter(clsPlanGrupoDetVM._PlanGrupoDetDes, VM.PlanGrupoDetDes),
                        new SqlParameter(clsPlanGrupoDetVM._PlanId, VM.PlanId),
                        new SqlParameter(clsPlanGrupoDetVM._PlanFlujoId, VM.PlanFlujoId),
                        new SqlParameter(clsPlanGrupoDetVM._SucursalId, VM.SucursalId),
                        new SqlParameter(clsPlanGrupoDetVM._CenCosId, VM.CenCosId),
                        new SqlParameter(clsPlanGrupoDetVM._Orden, VM.Orden),
                        new SqlParameter(clsPlanGrupoDetVM._EstadoId, VM.EstadoId)};
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
                        new SqlParameter(clsPlanGrupoDetVM._PlanGrupoDetId, VM.PlanGrupoDetId),
                        new SqlParameter(clsPlanGrupoDetVM._PlanGrupoId, VM.PlanGrupoId),
                        new SqlParameter(clsPlanGrupoDetVM._PlanGrupoDetDes, VM.PlanGrupoDetDes),
                        new SqlParameter(clsPlanGrupoDetVM._PlanId, VM.PlanId),
                        new SqlParameter(clsPlanGrupoDetVM._PlanFlujoId, VM.PlanFlujoId),
                        new SqlParameter(clsPlanGrupoDetVM._SucursalId, VM.SucursalId),
                        new SqlParameter(clsPlanGrupoDetVM._CenCosId, VM.CenCosId),
                        new SqlParameter(clsPlanGrupoDetVM._Orden, VM.Orden),
                        new SqlParameter(clsPlanGrupoDetVM._EstadoId, VM.EstadoId)};
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
                        new SqlParameter(clsPlanGrupoDetVM._PlanGrupoDetId, VM.PlanGrupoDetId),
                        new SqlParameter(clsPlanGrupoDetVM._PlanGrupoId, Convert.ToInt32(0))};
                    break;

                case DeleteFilters.PlanGrupoId:
                    mstrStoreProcName = "ctbPlanGrupoDetDelete";
                    moParameters = new SqlParameter[3] {
                        new SqlParameter("@DeleteFilter", mintDeleteFilter),
                        new SqlParameter(clsPlanGrupoDetVM._PlanGrupoDetId, Convert.ToInt32(0)),
                        new SqlParameter(clsPlanGrupoDetVM._PlanGrupoId, VM.PlanGrupoId)};
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
                        VM.PlanGrupoDetId = SysData.ToLong(oDataRow[clsPlanGrupoDetVM._PlanGrupoDetId]);
                        VM.PlanGrupoId = SysData.ToLong(oDataRow[clsPlanGrupoDetVM._PlanGrupoId]);
                        VM.PlanGrupoDetDes = SysData.ToStr(oDataRow[clsPlanGrupoDetVM._PlanGrupoDetDes]);
                        VM.PlanId = SysData.ToLong(oDataRow[clsPlanGrupoDetVM._PlanId]);
                        VM.PlanFlujoId = SysData.ToLong(oDataRow[clsPlanGrupoDetVM._PlanFlujoId]);
                        VM.SucursalId = SysData.ToLong(oDataRow[clsPlanGrupoDetVM._SucursalId]);
                        VM.CenCosId = SysData.ToLong(oDataRow[clsPlanGrupoDetVM._CenCosId]);
                        VM.Orden = SysData.ToLong(oDataRow[clsPlanGrupoDetVM._Orden]);
                        VM.EstadoId = SysData.ToLong(oDataRow[clsPlanGrupoDetVM._EstadoId]);
                        break;

                    case SelectFilters.ListBox:
                        VM.PlanGrupoDetId = SysData.ToLong(oDataRow[clsPlanGrupoDetVM._PlanGrupoDetId]);
                        VM.PlanGrupoDetDes = SysData.ToStr(oDataRow[clsPlanGrupoDetVM._PlanGrupoDetDes]);
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