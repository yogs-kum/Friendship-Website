using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DataAccess.Entity
{
    public static class clsDataFormatter
    {
        public static DataSet FormatDataSet(DataSet theDS)
        {
            foreach (DataTable theDT in theDS.Tables)
            {
                if (theDT.Rows.Count < 1)
                {
                    DataRow theDR = theDT.NewRow();
                    for (int i = 0; i < theDT.Columns.Count; i++)
                    {
                        if (theDT.Columns[i].DataType == typeof(System.Int32))
                            theDR[i] = 0;
                        else if (theDT.Columns[i].DataType == typeof(System.Decimal))
                            theDR[i] = 0;
                        else if (theDT.Columns[i].DataType == typeof(System.DateTime))
                            theDR[i] = DateTime.MinValue;
                        else if (theDT.Columns[i].DataType == typeof(System.Boolean))
                            theDR[i] = false;
                        else
                            theDR[i] = string.Empty;
                    }
                    theDT.Rows.Add(theDR);
                }
                foreach (DataRow theDR in theDT.Rows)
                {
                    for (int i = 0; i < theDT.Columns.Count; i++)
                    {
                        if (string.IsNullOrEmpty(theDR[i].ToString()))
                        {
                            if (theDT.Columns[i].DataType == typeof(System.Int32))
                                theDR[i] = 0;
                            else if (theDT.Columns[i].DataType == typeof(System.Decimal))
                                theDR[i] = 0;
                            else if (theDT.Columns[i].DataType == typeof(System.DateTime))
                                theDR[i] = DateTime.MinValue;
                            else if (theDT.Columns[i].DataType == typeof(System.Boolean))
                                theDR[i] = false;
                            else
                                theDR[i] = string.Empty;
                        }
                    }
                }
            }
            return theDS;
        }
    }
}
