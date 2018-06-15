using System;
using System.Data.SqlClient;
using System.Data;
using Contabilidad.Models.VM;

namespace Contabilidad.Models.DAC
{
   public class clsTipoUsuario : clsBase, IDisposable
   {
      public clsTipoUsuarioVM VM;

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
         TipoUsuarioDes = 2,
         Grid = 3,
         GridCheck = 4,
         TipoUsuarioCod = 5,
         Estado = 6,
         EstadoId = 7
      }

      public enum OrderByFilters : byte
      {
         None = 0,
         TipoUsuarioId = 1,
         TipoUsuarioDes = 2,
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
      public clsTipoUsuario()
      {
         mstrTableName = "segTipoUsuario";
         mstrClassName = "clsTipoUsuario";

         PropertyInit();
         FilterInit();
      }

      public clsTipoUsuario(string ConnectString) : this()
      {
         moConnection = new SqlConnection();

         mstrConnectionString = ConnectString;
      }

      public clsTipoUsuario(SqlConnection oConnection) : this()
      {
         moConnection = oConnection;
      }

      public clsTipoUsuario(SqlConnection oConnection, SelectFilters bytSelectFilter) : this()
      {
         moConnection = oConnection;
         mintSelectFilter = bytSelectFilter;
      }

      public clsTipoUsuario(SqlConnection oConnection, SelectFilters bytSelectFilter, WhereFilters bytWhereFilter) : this()
      {
         moConnection = oConnection;
         mintSelectFilter = bytSelectFilter;
         mintWhereFilter = bytWhereFilter;
      }

      public clsTipoUsuario(SqlConnection oConnection, SelectFilters bytSelectFilter, WhereFilters bytWhereFilter, OrderByFilters bytOrderByFilter) : this()
      {
         moConnection = oConnection;
         mintSelectFilter = bytSelectFilter;
         mintWhereFilter = bytWhereFilter;
         mintOrderByFilter = bytOrderByFilter;
      }

      public void PropertyInit()
      {
         VM = new clsTipoUsuarioVM();
         VM.TipoUsuarioId = 0;
         VM.TipoUsuarioCod = "";
         VM.TipoUsuarioDes = "";
         VM.EstadoId = 0;
      }

      protected override void SetPrimaryKey()
      {
         VM.TipoUsuarioId = mlngId;
      }

      protected override void SelectParameter()
      {
         string strSQL = null;

         mstrStoreProcName = "segTipoUsuarioSelect";

         switch (mintSelectFilter)
         {
            case SelectFilters.All:
               strSQL = " SELECT  " +
                        "    segTipoUsuario.TipoUsuarioId, " +
                        "    segTipoUsuario.TipoUsuarioCod, " +
                        "    segTipoUsuario.TipoUsuarioDes, " +
                        "    segTipoUsuario.EstadoId " +
                        " FROM  segTipoUsuario ";
               break;

            case SelectFilters.ListBox:
               strSQL = " SELECT  " +
                        "    segTipoUsuario.TipoUsuarioId, " +
                        "    segTipoUsuario.TipoUsuarioCod, " +
                        "    segTipoUsuario.TipoUsuarioDes " +
                        " FROM  segTipoUsuario ";
               break;

            case SelectFilters.Grid:
               strSQL = " SELECT  " +
                        "    segTipoUsuario.TipoUsuarioId, " +
                        "    segTipoUsuario.TipoUsuarioDes, " +
                        "    segTipoUsuario.TipoUsuarioCod, " +
                        "    segTipoUsuario.EstadoId, " +
                        "    parEstado.EstadoDes " +
                        " FROM  segTipoUsuario " +
                        " LEFT JOIN	parEstado ON segTipoUsuario.EstadoId = parEstado.EstadoId ";

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
               strSQL = " WHERE TipoUsuarioId = " + SysData.NumberToField(VM.TipoUsuarioId);
               break;

            case WhereFilters.EstadoId:
               strSQL = " WHERE segTipoUsuario.EstadoId = " + SysData.NumberToField(VM.EstadoId);
               break;

            case WhereFilters.Grid:
               break;

            case WhereFilters.Estado:
               strSQL = " WHERE TipoUsuarioId = " + SysData.NumberToField(VM.TipoUsuarioId);
               break;

         }

         return strSQL;
      }

