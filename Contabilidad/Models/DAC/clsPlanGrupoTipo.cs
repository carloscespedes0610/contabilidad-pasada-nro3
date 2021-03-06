﻿using System;
using System.Data.SqlClient;
using System.Data;
using Contabilidad.Models.VM;

namespace Contabilidad.Models.DAC
{
    public class clsPlanGrupoTipo : clsBase, IDisposable
    {

        public clsPlanGrupoTipoVM VM;

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
            PlanGrupoTipoDes = 2,
            Grid = 3,
            GridCheck = 4,
            PlanGrupoTipoCod = 5,
        }

        public enum OrderByFilters : byte
        {
            None = 0,
            PlanGrupoTipoId = 1,
            PlanGrupoTipoDes = 2,
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
        public clsPlanGrupoTipo()
        {
            mstrTableName = "ctbPlanGrupoTipo";
            mstrClassName = "clsPlanGrupoTipo";

            PropertyInit();
            FilterInit();
        }

        public clsPlanGrupoTipo(string ConnectString) : this()
        {
            moConnection = new SqlConnection();

            mstrConnectionString = ConnectString;
        }

        public clsPlanGrupoTipo(SqlConnection oConnection) : this()
        {
            moConnection = oConnection;
        }

        public clsPlanGrupoTipo(SqlConnection oConnection, SelectFilters bytSelectFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
        }

        public clsPlanGrupoTipo(SqlConnection oConnection, SelectFilters bytSelectFilter, WhereFilters bytWhereFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
            mintWhereFilter = bytWhereFilter;
        }

        public clsPlanGrupoTipo(SqlConnection oConnection, SelectFilters bytSelectFilter, WhereFilters bytWhereFilter, OrderByFilters bytOrderByFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
            mintWhereFilter = bytWhereFilter;
            mintOrderByFilter = bytOrderByFilter;
        }

        public void PropertyInit()
        {
            VM = new clsPlanGrupoTipoVM();

            VM.PlanGrupoTipoId = 0;
            VM.PlanGrupoTipoCod = "";
            VM.PlanGrupoTipoDes = "";
            VM.PlanGrupoTipoEsp = "";
            VM.EstadoId = 0;
        }

        protected override void SetPrimaryKey()
        {
            VM.PlanGrupoTipoId = mlngId;
        }

        protected override void SelectParameter()
        {
            string strSQL = null;

            mstrStoreProcName = "ctbPlanGrupoTipoSelect";

            switch (mintSelectFilter)
            {
                case SelectFilters.All:
                    strSQL = " SELECT  " +
                           "    ctbPlanGrupoTipo.PlanGrupoTipoId , " +
                           "    ctbPlanGrupoTipo.PlanGrupoTipoCod , " +
                           "    ctbPlanGrupoTipo.PlanGrupoTipoDes , " +
                           "    ctbPlanGrupoTipo.PlanGrupoTipoEsp , " +
                           "    ctbPlanGrupoTipo.EstadoId " +
                           " FROM ctbPlanGrupoTipo ";
                    break;

                case SelectFilters.RowCount:
                    mstrStoreProcName = "ctbPlanGrupoTipoSelect";
                    break;

                case SelectFilters.ListBox:
                    strSQL = " SELECT  " +
                          "    ctbPlanGrupoTipo.PlanGrupoTipoId , " +
                          "    ctbPlanGrupoTipo.PlanGrupoTipoCod , " +
                          "    ctbPlanGrupoTipo.PlanGrupoTipoDes , " +
                          "    ctbPlanGrupoTipo.EstadoId " +
                          " FROM ctbPlanGrupoTipo ";
                    break;

                case SelectFilters.Grid:
                    strSQL = " SELECT  " +
                            "    ctbPlanGrupoTipo.PlanGrupoTipoId , " +
                            "    ctbPlanGrupoTipo.PlanGrupoTipoCod , " +
                            "    ctbPlanGrupoTipo.PlanGrupoTipoDes , " +
                            "    ctbPlanGrupoTipo.PlanGrupoTipoEsp , " +
                            "    ctbPlanGrupoTipo.EstadoId, " +
                            "    parEstado.EstadoDes " +
                            " FROM ctbPlanGrupoTipo  " +
                            "    LEFT JOIN	parEstado ON ctbPlanGrupoTipo.EstadoId = parEstado.EstadoId ";
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
                case WhereFilters.None:
                   
                    break;

                case WhereFilters.PrimaryKey:
                    strSQL = " WHERE ctbPlanGrupoTipo.PlanGrupoTipoId = " + SysData.NumberToField(VM.PlanGrupoTipoId);
                    break;

                case WhereFilters.PlanGrupoTipoDes:
                    strSQL = " WHERE ctbPlanGrupoTipo.PlanGrupoTipoDes = " + SysData.StringToField(VM.PlanGrupoTipoDes);
                    break;

                case WhereFilters.Grid:
                    break;

                case WhereFilters.PlanGrupoTipoCod:
                    strSQL = " WHERE ctbPlanGrupoTipo.PlanGrupoTipoCod = " + SysData.StringToField(VM.PlanGrupoTipoDes);
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
                case OrderByFilters.PlanGrupoTipoId:
                    strSQL = " ORDER BY ctbPlanGrupoTipo.PlanGrupoTipoId ";
                    break;
                case OrderByFilters.PlanGrupoTipoDes:
                    strSQL = " ORDER BY ctbPlanGrupoTipo.PlanGrupoTipoDes ";
                    break;
                case OrderByFilters.Grid:
                    strSQL = " ORDER BY ctbPlanGrupoTipo.PlanGrupoTipoDes ";
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
                    mstrStoreProcName = "ctbPlanGrupoTipoInsert";
                    moParameters = new SqlParameter[6] {
                        new SqlParameter("@InsertFilter", mintInsertFilter),
                        new SqlParameter("@Id", SqlDbType.Int),
                        new SqlParameter(clsPlanGrupoTipoVM._PlanGrupoTipoCod, VM.PlanGrupoTipoCod),
                        new SqlParameter(clsPlanGrupoTipoVM._PlanGrupoTipoDes, VM.PlanGrupoTipoDes),
                        new SqlParameter(clsPlanGrupoTipoVM._PlanGrupoTipoEsp, VM.PlanGrupoTipoEsp),
                        new SqlParameter(clsPlanGrupoTipoVM._EstadoId, VM.EstadoId)};
                    moParameters[1].Direction = ParameterDirection.Output;
                    break;
            }
        }

