using System;
using System.Data.SqlClient;
using System.Data;
using Contabilidad.Models.VM;

namespace Contabilidad.Models.DAC
{
    public class clsCenCos : clsBase, IDisposable
    {
        public clsCenCosVM VM;

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
            CenCosDes = 2,
            Grid = 3,
            GridCheck = 4,
            CenCosCod = 5,
        }

        public enum OrderByFilters : byte
        {
            None = 0,
            CenCosId = 1,
            CenCosDes = 2,
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
        public clsCenCos()
        {
            mstrTableName = "ctbCenCos";
            mstrClassName = "clsCenCos";

            PropertyInit();
            FilterInit();
        }

        public clsCenCos(string ConnectString) : this()
        {
            moConnection = new SqlConnection();

            mstrConnectionString = ConnectString;
        }

        public clsCenCos(SqlConnection oConnection) : this()
        {
            moConnection = oConnection;
        }

        public clsCenCos(SqlConnection oConnection, SelectFilters bytSelectFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
        }

        public clsCenCos(SqlConnection oConnection, SelectFilters bytSelectFilter, WhereFilters bytWhereFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
            mintWhereFilter = bytWhereFilter;
        }

        public clsCenCos(SqlConnection oConnection, SelectFilters bytSelectFilter, WhereFilters bytWhereFilter, OrderByFilters bytOrderByFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
            mintWhereFilter = bytWhereFilter;
            mintOrderByFilter = bytOrderByFilter;
        }

        public void PropertyInit()
        {
            VM = new clsCenCosVM();

            VM.CenCosId = 0;
            VM.CenCosCod = "";
            VM.CenCosDes = "";
            VM.CenCosEsp = "";
            VM.CenCosGrupoId = 0;
            VM.EstadoId = 0;
        }

        protected override void SetPrimaryKey()
        {
            VM.CenCosId = mlngId;
        }

        protected override void SelectParameter()
        {
            string strSQL = null;

            mstrStoreProcName = "ctbCenCosSelect";

            switch (mintSelectFilter)
            {
                case SelectFilters.All:
                    strSQL = " SELECT  " +
                           "    ctbCenCos.CenCosId, " +
                           "    ctbCenCos.CenCosCod, " +
                           "    ctbCenCos.CenCosDes, " +
                           "    ctbCenCos.CenCosEsp, " +
                           "    ctbCenCos.CenCosGrupoId, " +
                           "    ctbCenCos.EstadoId " +
                           " FROM ctbCenCos ";
                    break;

                case SelectFilters.ListBox:
                    strSQL = " SELECT  " +
                           "    ctbCenCos.CenCosId, " +
                           "    ctbCenCos.CenCosCod, " +
                           "    ctbCenCos.CenCosDes " +
                           " FROM ctbCenCos ";
                    break;

                case SelectFilters.Grid:
                    strSQL = " SELECT  " +
                            "    ctbCenCos.CenCosId, " +
                            "    ctbCenCos.CenCosCod, " +
                            "    ctbCenCos.CenCosDes, " +
                            "    ctbCenCos.CenCosEsp, " +
                            "    ctbCenCosGrupo.CenCosGrupoId, " +
                            "    ctbCenCosGrupo.CenCosGrupoDes, " +
                            "    parEstado.EstadoId, " +
                            "    parEstado.EstadoDes  " +
                            " FROM ctbCenCos " +
                            "    LEFT JOIN ctbCenCosGrupo ON ctbCenCos.CenCosGrupoId = ctbCenCosGrupo.CenCosGrupoId " +
                            "    LEFT JOIN parEstado ON ctbCenCos.EstadoId = parEstado.EstadoId ";
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
                    strSQL = " WHERE CenCosId = " + SysData.NumberToField(VM.CenCosId);
                    break;

                case WhereFilters.CenCosDes:
                    break;

                case WhereFilters.Grid:
                    break;

                case WhereFilters.CenCosCod:
                    strSQL = " WHERE CenCosCod = " + SysData.StringToField(VM.CenCosCod);
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
                case OrderByFilters.CenCosId:
                    strSQL = " ORDER BY ctbCenCos.CenCosId ";
                    break;

                case OrderByFilters.CenCosDes:
                    strSQL = " ORDER BY ctbCenCos.CenCosDes ";
                    break;

                case OrderByFilters.Grid:
                    strSQL = " ORDER BY ctbCenCosGrupo.CenCosGrupoDes, ctbCenCos.CenCosDes ";
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
                    mstrStoreProcName = "ctbCenCosInsert";
                    moParameters = new SqlParameter[7] {
                        new SqlParameter("@InsertFilter", mintInsertFilter),
                        new SqlParameter("@Id", SqlDbType.Int),
                        new SqlParameter(clsCenCosVM._CenCosCod, VM.CenCosCod),
                        new SqlParameter(clsCenCosVM._CenCosDes, VM.CenCosDes),
                        new SqlParameter(clsCenCosVM._CenCosEsp, VM.CenCosEsp),
                        new SqlParameter(clsCenCosVM._CenCosGrupoId, VM.CenCosGrupoId),
                        new SqlParameter(clsCenCosVM._EstadoId, VM.EstadoId)};
                    moParameters[1].Direction = ParameterDirection.Output;
                    break;
            }
        }

