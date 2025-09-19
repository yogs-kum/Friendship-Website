using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.Security;

namespace BusinessProcess.Security
{
    public class BUser : ProcessBase, IUser
    {
        public BUser()
        { }

        public string GetGroupDetails(Int32 theGroupId)
        {
            clsUtility.Init_Hashtable();
            clsUtility.AddParameters("@GroupId", SqlDbType.Int, theGroupId.ToString());
            clsObject theGroupManager = new clsObject();
            DataSet theDS = (DataSet)theGroupManager.ReturnObject(clsUtility.theParams, "Pr_Security_GetGroupInfo", clsUtility.ObjectEnum.DataSet);
            return theDS.GetXml();
        }

        public string GetGroupModuleFunction()
        {
            clsUtility.Init_Hashtable();
            clsObject theGroupManager = new clsObject();
            DataSet theDS = (DataSet)theGroupManager.ReturnObject(clsUtility.theParams, "Pr_Security_GetGroupModuleFunction", clsUtility.ObjectEnum.DataSet);
            return theDS.GetXml();
        }

        public string SaveUpdateGroups(Int32 theGroupId, string theGroupName, Int32 theDeleted, Int32 theUserId, DataTable theGroupFeature)
        {
            try
            {
                clsObject theManager = new clsObject();
                
                clsUtility.Init_Hashtable();
                clsUtility.AddParameters("@GroupId", SqlDbType.Int, theGroupId.ToString());
                clsUtility.AddParameters("@GroupName", SqlDbType.VarChar, theGroupName);
                clsUtility.AddParameters("@Deleted", SqlDbType.Int, theDeleted.ToString());
                clsUtility.AddParameters("@UserId", SqlDbType.Int, theUserId.ToString());
                clsUtility.AddTableParameters("@GroupFeature", theGroupFeature);
                Int32 theRows = (Int32)theManager.ReturnObject(clsUtility.theParams, "Pr_Security_InsertUpdateGroups", clsUtility.ObjectEnum.ExecuteNonQuery);
                
                theRows = theRows > 0 ? theRows : 0;
                return theRows.ToString();
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public string GetUserDetails(Int32 theUserId)
        {
            clsUtility.Init_Hashtable();
            clsUtility.AddParameters("@UserId", SqlDbType.Int, theUserId.ToString());
            clsObject theUserManager = new clsObject();
            DataSet theDS = (DataSet)theUserManager.ReturnObject(clsUtility.theParams, "Pr_Security_GetUserInfo", clsUtility.ObjectEnum.DataSet);
            return theDS.GetXml();
        }

        public string SaveUpdateUsers(Int32 theId, string UserFirstName, string UserLastName, string theUserId, string theEmail, string thePassword,
            Int32 theOrganizationId, Int32 theDeleted, Int32 theOperatorId, DataTable theUserGroup, DataTable theUserLocation)
        {
            try
            {
                clsObject theManager = new clsObject();
                clsUtility.Init_Hashtable();
                clsUtility.AddParameters("@Id", SqlDbType.Int, theId.ToString());
                clsUtility.AddParameters("@UserFirstName", SqlDbType.VarChar, UserFirstName);
                clsUtility.AddParameters("@UserLastName", SqlDbType.VarChar, UserLastName);
                clsUtility.AddParameters("@UserId", SqlDbType.VarChar, theUserId);
                clsUtility.AddParameters("@EmailId", SqlDbType.VarChar, theEmail);
                clsUtility.AddParameters("@Password", SqlDbType.VarChar, thePassword);
                clsUtility.AddParameters("@OrganizationId", SqlDbType.Int, theOrganizationId.ToString());
                clsUtility.AddParameters("@Deleted", SqlDbType.Int, theDeleted.ToString());
                clsUtility.AddParameters("@OperatorId", SqlDbType.Int, theOperatorId.ToString());
                clsUtility.AddTableParameters("@TblUserGroup", theUserGroup);
                clsUtility.AddTableParameters("@TblUserLocation", theUserLocation);

                Int32 theRows = (Int32)theManager.ReturnObject(clsUtility.theParams, "Pr_Security_InsertUpdateUsers", clsUtility.ObjectEnum.ExecuteNonQuery);

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