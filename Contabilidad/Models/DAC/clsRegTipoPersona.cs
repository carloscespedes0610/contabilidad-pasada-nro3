using System;
using System.Data.SqlClient;
using System.Data;

namespace Contabilidad.Models.DAC
{
    public class clsRegTipoPersona : clsBase, IDisposable
    {
        private long mlngRegTipoPersonaId;
        private long mlngTipoPersonaId;
        private long mlngPlanGrupoId;
        private long mlngEstadoId;

        //******************************************************
        // Private Data To Match the Table Definition
        //******************************************************
        public long RegTipoPersonaId
        {
            get
            {
                return mlngRegTipoPersonaId;
            }

            set
            {
                mlngRegTipoPersonaId = value;
            }
        }

        public long TipoPersonaId
        {
            get
            {
                return mlngTipoPersonaId;
            }

            set
            {
                mlngTipoPersonaId = value;
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
            RegTipoPersonaDes = 2,
            Grid = 3,
            GridCheck = 4,
            TipoPersonaId = 5
        }

        public enum OrderByFilters : byte
        {
            None = 0,
            RegTipoPersonaId = 1,
            RegTipoPersonaDes = 2,
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
            mlngRegTipoPersonaId = 0;
            mlngTipoPersonaId = 0;
            mlngPlanGrupoId = 0;
            mlngEstadoId = 0;
        }

        protected override void SetPrimaryKey()
        {
            mlngRegTipoPersonaId = mlngId;
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
                    mstrStoreProcName = "ctbRegTipoPersonaSelect";
                    break;

                case SelectFilters.RowCount:
                    mstrStoreProcName = "ctbRegTipoPersonaSelect";
                    break;

                case SelectFilters.ListBox:
                    mstrStoreProcName = "ctbRegTipoPersonaSelect";
                    break;

                case SelectFilters.Grid:
                    mstrStoreProcName = "ctbRegTipoPersonaSelect";
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
                    Array.Resize(ref moParameters, moParameters.Length + 4);
                    moParameters[3] = new SqlParameter("@RegTipoPersonaId", Convert.ToInt32(0));
                    moParameters[4] = new SqlParameter("@TipoPersonaId", Convert.ToInt32(0));
                    moParameters[5] = new SqlParameter("@PlanGrupoId", Convert.ToInt32(0));
                    moParameters[6] = new SqlParameter("@EstadoId", Convert.ToInt32(0));
                    break;

                case WhereFilters.PrimaryKey:
                    Array.Resize(ref moParameters, moParameters.Length + 4);
                    moParameters[3] = new SqlParameter("@RegTipoPersonaId", mlngRegTipoPersonaId);
                    moParameters[4] = new SqlParameter("@TipoPersonaId", Convert.ToInt32(0));
                    moParameters[5] = new SqlParameter("@PlanGrupoId", Convert.ToInt32(0));
                    moParameters[6] = new SqlParameter("@EstadoId", Convert.ToInt32(0));
                    break;

                case WhereFilters.RegTipoPersonaDes:
                    break;
                //strSQL = " WHERE  ctbRegTipoPersona.RegTipoPersonaDes = " & StringToField(mstrRegTipoPersonaDes)

                case WhereFilters.Grid:
                    Array.Resize(ref moParameters, moParameters.Length + 4);
                    moParameters[3] = new SqlParameter("@RegTipoPersonaId", Convert.ToInt32(0));
                    moParameters[4] = new SqlParameter("@TipoPersonaId", Convert.ToInt32(0));
                    moParameters[5] = new SqlParameter("@PlanGrupoId", Convert.ToInt32(0));
                    moParameters[6] = new SqlParameter("@EstadoId", Convert.ToInt32(0));
                    break;

                case WhereFilters.TipoPersonaId:
                    Array.Resize(ref moParameters, moParameters.Length + 4);
                    moParameters[3] = new SqlParameter("@RegTipoPersonaId", Convert.ToInt32(0));
                    moParameters[4] = new SqlParameter("@TipoPersonaId", mlngTipoPersonaId);
                    moParameters[5] = new SqlParameter("@PlanGrupoId", Convert.ToInt32(0));
                    moParameters[6] = new SqlParameter("@EstadoId", Convert.ToInt32(0));
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
                    mstrStoreProcName = "ctbRegTipoPersonaInsert";
                    moParameters = new SqlParameter[5] {
                        new SqlParameter("@InsertFilter", mintInsertFilter),
                        new SqlParameter("@Id", SqlDbType.Int),
                        new SqlParameter("@TipoPersonaId", mlngTipoPersonaId),
                        new SqlParameter("@PlanGrupoId", mlngPlanGrupoId),
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
                    mstrStoreProcName = "ctbRegTipoPersonaUpdate";
                    moParameters = new SqlParameter[5] {
                        new SqlParameter("@UpdateFilter", mintUpdateFilter),
                        new SqlParameter("@RegTipoPersonaId", mlngRegTipoPersonaId),
                        new SqlParameter("@TipoPersonaId", mlngTipoPersonaId),
                        new SqlParameter("@PlanGrupoId", mlngPlanGrupoId),
                        new SqlParameter("@EstadoId", mlngEstadoId)};
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
                        new SqlParameter("@RegTipoPersonaId", mlngRegTipoPersonaId),
                        new SqlParameter("@TipoPersonaId", Convert.ToInt32(0))};
                    break;

                case DeleteFilters.TipoPersonaId:
                    mstrStoreProcName = "ctbRegTipoPersonaDelete";
                    moParameters = new SqlParameter[3] {
                        new SqlParameter("@DeleteFilter", mintDeleteFilter),
                        new SqlParameter("@RegTipoPersonaId", Convert.ToInt32(0)),
                        new SqlParameter("@TipoPersonaId", mlngTipoPersonaId)};
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
                        mlngRegTipoPersonaId = SysData.ToLong(oDataRow["RegTipoPersonaId"]);
                        mlngTipoPersonaId = SysData.ToLong(oDataRow["TipoPersonaId"]);
                        mlngPlanGrupoId = SysData.ToLong(oDataRow["PlanGrupoId"]);
                        mlngEstadoId = SysData.ToLong(oDataRow["EstadoId"]);
                        break;

                    case SelectFilters.ListBox:
                        mlngRegTipoPersonaId = SysData.ToLong(oDataRow["RegTipoPersonaId"]);
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