using System;
using System.Data.SqlClient;
using System.Data;
using Contabilidad.Models.VM;

namespace Contabilidad.Models.DAC
{
    public class clsPlan : clsBase, IDisposable
    {
        public clsPlanVM VM;

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
            PlanPadreId = 6,
            EstadoId = 7,
            TipoPlanId = 8,
            PlanHijoMAXorden = 9
        }

        public enum OrderByFilters : byte
        {
            None = 0,
            PlanId = 1,
            PlanDes = 2,
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
            All = 0,
            Orden = 1
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
            VM = new clsPlanVM();

            VM.PlanId = 0;
            VM.PlanCod = "";
            VM.PlanDes = "";
            VM.PlanEsp = "";
            VM.TipoPlanId = 0;
            VM.Orden = 0;
            VM.Nivel = 0;
            VM.MonedaId = 0;
            VM.TipoAmbitoId = 0;
            VM.PlanAjusteId = 0;
            VM.CapituloId = 0;
            VM.PlanPadreId = 0;
            VM.EstadoId = 0;
        }

        protected override void SetPrimaryKey()
        {
            VM.PlanId = mlngId;
        }

        protected override void SelectParameter()
        {
            string strSQL = null;

            mstrStoreProcName = "ctbPlanSelect";

            switch (mintSelectFilter)
            {
                case SelectFilters.All:
                    strSQL = " SELECT  " +
                           "    ctbPlan.PlanId, " +
                           "    ctbPlan.PlanCod, " +
                           "    ctbPlan.PlanDes, " +
                           "    ctbPlan.PlanEsp, " +
                           "    ctbPlan.TipoPlanId, " +
                           "    ctbPlan.Orden, " +
                           "    ctbPlan.Nivel, " +
                           "    ctbPlan.MonedaId, " +
                           "    ctbPlan.TipoAmbitoId, " +
                           "    ctbPlan.PlanAjusteId, " +
                           "    ctbPlan.CapituloId, " +
                           "    ctbPlan.PlanPadreId, " +
                           "    ctbPlan.EstadoId " +
                           " FROM ctbPlan ";
                    break;

                case SelectFilters.RowCount:
                    break;

                case SelectFilters.ListBox:
                    strSQL = " SELECT  " +
                           "    ctbPlan.PlanId, " +
                           "    ctbPlan.PlanCod, " +
                           "    ctbPlan.PlanDes " +
                           " FROM ctbPlan ";
                    break;

                case SelectFilters.Grid:
                    strSQL = " SELECT  " +
                          "    ctbPlan.PlanId, " +
                          "    ctbPlan.PlanCod, " +
                          "    ctbPlan.PlanDes, " +
                          "    ctbTipoPlan.TipoPlanId, " +
                          "    ctbTipoPlan.TipoPlanDes, " +
                          "    ctbPlan.Orden, " +
                          "    ctbPlan.Nivel, " +
                          "    ctbPlan.MonedaId, " +
                          "    parMoneda.MonedaDes, " +
                          "    ctbPlan.CapituloId, " +
                          "    ctbPlan.PlanPadreId, " +
                          "    ctbPlan.EstadoId, " +
                          "    parEstado.EstadoDes " +
                          " FROM ctbPlan "+
                          "    LEFT JOIN	ctbTipoPlan		ON ctbPlan.TipoPlanId = ctbTipoPlan.TipoPlanId  " +
                          "    LEFT JOIN	parMoneda		ON ctbPlan.MonedaId = parMoneda.MonedaId  " +
                          "    LEFT JOIN	parEstado		ON ctbPlan.EstadoId = parEstado.EstadoId   ";
                    break;

            }

            strSQL += WhereFilterGet() + OrderByFilterGet();

            Array.Resize(ref moParameters, 1);
            moParameters[0] = new SqlParameter("@SQL", strSQL);
        }

        

