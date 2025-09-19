using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

using DataAccess.Base;
using DataAccess.Common;

namespace DataAccess.Entity
{
    public class clsObject:ProcessBase
    {
        public object ReturnObject(Hashtable theParams, string theCommandText, clsUtility.ObjectEnum theobj)
        {
            string cmdpara, cmddbtype, cmdvalue;
            SqlCommand theSQLCommand = new SqlCommand();
            SqlTransaction theTransaction = (SqlTransaction)base._theTransaction;
            SqlConnection theConnection = (base.Connection != null) ? ((SqlConnection)base.Connection) : ((SqlConnection)DataMgr.GetConnection());
            theSQLCommand = ((base.Transaction != null) ? new SqlCommand(theCommandText, theConnection, theTransaction) : new SqlCommand(theCommandText, theConnection));
            for (int i = 1; i <= theParams.Count;)
            {
                cmdpara = theParams[i].ToString();
                cmddbtype = theParams[i + 1].ToString();
                cmdvalue = theParams[i + 2].ToString();
                theSQLCommand.Parameters.AddWithValue(cmdpara, cmdvalue);
                i = i + 3;
            }

            if (clsUtility.theTableParams.Count > 0)
            {
                Hashtable theTableParams = clsUtility.theTableParams;
                for (int i = 1; i <= theTableParams.Count;)
                {
                    theSQLCommand.Parameters.AddWithValue(theTableParams[i].ToString(), (DataTable)theTableParams[i + 1]);
                    i = i + 2;
                }
            }
            clsUtility.Init_Hashtable();
            theSQLCommand.CommandType = CommandType.StoredProcedure;
            switch (theCommandText.Substring(0, 6).ToUpper())
            {
                case "SELECT":
                    theSQLCommand.CommandType = CommandType.Text;
                    break;
                case "UPDATE":
                    theSQLCommand.CommandType = CommandType.Text;
                    break;
                case "INSERT":
                    theSQLCommand.CommandType = CommandType.Text;
                    break;
                case "DELETE":
                    theSQLCommand.CommandType = CommandType.Text;
                    break;
            }
            theSQLCommand.Connection = theConnection;
            try
            {
                switch (theobj)
                {
                    case clsUtility.ObjectEnum.DataSet:
                        {
                            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(theSQLCommand);
                            DataSet dataSet = new DataSet();
                            sqlDataAdapter.Fill(dataSet);
                            sqlDataAdapter.Dispose();
                            dataSet = clsDataFormatter.FormatDataSet(dataSet);
                            return dataSet;
                        }
                    case clsUtility.ObjectEnum.DataTable:
                        {
                            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(theSQLCommand);
                            DataTable dataTable = new DataTable();
                            sqlDataAdapter.Fill(dataTable);
                            sqlDataAdapter.Dispose();
                            return dataTable;
                        }
                    case clsUtility.ObjectEnum.DataRow:
                        {
                            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(theSQLCommand);
                            DataTable dataTable = new DataTable();
                            sqlDataAdapter.Fill(dataTable);
                            sqlDataAdapter.Dispose();
                            return dataTable.Rows[0];
                        }
                    case clsUtility.ObjectEnum.ExecuteNonQuery:
                        return theSQLCommand.ExecuteNonQuery();
                    default:
                        return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (theConnection != null && base.Connection == null)
                {
                    DataMgr.ReleaseConnection(theConnection);
                }
            }
        }
        public object ReturnObject(SqlParameter[] theParams, string theCommandText, clsUtility.ObjectEnum theobj)
        {
            //string cmdpara, cmddbtype, cmdvalue;
            SqlCommand theSQLCommand = new SqlCommand();
            SqlTransaction theTransaction = (SqlTransaction)base._theTransaction;
            SqlConnection theConnection = (base.Connection != null) ? ((SqlConnection)base.Connection) : ((SqlConnection)DataMgr.GetConnection());
            theSQLCommand = ((base.Transaction != null) ? new SqlCommand(theCommandText, theConnection, theTransaction) : new SqlCommand(theCommandText, theConnection));
            if (theParams.Length > 0)
            {
                theSQLCommand.Parameters.AddRange(theParams);
            }
            theSQLCommand.CommandType = CommandType.StoredProcedure;
            switch (theCommandText.Substring(0, 6).ToUpper())
            {
                case "SELECT":
                case "UPDATE":
                case "INSERT":
                case "DELETE":
                    theSQLCommand.CommandType = CommandType.Text;
                    break;
            }
            theSQLCommand.Connection = theConnection;
            try
            {
                switch (theobj)
                {
                    case clsUtility.ObjectEnum.DataSet:
                        {
                            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(theSQLCommand);
                            DataSet dataSet = new DataSet();
                            sqlDataAdapter.Fill(dataSet);
                            sqlDataAdapter.Dispose();
                            dataSet = clsDataFormatter.FormatDataSet(dataSet);
                            return dataSet;
                        }
                    case clsUtility.ObjectEnum.DataTable:
                        {
                            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(theSQLCommand);
                            DataTable dataTable = new DataTable();
                            sqlDataAdapter.Fill(dataTable);
                            sqlDataAdapter.Dispose();
                            return dataTable;
                        }
                    case clsUtility.ObjectEnum.DataRow:
                        {
                            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(theSQLCommand);
                            DataTable dataTable = new DataTable();
                            sqlDataAdapter.Fill(dataTable);
                            sqlDataAdapter.Dispose();
                            return dataTable.Rows[0];
                        }
                    case clsUtility.ObjectEnum.ExecuteNonQuery:
                        return theSQLCommand.ExecuteNonQuery();
                    default:
                        return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (theConnection != null && base.Connection == null)
                {
                    DataMgr.ReleaseConnection(theConnection);
                }
            }
        }
    }
}
