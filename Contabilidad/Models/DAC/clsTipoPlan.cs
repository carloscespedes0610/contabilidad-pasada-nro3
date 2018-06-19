using System;
using System.Data.SqlClient;
using System.Data;
using Contabilidad.Models.VM;

namespace Contabilidad.Models.DAC
{
    public class clsTipoPlan : clsBase, IDisposable
    {
        public clsTipoPlanVM VM;

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
            TipoPlanDes = 2,
            Grid = 3,
            GridCheck = 4,
            EstadoId = 5
        }

        public enum OrderByFilters : byte
        {
            None = 0,
            TipoPlanId = 1,
            TipoPlanDes = 2,
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
        public clsTipoPlan()
        {
            mstrTableName = "ctbTipoPlan";
            mstrClassName = "clsTipoPlan";

            PropertyInit();
            FilterInit();
        }

        public clsTipoPlan(string ConnectString) : this()
        {
            moConnection = new SqlConnection();

            mstrConnectionString = ConnectString;
        }

        public clsTipoPlan(SqlConnection oConnection) : this()
        {
            moConnection = oConnection;
        }

        public clsTipoPlan(SqlConnection oConnection, SelectFilters bytSelectFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
        }

        public clsTipoPlan(SqlConnection oConnection, SelectFilters bytSelectFilter, WhereFilters bytWhereFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
            mintWhereFilter = bytWhereFilter;
        }

        public clsTipoPlan(SqlConnection oConnection, SelectFilters bytSelectFilter, WhereFilters bytWhereFilter, OrderByFilters bytOrderByFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
            mintWhereFilter = bytWhereFilter;
            mintOrderByFilter = bytOrderByFilter;
        }

        public void PropertyInit()
        {
            VM = new clsTipoPlanVM();
            VM.TipoPlanId = 0;
            VM.TipoPlanDes = "";
            VM.EstadoId = 0;
        }

        protected override void SetPrimaryKey()
        {
            VM.TipoPlanId = mlngId;
        }

        protected override void SelectParameter()
        {
            string strSQL = null;

            mstrStoreProcName = "ctbTipoPlanSelect";

            switch (mintSelectFilter)
            {
                case SelectFilters.All:
                    strSQL = " SELECT  " +
                           "    ctbTipoPlan.TipoPlanId, " +
                           "    ctbTipoPlan.TipoPlanDes, " +
                           "    ctbTipoPlan.EstadoId " +
                           " FROM ctbTipoPlan ";
                    break;

                case SelectFilters.RowCount:
                    break;

                case SelectFilters.ListBox:
                    strSQL = " SELECT  " +
                           "    ctbTipoPlan.TipoPlanId, " +
                           "    ctbTipoPlan.TipoPlanDes " +
                           " FROM ctbTipoPlan ";
                    break;

                case SelectFilters.Grid:
                    strSQL = " SELECT  " +
                           "    ctbTipoPlan.TipoPlanId, " +
                           "    ctbTipoPlan.TipoPlanDes, " +
                           "    ctbTipoPlan.EstadoId, " +
                           "    parEstado.EstadoDes " +
                           " FROM ctbTipoPlan " +
                           "    LEFT JOIN	parEstado ON ctbTipoPlan.EstadoId = parEstado.EstadoId  ";
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
                    strSQL = " WHERE ctbTipoPlan.TipoPlanId = " + SysData.NumberToField(VM.TipoPlanId);
                    break;

                case WhereFilters.TipoPlanDes:
                    strSQL = " WHERE ctbTipoPlan.TipoPlanDes = " + SysData.StringToField(VM.TipoPlanDes);
                    break;

                case WhereFilters.Grid:
                    
                    break;

                case WhereFilters.GridCheck:
                    break;

                case WhereFilters.EstadoId:
                    strSQL = " WHERE ctbTipoPlan.EstadoId = " + SysData.NumberToField(VM.EstadoId);
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
                case OrderByFilters.TipoPlanId:
                    strSQL = " ORDER BY ctbTipoPlan.TipoPlanId ";
                    break;
                case OrderByFilters.TipoPlanDes:
                    strSQL = " ORDER BY ctbTipoPlan.TipoPlanDes ";
                    break;
                case OrderByFilters.Grid:
                    strSQL = " ORDER BY ctbTipoPlan.TipoPlanDes ";
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
                    mstrStoreProcName = "ctbTipoPlanInsert";
                    moParameters = new SqlParameter[4] {
                        new SqlParameter("@InsertFilter", mintInsertFilter),
                        new SqlParameter("@Id", SqlDbType.Int),
                        new SqlParameter(clsTipoPlanVM._TipoPlanDes, VM.TipoPlanDes),
                        new SqlParameter(clsTipoPlanVM._EstadoId, VM.EstadoId)};
                    moParameters[1].Direction = ParameterDirection.Output;
                    break;
            }
        }

        protected override void UpdateParameter()
        {
            switch (mintUpdateFilter)
            {
                case UpdateFilters.All:
                    mstrStoreProcName = "ctbTipoPlanUpdate";
                    moParameters = new SqlParameter[4] {
                        new SqlParameter("@UpdateFilter", mintUpdateFilter),
                        new SqlParameter(clsTipoPlanVM._TipoPlanId, VM.TipoPlanId),
                        new SqlParameter(clsTipoPlanVM._TipoPlanDes, VM.TipoPlanDes),
                        new SqlParameter(clsTipoPlanVM._EstadoId, VM.EstadoId)};
                    break;
            }
        }

        protected override void DeleteParameter()
        {
            switch (mintDeleteFilter)
            {
                case DeleteFilters.All:
                    mstrStoreProcName = "ctbTipoPlanDelete";
                    moParameters = new SqlParameter[2] {
                        new SqlParameter("@DeleteFilter", mintDeleteFilter),
                        new SqlParameter(clsTipoPlanVM._TipoPlanId, VM.TipoPlanId)};
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
                        VM.TipoPlanId = SysData.ToLong(oDataRow[clsTipoPlanVM._TipoPlanId]);
                        VM.TipoPlanDes = SysData.ToStr(oDataRow[clsTipoPlanVM._TipoPlanDes]);
                        VM.EstadoId = SysData.ToLong(oDataRow[clsTipoPlanVM._EstadoId]);
                        break;

                    case SelectFilters.ListBox:
                        VM.TipoPlanId = SysData.ToLong(oDataRow[clsTipoPlanVM._TipoPlanId]);
                        VM.TipoPlanDes = SysData.ToStr(oDataRow[clsTipoPlanVM._TipoPlanDes]);
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

            if (VM.TipoPlanDes.Length == 0)
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