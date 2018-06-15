using System;
using System.Data.SqlClient;
using System.Data;
using Contabilidad.Models.VM;

namespace Contabilidad.Models.DAC
{
    public class clsModulo : clsBase, IDisposable
    {
        public clsModuloVM VM;

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
            ModuloDes = 2,
            Grid = 3,
            GridCheck = 4,
            ModuloCod = 5,
            EstadoId = 6
        }

        public enum OrderByFilters : byte
        {
            None = 0,
            ModuloId = 1,
            ModuloDes = 2,
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
        public clsModulo()
        {
            mstrTableName = "segModulo";
            mstrClassName = "clsModulo";

            PropertyInit();
            FilterInit();
        }

        public clsModulo(string ConnectString) : this()
        {
            moConnection = new SqlConnection();

            mstrConnectionString = ConnectString;
        }

        public clsModulo(SqlConnection oConnection) : this()
        {
            moConnection = oConnection;
        }

        public clsModulo(SqlConnection oConnection, SelectFilters bytSelectFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
        }

        public clsModulo(SqlConnection oConnection, SelectFilters bytSelectFilter, WhereFilters bytWhereFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
            mintWhereFilter = bytWhereFilter;
        }

        public clsModulo(SqlConnection oConnection, SelectFilters bytSelectFilter, WhereFilters bytWhereFilter, OrderByFilters bytOrderByFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
            mintWhereFilter = bytWhereFilter;
            mintOrderByFilter = bytOrderByFilter;
        }

        public void PropertyInit()
        {
            VM = new clsModuloVM();
            VM.ModuloId = 0;
            VM.ModuloCod = "";
            VM.ModuloDes = "";
            VM.ModuloEsp = "";
            VM.EstadoId = 0;
        }

        protected override void SetPrimaryKey()
        {
            VM.ModuloId = mlngId;
        }

        protected override void SelectParameter()
        {
            string strSQL = null;

            mstrStoreProcName = "segModuloSelect";

            switch (mintSelectFilter)
            {
                case SelectFilters.All:
                    strSQL = " SELECT  " +
                             "    segModulo.ModuloId, " +
                             "    segModulo.ModuloCod, " +
                             "    segModulo.ModuloDes, " +
                             "    segModulo.ModuloEsp, " +
                             "    segModulo.EstadoId " +
                             " FROM  segModulo ";
                    break;

                case SelectFilters.ListBox:
                    strSQL = " SELECT  " +
                             "    segModulo.ModuloId, " +
                             "    segModulo.ModuloCod, " +
                             "    segModulo.ModuloDes " +
                             " FROM  segModulo ";
                    break;

                case SelectFilters.Grid:
                    strSQL = " SELECT  " +
                             "    segModulo.ModuloId, " +
                             "    segModulo.ModuloCod, " +
                             "    segModulo.ModuloDes, " +
                             "    segModulo.ModuloEsp, " +
                             "    segModulo.EstadoId " +
                             "    parEstado.EstadoDes " +
                             " FROM  segModulo " +
                             " LEFT JOIN parEstado ON segModulo.EstadoId = parEstado.EstadoId	";

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
                    strSQL = " WHERE ModuloId = " + SysData.NumberToField(VM.ModuloId);
                    break;

                case WhereFilters.ModuloDes:
                    strSQL = " WHERE segModulo.ModuloDes = " + SysData.StringToField(VM.ModuloDes);
                    break;

                case WhereFilters.None:
                    strSQL = " WHERE segModulo.EstadoId = " + SysData.NumberToField(VM.EstadoId);
                    break;

                case WhereFilters.EstadoId:
                    strSQL = " WHERE segModulo.EstadoId = " + SysData.NumberToField(VM.EstadoId);
                    break;

                case WhereFilters.Grid:
                    break;
            }

            return strSQL;
        }

