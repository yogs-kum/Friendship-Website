using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Application.Presentation
{
    public class AccessManager
    {
        public static Boolean HasModuleAcces(int theModuleId, DataTable theDT)
        {
            DataView theDV = new DataView(theDT);
            theDV.RowFilter = "Id=" + theModuleId.ToString();
            if (theDV.Count > 0)
                return true;
            else
                return false;
        }
        public static Boolean HasFunctionAccess(int theFunctionId, DataTable theDT)
        {
            DataView theDV = new DataView(theDT);
            theDV.RowFilter = "FunctionId=" + theFunctionId.ToString();
            if (theDV.Count > 0)
                return true;
            else
                return false;
        }

        public static Boolean HasFeatureAccess(int theFunctionId, int theFeatureId, DataTable theDT)
        {
            DataView theDV = new DataView(theDT);
            theDV.RowFilter = "FunctionId=" + theFunctionId.ToString() + "and FeatureId=" + theFeatureId.ToString();
            if (theDV.Count > 0)
                return true;
            else
                return false;
        }
    }
}