        private string WhereFilterGet()
        {
            string strSQL = null;

            switch (mintWhereFilter)
            {
                case WhereFilters.PrimaryKey:
                    strSQL = " WHERE ctbPlan.PlanId = " + SysData.NumberToField(VM.PlanId);
                    break;

                case WhereFilters.PlanDes:
                    strSQL = " WHERE ctbPlan.PlanDes = " + SysData.StringToField(VM.PlanDes);
                    break;

                case WhereFilters.Grid:
                    break;

                case WhereFilters.PlanCod:
                    strSQL = " WHERE ctbPlan.PlanCod = " + SysData.StringToField(VM.PlanCod);
                    break;

                case WhereFilters.GridCheck:
                    break;

                case WhereFilters.PlanPadreId:
                    strSQL = " WHERE ctbPlan.PlanPadreId = " + SysData.NumberToField(VM.PlanPadreId);
                    break;

                case WhereFilters.EstadoId:
                    strSQL = " WHERE ctbPlan.EstadoId = " + SysData.NumberToField(VM.EstadoId);
                    break;

                case WhereFilters.TipoPlanId:
                    strSQL = " WHERE ctbPlan.TipoPlanId = " + SysData.NumberToField(VM.TipoPlanId);
                    break;

                case WhereFilters.PlanHijoMAXorden:
                    strSQL = " WHERE ctbPlan.PlanPadreId = " + SysData.NumberToField(VM.PlanPadreId) +
                            "  AND   ctbPlan.EstadoId = " + SysData.NumberToField(VM.EstadoId) +
                            "  AND   ctbPlan.Orden = " +
                                         " ( SELECT MAX(Orden) FROM ctbPlan " +
                                          "  WHERE	PlanPadreId = "+ SysData.NumberToField(VM.PlanPadreId) + ")";
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
                case OrderByFilters.PlanId:
                    strSQL = " ORDER BY  ctbPlan.PlanId ";
                    break;
                case OrderByFilters.PlanDes:
                    strSQL = " ORDER BY  ctbPlan.PlanDes ";
                    break;
                case OrderByFilters.Grid:
                    strSQL = " ORDER BY  ctbPlan.PlanCod, ctbPlan.Orden ";
                    break;
                case OrderByFilters.GridCheck:
                    break;
                case OrderByFilters.Orden:
                    strSQL = " ORDER BY  ctbPlan.Orden ";
                    break;
            }

            return strSQL;
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
                        new SqlParameter(clsPlanVM._PlanCod, VM.PlanCod),
                        new SqlParameter(clsPlanVM._PlanDes, VM.PlanDes),
                        new SqlParameter(clsPlanVM._PlanEsp, VM.PlanEsp),
                        new SqlParameter(clsPlanVM._TipoPlanId, VM.TipoPlanId),
                        new SqlParameter(clsPlanVM._Orden, VM.Orden),
                        new SqlParameter(clsPlanVM._Nivel, VM.Nivel),
                        new SqlParameter(clsPlanVM._MonedaId, VM.MonedaId),
                        new SqlParameter(clsPlanVM._TipoAmbitoId, VM.TipoAmbitoId),
                        new SqlParameter(clsPlanVM._PlanAjusteId, VM.PlanAjusteId),
                        new SqlParameter(clsPlanVM._CapituloId, VM.CapituloId),
                        new SqlParameter(clsPlanVM._PlanPadreId, VM.PlanPadreId),
                        new SqlParameter(clsPlanVM._EstadoId, VM.EstadoId)};
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
                        new SqlParameter(clsPlanVM._PlanId, VM.PlanId),
                        new SqlParameter(clsPlanVM._PlanCod, VM.PlanCod),
                        new SqlParameter(clsPlanVM._PlanDes, VM.PlanDes),
                        new SqlParameter(clsPlanVM._PlanEsp, VM.PlanEsp),
                        new SqlParameter(clsPlanVM._TipoPlanId, VM.TipoPlanId),
                        new SqlParameter(clsPlanVM._Orden, VM.Orden),
                        new SqlParameter(clsPlanVM._Nivel, VM.Nivel),
                        new SqlParameter(clsPlanVM._MonedaId, VM.MonedaId),
                        new SqlParameter(clsPlanVM._TipoAmbitoId, VM.TipoAmbitoId),
                        new SqlParameter(clsPlanVM._PlanAjusteId, VM.PlanAjusteId),
                        new SqlParameter(clsPlanVM._CapituloId, VM.CapituloId),
                        new SqlParameter(clsPlanVM._PlanPadreId, VM.PlanPadreId),
                        new SqlParameter(clsPlanVM._EstadoId, VM.EstadoId)};
                    break;

                case UpdateFilters.Orden:
                    mstrStoreProcName = "ctbPlanUpdate";
                    moParameters = new SqlParameter[14] {
                        new SqlParameter("@UpdateFilter", mintUpdateFilter),  // la diferencia entre All, es aqui solo importa el PlanId y el Orden
                        new SqlParameter(clsPlanVM._PlanId, VM.PlanId),
                        new SqlParameter(clsPlanVM._PlanCod, Convert.ToString("")),
                        new SqlParameter(clsPlanVM._PlanDes, Convert.ToString("")),
                        new SqlParameter(clsPlanVM._PlanEsp, Convert.ToString("")),
                        new SqlParameter(clsPlanVM._TipoPlanId, Convert.ToInt32(0)),
                        new SqlParameter(clsPlanVM._Orden, VM.Orden),
                        new SqlParameter(clsPlanVM._Nivel, Convert.ToInt32(0)),
                        new SqlParameter(clsPlanVM._MonedaId, Convert.ToInt32(0)),
                        new SqlParameter(clsPlanVM._TipoAmbitoId, Convert.ToInt32(0)),
                        new SqlParameter(clsPlanVM._PlanAjusteId, Convert.ToInt32(0)),
                        new SqlParameter(clsPlanVM._CapituloId, Convert.ToInt32(0)),
                        new SqlParameter(clsPlanVM._PlanPadreId, Convert.ToInt32(0)),
                        new SqlParameter(clsPlanVM._EstadoId, Convert.ToInt32(0))};
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
                        new SqlParameter(clsPlanVM._PlanId, VM.PlanId)};
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
                        VM.PlanId = SysData.ToLong(oDataRow[clsPlanVM._PlanId]);
                        VM.PlanCod = SysData.ToStr(oDataRow[clsPlanVM._PlanCod]);
                        VM.PlanDes = SysData.ToStr(oDataRow[clsPlanVM._PlanDes]);
                        VM.PlanEsp = SysData.ToStr(oDataRow[clsPlanVM._PlanEsp]);
                        VM.TipoPlanId = SysData.ToLong(oDataRow[clsPlanVM._TipoPlanId]);
                        VM.Orden = SysData.ToLong(oDataRow[clsPlanVM._Orden]);
                        VM.Nivel = SysData.ToLong(oDataRow[clsPlanVM._Nivel]);
                        VM.MonedaId = SysData.ToLong(oDataRow[clsPlanVM._MonedaId]);
                        VM.TipoAmbitoId = SysData.ToLong(oDataRow[clsPlanVM._TipoAmbitoId]);
                        VM.PlanAjusteId = SysData.ToLong(oDataRow[clsPlanVM._PlanAjusteId]);
                        VM.CapituloId = SysData.ToLong(oDataRow[clsPlanVM._CapituloId]);
                        VM.PlanPadreId = SysData.ToLong(oDataRow[clsPlanVM._PlanPadreId]);
                        VM.EstadoId = SysData.ToLong(oDataRow[clsPlanVM._EstadoId]);
                        break;

                    case SelectFilters.ListBox:
                        VM.PlanId = SysData.ToLong(oDataRow[clsPlanVM._PlanId]);
                        VM.PlanCod = SysData.ToStr(oDataRow[clsPlanVM._PlanCod]);
                        VM.PlanDes = SysData.ToStr(oDataRow[clsPlanVM._PlanDes]);
                        break;

                    case SelectFilters.Grid:
                        VM.PlanId = SysData.ToLong(oDataRow[clsPlanVM._PlanId]);
                        VM.PlanCod = SysData.ToStr(oDataRow[clsPlanVM._PlanCod]);
                        VM.PlanDes = SysData.ToStr(oDataRow[clsPlanVM._PlanDes]);
                        VM.TipoPlanId = SysData.ToLong(oDataRow[clsPlanVM._TipoPlanId]);
                        VM.TipoPlanDes = SysData.ToStr(oDataRow[clsPlanVM._TipoPlanDes]);
                        VM.Orden = SysData.ToLong(oDataRow[clsPlanVM._Orden]);
                        VM.Nivel = SysData.ToLong(oDataRow[clsPlanVM._Nivel]);
                        VM.MonedaId = SysData.ToLong(oDataRow[clsPlanVM._MonedaId]);
                        VM.MonedaDes = SysData.ToStr(oDataRow[clsPlanVM._MonedaDes]);
                        VM.CapituloId = SysData.ToLong(oDataRow[clsPlanVM._CapituloId]);
                        VM.PlanPadreId = SysData.ToLong(oDataRow[clsPlanVM._PlanPadreId]);
                        VM.EstadoId = SysData.ToLong(oDataRow[clsPlanVM._EstadoId]);
                        VM.EstadoDes = SysData.ToStr(oDataRow[clsPlanVM._EstadoDes]);
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

            if (VM.PlanCod.Length == 0)
            {
                strMsg += "Código es Requerido" + Environment.NewLine;
            }

            if (VM.PlanDes.Length == 0)
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

        public int getMintRowsCount() {
            return mintRowsCount;
        }

    }
}