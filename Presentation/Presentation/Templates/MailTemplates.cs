using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.IO;
using Application.Presentation;
using Application.Common;

namespace Presentation
{
    public class MailTemplates
    {
        public MailTemplates()
        { }

        public string CreateNewUser(string UserName, string theUserId, string thePassword)
        {
            string theMailMsg = "Welcome " + UserName + "\r\n\r\nPlease use below credentials to log -in into Framwork application. \r\n\r\n" +
                "UserId:" + theUserId + "\r\nPassword:" + thePassword + "\r\n\r\n You are highly recommneded to change password after first login." +
                "\r\n\r\nWith Regards \r\nTeam Framework";
            return theMailMsg;
        }

        public void SendUserRegistrationMail(string UserName, string theUserId, String thePassword, string theEmail, string theToken)
        {
            string theBody = string.Empty;
            using (StreamReader theReader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Templates/UserRegister.html")))
            {
                theBody = theReader.ReadToEnd();
            }
            theBody = theBody.Replace("[Name]", UserName);
            theBody = theBody.Replace("[UserName]", theUserId);
            theBody = theBody.Replace("[SystemGeneratedPassword]", thePassword);
            theBody = theBody.Replace("[Token]", theToken);
            theBody = theBody.Replace("[OrganizationName]", HttpContext.Current.Session["OrganizationName"].ToString());


            clsMail theMail = new clsMail();
            theMail.SendMail(theEmail, "User Registration", theBody, EmailCategory.UserRegistration.ToString(), HttpContext.Current.Session["OrganizationId"].ToString(), "0");
        }


        public void SendProfileRegistrationMail(string theFirstName, string theLastName, string theUserId, String thePassword, string theEmail, string theToken)
        {
            string theBody = string.Empty;
            using (StreamReader theReader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Register.html")))
            {
                theBody = theReader.ReadToEnd();
            }
            theBody = theBody.Replace("[FirstName]", theFirstName);
            theBody = theBody.Replace("[LastName]", theLastName);
            theBody = theBody.Replace("[UserName]", theUserId);
            theBody = theBody.Replace("[SystemGeneratedPassword]", thePassword);
            
            clsMail theMail = new clsMail();
            theMail.SendMail(theEmail, "NDFriends Registration", theBody, EmailCategory.ProfileRegistration.ToString(), "1", "0");
        }

        public void SendProfileRegistrationMail_Admin(string theFirstName, string theLastName, string theUserId, String thePassword, string theEmail, string theToken)
        {
            string theAdminMailId = System.Configuration.ConfigurationManager.AppSettings["ToMailId"].ToString();

            string theBody = string.Empty;
            using (StreamReader theReader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Templates/RegisterAdmin.html")))
            {
                theBody = theReader.ReadToEnd();
            }
            theBody = theBody.Replace("[FirstName]", theFirstName);
            theBody = theBody.Replace("[LastName]", theLastName);
            theBody = theBody.Replace("[Email]", theEmail);
            theBody = theBody.Replace("[UserName]", theUserId);
            theBody = theBody.Replace("[SystemGeneratedPassword]", thePassword);

            clsMail theMail = new clsMail();
            theMail.SendMail(theAdminMailId, "NDFriends Registration", theBody, EmailCategory.ProfileRegistration.ToString(), "1", "0");
        }

        public void SendForgetPasswordMail(string UserName, string theUserId, String thePassword, string theEmail, string OrganizationId, string theToken)
        {
            string theBody = string.Empty;
            using (StreamReader theReader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Templates/ForgetPassword.html")))
            {
                theBody = theReader.ReadToEnd();
            }
            theBody = theBody.Replace("[Name]", UserName);
            theBody = theBody.Replace("[UserName]", theUserId);
            theBody = theBody.Replace("[SystemGeneratedPassword]", thePassword);
            theBody = theBody.Replace("[Token]", theToken);
            ///
            clsMail theMail = new clsMail();
            theMail.SendMail(theEmail, "Password Recovery", theBody, EmailCategory.ForgetPassword.ToString(), OrganizationId.ToString(), "0");
        }
                
        public void SendSupportReplyMail(string UserName, string theEmail)
        {
            string theBody = string.Empty;
            using (StreamReader theReader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Templates/SupportReply.html")))
            {
                theBody = theReader.ReadToEnd();
            }
            theBody = theBody.Replace("[Name]", UserName);

            clsMail theMail = new clsMail();
            theMail.SendMail(theEmail, "Acknowledgement", theBody, EmailCategory.Support.ToString(), HttpContext.Current.Session["OrganizationId"].ToString(), "0");
        }

    }
}