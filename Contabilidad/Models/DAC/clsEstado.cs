using System;
using System.Data.SqlClient;
using System.Data;
using Contabilidad.Models.VM;

namespace Contabilidad.Models.DAC
{
    public class clsEstado : clsBase, IDisposable
    {
        public clsEstadoVM VM;

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
            EstadoDes = 2,
            Grid = 3,
            GridCheck = 4,
            EstadoCod = 5,
            GridEstadoId = 6,
            AplicacionId = 7
        }

        public enum OrderByFilters : byte
        {
            None = 0,
            EstadoId = 1,
            EstadoDes = 2,
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
        public clsEstado()
        {
            mstrTableName = "parEstado";
            mstrClassName = "clsEstado";

            PropertyInit();
            FilterInit();
        }

        public clsEstado(string ConnectString) : this()
        {
            moConnection = new SqlConnection();

            mstrConnectionString = ConnectString;
        }

        public clsEstado(SqlConnection oConnection) : this()
        {
            moConnection = oConnection;
        }

        public clsEstado(SqlConnection oConnection, SelectFilters bytSelectFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
        }

        public clsEstado(SqlConnection oConnection, SelectFilters bytSelectFilter, WhereFilters bytWhereFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
            mintWhereFilter = bytWhereFilter;
        }

        public clsEstado(SqlConnection oConnection, SelectFilters bytSelectFilter, WhereFilters bytWhereFilter, OrderByFilters bytOrderByFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
            mintWhereFilter = bytWhereFilter;
            mintOrderByFilter = bytOrderByFilter;
        }

        public void PropertyInit()
        {
            VM = new clsEstadoVM();
            VM.EstadoId = 0;
            VM.EstadoCod = "";
            VM.EstadoDes = "";
            VM.AplicacionId = 0;
        }

        protected override void SetPrimaryKey()
        {
            VM.EstadoId = mlngId;
        }

        protected override void SelectParameter()
        {
            string strSQL = null;

            mstrStoreProcName = "parEstadoSelect";

            switch (mintSelectFilter)
            {
                case SelectFilters.All:
                    strSQL = " SELECT  " +
                             "    parEstado.EstadoId, " +
                             "    parEstado.EstadoCod, " +
                             "    parEstado.EstadoDes, " +
                             "    parEstado.AplicacionId " +
                             " FROM  parEstado ";
                    break;

                case SelectFilters.ListBox:
                    strSQL = " SELECT  " +
                             "    parEstado.EstadoId, " +
                             "    parEstado.EstadoCod, " +
                             "    parEstado.EstadoDes " +
                             " FROM  parEstado ";
                    break;

                case SelectFilters.Grid:
                    strSQL = " SELECT  " +
                             "    parEstado.EstadoId, " +
                             "    parEstado.EstadoCod, " +
                             "    parEstado.EstadoDes, " +
                             "    parEstado.AplicacionId " +
                             "    segAplicacion.AplicacionDes " +
                             " FROM  parEstado " +
                             " LEFT JOIN segAplicacion ON parEstado.AplicacionId = segAplicacion.AplicacionId	";

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
                    strSQL = " WHERE EstadoId = " + SysData.NumberToField(VM.EstadoId);
                    break;

                case WhereFilters.AplicacionId:
                    strSQL = " WHERE parEstado.AplicacionId = " + SysData.NumberToField(VM.AplicacionId);
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
                case OrderByFilters.EstadoId:
                    strSQL = " ORDER BY parEstado.EstadoId ";
                    break;

                case OrderByFilters.Grid:
                    strSQL = " ORDER BY parEstado.EstadoDes";
                    break;

                case OrderByFilters.EstadoDes:
                    strSQL = " ORDER BY parEstado.EstadoDes DESC ";
                    break;
            }

            return strSQL;
        }

        protected override void InsertParameter()
        {
            switch (mintInsertFilter)
            {
                case InsertFilters.All:
                    mstrStoreProcName = "parEstadoInsert";
                    moParameters = new SqlParameter[5] {
                        new SqlParameter("InsertFilter", mintInsertFilter),
                        new SqlParameter(clsEstadoVM._EstadoId, VM.EstadoId),
                        new SqlParameter(clsEstadoVM._EstadoCod, VM.EstadoCod),
                        new SqlParameter(clsEstadoVM._EstadoDes, VM.EstadoDes),
                        new SqlParameter(clsEstadoVM._AplicacionId, VM.AplicacionId)};
                    moParameters[1].Direction = ParameterDirection.Output;
                    break;
            }
        }

        protected override void UpdateParameter()
        {
            switch (mintUpdateFilter)
            {
                case UpdateFilters.All:
                    mstrStoreProcName = "parEstadoUpdate";
                    moParameters = new SqlParameter[5] {
                        new SqlParameter("UpdateFilter", mintUpdateFilter),
                        new SqlParameter(clsEstadoVM._EstadoId, VM.EstadoId),
                        new SqlParameter(clsEstadoVM._EstadoCod, VM.EstadoCod),
                        new SqlParameter(clsEstadoVM._EstadoDes, VM.EstadoDes),
                        new SqlParameter(clsEstadoVM._AplicacionId, VM.AplicacionId)};
                    break;
            }
        }

        protected override void DeleteParameter()
        {
            switch (mintDeleteFilter)
            {
                case DeleteFilters.All:
                    mstrStoreProcName = "parEstadoDelete";
                    moParameters = new SqlParameter[2] {
                        new SqlParameter("DeleteFilter", mintDeleteFilter),
                        new SqlParameter(clsEstadoVM._EstadoId, VM.EstadoId)};
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
                        VM.EstadoId = SysData.ToLong(oDataRow[clsEstadoVM._EstadoId]);
                        VM.EstadoCod = SysData.ToStr(oDataRow[clsEstadoVM._EstadoCod]);
                        VM.EstadoDes = SysData.ToStr(oDataRow[clsEstadoVM._EstadoDes]);
                        VM.AplicacionId = SysData.ToLong(oDataRow[clsEstadoVM._AplicacionId]);
                        break;

                    case SelectFilters.ListBox:
                        VM.EstadoId = SysData.ToLong(oDataRow[clsEstadoVM._EstadoId]);
                        VM.EstadoCod = SysData.ToStr(oDataRow[clsEstadoVM._EstadoCod]);
                        VM.EstadoDes = SysData.ToStr(oDataRow[clsEstadoVM._EstadoDes]);
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

            if (VM.EstadoId <= 0) { strMsg += "El Estado ID es Requerido" + Environment.NewLine; }

            if (VM.EstadoCod.Length <= 0) { strMsg += "El Código es Requerido" + Environment.NewLine; }

            if (VM.EstadoDes.Length <= 0) { strMsg += "La Descripcinón es Requerido" + Environment.NewLine; }

            if (VM.AplicacionId <= 0) { strMsg += "El ID de la Aplicación es Requerido" + Environment.NewLine; }

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