using System;
using System.Data.SqlClient;
using System.Data;
using Contabilidad.Models.VM;

namespace Contabilidad.Models.DAC
{
    public class clsCenCosGrupo : clsBase, IDisposable
    {
        public clsCenCosGrupoVM VM;

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
            CenCosGrupoDes = 2,
            Grid = 3,
            GridCheck = 4,
            CenCosGrupoCod = 5,
        }

        public enum OrderByFilters : byte
        {
            None = 0,
            CenCosGrupoId = 1,
            CenCosGrupoDes = 2,
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
        public clsCenCosGrupo()
        {
            mstrTableName = "ctbCenCosGrupo";
            mstrClassName = "clsCenCosGrupo";

            PropertyInit();
            FilterInit();
        }

        public clsCenCosGrupo(string ConnectString) : this()
        {
            moConnection = new SqlConnection();

            mstrConnectionString = ConnectString;
        }

        public clsCenCosGrupo(SqlConnection oConnection) : this()
        {
            moConnection = oConnection;
        }

        public clsCenCosGrupo(SqlConnection oConnection, SelectFilters bytSelectFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
        }

        public clsCenCosGrupo(SqlConnection oConnection, SelectFilters bytSelectFilter, WhereFilters bytWhereFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
            mintWhereFilter = bytWhereFilter;
        }

        public clsCenCosGrupo(SqlConnection oConnection, SelectFilters bytSelectFilter, WhereFilters bytWhereFilter, OrderByFilters bytOrderByFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
            mintWhereFilter = bytWhereFilter;
            mintOrderByFilter = bytOrderByFilter;
        }

        public void PropertyInit()
        {
            VM = new clsCenCosGrupoVM();

            VM.CenCosGrupoId = 0;
            VM.CenCosGrupoCod = "";
            VM.CenCosGrupoDes = "";
            VM.CenCosGrupoEsp = "";
            VM.EstadoId = 0;
        }

        protected override void SetPrimaryKey()
        {
            VM.CenCosGrupoId = mlngId;
        }

        protected override void SelectParameter()
        {
            string strSQL = null;

            mstrStoreProcName = "ctbCenCosGrupoSelect";

            switch (mintSelectFilter)
            {
                case SelectFilters.All:
                    strSQL = " SELECT  " +
                           "    ctbCenCosGrupo.CenCosGrupoId, " +
                           "    ctbCenCosGrupo.CenCosGrupoCod, " +
                           "    ctbCenCosGrupo.CenCosGrupoDes, " +
                           "    ctbCenCosGrupo.CenCosGrupoEsp, " +
                           "    ctbCenCosGrupo.EstadoId " +
                           " FROM ctbCenCosGrupo ";
                    break;

                case SelectFilters.ListBox:
                    strSQL = " SELECT  " +
                           "    ctbCenCosGrupo.CenCosGrupoId, " +
                           "    ctbCenCosGrupo.CenCosGrupoCod, " +
                           "    ctbCenCosGrupo.CenCosGrupoDes " +
                           " FROM ctbCenCosGrupo ";
                    break;

                case SelectFilters.Grid:
                    strSQL = " SELECT  " +
                           "    ctbCenCosGrupo.CenCosGrupoId, " +
                           "    ctbCenCosGrupo.CenCosGrupoCod, " +
                           "    ctbCenCosGrupo.CenCosGrupoDes, " +
                           "    ctbCenCosGrupo.CenCosGrupoEsp, " +
                           "    parEstado.EstadoId, " +
                           "    parEstado.EstadoDes " +
                           " FROM ctbCenCosGrupo " +
                           "    LEFT JOIN parEstado ON ctbCenCosGrupo.EstadoId = parEstado.EstadoId ";
                    break;

                case SelectFilters.GridCheck:
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
                    strSQL = " WHERE CenCosGrupoId = " + SysData.NumberToField(VM.CenCosGrupoId);
                    break;

                case WhereFilters.CenCosGrupoDes:
                    break;

                case WhereFilters.Grid:
                    break;

                case WhereFilters.CenCosGrupoCod:
                    strSQL = " WHERE CenCosGrupoCod = " + SysData.StringToField(VM.CenCosGrupoCod);
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
                case OrderByFilters.CenCosGrupoId:
                    strSQL = " ORDER BY ctbCenCosGrupo.CenCosGrupoId ";
                    break;

                case OrderByFilters.CenCosGrupoDes:
                    strSQL = " ORDER BY ctbCenCosGrupo.CenCosGrupoDes ";
                    break;

                case OrderByFilters.Grid:
                    strSQL = " ORDER BY ctbCenCosGrupo.CenCosGrupoDes ";
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
                    mstrStoreProcName = "ctbCenCosGrupoInsert";
                    moParameters = new SqlParameter[6] {
                        new SqlParameter("@InsertFilter", mintInsertFilter),
                        new SqlParameter("@Id", SqlDbType.Int),
                        new SqlParameter(clsCenCosGrupoVM._CenCosGrupoCod, VM.CenCosGrupoCod),
                        new SqlParameter(clsCenCosGrupoVM._CenCosGrupoDes, VM.CenCosGrupoDes),
                        new SqlParameter(clsCenCosGrupoVM._CenCosGrupoEsp, VM.CenCosGrupoEsp),
                        new SqlParameter(clsCenCosGrupoVM._EstadoId, VM.EstadoId)};
                    moParameters[1].Direction = ParameterDirection.Output;
                    break;
            }
        }

