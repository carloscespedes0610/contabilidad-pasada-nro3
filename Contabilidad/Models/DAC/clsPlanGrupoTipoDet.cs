using System;
using System.Data.SqlClient;
using System.Data;
using Contabilidad.Models.VM;

namespace Contabilidad.Models.DAC
{
    public class clsPlanGrupoTipoDet : clsBase, IDisposable
    {
        public clsPlanGrupoTipoDetVM VM;

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
            PlanGrupoTipoDetDes = 2,
            Grid = 3,
            GridCheck = 4,
            PlanGrupoTipoDetCod = 5,
        }

        public enum OrderByFilters : byte
        {
            None = 0,
            PlanGrupoTipoDetId = 1,
            PlanGrupoTipoDetDes = 2,
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
        public clsPlanGrupoTipoDet()
        {
            mstrTableName = "ctbPlanGrupoTipoDet";
            mstrClassName = "clsPlanGrupoTipoDet";

            PropertyInit();
            FilterInit();
        }

        public clsPlanGrupoTipoDet(string ConnectString) : this()
        {
            moConnection = new SqlConnection();

            mstrConnectionString = ConnectString;
        }

        public clsPlanGrupoTipoDet(SqlConnection oConnection) : this()
        {
            moConnection = oConnection;
        }

        public clsPlanGrupoTipoDet(SqlConnection oConnection, SelectFilters bytSelectFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
        }

        public clsPlanGrupoTipoDet(SqlConnection oConnection, SelectFilters bytSelectFilter, WhereFilters bytWhereFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
            mintWhereFilter = bytWhereFilter;
        }

        public clsPlanGrupoTipoDet(SqlConnection oConnection, SelectFilters bytSelectFilter, WhereFilters bytWhereFilter, OrderByFilters bytOrderByFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
            mintWhereFilter = bytWhereFilter;
            mintOrderByFilter = bytOrderByFilter;
        }

        public void PropertyInit()
        {
            VM = new clsPlanGrupoTipoDetVM();
            VM.PlanGrupoTipoDetId = 0;
            VM.PlanGrupoTipoDetCod = "";
            VM.PlanGrupoTipoDetDes = "";
            VM.PlanGrupoTipoDetEsp = "";
            VM.PlanGrupoTipoId = 0;
            VM.EstadoId = 0;
        }

        protected override void SetPrimaryKey()
        {
            VM.PlanGrupoTipoDetId = mlngId;
        }

        protected override void SelectParameter()
        {
            string strSQL = null;

            mstrStoreProcName = "ctbPlanGrupoTipoDetSelect";

            switch (mintSelectFilter)
            {
                case SelectFilters.All:
                    strSQL = " SELECT  " +
                           "    ctbPlanGrupoTipoDet.PlanGrupoTipoDetId, " +
                           "    ctbPlanGrupoTipoDet.PlanGrupoTipoDetCod, " +
                           "    ctbPlanGrupoTipoDet.PlanGrupoTipoDetDes, " +
                           "    ctbPlanGrupoTipoDet.PlanGrupoTipoDetEsp, " +
                           "    ctbPlanGrupoTipoDet.PlanGrupoTipoId, " +
                           "    ctbPlanGrupoTipoDet.EstadoId " +
                           " FROM ctbPlanGrupoTipoDet ";
                    break;

                case SelectFilters.RowCount:
                    mstrStoreProcName = "ctbPlanGrupoTipoDetSelect";
                    break;

                case SelectFilters.ListBox:
                    strSQL = " SELECT  " +
                           "    ctbPlanGrupoTipoDet.PlanGrupoTipoDetId, " +
                           "    ctbPlanGrupoTipoDet.PlanGrupoTipoDetCod, " +
                           "    ctbPlanGrupoTipoDet.PlanGrupoTipoDetDes, " +
                           "    ctbPlanGrupoTipoDet.PlanGrupoTipoId " +
                           " FROM ctbPlanGrupoTipoDet ";
                    break;

                case SelectFilters.Grid:
                    strSQL = " SELECT  " +
                          "    ctbPlanGrupoTipoDet.PlanGrupoTipoDetId, " +
                          "    ctbPlanGrupoTipoDet.PlanGrupoTipoDetCod, " +
                          "    ctbPlanGrupoTipoDet.PlanGrupoTipoDetDes, " +
                          "    ctbPlanGrupoTipoDet.PlanGrupoTipoDetEsp, " +
                          "    ctbPlanGrupoTipoDet.PlanGrupoTipoId, " +
                          "    ctbPlanGrupoTipo.PlanGrupoTipoDes, " +
                          "    ctbPlanGrupoTipoDet.EstadoId, " +
                          "    parEstado.EstadoDes " +
                          " FROM ctbPlanGrupoTipoDet " +
                          "    LEFT JOIN	ctbPlanGrupoTipo	ON ctbPlanGrupoTipoDet.PlanGrupoTipoId = ctbPlanGrupoTipo.PlanGrupoTipoId  " +
                          "    LEFT JOIN	parEstado		ON ctbPlanGrupoTipoDet.EstadoId = parEstado.EstadoId ";
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
                    strSQL = " WHERE ctbPlanGrupoTipoDet.PlanGrupoTipoDetId = " + SysData.NumberToField(VM.PlanGrupoTipoDetId);
                    break;

                case WhereFilters.PlanGrupoTipoDetDes:
                    strSQL = " WHERE ctbPlanGrupoTipoDet.PlanGrupoTipoDetDes = " + SysData.StringToField(VM.PlanGrupoTipoDetDes);
                    break;

                case WhereFilters.Grid:
                    break;

                case WhereFilters.PlanGrupoTipoDetCod:
                    strSQL = " WHERE ctbPlanGrupoTipoDet.PlanGrupoTipoDetCod = " + SysData.StringToField(VM.PlanGrupoTipoDetCod);
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
                case OrderByFilters.None:
                    break;
                case OrderByFilters.PlanGrupoTipoDetId:
                    strSQL = " ORDER BY ctbPlanGrupoTipoDet.PlanGrupoTipoDetId ";
                    break;
                case OrderByFilters.PlanGrupoTipoDetDes:
                    strSQL = " ORDER BY ctbPlanGrupoTipoDet.PlanGrupoTipoDetDes ";
                    break;
                case OrderByFilters.Grid:
                    strSQL = " ORDER BY ctbPlanGrupoTipo.PlanGrupoTipoDes, ctbPlanGrupoTipoDet.PlanGrupoTipoDetDes ";
                    break;
                case OrderByFilters.GridCheck:
                    break;
            }

            return strSQL;

        }


