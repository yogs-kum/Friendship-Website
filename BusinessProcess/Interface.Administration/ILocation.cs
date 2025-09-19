using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Administration
{
    public interface ILocation
    {
        string GetLocationInfo(Int32 theLocationId);
        string SaveUpdateLocation(Int32 theLocationId, string theCode, string theName, Int32 theOrganizationId, string theAddress1, string theAddress2,
            string theCity, Int32 theStateId, Int32 theCountryId, string thePin, string thePhone, string theEmail, Int32 theDeleted, Int32 theUserId);
    }
}
