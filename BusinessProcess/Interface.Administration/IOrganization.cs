using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Interface.Administration
{
    public interface IOrganization
    {
        string GetOrganizationInfo(Int32 theOrganizationId);
        string SaveUpdateOrganization(Int32 theOrganizationId, string theCode, string theName, string theShortName, string theToken, string theAddress1,
            string theAddress2, string theCity, Int32 theStateId, Int32 theCountryId, string thePin, string thePhone, string theEmail, string theCFirstName, string theCLastName, string thePassword,
            string theWebsite, string theLogoFile, Int32 theDeleted, Int32 theUserId);
        
    }
}