        private string OrderByFilterGet()
        {
            string strSQL = null;

            switch (mintOrderByFilter)
            {
                case OrderByFilters.ModuloId:
                    strSQL = " ORDER BY segModulo.ModuloId ";
                    break;

                case OrderByFilters.Grid:
                    strSQL = " ORDER BY segModulo.ModuloDes";
                    break;

                case OrderByFilters.ModuloDes:
                    strSQL = " ORDER BY segModulo.ModuloDes DESC ";
                    break;
            }

            return strSQL;
        }

        protected override void InsertParameter()
        {
            switch (mintInsertFilter)
            {
                case InsertFilters.All:
                    mstrStoreProcName = "segModuloInsert";
                    moParameters = new SqlParameter[7] {
                        new SqlParameter("InsertFilter", mintInsertFilter),
                        new SqlParameter("Id", SqlDbType.Int),
                        new SqlParameter(clsModuloVM._ModuloId, VM.ModuloId),
                        new SqlParameter(clsModuloVM._ModuloCod, VM.ModuloCod),
                        new SqlParameter(clsModuloVM._ModuloDes, VM.ModuloDes),
                        new SqlParameter(clsModuloVM._ModuloEsp, VM.ModuloEsp),
                        new SqlParameter(clsModuloVM._EstadoId, VM.EstadoId)};
                    moParameters[1].Direction = ParameterDirection.Output;
                    break;
            }
        }

        protected override void UpdateParameter()
        {
            switch (mintUpdateFilter)
            {
                case UpdateFilters.All:
                    mstrStoreProcName = "segModuloUpdate";
                    moParameters = new SqlParameter[6] {
                        new SqlParameter("UpdateFilter", mintUpdateFilter),
                        new SqlParameter(clsModuloVM._ModuloId, VM.ModuloId),
                        new SqlParameter(clsModuloVM._ModuloCod, VM.ModuloCod),
                        new SqlParameter(clsModuloVM._ModuloDes, VM.ModuloDes),
                        new SqlParameter(clsModuloVM._ModuloEsp, VM.ModuloEsp),
                        new SqlParameter(clsModuloVM._EstadoId, VM.EstadoId)};
                    break;
            }
        }

        protected override void DeleteParameter()
        {
            switch (mintDeleteFilter)
            {
                case DeleteFilters.All:
                    mstrStoreProcName = "segModuloDelete";
                    moParameters = new SqlParameter[2] {
                        new SqlParameter("DeleteFilter", mintDeleteFilter),
                        new SqlParameter(clsModuloVM._ModuloId, VM.ModuloId) };
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
                        VM.ModuloId = SysData.ToLong(oDataRow[clsModuloVM._ModuloId]);
                        VM.ModuloCod = SysData.ToStr(oDataRow[clsModuloVM._ModuloCod]);
                        VM.ModuloDes = SysData.ToStr(oDataRow[clsModuloVM._ModuloDes]);
                        VM.ModuloEsp = SysData.ToStr(oDataRow[clsModuloVM._ModuloEsp]);
                        VM.EstadoId = SysData.ToLong(oDataRow[clsModuloVM._EstadoId]);
                        break;

                    case SelectFilters.ListBox:
                        VM.ModuloId = SysData.ToLong(oDataRow[clsModuloVM._ModuloId]);
                        VM.ModuloCod = SysData.ToStr(oDataRow[clsModuloVM._ModuloCod]);
                        VM.ModuloDes = SysData.ToStr(oDataRow[clsModuloVM._ModuloDes]);
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

            if (VM.ModuloId <= 0) { strMsg += "El Estado ID es Requerido" + Environment.NewLine; }

            if (VM.ModuloCod.Length <= 0) { strMsg += "El Código es Requerido" + Environment.NewLine; }

            if (VM.EstadoDes.Length <= 0) { strMsg += "La Descripcinón es Requerido" + Environment.NewLine; }

            if (VM.EstadoId <= 0) { strMsg += "El ID del Estado es Requerido" + Environment.NewLine; }

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