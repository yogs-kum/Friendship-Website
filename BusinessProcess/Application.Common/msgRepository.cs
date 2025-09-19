using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Configuration;
using System.Web;

namespace Application.Common
{
    public class msgRepository
    {
        private static XmlDocument theDocument=null;

        public static RawMessage GetMessage(string MessageId)
        {
            if(theDocument==null)
            {
                theDocument = new XmlDocument();
                string theExecutingDir = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
                string theAddress = ConfigurationManager.AppSettings["MsgRepository"].ToString();
                theAddress = theExecutingDir + theAddress;
                theDocument.Load(theAddress);
            }
            XmlElement theRoot = theDocument.DocumentElement;
            XmlNode theNode = theRoot.SelectSingleNode("Message[@Id='" + MessageId.Trim() + "']");
            if (theNode != null)
            {
                string text1 = theNode.Attributes["Id"].Value;
                string text2 = theNode.Attributes["Text"].Value;
                string text3 = theNode.Attributes["Type"].Value;
                return new RawMessage(text1, text2, text3);
            }
            return new RawMessage("", "", "");
        }

    }
}
