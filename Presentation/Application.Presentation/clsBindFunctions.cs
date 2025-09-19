using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data;

namespace Application.Presentation
{
    public class clsBindFunctions
    {
        public clsBindFunctions()
        { }

        public List<SelectListItem> DropDownBindingList(DataTable theDT)
        {
            List<SelectListItem> theList = new List<SelectListItem>();
            foreach (DataRow theDR in theDT.Rows)
            {
                theList.Add(new SelectListItem()
                {
                    Text = theDR["Name"].ToString(),
                    Value = theDR["Id"].ToString()
                });
            }
            return theList;
        }
    }
}