        protected override void UpdateParameter()
        {
            switch (mintUpdateFilter)
            {
                case UpdateFilters.All:
                    mstrStoreProcName = "ctbPlanGrupoTipoUpdate";
                    moParameters = new SqlParameter[6] {
                        new SqlParameter("@UpdateFilter", mintUpdateFilter),
                        new SqlParameter(clsPlanGrupoTipoVM._PlanGrupoTipoId, VM.PlanGrupoTipoId),
                        new SqlParameter(clsPlanGrupoTipoVM._PlanGrupoTipoCod, VM.PlanGrupoTipoCod),
                        new SqlParameter(clsPlanGrupoTipoVM._PlanGrupoTipoDes, VM.PlanGrupoTipoDes),
                        new SqlParameter(clsPlanGrupoTipoVM._PlanGrupoTipoEsp, VM.PlanGrupoTipoEsp),
                        new SqlParameter(clsPlanGrupoTipoVM._EstadoId, VM.EstadoId)};
                    break;
            }
        }

        protected override void DeleteParameter()
        {
            switch (mintDeleteFilter)
            {
                case DeleteFilters.All:
                    mstrStoreProcName = "ctbPlanGrupoTipoDelete";
                    moParameters = new SqlParameter[2] {
                        new SqlParameter("@DeleteFilter", mintDeleteFilter),
                        new SqlParameter(clsPlanGrupoTipoVM._PlanGrupoTipoId, VM.PlanGrupoTipoId)};
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
                        VM.PlanGrupoTipoId = SysData.ToLong(oDataRow[clsPlanGrupoTipoVM._PlanGrupoTipoId]);
                        VM.PlanGrupoTipoCod = SysData.ToStr(oDataRow[clsPlanGrupoTipoVM._PlanGrupoTipoCod]);
                        VM.PlanGrupoTipoDes = SysData.ToStr(oDataRow[clsPlanGrupoTipoVM._PlanGrupoTipoDes]);
                        VM.PlanGrupoTipoEsp = SysData.ToStr(oDataRow[clsPlanGrupoTipoVM._PlanGrupoTipoEsp]);
                        VM.EstadoId = SysData.ToLong(oDataRow[clsPlanGrupoTipoVM._EstadoId]);
                        break;

                    case SelectFilters.ListBox:
                        VM.PlanGrupoTipoId = SysData.ToLong(oDataRow[clsPlanGrupoTipoVM._PlanGrupoTipoId]);
                        VM.PlanGrupoTipoCod = SysData.ToStr(oDataRow[clsPlanGrupoTipoVM._PlanGrupoTipoCod]);
                        VM.PlanGrupoTipoDes = SysData.ToStr(oDataRow[clsPlanGrupoTipoVM._PlanGrupoTipoDes]);
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

            if (VM.PlanGrupoTipoCod.Length == 0)
            {
                strMsg += "Código es Requerido" + Environment.NewLine;
            }

            if (VM.PlanGrupoTipoDes.Length == 0)
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