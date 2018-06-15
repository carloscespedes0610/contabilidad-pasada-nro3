using System;
using System.Data;
using System.Data.SqlClient;

namespace Contabilidad.Models.DAC
{
    public abstract class clsBase
    {
        protected SqlConnection moConnection;
        protected SqlTransaction moTransaction;
        protected SqlDataAdapter moDataAdapter;
        protected DataSet moDataSet;
        protected SqlParameter[] moParameters;

        protected long mlngId;

        protected int mintRow;
        protected int mintRowsCount;
        protected int mintRowsMax;

        protected string mstrConnectionString;
        protected string mstrTableName;
        protected string mstrClassName;
        protected string mstrSQL;
        protected string mstrStoreProcName;

        protected abstract void SelectParameter();
        protected abstract void InsertParameter();
        protected abstract void UpdateParameter();
        protected abstract void DeleteParameter();

        protected abstract void SetPrimaryKey();

        protected abstract void Retrieve(DataRow oDataRow);
        public abstract bool Validate();

        public string ConnectionString
        {
            get
            {
                return mstrConnectionString;
            }

            set
            {
                mstrConnectionString = value;
            }
        }

        public SqlConnection Connection
        {
            get
            {
                return moConnection;
            }

            set
            {
                moConnection = value;
            }
        }

        public SqlTransaction Transaction
        {
            get
            {
                return moTransaction;
            }

            set
            {
                moTransaction = value;
            }
        }

        public DataSet DataSet
        {
            get
            {
                return moDataSet;
            }

            set
            {
                moDataSet = value;
            }
        }

        public int RowPosition
        {
            get
            {
                return mintRow;
            }

            set
            {
                mintRow = value;
            }
        }

        public int RowsCount
        {
            get
            {
                return mintRowsCount;
            }

            set
            {
                mintRowsCount = value;
            }
        }

        public int RowsMax
        {
            get
            {
                return mintRowsMax;
            }

            set
            {
                mintRowsMax = value;
            }
        }

        public string TableName
        {
            get
            {
                return mstrTableName;
            }
        }

        public string ClassName
        {
            get
            {
                return mstrClassName;
            }
        }

        public string SQL
        {
            get
            {
                return mstrSQL;
            }
        }

        public bool Open()
        {
            bool returnValue = false;

            try
            {
                moDataSet = new DataSet();

                SelectParameter();

                moDataAdapter = new SqlDataAdapter(mstrStoreProcName, moConnection);
                moDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                moDataAdapter.SelectCommand.Parameters.AddRange(moParameters);
                moDataAdapter.Fill(moDataSet, mstrTableName);
                moDataAdapter.Dispose();

                mintRow = 0;
                mintRowsCount = Convert.ToInt32(moDataSet.Tables[mstrTableName].Rows.Count);

                returnValue = true;
            }

            catch (Exception exp)
            {
                throw (exp);
            }

            return returnValue;
        }

        public bool Read()
        {
            bool returnValue = false;
            DataRow oDataRow = default(DataRow);

            try
            {
                if (mintRowsCount > 0)
                {
                    if (mintRow <= mintRowsCount - 1)
                    {
                        oDataRow = moDataSet.Tables[mstrTableName].Rows[mintRow];

                        Retrieve(oDataRow);
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

        public bool Find()
        {
            bool returnValue = false;

            try
            {
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

        public bool FindOnly()
        {
            bool returnValue = false;

            try
            {
                if (Open())
                {
                    if (mintRowsCount > 0)
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

        public void MoveNext()
        {
            if (mintRowsCount > 0)
            {
                if (mintRow < mintRowsCount)
                {
                    mintRow++;
                }
            }
        }

        public void MovePrevious()
        {
            if (mintRowsCount > 0)
            {
                if (mintRow > 0)
                {
                    mintRow--;
                }
            }
        }

        public void MoveFirst()
        {
            if (mintRowsCount > 0)
            {
                mintRow = 0;
            }
        }

        public void MoveLast()
        {
            if (mintRowsCount > 0)
            {
                mintRow = mintRowsCount - 1;
            }
        }

        public bool Insert()
        {
            bool returnValue = false;
            SqlCommand oCommand = default(SqlCommand);
            int intRecordsAffected = 0;

            try
            {
                if (Validate())
                {
                    InsertParameter();

                    oCommand = new SqlCommand();
                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.Parameters.AddRange(moParameters);
                    oCommand.CommandText = mstrStoreProcName;
                    oCommand.Connection = moConnection;
                    oCommand.Transaction = moTransaction;

                    intRecordsAffected = Convert.ToInt32(oCommand.ExecuteNonQuery());

                    if (intRecordsAffected > 0)
                    {
                        mlngId = Convert.ToInt64(oCommand.Parameters["@Id"].Value);
                        SetPrimaryKey();
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

        public bool Update()
        {
            bool returnValue = false;
            SqlCommand oCommand = default(SqlCommand);
            int intRecordsAffected = 0;

            try
            {
                if (Validate())
                {
                    UpdateParameter();

                    oCommand = new SqlCommand();
                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.Parameters.AddRange(moParameters);
                    oCommand.CommandText = mstrStoreProcName;
                    oCommand.Connection = moConnection;
                    oCommand.Transaction = moTransaction;

                    intRecordsAffected = Convert.ToInt32(oCommand.ExecuteNonQuery());

                    if (intRecordsAffected > 0)
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

        public bool UpdateOnly()
        {
            bool returnValue = false;
            SqlCommand oCommand = default(SqlCommand);
            int intRecordsAffected = 0;

            try
            {
                UpdateParameter();

                oCommand = new SqlCommand();
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddRange(moParameters);
                oCommand.CommandText = mstrStoreProcName;
                oCommand.Connection = moConnection;
                oCommand.Transaction = moTransaction;

                intRecordsAffected = Convert.ToInt32(oCommand.ExecuteNonQuery());

                if (intRecordsAffected > 0)
                {
                    returnValue = true;
                }
            }

            catch (Exception exp)
            {
                throw (exp);
            }

            return returnValue;
        }

        public bool Delete()
        {
            bool returnValue = false;
            SqlCommand oCommand = default(SqlCommand);
            int intRecordsAffected = 0;

            try
            {
                DeleteParameter();

                oCommand = new SqlCommand();
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddRange(moParameters);
                oCommand.CommandText = mstrStoreProcName;
                oCommand.Connection = moConnection;
                oCommand.Transaction = moTransaction;

                intRecordsAffected = Convert.ToInt32(oCommand.ExecuteNonQuery());

                if (intRecordsAffected > 0)
                {
                    returnValue = true;
                }
            }

            catch (Exception exp)
            {
                throw (exp);
            }

            return returnValue;
        }

        public int SelectRowCount()
        {
            int returnValue = 0;
            returnValue = 0;

            try
            {
                if (Open())
                {
                    return mintRowsCount;
                }
            }

            catch (Exception exp)
            {
                throw (exp);
            }

            return returnValue;
        }

        public void BeginTransaction()
        {
            try
            {
                if (clsAppInfo.Connection.State == System.Data.ConnectionState.Open)
                    moTransaction = clsAppInfo.Connection.BeginTransaction(IsolationLevel.ReadCommitted);
            }

            catch (Exception exp)
            {
                throw (exp);
            }
        }

        public void Commit()
        {
            try
            {
                moTransaction.Commit();
            }

            catch (Exception exp)
            {
                throw (exp);
            }
        }

        public void Rollback()
        {
            try
            {
                moTransaction.Rollback();
            }

            catch (Exception exp)
            {
                throw (exp);
            }
        }

    }
}