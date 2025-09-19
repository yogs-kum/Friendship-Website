using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using Application.Common;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.Administration;

namespace BusinessProcess.Administration
{
    public class BOrganization : ProcessBase, IOrganization
    {
        public BOrganization()
        { }

        public string GetOrganizationInfo(Int32 theOrganizationId)
        {
            clsObject theOrganizationManager = new clsObject();
            clsUtility.Init_Hashtable();
            clsUtility.AddParameters("@OrganizationId", SqlDbType.Int, theOrganizationId.ToString());
            DataSet theDS = (DataSet)theOrganizationManager.ReturnObject(clsUtility.theParams, "Pr_Admin_GetOrganizationInfo", clsUtility.ObjectEnum.DataSet);
            return theDS.GetXml();
        }

        public string SaveUpdateOrganization(Int32 theOrganizationId, string theCode, string theName, string theShortName, string theToken, string theAddress1,
            string theAddress2, string theCity, Int32 theStateId, Int32 theCountryId, string thePin, string thePhone, string theEmail, string theCFirstName, string theCLastName, string thePassword,
            string theWebsite, string theLogoFile, Int32 theDeleted, Int32 theUserId)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                clsObject theManager = new clsObject();
                theManager.Connection = this.Connection;
                theManager.Transaction = this.Transaction;

                clsUtility.Init_Hashtable();
                clsUtility.AddParameters("@OrgId", SqlDbType.Int, theOrganizationId.ToString());
                clsUtility.AddParameters("@Code", SqlDbType.VarChar, theCode);
                clsUtility.AddParameters("@Name", SqlDbType.VarChar, theName);
                clsUtility.AddParameters("@ShortName", SqlDbType.VarChar, theShortName);
                clsUtility.AddParameters("@Token", SqlDbType.VarChar, theToken);
                clsUtility.AddParameters("@Address1", SqlDbType.VarChar, theAddress1);
                clsUtility.AddParameters("@Address2", SqlDbType.VarChar, theAddress2);
                clsUtility.AddParameters("@City", SqlDbType.VarChar, theCity);
                clsUtility.AddParameters("@StateId", SqlDbType.Int, theStateId.ToString());
                clsUtility.AddParameters("@CountryId", SqlDbType.Int, theCountryId.ToString());
                clsUtility.AddParameters("@Pin", SqlDbType.VarChar, thePin);
                clsUtility.AddParameters("@Phone", SqlDbType.VarChar, thePhone);
                clsUtility.AddParameters("@Email", SqlDbType.VarChar, theEmail);
                clsUtility.AddParameters("@CFirstName", SqlDbType.VarChar, theCFirstName);
                clsUtility.AddParameters("@CLastName", SqlDbType.VarChar, theCLastName);
                clsUtility.AddParameters("@Password", SqlDbType.VarChar, thePassword);
                clsUtility.AddParameters("@Website", SqlDbType.VarChar, theWebsite);
                clsUtility.AddParameters("@LogoFile", SqlDbType.VarChar, theLogoFile);
                clsUtility.AddParameters("@Deleted", SqlDbType.Int, theDeleted.ToString());
                clsUtility.AddParameters("@UserId", SqlDbType.Int, theUserId.ToString());
                clsUtility.AddParameters("@DBKey", SqlDbType.VarChar, appAccess.theDBKey);
                Int32 theRows = (Int32)theManager.ReturnObject(clsUtility.theParams, "Pr_Admin_InsertUpdateOrganization", clsUtility.ObjectEnum.ExecuteNonQuery);

                DataMgr.CommitTransaction(this.Transaction);
                theRows = theRows > 0 ? theRows : 0;
                return theRows.ToString();
            }
            catch (Exception err)
            {
                DataMgr.RollbackTransaction(this.Transaction);
                throw err;
            }
        }

    }
}
