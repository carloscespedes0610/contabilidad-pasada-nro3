using System;
using System.Data.SqlClient;
using System.Data;
using Contabilidad.Models.VM;

namespace Contabilidad.Models.DAC
{
    public class clsPlanGrupo : clsBase, IDisposable
    {

        public clsPlanGrupoVM VM;

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
            VM = new clsPlanGrupoVM();

            VM.PlanGrupoId = 0;
            VM.PlanGrupoCod = "";
            VM.PlanGrupoDes = "";
            VM.PlanGrupoEsp = "";
            VM.PlanGrupoTipoId = 0;
            VM.PlanGrupoTipoDetId = 0;
            VM.NroCuentas = 0;
            VM.MonedaId = 0;
            VM.VerificaMto = false;
            VM.EstadoId = 0;
        }

        protected override void SetPrimaryKey()
        {
            VM.PlanGrupoId = mlngId;
        }

        protected override void SelectParameter()
        {
            string strSQL = null;

            mstrStoreProcName = "ctbPlanGrupoSelect";

            switch (mintSelectFilter)
            {
                case SelectFilters.All:
                    strSQL = " SELECT  " +
                           "    ctbPlanGrupo.PlanGrupoId, " +
                           "    ctbPlanGrupo.PlanGrupoCod, " +
                           "    ctbPlanGrupo.PlanGrupoDes, " +
                           "    ctbPlanGrupo.PlanGrupoEsp, " +
                           "    ctbPlanGrupo.PlanGrupoTipoId, " +
                           "    ctbPlanGrupo.PlanGrupoTipoDetId, " +
                           "    ctbPlanGrupo.NroCuentas, " +
                           "    ctbPlanGrupo.MonedaId, " +
                           "    ctbPlanGrupo.VerificaMto, " +
                           "    ctbPlanGrupo.EstadoId " +
                           " FROM ctbPlanGrupo ";
                    break;

                case SelectFilters.RowCount:
                    break;

                case SelectFilters.ListBox:
                    strSQL = " SELECT  " +
                           "    ctbPlanGrupo.PlanGrupoId, " +
                           "    ctbPlanGrupo.PlanGrupoCod, " +
                           "    ctbPlanGrupo.PlanGrupoDes " +
                           " FROM ctbPlanGrupo ";
                    break;

                case SelectFilters.Grid:
                    strSQL = " SELECT  " +
                           "    ctbPlanGrupo.PlanGrupoId, " +
                           "    ctbPlanGrupo.PlanGrupoCod, " +
                           "    ctbPlanGrupo.PlanGrupoDes, " +
                           "    ctbPlanGrupo.PlanGrupoEsp, " +
                           "    ctbPlanGrupo.PlanGrupoTipoId, " +
                           "    ctbPlanGrupoTipo.PlanGrupoTipoDes, " +
                           "    ctbPlanGrupoTipoDet.PlanGrupoTipoDetId, " +
                           "    ctbPlanGrupoTipoDet.PlanGrupoTipoDetDes, " +
                           "    ctbPlanGrupo.NroCuentas, " +
                           "    ctbPlanGrupo.MonedaId, " +
                           "    parMoneda.MonedaDes, " +
                           "    ctbPlanGrupo.VerificaMto, " +
                           "    ctbPlanGrupo.EstadoId, " +
                           "    parEstado.EstadoDes  " +
                           " FROM ctbPlanGrupo    " +
                           "    LEFT JOIN	ctbPlanGrupoTipo	ON ctbPlanGrupo.PlanGrupoTipoId = ctbPlanGrupoTipo.PlanGrupoTipoId  " +
                           "    LEFT JOIN	ctbPlanGrupoTipoDet	ON ctbPlanGrupo.PlanGrupoTipoDetId = ctbPlanGrupoTipoDet.PlanGrupoTipoDetId  " +
                           "    LEFT JOIN	parMoneda			ON ctbPlanGrupo.MonedaId = parMoneda.MonedaId  " +
                           "    LEFT JOIN	parEstado			ON ctbPlanGrupo.EstadoId = parEstado.EstadoId ";
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
                    strSQL = " WHERE ctbPlanGrupo.PlanGrupoId = " + SysData.NumberToField(VM.PlanGrupoId);
                    break;

                case WhereFilters.PlanGrupoDes:
                    strSQL = " WHERE ctbPlanGrupo.PlanGrupoDes = " + SysData.StringToField(VM.PlanGrupoDes);
                    break;

                case WhereFilters.Grid:
                    break;

                case WhereFilters.PlanGrupoCod:
                    strSQL = " WHERE ctbPlanGrupo.PlanGrupoCod = " + SysData.StringToField(VM.PlanGrupoDes);
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
                case OrderByFilters.PlanGrupoId:
                    strSQL = " ORDER BY ctbPlanGrupo.PlanGrupoId ";
                    break;
                case OrderByFilters.PlanGrupoDes:
                    strSQL = " ORDER BY ctbPlanGrupo.PlanGrupoDes ";
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
                    mstrStoreProcName = "ctbPlanGrupoInsert";
                    moParameters = new SqlParameter[11] {
                        new SqlParameter("@InsertFilter", mintInsertFilter),
                        new SqlParameter("@Id", SqlDbType.Int),
                        new SqlParameter(clsPlanGrupoVM._PlanGrupoCod, VM.PlanGrupoCod),
                        new SqlParameter(clsPlanGrupoVM._PlanGrupoDes, VM.PlanGrupoDes),
                        new SqlParameter(clsPlanGrupoVM._PlanGrupoEsp, VM.PlanGrupoEsp),
                        new SqlParameter(clsPlanGrupoVM._PlanGrupoTipoId, VM.PlanGrupoTipoId),
                        new SqlParameter(clsPlanGrupoVM._PlanGrupoTipoDetId, VM.PlanGrupoTipoDetId),
                        new SqlParameter(clsPlanGrupoVM._NroCuentas, VM.NroCuentas),
                        new SqlParameter(clsPlanGrupoVM._MonedaId, VM.MonedaId),
                        new SqlParameter(clsPlanGrupoVM._VerificaMto, VM.VerificaMto),
                        new SqlParameter(clsPlanGrupoVM._EstadoId, VM.EstadoId)};
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
                        new SqlParameter(clsPlanGrupoVM._PlanGrupoId, VM.PlanGrupoId),
                        new SqlParameter(clsPlanGrupoVM._PlanGrupoCod, VM.PlanGrupoCod),
                        new SqlParameter(clsPlanGrupoVM._PlanGrupoDes, VM.PlanGrupoDes),
                        new SqlParameter(clsPlanGrupoVM._PlanGrupoEsp, VM.PlanGrupoEsp),
                        new SqlParameter(clsPlanGrupoVM._PlanGrupoTipoId, VM.PlanGrupoTipoId),
                        new SqlParameter(clsPlanGrupoVM._PlanGrupoTipoDetId, VM.PlanGrupoTipoDetId),
                        new SqlParameter(clsPlanGrupoVM._NroCuentas, VM.NroCuentas),
                        new SqlParameter(clsPlanGrupoVM._MonedaId, VM.MonedaId),
                        new SqlParameter(clsPlanGrupoVM._VerificaMto, VM.VerificaMto),
                        new SqlParameter(clsPlanGrupoVM._EstadoId, VM.EstadoId)};
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
                        new SqlParameter(clsPlanGrupoVM._PlanGrupoId, VM.PlanGrupoId)};
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
                        VM.PlanGrupoId = SysData.ToLong(oDataRow[clsPlanGrupoVM._PlanGrupoId]);
                        VM.PlanGrupoCod = SysData.ToStr(oDataRow[clsPlanGrupoVM._PlanGrupoCod]);
                        VM.PlanGrupoDes = SysData.ToStr(oDataRow[clsPlanGrupoVM._PlanGrupoDes]);
                        VM.PlanGrupoEsp = SysData.ToStr(oDataRow[clsPlanGrupoVM._PlanGrupoEsp]);
                        VM.PlanGrupoTipoId = SysData.ToLong(oDataRow[clsPlanGrupoVM._PlanGrupoTipoId]);
                        VM.PlanGrupoTipoDetId = SysData.ToLong(oDataRow[clsPlanGrupoVM._PlanGrupoTipoDetId]);
                        VM.NroCuentas = SysData.ToLong(oDataRow[clsPlanGrupoVM._NroCuentas]);
                        VM.MonedaId = SysData.ToLong(oDataRow[clsPlanGrupoVM._MonedaId]);
                        VM.VerificaMto = SysData.ToBoolean(oDataRow[clsPlanGrupoVM._VerificaMto]);
                        VM.EstadoId = SysData.ToLong(oDataRow[clsPlanGrupoVM._EstadoId]);
                        break;

                    case SelectFilters.ListBox:
                        VM.PlanGrupoId = SysData.ToLong(oDataRow[clsPlanGrupoVM._PlanGrupoId]);
                        VM.PlanGrupoCod = SysData.ToStr(oDataRow[clsPlanGrupoVM._PlanGrupoCod]);
                        VM.PlanGrupoDes = SysData.ToStr(oDataRow[clsPlanGrupoVM._PlanGrupoDes]);
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

            if (VM.PlanGrupoCod.Length == 0)
            {
                strMsg += "Código es Requerido" + Environment.NewLine;
            }

            if (VM.PlanGrupoDes.Length == 0)
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