using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.IO;
using System.Net.Http;

namespace Application.Presentation
{
    public class clsMail
    {
        public clsMail()
        { }

        public void SendMail(string toMailId, string theSubject, string theMessage, string theEmailCategoryId, string theOrganizationId, string theRoleId)
        {
            SaveUpdateApplicationEmailLog(toMailId, theSubject, theMessage, "", theEmailCategoryId, theOrganizationId, theRoleId, HttpContext.Current, "0", "0", "");
        }
        public void SendMailWithAttachment(string toMailId, string theSubject, string theMessage, HttpPostedFileBase fileUploader, string theEmailCategoryId, string theOrganizationId, string theRoleId)
        {
            HttpContext theContext = HttpContext.Current;
            if (!Directory.Exists(theContext.Server.MapPath("~/EmailAttachement")))
            {
                Directory.CreateDirectory(theContext.Server.MapPath("~/EmailAttachement"));
            }
            string theAttachementFile = "";
            if (fileUploader != null)
            {
                string theAttachementFileExt = Path.GetExtension(fileUploader.FileName);
                theAttachementFile = theContext.Session["UserId"].ToString() + "_" + theContext.Session["LoginRole"].ToString() + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + theAttachementFileExt;

                fileUploader.SaveAs(theContext.Server.MapPath("~/EmailAttachement/") + theAttachementFile);
            }
            SaveUpdateApplicationEmailLog(toMailId, theSubject, theMessage, theAttachementFile, theEmailCategoryId, theOrganizationId, theRoleId, HttpContext.Current, "0", "0", "");
        }

        public void SendMail_Context(string toMailId, string theSubject, string theMessage, string theEmailCategoryId, string theOrganizationId, string theRoleId, HttpContext theContext)
        {
            SaveUpdateApplicationEmailLog(toMailId, theSubject, theMessage, "", theEmailCategoryId, theOrganizationId, theRoleId, theContext, "0", "0", "");
        }
        public void SendMailWithAttachment_Context(string toMailId, string theSubject, string theMessage, HttpPostedFileBase fileUploader, string theEmailCategoryId, string theOrganizationId, string theRoleId, HttpContext theContext)
        {
            if (!Directory.Exists(theContext.Server.MapPath("~/EmailAttachement")))
            {
                Directory.CreateDirectory(theContext.Server.MapPath("~/EmailAttachement"));
            }
            string theAttachementFile = "";
            if (fileUploader != null)
            {
                string theAttachementFileExt = Path.GetExtension(fileUploader.FileName);
                theAttachementFile = theContext.Session["UserId"].ToString() + "_" + theContext.Session["LoginRole"].ToString() + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + theAttachementFileExt;

                fileUploader.SaveAs(theContext.Server.MapPath("~/EmailAttachement/") + theAttachementFile);
            }
            SaveUpdateApplicationEmailLog(toMailId, theSubject, theMessage, theAttachementFile, theEmailCategoryId, theOrganizationId, theRoleId, theContext, "0", "0", "");
        }

        public void SchedulerSendMail(string Id, string toMailId, string theSubject, string theMessage, string AttachementFile, HttpContext theContext, string theEmailCategoryId, string theOrganizationId, string theRoleId)
        {
            if (ConfigurationManager.AppSettings["SendMail"].ToString() == "0")
                return;

            
            string theAttachementFile = "";
            if (AttachementFile != null && AttachementFile != "")
            {
                if (File.Exists(theContext.Server.MapPath("~/EmailAttachement/") + AttachementFile))
                {
                    theAttachementFile = theContext.Server.MapPath("~/EmailAttachement/") + AttachementFile;
                }
            }

            try
            {
                MailMessage theMsg = new MailMessage();
                SmtpClient theSMTPClient = new SmtpClient();
                string EmailDisplayName = "ND Friends";
                
                theMsg.From = new MailAddress(ConfigurationManager.AppSettings["FromMailId"].ToString(), EmailDisplayName);

                string[] ToMuliId = toMailId.Split(';');

                foreach (string ToEMailId in ToMuliId)
                {
                    theMsg.To.Add(new MailAddress(ToEMailId)); //adding multiple TO Email Id
                }
                theMessage = theMessage.Replace(@"\t", "");
                theMessage = theMessage.Replace(@"\", "");
                theMessage = theMessage.Replace('\"', '"');

                theMsg.Subject = theSubject;
                theMsg.IsBodyHtml = true;
                theMsg.Body = theMessage;
                if (AttachementFile != null && AttachementFile != "")
                {
                    theMsg.Attachments.Add(new Attachment(theAttachementFile));
                }
                theSMTPClient.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]);
                theSMTPClient.Host = ConfigurationManager.AppSettings["SMTPServer"].ToString();
                theSMTPClient.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"] == null ? 1 : Convert.ToInt32(ConfigurationManager.AppSettings["EnableSsl"].ToString()));
                theSMTPClient.UseDefaultCredentials = false;
                theSMTPClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["FromMailId"].ToString(), ConfigurationManager.AppSettings["Password"].ToString());
                theSMTPClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                theSMTPClient.Send(theMsg);

                SaveUpdateApplicationEmailLog(toMailId, theSubject, theMessage, AttachementFile, theEmailCategoryId, theOrganizationId, theRoleId, theContext, Id.ToString(), "1", "");

            }
            catch (Exception err)
            {
                SaveUpdateApplicationEmailLog(toMailId, theSubject, theMessage, AttachementFile, theEmailCategoryId, theOrganizationId, theRoleId, theContext, Id.ToString(), "2", err.Message.ToString());
            }
        }

        public void SaveUpdateApplicationEmailLog(string toMailId, string theSubject, string theMessage, string theAttachementFile, string theEmailCategoryId, string theOrganizationId, string theRoleId, HttpContext theContext, string theId, string theStatus, string theErrorMessage)
        {

            var theParameter = new FormUrlEncodedContent(new[]{
                new KeyValuePair<string,string>("theId", theId.ToString()),
                new KeyValuePair<string,string>("theToMail",toMailId.ToString()),
                new KeyValuePair<string,string>("theSubject", theSubject.ToString()),
                new KeyValuePair<string,string>("theMessage",theMessage.ToString()),
                new KeyValuePair<string,string>("theAttachementFile",theAttachementFile.ToString()),
                new KeyValuePair<string,string>("theStatus",theStatus.ToString()),
                new KeyValuePair<string,string>("theErrorMessage",theErrorMessage.ToString()),
                new KeyValuePair<string,string>("theEmailCategoryId",theEmailCategoryId.ToString()),
                new KeyValuePair<string,string>("theOrganizationId",theOrganizationId),
                new KeyValuePair<string,string>("theRoleId",theRoleId) });

            var theResult = ObjectFactory.PostOnServerAsync("Administration/SystemUtility/SaveUpdateApplicationEmailLog", theParameter, "SystemUtilityController", theContext.Request.RequestContext.HttpContext);

        }
    }
}