        protected override void InsertParameter()
        {
            switch (mintInsertFilter)
            {
                case InsertFilters.All:
                    mstrStoreProcName = "ctbPlanGrupoTipoDetInsert";
                    moParameters = new SqlParameter[7] {
                        new SqlParameter("@InsertFilter", mintInsertFilter),
                        new SqlParameter("@Id", SqlDbType.Int),
                        new SqlParameter(clsPlanGrupoTipoDetVM._PlanGrupoTipoDetCod, VM.PlanGrupoTipoDetCod),
                        new SqlParameter(clsPlanGrupoTipoDetVM._PlanGrupoTipoDetDes, VM.PlanGrupoTipoDetDes),
                        new SqlParameter(clsPlanGrupoTipoDetVM._PlanGrupoTipoDetEsp, VM.PlanGrupoTipoDetEsp),
                        new SqlParameter(clsPlanGrupoTipoDetVM._PlanGrupoTipoId, VM.PlanGrupoTipoId),
                        new SqlParameter(clsPlanGrupoTipoDetVM._EstadoId, VM.EstadoId)};
                    moParameters[1].Direction = ParameterDirection.Output;
                    break;
            }
        }

        protected override void UpdateParameter()
        {
            switch (mintUpdateFilter)
            {
                case UpdateFilters.All:
                    mstrStoreProcName = "ctbPlanGrupoTipoDetUpdate";
                    moParameters = new SqlParameter[7] {
                        new SqlParameter("@UpdateFilter", mintUpdateFilter),
                        new SqlParameter(clsPlanGrupoTipoDetVM._PlanGrupoTipoDetId, VM.PlanGrupoTipoDetId),
                        new SqlParameter(clsPlanGrupoTipoDetVM._PlanGrupoTipoDetCod, VM.PlanGrupoTipoDetCod),
                        new SqlParameter(clsPlanGrupoTipoDetVM._PlanGrupoTipoDetDes, VM.PlanGrupoTipoDetDes),
                        new SqlParameter(clsPlanGrupoTipoDetVM._PlanGrupoTipoDetEsp, VM.PlanGrupoTipoDetEsp),
                        new SqlParameter(clsPlanGrupoTipoDetVM._PlanGrupoTipoId, VM.PlanGrupoTipoId),
                        new SqlParameter(clsPlanGrupoTipoDetVM._EstadoId, VM.EstadoId)};
                    break;
            }
        }

        protected override void DeleteParameter()
        {
            switch (mintDeleteFilter)
            {
                case DeleteFilters.All:
                    mstrStoreProcName = "ctbPlanGrupoTipoDetDelete";
                    moParameters = new SqlParameter[2] {
                        new SqlParameter("@DeleteFilter", mintDeleteFilter),
                        new SqlParameter(clsPlanGrupoTipoDetVM._PlanGrupoTipoDetId, VM.PlanGrupoTipoDetId)};
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
                        VM.PlanGrupoTipoDetId = SysData.ToLong(oDataRow[clsPlanGrupoTipoDetVM._PlanGrupoTipoDetId]);
                        VM.PlanGrupoTipoDetCod = SysData.ToStr(oDataRow[clsPlanGrupoTipoDetVM._PlanGrupoTipoDetCod]);
                        VM.PlanGrupoTipoDetDes = SysData.ToStr(oDataRow[clsPlanGrupoTipoDetVM._PlanGrupoTipoDetDes]);
                        VM.PlanGrupoTipoDetEsp = SysData.ToStr(oDataRow[clsPlanGrupoTipoDetVM._PlanGrupoTipoDetEsp]);
                        VM.PlanGrupoTipoId = SysData.ToLong(oDataRow[clsPlanGrupoTipoDetVM._PlanGrupoTipoId]);
                        VM.EstadoId = SysData.ToLong(oDataRow[clsPlanGrupoTipoDetVM._EstadoId]);
                        break;

                    case SelectFilters.ListBox:
                        VM.PlanGrupoTipoDetId = SysData.ToLong(oDataRow[clsPlanGrupoTipoDetVM._PlanGrupoTipoDetId]);
                        VM.PlanGrupoTipoDetCod = SysData.ToStr(oDataRow[clsPlanGrupoTipoDetVM._PlanGrupoTipoDetCod]);
                        VM.PlanGrupoTipoDetDes = SysData.ToStr(oDataRow[clsPlanGrupoTipoDetVM._PlanGrupoTipoDetDes]);
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

            if (VM.PlanGrupoTipoDetCod.Length == 0)
            {
                strMsg += "Código es Requerido" + Environment.NewLine;
            }

            if (VM.PlanGrupoTipoDetDes.Length == 0)
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