        protected override void UpdateParameter()
        {
            switch (mintUpdateFilter)
            {
                case UpdateFilters.All:
                    mstrStoreProcName = "ctbCenCosUpdate";
                    moParameters = new SqlParameter[7] {
                        new SqlParameter("@UpdateFilter", mintUpdateFilter),
                        new SqlParameter(clsCenCosVM._CenCosId, VM.CenCosId),
                        new SqlParameter(clsCenCosVM._CenCosCod, VM.CenCosCod),
                        new SqlParameter(clsCenCosVM._CenCosDes, VM.CenCosDes),
                        new SqlParameter(clsCenCosVM._CenCosEsp, VM.CenCosEsp),
                        new SqlParameter(clsCenCosVM._CenCosGrupoId, VM.CenCosGrupoId),
                        new SqlParameter(clsCenCosVM._EstadoId, VM.EstadoId)};
                    break;
            }
        }

        protected override void DeleteParameter()
        {
            switch (mintDeleteFilter)
            {
                case DeleteFilters.All:
                    mstrStoreProcName = "ctbCenCosDelete";
                    moParameters = new SqlParameter[2] {
                        new SqlParameter("@DeleteFilter", mintDeleteFilter),
                        new SqlParameter(clsCenCosVM._CenCosId, VM.CenCosId)};
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
                        VM.CenCosId = SysData.ToLong(oDataRow[clsCenCosVM._CenCosId]);
                        VM.CenCosCod = SysData.ToStr(oDataRow[clsCenCosVM._CenCosCod]);
                        VM.CenCosDes = SysData.ToStr(oDataRow[clsCenCosVM._CenCosDes]);
                        VM.CenCosEsp = SysData.ToStr(oDataRow[clsCenCosVM._CenCosEsp]);
                        VM.CenCosGrupoId = SysData.ToLong(oDataRow[clsCenCosVM._CenCosGrupoId]);
                        VM.EstadoId = SysData.ToLong(oDataRow[clsCenCosVM._EstadoId]);
                        break;

                    case SelectFilters.ListBox:
                        VM.CenCosId = SysData.ToLong(oDataRow[clsCenCosVM._CenCosId]);
                        VM.CenCosCod = SysData.ToStr(oDataRow[clsCenCosVM._CenCosCod]);
                        VM.CenCosDes = SysData.ToStr(oDataRow[clsCenCosVM._CenCosDes]);
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

            if (VM.CenCosCod.Length == 0)
            {
                strMsg += "Código es Requerido" + Environment.NewLine;
            }

            if (VM.CenCosDes.Length == 0)
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