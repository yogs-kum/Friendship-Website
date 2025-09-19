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
    public class BLocation : ProcessBase, ILocation
    {
        public BLocation()
        { }

        public string GetLocationInfo(Int32 theLocationId)
        {
            clsObject theLocationManager = new clsObject();
            clsUtility.Init_Hashtable();
            clsUtility.AddParameters("@LocationId", SqlDbType.Int, theLocationId.ToString());
            DataSet theDS = (DataSet)theLocationManager.ReturnObject(clsUtility.theParams, "Pr_Admin_GetLocationInfo", clsUtility.ObjectEnum.DataSet);
            return theDS.GetXml();
        }

        public string SaveUpdateLocation(Int32 theLocationId, string theCode, string theName, Int32 theOrganizationId, string theAddress1, string theAddress2,
            string theCity, Int32 theStateId, Int32 theCountryId, string thePin, string thePhone, string theEmail, Int32 theDeleted, Int32 theUserId)
        {
            try
            {
                clsObject theLocationManager = new clsObject();
                clsUtility.Init_Hashtable();
                clsUtility.AddParameters("@LocationId", SqlDbType.Int, theLocationId.ToString());
                clsUtility.AddParameters("@Code", SqlDbType.VarChar, theCode);
                clsUtility.AddParameters("@Name", SqlDbType.VarChar, theName);
                clsUtility.AddParameters("@OrganizationId", SqlDbType.Int, theOrganizationId.ToString());
                clsUtility.AddParameters("@Address1", SqlDbType.VarChar, theAddress1);
                clsUtility.AddParameters("@Address2", SqlDbType.VarChar, theAddress2);
                clsUtility.AddParameters("@City", SqlDbType.VarChar, theCity);
                clsUtility.AddParameters("@StateId", SqlDbType.Int, theStateId.ToString());
                clsUtility.AddParameters("@CountryId", SqlDbType.Int, theCountryId.ToString());
                clsUtility.AddParameters("@Pin", SqlDbType.VarChar, thePin);
                clsUtility.AddParameters("@Phone", SqlDbType.VarChar, thePhone);
                clsUtility.AddParameters("@Email", SqlDbType.VarChar, theEmail);
                clsUtility.AddParameters("@Deleted", SqlDbType.Int, theDeleted.ToString());
                clsUtility.AddParameters("@UserId", SqlDbType.Int, theUserId.ToString());
                Int32 theRows = (Int32)theLocationManager.ReturnObject(clsUtility.theParams, "Pr_Admin_InsertUpdateLocation", clsUtility.ObjectEnum.ExecuteNonQuery);

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
