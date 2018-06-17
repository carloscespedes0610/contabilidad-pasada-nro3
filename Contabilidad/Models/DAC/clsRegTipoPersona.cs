using System;
using System.Data.SqlClient;
using System.Data;
using Contabilidad.Models.VM;

namespace Contabilidad.Models.DAC
{
    public class clsRegTipoPersona : clsBase, IDisposable
    {
        public clsRegTipoPersonaVM VM;

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
            Grid = 2,
            GridCheck = 3,
            TipoPersonaId = 4
        }

        public enum OrderByFilters : byte
        {
            None = 0,
            RegTipoPersonaId = 1,
            Grid = 2,
            GridCheck = 3
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
            TipoPersonaId = 1
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
        public clsRegTipoPersona()
        {
            mstrTableName = "ctbRegTipoPersona";
            mstrClassName = "clsRegTipoPersona";

            PropertyInit();
            FilterInit();
        }

        public clsRegTipoPersona(string ConnectString) : this()
        {
            moConnection = new SqlConnection();

            mstrConnectionString = ConnectString;
        }

        public clsRegTipoPersona(SqlConnection oConnection) : this()
        {
            moConnection = oConnection;
        }

        public clsRegTipoPersona(SqlConnection oConnection, SelectFilters bytSelectFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
        }

        public clsRegTipoPersona(SqlConnection oConnection, SelectFilters bytSelectFilter, WhereFilters bytWhereFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
            mintWhereFilter = bytWhereFilter;
        }

        public clsRegTipoPersona(SqlConnection oConnection, SelectFilters bytSelectFilter, WhereFilters bytWhereFilter, OrderByFilters bytOrderByFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
            mintWhereFilter = bytWhereFilter;
            mintOrderByFilter = bytOrderByFilter;
        }

        public void PropertyInit()
        {
            VM = new clsRegTipoPersonaVM();
            VM.RegTipoPersonaId = 0;
            VM.TipoPersonaId = 0;
            VM.PlanGrupoId = 0;
            VM.EstadoId = 0;
        }

        protected override void SetPrimaryKey()
        {
            VM.RegTipoPersonaId = mlngId;
        }

        protected override void SelectParameter()
        {
            string strSQL = null;

            mstrStoreProcName = "ctbRegTipoPersonaSelect";

            switch (mintSelectFilter)
            {
                case SelectFilters.All:
                    strSQL = " SELECT  " +
                           "    ctbRegTipoPersona.RegTipoPersonaId, " +
                           "    ctbRegTipoPersona.TipoPersonaId, " +
                           "    ctbRegTipoPersona.PlanGrupoId, " +
                           "    ctbRegTipoPersona.EstadoId " +
                           " FROM ctbRegTipoPersona ";
                    break;

                case SelectFilters.RowCount:
                    mstrStoreProcName = "ctbRegTipoPersonaSelect";
                    break;

                case SelectFilters.ListBox:
                    strSQL = " SELECT  " +
                            "    ctbRegTipoPersona.RegTipoPersonaId " +
                            " FROM ctbRegTipoPersona ";
                    break;

                case SelectFilters.Grid:
                    strSQL = " SELECT  " +
                           "    ctbRegTipoPersona.RegTipoPersonaId, " +
                           "    parTipoPersona.TipoPersonaId, " +
                           "    parTipoPersona.TipoPersonaDes, " +
                           "    ctbPlanGrupo.PlanGrupoId, " +
                           "    ctbPlanGrupo.PlanGrupoDes, " +
                           "    parEstado.EstadoId, " +
                           "    parEstado.EstadoDes " +
                           " FROM ctbRegTipoPersona  " +
                           "    LEFT JOIN	parTipoPersona	ON ctbRegTipoPersona.TipoPersonaId = parTipoPersona.TipoPersonaId " +
                           "    LEFT JOIN	ctbPlanGrupo	ON ctbRegTipoPersona.PlanGrupoId = ctbPlanGrupo.PlanGrupoId " +
                           "    LEFT JOIN	parEstado		ON ctbRegTipoPersona.EstadoId = parEstado.EstadoId ";
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
                    Array.Resize(ref moParameters, moParameters.Length + 4);
                    break;

                case WhereFilters.PrimaryKey:
                    strSQL = " WHERE ctbRegTipoPersona.RegTipoPersonaId = " + SysData.NumberToField(VM.RegTipoPersonaId);
                    break;

                case WhereFilters.Grid:
                    break;

                case WhereFilters.TipoPersonaId:
                    strSQL = " WHERE ctbRegTipoPersona.TipoPersonaId = " + SysData.NumberToField(VM.TipoPersonaId);

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
                case OrderByFilters.RegTipoPersonaId:
                    strSQL = " ORDER BY ctbRegTipoPersona.RegTipoPersonaId ";
                    break;
                case OrderByFilters.Grid:
                    strSQL = " ORDER BY parTipoPersona.TipoPersonaDes, ctbPlanGrupo.PlanGrupoDes ";
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
                    mstrStoreProcName = "ctbRegTipoPersonaInsert";
                    moParameters = new SqlParameter[5] {
                        new SqlParameter("@InsertFilter", mintInsertFilter),
                        new SqlParameter("@Id", SqlDbType.Int),
                        new SqlParameter(clsRegTipoPersonaVM._TipoPersonaId, VM.TipoPersonaId),
                        new SqlParameter(clsRegTipoPersonaVM._PlanGrupoId, VM.PlanGrupoId),
                        new SqlParameter(clsRegTipoPersonaVM._EstadoId, VM.EstadoId)};
                    moParameters[1].Direction = ParameterDirection.Output;
                    break;
            }
        }

