using System;
using System.Data.SqlClient;
using System.Data;
using Contabilidad.Models.VM;

namespace Contabilidad.Models.DAC
{
    public class clsAutoriza : clsBase, IDisposable
    {
        public clsAutorizaVM VM;

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
            AutorizaDes = 2,
            Grid = 3,
            GridAutorizaId = 4,
            TipoUsuarioIdAppId = 5,
            AutorizaItemSel = 6
        }

        public enum OrderByFilters : byte
        {
            None = 0,
            AutorizaId = 1,
            AutorizaDes = 2,
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
            TipoUsuarioAutorizaSel = 1
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
        public clsAutoriza()
        {
            mstrTableName = "segAutoriza";
            mstrClassName = "clsAutoriza";

            PropertyInit();
            FilterInit();
        }

        public clsAutoriza(string ConnectString) : this()
        {
            moConnection = new SqlConnection();

            mstrConnectionString = ConnectString;
        }

        public clsAutoriza(SqlConnection oConnection) : this()
        {
            moConnection = oConnection;
        }

        public clsAutoriza(SqlConnection oConnection, SelectFilters bytSelectFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
        }

        public clsAutoriza(SqlConnection oConnection, SelectFilters bytSelectFilter, WhereFilters bytWhereFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
            mintWhereFilter = bytWhereFilter;
        }

        public clsAutoriza(SqlConnection oConnection, SelectFilters bytSelectFilter, WhereFilters bytWhereFilter, OrderByFilters bytOrderByFilter) : this()
        {
            moConnection = oConnection;
            mintSelectFilter = bytSelectFilter;
            mintWhereFilter = bytWhereFilter;
            mintOrderByFilter = bytOrderByFilter;
        }

        public void PropertyInit()
        {
            VM = new clsAutorizaVM();
            VM.AutorizaId = 0;
            VM.TipoUsuarioId = 0;
            VM.AutorizaDes = "";
            VM.RegistroId = 0;
            VM.AutorizaItemId = 0;
            VM.ModuloId = 0;
        }

        protected override void SetPrimaryKey()
        {
            VM.AutorizaId = mlngId;
        }

        protected override void SelectParameter()
        {
            string strSQL = null;

            mstrStoreProcName = "segAutorizaSelect";

            switch (mintSelectFilter)
            {
                case SelectFilters.All:
                    strSQL = " SELECT  " +
                            "    segAutoriza.AutorizaId, " +
                            "    segAutoriza.TipoUsuarioId, " +
                            "    segAutoriza.ModuloId, " +
                            "    segAutoriza.AutorizaDes, " +
                            "    segAutoriza.RegistroId, " +
                            "    segAutoriza.AutorizaItemId " +
                            " FROM segAutoriza ";
                    break;

                case SelectFilters.ListBox:
                    strSQL = " SELECT  " +
                            "    segAutoriza.AutorizaId, " +
                            "    segAutoriza.ModuloId, " +
                            "    segAutoriza.AutorizaDes " +
                            " FROM segAutoriza ";
                    break;

                case SelectFilters.Grid:
                    strSQL = " SELECT  " +
                            "    segAutoriza.AutorizaId, " +
                            "    segTipoUsuario.TipoUsuarioId, " +
                            "    segTipoUsuario.TipoUsuarioDes, " +
                            "    segModulo.ModuloId, " +
                            "    segModulo.ModuloDes, " +
                            "    segAutoriza.AutorizaDes, " +
                            "    segAutoriza.RegistroId, " +
                            "    segAutorizaItem.AutorizaItemId, " +
                            "    segAutorizaItem.AutorizaItemDes " +
                            " FROM	segAutoriza " +
                            " LEFT JOIN	segTipoUsuario ON segAutoriza.TipoUsuarioId = segTipoUsuario.TipoUsuarioId	" +
                            " LEFT JOIN	segModulo ON segAutoriza.ModuloId = segModulo.ModuloId	" +
                            " LEFT JOIN	segAutorizaItem  ON segAutoriza.ModuloId = segAutorizaItem.ModuloId	";
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
                    strSQL = " WHERE AutorizaId = " + SysData.NumberToField(VM.AutorizaId);
                    break;

                case WhereFilters.TipoUsuarioIdAppId:
                    strSQL = " WHERE segAutoriza.TipoUsuarioId = " + SysData.NumberToField(VM.TipoUsuarioId) +
                             " AND segAutoriza.AutorizaItemId = " + SysData.NumberToField(VM.AutorizaItemId) +
                             " AND segAutoriza.RegistroId = " + SysData.NumberToField(VM.RegistroId);
                    break;

                case WhereFilters.Grid:
                    break;

                case WhereFilters.AutorizaItemSel:
                    strSQL = " WHERE segAutoriza.TipoUsuarioId = " + SysData.NumberToField(VM.TipoUsuarioId) +
                             " AND segAutoriza.AutorizaItemId = " + SysData.NumberToField(VM.AutorizaItemId);
                    break;
            }

            return strSQL;
        }

