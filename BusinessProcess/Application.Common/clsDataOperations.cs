using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Data;
using System.Dynamic;

namespace Application.Common
{
    public static class clsDataOperations
    {
        public static XmlElement Serialize(object theObject)
        {
            MemoryStream theStream = new MemoryStream();
            XmlSerializer theSerializer = new XmlSerializer(theObject.GetType());
            theSerializer.Serialize(theStream, theObject);
            theStream.Position = 0;
            XmlDocument theXMLDocument = new XmlDocument();
            theXMLDocument.Load(theStream);
            return theXMLDocument.DocumentElement;
        }

        public static object Deserialize(XmlElement theElement, Type theType)
        {
            Stream theStream = StringToStream(theElement.OuterXml);
            XmlSerializer theSerializer = new XmlSerializer(theType);
            return theSerializer.Deserialize(theStream);
        }

        public static Stream StringToStream(string theInput)
        {
            byte[] theByte = Encoding.UTF8.GetBytes(theInput);
            MemoryStream theStream = new MemoryStream(theByte);
            theStream.Position = 0;
            return theStream;
        }

        public static DataSet ConvertToDataset(string theInput)
        {
            theInput = theInput.Substring(1);
            theInput = theInput.Replace("\\r\\n", "");
            byte[] theByte = Encoding.UTF8.GetBytes(theInput.Substring(0, theInput.Length - 1));
            System.IO.MemoryStream theSteam = new System.IO.MemoryStream(theByte, 0, theByte.Length);
            DataSet theDS = new DataSet();
            theDS.ReadXml(theSteam);
            return theDS;
        }

        public static List<dynamic> DataTableToList(DataTable theDT)
        {
            var theList = new List<dynamic>();
            foreach (DataRow theDR in theDT.Rows)
            {
                var theObject = (IDictionary<string, object>)new ExpandoObject();
                foreach (DataColumn theCol in theDT.Columns)
                {
                    theObject.Add(theCol.ColumnName, theDR[theCol.ColumnName]);
                }
                theList.Add(theObject);
            }
            return theList;
        }

        public static string NumberToText(int number, bool isUK)
        {
            if (number == 0) return "Zero";
            string and = isUK ? "and " : ""; // deals with UK or US numbering
            if (number == -2147483648) return "Minus Two Billion One Hundred " + and +
            "Forty Seven Million Four Hundred " + and + "Eighty Three Thousand " +
            "Six Hundred " + and + "Forty Eight";
            int[] num = new int[4];
            int first = 0;
            int u, h, t;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (number < 0)
            {
                sb.Append("Minus ");
                number = -number;
            }
            string[] words0 = { "", "One ", "Two ", "Three ", "Four ", "Five ", "Six ", "Seven ", "Eight ", "Nine " };
            string[] words1 = { "Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ", "Fifteen ", "Sixteen ", "Seventeen ", "Eighteen ", "Nineteen " };
            string[] words2 = { "Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ", "Seventy ", "Eighty ", "Ninety " };
            string[] words3 = { "Thousand ", "Million ", "Billion " };
            num[0] = number % 1000;           // units
            num[1] = number / 1000;
            num[2] = number / 1000000;
            num[1] = num[1] - 1000 * num[2];  // thousands
            num[3] = number / 1000000000;     // billions
            num[2] = num[2] - 1000 * num[3];  // millions
            for (int i = 3; i > 0; i--)
            {
                if (num[i] != 0)
                {
                    first = i;
                    break;
                }
            }
            for (int i = first; i >= 0; i--)
            {
                if (num[i] == 0) continue;
                u = num[i] % 10;              // ones
                t = num[i] / 10;
                h = num[i] / 100;             // hundreds
                t = t - 10 * h;               // tens
                if (h > 0) sb.Append(words0[h] + "Hundred ");
                if (u > 0 || t > 0)
                {
                    if (h > 0 || i < first) sb.Append(and);
                    if (t == 0)
                        sb.Append(words0[u]);
                    else if (t == 1)
                        sb.Append(words1[u]);
                    else
                        sb.Append(words2[t - 2] + words0[u]);
                }
                if (i != 0) sb.Append(words3[i - 1]);
            }
            return sb.ToString().TrimEnd();
        }
    }
}