        protected override void UpdateParameter()
        {
            switch (mintUpdateFilter)
            {
                case UpdateFilters.All:
                    mstrStoreProcName = "ctbRegTipoPersonaUpdate";
                    moParameters = new SqlParameter[5] {
                        new SqlParameter("@UpdateFilter", mintUpdateFilter),
                        new SqlParameter(clsRegTipoPersonaVM._RegTipoPersonaId, VM.RegTipoPersonaId),
                        new SqlParameter(clsRegTipoPersonaVM._TipoPersonaId, VM.TipoPersonaId),
                        new SqlParameter(clsRegTipoPersonaVM._PlanGrupoId, VM.PlanGrupoId),
                        new SqlParameter(clsRegTipoPersonaVM._EstadoId, VM.EstadoId)};
                    break;
            }
        }

        protected override void DeleteParameter()
        {
            switch (mintDeleteFilter)
            {
                case DeleteFilters.All:
                    mstrStoreProcName = "ctbRegTipoPersonaDelete";
                    moParameters = new SqlParameter[3] {
                        new SqlParameter("@DeleteFilter", mintDeleteFilter),
                        new SqlParameter(clsRegTipoPersonaVM._RegTipoPersonaId, VM.RegTipoPersonaId),
                        new SqlParameter(clsRegTipoPersonaVM._TipoPersonaId, Convert.ToInt32(0))};
                    break;

                case DeleteFilters.TipoPersonaId:
                    mstrStoreProcName = "ctbRegTipoPersonaDelete";
                    moParameters = new SqlParameter[3] {
                        new SqlParameter("@DeleteFilter", mintDeleteFilter),
                        new SqlParameter(clsRegTipoPersonaVM._RegTipoPersonaId, Convert.ToInt32(0)),
                        new SqlParameter(clsRegTipoPersonaVM._TipoPersonaId, VM.TipoPersonaId)};
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
                        VM.RegTipoPersonaId = SysData.ToLong(oDataRow[clsRegTipoPersonaVM._RegTipoPersonaId]);
                        VM.TipoPersonaId = SysData.ToLong(oDataRow[clsRegTipoPersonaVM._TipoPersonaId]);
                        VM.PlanGrupoId = SysData.ToLong(oDataRow[clsRegTipoPersonaVM._PlanGrupoId]);
                        VM.EstadoId = SysData.ToLong(oDataRow[clsRegTipoPersonaVM._EstadoId]);
                        break;

                    case SelectFilters.ListBox:
                        VM.RegTipoPersonaId = SysData.ToLong(oDataRow[clsRegTipoPersonaVM._RegTipoPersonaId]);
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