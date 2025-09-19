using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

using System.Data;
using System.Data.SqlClient;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.Security;
using System.Collections;
using Application.Common;

namespace BusinessProcess.Security
{
    public class BLogin : ProcessBase, ILogin
    {
        public BLogin()
        {

        }

        public string GetUserDetails(string theUserId)
        {
            clsUtility.Init_Hashtable();
            clsUtility.AddParameters("@UserId", SqlDbType.VarChar, theUserId);
            clsObject theSecurityManager = new clsObject();
            DataSet theDS = (DataSet)theSecurityManager.ReturnObject(clsUtility.theParams, "Pr_Security_GetUserDetails", clsUtility.ObjectEnum.DataSet);
            return theDS.GetXml();
        }

        public string ChangePassword(int theUserId, string thePassword)
        {
            try
            {
                clsObject theManager = new clsObject();
                
                clsUtility.Init_Hashtable();
                clsUtility.AddParameters("@UserId", SqlDbType.Int, theUserId.ToString());
                clsUtility.AddParameters("@Password", SqlDbType.VarChar, thePassword);
                Int32 theRows = (Int32)theManager.ReturnObject(clsUtility.theParams, "PR_Security_ChangePassword", clsUtility.ObjectEnum.ExecuteNonQuery);
                
                theRows = theRows > 0 ? theRows : 0;
                return theRows.ToString();
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public bool isAPIUserHasAccess(string theUserId, string thePassword, string theConroller)
        {
            clsUtility.Init_Hashtable();
            clsUtility.AddParameters("@UserId", SqlDbType.VarChar, theUserId);
            clsUtility.AddParameters("@Password", SqlDbType.VarChar, thePassword);
            clsUtility.AddParameters("@Controller", SqlDbType.VarChar, theConroller);
            clsObject theSecurityManager = new clsObject();
            DataTable theDT = (DataTable)theSecurityManager.ReturnObject(clsUtility.theParams, "Pr_Security_AuthenticateAPIUser", clsUtility.ObjectEnum.DataTable);
            if (theDT.Rows.Count > 0)
                return true;
            return false;
        }

        public string GetForgetPasswordDetails(string theUserId)
        {
            clsUtility.Init_Hashtable();
            clsUtility.AddParameters("@UserId", SqlDbType.VarChar, theUserId);
            clsObject theSecurityManager = new clsObject();
            DataSet theDS = (DataSet)theSecurityManager.ReturnObject(clsUtility.theParams, "Pr_Security_GetForgetPasswordDetails", clsUtility.ObjectEnum.DataSet);
            return theDS.GetXml();
        }

        public string SaveLoginLog(Int32 theUserId, string theUserName, string theSessionId, string theIPAddress, string theComputerName)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                clsObject theLogManager = new clsObject();
                theLogManager.Connection = this.Connection;

                clsUtility.Init_Hashtable();
                clsUtility.AddParameters("@UserId", SqlDbType.Int, theUserId.ToString());
                clsUtility.AddParameters("@UserName", SqlDbType.VarChar, theUserName);
                clsUtility.AddParameters("@SessionId", SqlDbType.VarChar, theSessionId.ToString());
                clsUtility.AddParameters("@IPAddress", SqlDbType.VarChar, theIPAddress.ToString());
                clsUtility.AddParameters("@ComputerName", SqlDbType.VarChar, theComputerName.ToString());
                Int32 theRows = (Int32)theLogManager.ReturnObject(clsUtility.theParams, "Pr_Security_SaveLoginLog", clsUtility.ObjectEnum.ExecuteNonQuery);
                theRows = theRows > 0 ? theRows : 0;
                return theRows.ToString();
            }
            catch (Exception err)
            {
                throw err;
            }
        }

    }
}
