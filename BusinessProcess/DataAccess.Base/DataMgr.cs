using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Application.Common;

namespace DataAccess.Base
{
    public class DataMgr
    {
        public DataMgr()
        {

        }

        public static object GetConnection()
        {
            string theConString = clsEncryptDecrypt.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);
            SqlConnection theConn = new SqlConnection(theConString);
            theConn.Open();
            return theConn;
        }

        public static object GetConnectionLog()
        {
            string theConString = clsEncryptDecrypt.Decrypt(ConfigurationManager.AppSettings["ConnectionStringLog"]);
            SqlConnection theConn = new SqlConnection(theConString);
            theConn.Open();
            return theConn;
        }

        public static void ReleaseConnection(object theConnection)
        {
            SqlConnection theSQLConnection = (SqlConnection)theConnection;
            if (theSQLConnection != null)
            {
                if (theSQLConnection.State != 0)
                {
                    theSQLConnection.Close();
                }
                theSQLConnection.Dispose();
            }
        }

        public static object BeginTransaction(object theConnection)
        {
            return ((SqlConnection)theConnection).BeginTransaction();
        }

        public static void CommitTransaction(object theTransaction)
        {
            ((SqlTransaction)theTransaction).Commit();
            ReleaseConnection(((SqlTransaction)theTransaction).Connection);
        }

        public static void RollbackTransaction(object theTransaction)
        {
            ((SqlTransaction)theTransaction).Rollback();
            ReleaseConnection(((SqlTransaction)theTransaction).Connection);
        }
    }
}
