using System;
using System.Data.SqlClient;
using System.Data;
using Contabilidad.Models.DAC;
using Contabilidad.Models.VM;

namespace Contabilidad.Models.DAC
{
   public class clsTipoPersona : clsBase, IDisposable
   {
      public clsTipoPersonaVM VM = new clsTipoPersonaVM();

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
         TipoPersonaDes = 2,
         Grid = 3,
         GridCheck = 4,
         TipoPersonaCod = 5,
         EstadoId = 6
      }

      public enum OrderByFilters : byte
      {
         None = 0,
         TipoPersonaId = 1,
         TipoPersonaDes = 2,
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
      public clsTipoPersona()
      {
         mstrTableName = "parTipoPersona";
         mstrClassName = "clsTipoPersona";

         PropertyInit();
         FilterInit();
      }

      public clsTipoPersona(string ConnectString) : this()
      {
         moConnection = new SqlConnection();

         mstrConnectionString = ConnectString;
      }

      public clsTipoPersona(SqlConnection oConnection) : this()
      {
         moConnection = oConnection;
      }

      public clsTipoPersona(SqlConnection oConnection, SelectFilters bytSelectFilter) : this()
      {
         moConnection = oConnection;
         mintSelectFilter = bytSelectFilter;
      }

      public clsTipoPersona(SqlConnection oConnection, SelectFilters bytSelectFilter, WhereFilters bytWhereFilter) : this()
      {
         moConnection = oConnection;
         mintSelectFilter = bytSelectFilter;
         mintWhereFilter = bytWhereFilter;
      }

      public clsTipoPersona(SqlConnection oConnection, SelectFilters bytSelectFilter, WhereFilters bytWhereFilter, OrderByFilters bytOrderByFilter) : this()
      {
         moConnection = oConnection;
         mintSelectFilter = bytSelectFilter;
         mintWhereFilter = bytWhereFilter;
         mintOrderByFilter = bytOrderByFilter;
      }

      public void PropertyInit()
      {
         VM = new clsTipoPersonaVM();
         VM.TipoPersonaId = 0;
         VM.TipoPersonaCod = "";
         VM.TipoPersonaDes = "";
         VM.TipoRelacionId = 0;
         VM.EstadoId = 0;
      }

      protected override void SetPrimaryKey()
      {
         VM.TipoPersonaId = mlngId;
      }

      protected override void SelectParameter()
      {
         string strSQL = null;

         mstrStoreProcName = "parTipoPersonaSelect";

         switch (mintSelectFilter)
         {
            case SelectFilters.All:
               strSQL = " SELECT  " +
                        "    parTipoPersona.TipoPersonaId, " +
                        "    parTipoPersona.TipoPersonaCod, " +
                        "    parTipoPersona.TipoPersonaDes, " +
                        "    parTipoPersona.TipoRelacionId, " +
                        "    parTipoPersona.EstadoId " +
                        "  FROM  parTipoPersona ";
               break;

            case SelectFilters.ListBox:
               strSQL = " SELECT  " +
                        "    parTipoPersona.TipoPersonaId, " +
                        "    parTipoPersona.TipoPersonaCod, " +
                        "    parTipoPersona.TipoPersonaDes " +
                        " FROM  parTipoPersona ";
               break;

            case SelectFilters.Grid:
               strSQL = " SELECT  " +
                        "    parTipoPersona.TipoPersonaId, " +
                        "    parTipoPersona.TipoPersonaCod, " +
                        "    parTipoPersona.TipoPersonaDes, " +
                        "    parTipoPersona.TipoRelacionId, " +
                        "    parTipoRelacion.TipoRelacionDes, " +
                        "    parTipoPersona.EstadoId, " +
                        "    parEstado.EstadoDes " +
                        " FROM  parTipoPersona " +
               "  LEFT JOIN	parTipoRelacion	ON parTipoPersona.TipoRelacionId = parTipoRelacion.TipoRelacionId " +
               "  LEFT JOIN	parEstado ON parTipoPersona.EstadoId = parEstado.EstadoId ";
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
               strSQL = " WHERE parTipoPersona.TipoPersonaId = " + SysData.NumberToField(VM.TipoPersonaId);
               break;

            case WhereFilters.TipoPersonaCod:
               strSQL = " WHERE parTipoPersona.TipoPersonaCod = " + SysData.StringToField(VM.TipoPersonaCod);
               break;

            case WhereFilters.EstadoId:
               strSQL = " WHERE parTipoPersona.EstadoId = " + SysData.NumberToField(VM.EstadoId);
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
            case OrderByFilters.TipoPersonaId:
               strSQL = " ORDER BY parTipoPersona.TipoPersonaId ";
               break;

            case OrderByFilters.Grid:
               strSQL = " ORDER BY parTipoRelacion.TipoRelacionDes, parTipoPersona.TipoPersonaDes ";
               break;

            case OrderByFilters.TipoPersonaDes:
               strSQL = " ORDER BY parTipoPersona.TipoPersonaDes ";
               break;
         }

         return strSQL;
      }