        private string OrderByFilterGet()
        {
            string strSQL = null;

            switch (mintOrderByFilter)
            {
                case OrderByFilters.AutorizaId:
                    break;

                case OrderByFilters.AutorizaDes:
                    break;

                case OrderByFilters.Grid:
                    strSQL = " ORDER BY segAutorizaItem.AutorizaItemDes, segTipoUsuario.TipoUsuarioDes ";
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
                    mstrStoreProcName = "segAutorizaInsert";
                    moParameters = new SqlParameter[7] {
                        new SqlParameter("@InsertFilter", mintInsertFilter),
                        new SqlParameter("@Id", SqlDbType.Int),
                        new SqlParameter(clsAutorizaVM._TipoUsuarioId,VM.TipoUsuarioId),
                        new SqlParameter(clsAutorizaVM._AutorizaDes,VM.AutorizaDes),
                        new SqlParameter(clsAutorizaVM._RegistroId,VM.RegistroId),
                        new SqlParameter(clsAutorizaVM._AutorizaItemId,VM.AutorizaItemId),
                        new SqlParameter(clsAutorizaVM._ModuloId,VM.ModuloId) };
                    moParameters[1].Direction = ParameterDirection.Output;
                    break;
            }
        }

        protected override void UpdateParameter()
        {
            switch (mintUpdateFilter)
            {
                case UpdateFilters.All:
                    mstrStoreProcName = "segAutorizaUpdate";
                    moParameters = new SqlParameter[7] {
                        new SqlParameter("@UpdateFilter", mintUpdateFilter),
                        new SqlParameter(clsAutorizaVM._AutorizaId,VM.AutorizaId),
                        new SqlParameter(clsAutorizaVM._TipoUsuarioId,VM.TipoUsuarioId),
                        new SqlParameter(clsAutorizaVM._AutorizaDes,VM.AutorizaDes),
                        new SqlParameter(clsAutorizaVM._RegistroId,VM.RegistroId),
                        new SqlParameter(clsAutorizaVM._AutorizaItemId,VM.AutorizaItemId),
                        new SqlParameter(clsAutorizaVM._ModuloId,VM.ModuloId)};
                    break;
            }
        }

        protected override void DeleteParameter()
        {
            switch (mintDeleteFilter)
            {
                case DeleteFilters.All:
                    mstrStoreProcName = "segAutorizaDelete";
                    moParameters = new SqlParameter[7] {
                        new SqlParameter("@DeleteFilter", mintDeleteFilter),
                        new SqlParameter(clsAutorizaVM._AutorizaId,VM.AutorizaId),
                        new SqlParameter(clsAutorizaVM._TipoUsuarioId,VM.TipoUsuarioId),
                        new SqlParameter(clsAutorizaVM._AutorizaDes,VM.AutorizaDes),
                        new SqlParameter(clsAutorizaVM._RegistroId,VM.RegistroId),
                        new SqlParameter(clsAutorizaVM._AutorizaItemId,VM.AutorizaItemId),
                        new SqlParameter(clsAutorizaVM._ModuloId,VM.ModuloId)};
                    break;

                case DeleteFilters.TipoUsuarioAutorizaSel:
                    mstrStoreProcName = "segAutorizaDelete";
                    moParameters = new SqlParameter[7] {
                        new SqlParameter("@DeleteFilter", mintDeleteFilter),
                        new SqlParameter(clsAutorizaVM._AutorizaId,VM.AutorizaId),
                        new SqlParameter(clsAutorizaVM._TipoUsuarioId,VM.TipoUsuarioId),
                        new SqlParameter(clsAutorizaVM._AutorizaDes,VM.AutorizaDes),
                        new SqlParameter(clsAutorizaVM._RegistroId,VM.RegistroId),
                        new SqlParameter(clsAutorizaVM._AutorizaItemId,VM.AutorizaItemId),
                        new SqlParameter(clsAutorizaVM._ModuloId,VM.ModuloId)};
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
                        VM.AutorizaId = SysData.ToLong(oDataRow[clsAutorizaVM._AutorizaId]);
                        VM.TipoUsuarioId = SysData.ToLong(oDataRow[clsAutorizaVM._TipoUsuarioId]);
                        VM.AutorizaDes = SysData.ToStr(oDataRow[clsAutorizaVM._AutorizaDes]);
                        VM.RegistroId = SysData.ToLong(oDataRow[clsAutorizaVM._RegistroId]);
                        VM.AutorizaItemId = SysData.ToLong(oDataRow[clsAutorizaVM._AutorizaItemId]);
                        VM.ModuloId = SysData.ToLong(oDataRow[clsAutorizaVM._ModuloId]);
                        break;

                    case SelectFilters.ListBox:
                        VM.AutorizaId = SysData.ToLong(oDataRow[clsAutorizaVM._AutorizaId]);
                        VM.AutorizaDes = SysData.ToStr(oDataRow[clsAutorizaVM._AutorizaDes]);
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

            if (VM.TipoUsuarioId <= 0) { strMsg += "El Tipo de Usuario es Requerido" + Environment.NewLine; }

            if (VM.ModuloId <= 0) { strMsg += "El Modulo es Requerido" + Environment.NewLine; }

            if (VM.AutorizaItemId <= 0) { strMsg += "El Autoriza Item es Requerido" + Environment.NewLine; }

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