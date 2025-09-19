using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Collections;

namespace Interface.Security
{
    public interface ILogin
    {
        //
        string GetUserDetails(string theUserId);
        string ChangePassword(int theUserId, string thePassword);
        string GetForgetPasswordDetails(string theUserId);
        string SaveLoginLog(Int32 theUserId, string theUserName, string theSessionId, string theIPAddress, string theComputerName);

    }
}
