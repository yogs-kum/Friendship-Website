using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public class appAccess
    {
        //// DB Access Key ////
        public static string theDBKey = "o5jQQs6yEHqwBc68TTYjV01VNBH6sgDB";

        //// Define Static variables for all Feature in Mst_Function table///// 
        public static int Organization = 1;
        public static int Location = 2;
        public static int UserGroup = 3;
        public static int UserMaster = 4;
        public static int ProfileDashboard = 5;
        public static int ProfileMaster = 6;
    }

    public class ModuleAccess
    {
        //// Define Static variables for all Modules in Mst_Module table///-----/

        public static int Administration = 1;
        public static int Security = 2;
        public static int Profile = 3;
    }

    public class FeatureAccess
    {
        //// Define static variables for all Functions in Mst_Feature table/////
        public static int Save = 1;
        public static int Update = 2;
        public static int View = 3;
        public static int Delete = 4;
    }

    public class EmailCategory
    {
        //// Define Static variables for all Modules in Mst_EmailCategory table///-----/


        public static int UserRegistration = 1;
        public static int ProfileRegistration = 2;
        public static int ProfilePending = 3;
        public static int ProfileComplete = 4;
        public static int ProfileVerified = 5;
        public static int ProfileNotVerified = 6;
        public static int ProfileActivated = 7;
        public static int ProfileMatchFound = 8;
        public static int Support = 9;
        public static int ForgetPassword = 10;
    }
}