      protected override void InsertParameter()
      {
         switch (mintInsertFilter)
         {
            case InsertFilters.All:
               mstrStoreProcName = "parTipoPersonaInsert";
               moParameters = new SqlParameter[6] {
                        new SqlParameter("InsertFilter", mintInsertFilter),
                        new SqlParameter("Id", SqlDbType.Int),
                        new SqlParameter(clsTipoPersonaVM._TipoPersonaCod, VM.TipoPersonaCod),
                        new SqlParameter(clsTipoPersonaVM._TipoPersonaDes, VM.TipoPersonaDes),
                        new SqlParameter(clsTipoPersonaVM._TipoRelacionId, VM.TipoRelacionId),
                        new SqlParameter(clsTipoPersonaVM._EstadoId, VM.EstadoId)};
               moParameters[1].Direction = ParameterDirection.Output;
               break;
         }
      }

      protected override void UpdateParameter()
      {
         switch (mintUpdateFilter)
         {
            case UpdateFilters.All:
               mstrStoreProcName = "parTipoPersonaUpdate";
               moParameters = new SqlParameter[6] {
                        new SqlParameter("@UpdateFilter", mintUpdateFilter),
                        new SqlParameter(clsTipoPersonaVM._TipoPersonaId, VM.TipoPersonaId),
                        new SqlParameter(clsTipoPersonaVM._TipoPersonaCod, VM.TipoPersonaCod),
                        new SqlParameter(clsTipoPersonaVM._TipoPersonaDes, VM.TipoPersonaDes),
                        new SqlParameter(clsTipoPersonaVM._TipoRelacionId, VM.TipoRelacionId),
                        new SqlParameter(clsTipoPersonaVM._EstadoId, VM.EstadoId)};
               break;
         }
      }

      protected override void DeleteParameter()
      {
         switch (mintDeleteFilter)
         {
            case DeleteFilters.All:
               mstrStoreProcName = "parTipoPersonaDelete";
               moParameters = new SqlParameter[2] {
                        new SqlParameter("@DeleteFilter", mintDeleteFilter),
                        new SqlParameter(clsTipoPersonaVM._TipoPersonaId, VM.TipoPersonaId)};
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
                  VM.TipoPersonaId = SysData.ToLong(oDataRow[clsTipoPersonaVM._TipoPersonaId]);
                  VM.TipoPersonaCod = SysData.ToStr(oDataRow[clsTipoPersonaVM._TipoPersonaCod]);
                  VM.TipoPersonaDes = SysData.ToStr(oDataRow[clsTipoPersonaVM._TipoPersonaDes]);
                  VM.TipoRelacionId = SysData.ToLong(oDataRow[clsTipoPersonaVM._TipoRelacionId]);
                  VM.EstadoId = SysData.ToLong(oDataRow[clsTipoPersonaVM._EstadoId]);
                  break;

               case SelectFilters.ListBox:
                  VM.TipoPersonaId = SysData.ToLong(oDataRow[clsTipoPersonaVM._TipoPersonaId]);
                  VM.TipoPersonaCod = SysData.ToStr(oDataRow[clsTipoPersonaVM._TipoPersonaCod]);
                  VM.TipoPersonaDes = SysData.ToStr(oDataRow[clsTipoPersonaVM._TipoPersonaDes]);
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

         if (VM.TipoRelacionId == 0) { strMsg += "Tipo Relación es Requerido" + Environment.NewLine; }

         if (VM.TipoPersonaCod.Length == 0) { strMsg += "Código es Requerido" + Environment.NewLine; }

         if (VM.TipoPersonaDes.Length == 0) { strMsg += "Descipción del Tipo Usuario es Requerido" + Environment.NewLine; }

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