        protected override void UpdateParameter()
        {
            switch (mintUpdateFilter)
            {
                case UpdateFilters.All:
                    mstrStoreProcName = "ctbCenCosGrupoUpdate";
                    moParameters = new SqlParameter[6] {
                        new SqlParameter("@UpdateFilter", mintUpdateFilter),
                        new SqlParameter(clsCenCosGrupoVM._CenCosGrupoId, VM.CenCosGrupoId),
                        new SqlParameter(clsCenCosGrupoVM._CenCosGrupoCod, VM.CenCosGrupoCod),
                        new SqlParameter(clsCenCosGrupoVM._CenCosGrupoDes, VM.CenCosGrupoDes),
                        new SqlParameter(clsCenCosGrupoVM._CenCosGrupoEsp, VM.CenCosGrupoEsp),
                        new SqlParameter(clsCenCosGrupoVM._EstadoId, VM.EstadoId)};
                    break;
            }
        }

        protected override void DeleteParameter()
        {
            switch (mintDeleteFilter)
            {
                case DeleteFilters.All:
                    mstrStoreProcName = "ctbCenCosGrupoDelete";
                    moParameters = new SqlParameter[2] {
                        new SqlParameter("@DeleteFilter", mintDeleteFilter),
                        new SqlParameter(clsCenCosGrupoVM._CenCosGrupoId, VM.CenCosGrupoId)};
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
                        VM.CenCosGrupoId = SysData.ToLong(oDataRow[clsCenCosGrupoVM._CenCosGrupoId]);
                        VM.CenCosGrupoCod = SysData.ToStr(oDataRow[clsCenCosGrupoVM._CenCosGrupoCod]);
                        VM.CenCosGrupoDes = SysData.ToStr(oDataRow[clsCenCosGrupoVM._CenCosGrupoDes]);
                        VM.CenCosGrupoEsp = SysData.ToStr(oDataRow[clsCenCosGrupoVM._CenCosGrupoEsp]);
                        VM.EstadoId = SysData.ToLong(oDataRow[clsCenCosGrupoVM._EstadoId]);
                        break;

                    case SelectFilters.ListBox:
                        VM.CenCosGrupoId = SysData.ToLong(oDataRow[clsCenCosGrupoVM._CenCosGrupoId]);
                        VM.CenCosGrupoCod = SysData.ToStr(oDataRow[clsCenCosGrupoVM._CenCosGrupoCod]);
                        VM.CenCosGrupoDes = SysData.ToStr(oDataRow[clsCenCosGrupoVM._CenCosGrupoDes]);
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

            if (VM.CenCosGrupoCod.Length == 0)
            {
                strMsg += "Código es Requerido" + Environment.NewLine;
            }

            if (VM.CenCosGrupoDes.Length == 0)
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