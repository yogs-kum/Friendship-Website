using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Interface.Security
{
    public interface IUser
    {
        string GetGroupDetails(Int32 theGroupId);
        string GetGroupModuleFunction();
        string SaveUpdateGroups(Int32 theGroupId, string theGroupName, Int32 theDeleted, Int32 theUserId, DataTable theGroupFeature);
        string GetUserDetails(Int32 theUserId);
        string SaveUpdateUsers(Int32 theId, string UserFirstName, string UserLastName, string theUserId, string theEmail, string thePassword,
            Int32 theOrganizationId, Int32 theDeleted, Int32 theOperatorId, DataTable theUserGroup, DataTable theUserLocation);
    }
}
