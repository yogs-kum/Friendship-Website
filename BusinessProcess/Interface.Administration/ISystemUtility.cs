using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Administration
{
    public interface ISystemUtility
    {
        string RefreshSystemCache(string theKey);
        string GetSchedulerMailList(Int32 theStatus);
        string SaveApplicationLog(Int32 theUserId, string theUserName, string theSessionId, string theIPAddress, string theComputerName, string thePageAccessed, string theControllerName, string theActionName, string theActionParameter, DateTime theLogOutTime);
        string SaveUpdateApplicationEmailLog(Int32 theId, string theToMail, string theSubject, string theMessage, string theAttachementFile, Int32 theStatus, string theErrorMessage, Int32 theEmailCategoryId, Int32 theOrganizationId, Int32 theRoleId);
    }
}
