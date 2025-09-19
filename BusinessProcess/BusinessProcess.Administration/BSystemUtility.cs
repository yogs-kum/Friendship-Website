using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.Administration;

namespace BusinessProcess.Administration
{
    public class BSystemUtility : ProcessBase, ISystemUtility
    {
        public BSystemUtility()
        { }

        public string RefreshSystemCache(string theKey)
        {
            clsUtility.Init_Hashtable();
            clsUtility.AddParameters("@Key", SqlDbType.VarChar, theKey);
            clsObject theCacheManager = new clsObject();
            DataSet theDS = (DataSet)theCacheManager.ReturnObject(clsUtility.theParams, "PR_Admin_RefreshSystemCache", clsUtility.ObjectEnum.DataSet);
            return theDS.GetXml();
        }

        public string GetSchedulerMailList(Int32 theStatus)
        {
            clsUtility.Init_Hashtable();
            clsUtility.AddParameters("@Status", SqlDbType.Int, theStatus.ToString());
            clsObject theSchedulerManager = new clsObject();
            DataSet theDS = (DataSet)theSchedulerManager.ReturnObject(clsUtility.theParams, "Pr_GetSchedulerMailList", clsUtility.ObjectEnum.DataSet);
            return theDS.GetXml();
        }

        public string SaveApplicationLog(Int32 theUserId, string theUserName, string theSessionId, string theIPAddress, string theComputerName, string thePageAccessed, string theControllerName, string theActionName, string theActionParameter, DateTime theLogOutTime)
        {
            try
            {
                this.Connection = DataMgr.GetConnectionLog();
                clsObject theLogManager = new clsObject();
                theLogManager.Connection = this.Connection;

                clsUtility.Init_Hashtable();
                clsUtility.AddParameters("@UserId", SqlDbType.Int, theUserId.ToString());
                clsUtility.AddParameters("@UserName", SqlDbType.VarChar, theUserName);
                clsUtility.AddParameters("@SessionId", SqlDbType.VarChar, theSessionId.ToString());
                clsUtility.AddParameters("@IPAddress", SqlDbType.VarChar, theIPAddress.ToString());
                clsUtility.AddParameters("@ComputerName", SqlDbType.VarChar, theComputerName.ToString());
                clsUtility.AddParameters("@PageAccessed", SqlDbType.VarChar, thePageAccessed.ToString());
                clsUtility.AddParameters("@ControllerName", SqlDbType.VarChar, theControllerName.ToString());
                clsUtility.AddParameters("@ActionName", SqlDbType.VarChar, theActionName.ToString());
                clsUtility.AddParameters("@ActionParameter", SqlDbType.VarChar, theActionParameter.ToString());
                clsUtility.AddParameters("@LogOutTime", SqlDbType.DateTime, theLogOutTime.ToString());
                Int32 theRows = (Int32)theLogManager.ReturnObject(clsUtility.theParams, "Pr_NDFriends2022_Admin_SaveApplicationLog", clsUtility.ObjectEnum.ExecuteNonQuery);
                DataMgr.ReleaseConnection(this.Connection);
                theRows = theRows > 0 ? theRows : 0;
                return theRows.ToString();
            }
            catch (Exception err)
            {
                DataMgr.ReleaseConnection(this.Connection);
                throw err;
            }
        }
        public string SaveUpdateApplicationEmailLog(Int32 theId, string theToMail, string theSubject, string theMessage, string theAttachementFile, Int32 theStatus, string theErrorMessage, Int32 theEmailCategoryId, Int32 theOrganizationId, Int32 theRoleId)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                clsObject theEmailLogManager = new clsObject();
                theEmailLogManager.Connection = this.Connection;

                clsUtility.Init_Hashtable();
                clsUtility.AddParameters("@Id", SqlDbType.Int, theId.ToString());
                clsUtility.AddParameters("@ToMail", SqlDbType.VarChar, theToMail.ToString());
                clsUtility.AddParameters("@Subject", SqlDbType.VarChar, theSubject.ToString());
                clsUtility.AddParameters("@Message", SqlDbType.VarChar, theMessage.ToString());
                clsUtility.AddParameters("@AttachementFile", SqlDbType.VarChar, theAttachementFile.ToString());
                clsUtility.AddParameters("@Status", SqlDbType.Int, theStatus.ToString());
                clsUtility.AddParameters("@ErrorMessage", SqlDbType.VarChar, theErrorMessage.ToString());
                clsUtility.AddParameters("@EmailCategoryId", SqlDbType.Int, theEmailCategoryId.ToString());
                clsUtility.AddParameters("@OrganizationId", SqlDbType.Int, theOrganizationId.ToString());
                clsUtility.AddParameters("@RoleId", SqlDbType.Int, theRoleId.ToString());
                Int32 theRows = (Int32)theEmailLogManager.ReturnObject(clsUtility.theParams, "Pr_SaveUpdateApplicationEmailLog", clsUtility.ObjectEnum.ExecuteNonQuery);
                DataMgr.ReleaseConnection(this.Connection);
                theRows = theRows > 0 ? theRows : 0;
                return theRows.ToString();
            }
            catch (Exception err)
            {
                DataMgr.ReleaseConnection(this.Connection);
                throw err;
            }
        }

    }
}
