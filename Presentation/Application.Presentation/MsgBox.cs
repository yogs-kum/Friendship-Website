using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using System.Web;

namespace Application.Presentation
{
    public abstract class MsgBox
    {
        public static void Show(string MessageId, HttpContextBase theContext)
        {
            RawMessage theMessage = msgRepository.GetMessage(MessageId);
            Show(theMessage.Text, theMessage.Type, theContext);
        }

        public static void Show(string MessageId, msgBuilder MessageBuilder, HttpContextBase theContext)
        {
            RawMessage theMessage = msgRepository.GetMessage(MessageId);
            MessageBuilder.MsgRepository[MessageId] = theMessage.ToString();
            string theDynamicMsg = MessageBuilder.BuildMessage(MessageId);
            Show(theDynamicMsg, theMessage.Type, theContext);
        }

        public static void Show(string Message, string MessageType, HttpContextBase theContext)
        {
            string theMsgText = "";
            if (MessageType == "!")
            { theMsgText = "alert"; }
            else { theMsgText = "return confirm"; }
            Message = Message.Replace(">", "");
            Message = Message.Replace("\n", "\\n");
            Message = Message.Replace("\r", "\\r");
            Message = Message.Replace("'", "");
            HttpContext.Current.Session["ScriptMessage"] = Message;
            HttpContext.Current.Session["ScriptType"] = theMsgText;
            //Message = "<script type='text/javascript'>" + theMsgText + "('" + Message + "')</script>";

            theContext.Response.Write(Message);
        }

    }
}
