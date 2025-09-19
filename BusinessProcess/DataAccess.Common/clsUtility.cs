using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Common
{
    public class clsUtility
    {
        private static Int32 thePKey;
        private static Int32 theTableKey;
        public static Hashtable theParams = new Hashtable();
        public static Hashtable theTableParams = new Hashtable();

        public static void Init_Hashtable()
        {
            theParams = new Hashtable();
            theTableParams = new Hashtable();
            thePKey = 1;
            theTableKey = 1;
        }

        public static void AddParameters(string theParameterName, SqlDbType theParameterType, string theParameterValue)
        {
            theParams.Add(thePKey, theParameterName);
            thePKey++;
            theParams.Add(thePKey, theParameterType);
            thePKey++;
            theParams.Add(thePKey, theParameterValue);
            thePKey++;
        }

        public static void AddTableParameters(string theParameterName, DataTable theTable)
        {
            theTableParams.Add(theTableKey, theParameterName);
            theTableKey++;
            theTableParams.Add(theTableKey, theTable);
            theTableKey++;
        }
        public enum ObjectEnum
        {
            DataSet,DataTable,DataRow,ExecuteNonQuery
        }


    }
}