      private string OrderByFilterGet()
      {
         string strSQL = null;

         switch (mintOrderByFilter)
         {
            case OrderByFilters.TipoUsuarioId:
               strSQL = " ORDER BY segTipoUsuario.TipoUsuarioId ";
               break;

            case OrderByFilters.Grid:
               strSQL = " ORDER BY segTipoUsuario.TipoUsuarioDes";
               break;

            case OrderByFilters.TipoUsuarioDes:
               strSQL = " ORDER BY segTipoUsuario.TipoUsuarioDes ";
               break;
         }

         return strSQL;
      }

      protected override void InsertParameter()
      {
         switch (mintInsertFilter)
         {
            case InsertFilters.All:
               mstrStoreProcName = "segTipoUsuarioInsert";
               moParameters = new SqlParameter[5] {
                        new SqlParameter("InsertFilter", mintInsertFilter),
                        new SqlParameter("Id", SqlDbType.Int),
                        new SqlParameter(clsTipoUsuarioVM._TipoUsuarioCod, VM.TipoUsuarioCod),
                        new SqlParameter(clsTipoUsuarioVM._TipoUsuarioDes, VM.TipoUsuarioDes),
                        new SqlParameter(clsTipoUsuarioVM._EstadoId, VM.EstadoId)};
               moParameters[1].Direction = ParameterDirection.Output;
               break;
         }
      }

      protected override void UpdateParameter()
      {
         switch (mintUpdateFilter)
         {
            case UpdateFilters.All:
               mstrStoreProcName = "segTipoUsuarioUpdate";
               moParameters = new SqlParameter[5] {
                        new SqlParameter("UpdateFilter", mintUpdateFilter),
                        new SqlParameter(clsTipoUsuarioVM._TipoUsuarioId, VM.TipoUsuarioId),
                        new SqlParameter(clsTipoUsuarioVM._TipoUsuarioCod, VM.TipoUsuarioCod),
                        new SqlParameter(clsTipoUsuarioVM._TipoUsuarioDes, VM.TipoUsuarioDes),
                        new SqlParameter(clsTipoUsuarioVM._EstadoId, VM.EstadoId)};
               break;
         }
      }

      protected override void DeleteParameter()
      {
         switch (mintDeleteFilter)
         {
            case DeleteFilters.All:
               mstrStoreProcName = "segTipoUsuarioDelete";
               moParameters = new SqlParameter[2] {
                        new SqlParameter("DeleteFilter", mintDeleteFilter),
                        new SqlParameter(clsTipoUsuarioVM._TipoUsuarioId, VM.TipoUsuarioId)};
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
                  VM.TipoUsuarioId = SysData.ToLong(oDataRow[clsTipoUsuarioVM._TipoUsuarioId]);
                  VM.TipoUsuarioCod = SysData.ToStr(oDataRow[clsTipoUsuarioVM._TipoUsuarioCod]);
                  VM.TipoUsuarioDes = SysData.ToStr(oDataRow[clsTipoUsuarioVM._TipoUsuarioDes]);
                  VM.EstadoId = SysData.ToLong(oDataRow[clsTipoUsuarioVM._EstadoId]);
                  break;

               case SelectFilters.ListBox:
                  VM.TipoUsuarioId = SysData.ToLong(oDataRow[clsTipoUsuarioVM._TipoUsuarioId]);
                  VM.TipoUsuarioCod = SysData.ToStr(oDataRow[clsTipoUsuarioVM._TipoUsuarioCod]);
                  VM.TipoUsuarioDes = SysData.ToStr(oDataRow[clsTipoUsuarioVM._TipoUsuarioDes]);
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

         if (VM.TipoUsuarioCod.Length == 0) { strMsg += "Código es Requerido" + Environment.NewLine; }

         if (VM.TipoUsuarioDes.Length == 0) { strMsg += "Descipción del Tipo Usuario es Requerido" + Environment.NewLine; }

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