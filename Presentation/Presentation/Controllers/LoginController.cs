using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Configuration;
using Application.Presentation;
using Application.Common;
using System.Net.Http;
using System.Net;
using System.Net.Mail;
using System.IO;
using Presentation.Models;


namespace Presentation.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult frmLogin()
        {
            Session.Clear();
            Session.Abandon();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void ForgetPassword(string theEmail)
        {
            string theParameter = string.Format("?theUserId={0}", theEmail);
            string theResult = ObjectFactory.GetStringAsync("Security/Login/GetForgetPasswordDetails", theParameter, "LoginController");
            DataSet theDS = clsDataOperations.ConvertToDataset(theResult);
            MailTemplates theEmailTemplate = new MailTemplates();
            theEmailTemplate.SendForgetPasswordMail(theDS.Tables[0].Rows[0]["UserName"].ToString(), theDS.Tables[0].Rows[0]["UserId"].ToString(), clsEncryptDecrypt.Decrypt(theDS.Tables[0].Rows[0]["Password"].ToString()), theEmail, theDS.Tables[0].Rows[0]["OrganizationId"].ToString(), theDS.Tables[0].Rows[0]["Token"].ToString());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel theModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View("frmLogin");

                string theParameter = string.Format("?theUserId={0}", theModel.theUserId);
                string theResult = ObjectFactory.GetStringAsync("Security/Login/GetUserDetails", theParameter, "LoginController");
                DataSet theDS = clsDataOperations.ConvertToDataset(theResult);
                Session.Add("ProfileComplete", 0);
                if (theDS.Tables.Count > 0)
                {
                    if (theModel.thePassword == clsEncryptDecrypt.Decrypt(theDS.Tables[0].Rows[0]["Password"].ToString()))
                    {
                        Session.Add("User", theDS);
                        Session.Add("UserId", theDS.Tables[0].Rows[0]["Id"].ToString());
                        //Session.Add("UserIdKey", clsEncryptDecrypt.Encrypt(theDS.Tables[0].Rows[0]["Id"].ToString()));
                        Session.Add("Role", theDS.Tables[0].Rows[0]["Role"].ToString());
                        Session.Add("LoginRole", theDS.Tables[0].Rows[0]["Role"].ToString());
                        Session.Add("OrganizationId", theDS.Tables[0].Rows[0]["OrganizationId"].ToString());
                        Session.Add("OrganizationName", theDS.Tables[0].Rows[0]["Name"].ToString());
                        Session.Add("LogoPath", ConfigurationManager.AppSettings["OrganizationUploads"].ToString() + "\\" + theDS.Tables[0].Rows[0]["LogoFile"].ToString());
                        Session.Add("UserName", theDS.Tables[0].Rows[0]["UserFirstName"].ToString() + " " + theDS.Tables[0].Rows[0]["UserLastName".ToString()]);
                        Session.Add("UserFirstName", theDS.Tables[0].Rows[0]["UserFirstName"].ToString());
                        Session.Add("UserLastName", theDS.Tables[0].Rows[0]["UserLastName"].ToString());
                        Session.Add("ProfileComplete", theDS.Tables[0].Rows[0]["ProfileComplete"].ToString());

                        Logger.LogInfo(string.Format("User {0} logged in successfully.", theModel.theUserId));

                        var theParameterLoginLog = new FormUrlEncodedContent(new[]{
                        new KeyValuePair<string,string>("theUserId", theDS.Tables[0].Rows[0]["Id"].ToString()),
                        new KeyValuePair<string,string>("theUserName",theDS.Tables[0].Rows[0]["UserFirstName"].ToString() + " " + theDS.Tables[0].Rows[0]["UserLastName"].ToString()),
                        new KeyValuePair<string,string>("theSessionId", System.Web.HttpContext.Current.Session.SessionID),
                        new KeyValuePair<string,string>("theIPAddress",System.Web.HttpContext.Current.Request.UserHostAddress),
                        new KeyValuePair<string,string>("theComputerName",System.Web.HttpContext.Current.Request.UserHostName)
                        });

                        var theResultLoginLog = ObjectFactory.PostOnServerAsync("Security/Login/SaveLoginLog", theParameterLoginLog, "LoginController", HttpContext);

                        if (theDS.Tables[0].Rows[0]["Role"].ToString() == "3")
                        {

                            if (theDS.Tables[0].Rows[0]["ProfileComplete"].ToString() == "1")
                            {
                                var profileUrl = Request.Url.AbsoluteUri.Replace(Request.Url.AbsolutePath, "") + @"/Profile/Profile/frmProfile?ID=" + HttpUtility.UrlEncode(clsEncryptDecrypt.Encrypt(theDS.Tables[0].Rows[0]["Id"].ToString()));
                                return RedirectPermanent(profileUrl);
                            }
                            else
                            {
                                return RedirectToActionPermanent("frmEditProfile", "Profile", new { Area = "Profile" });
                            }
                        }
                        else
                        {
                            return RedirectToActionPermanent("frmAppHome", "AppHome");
                        }
                    }
                }
                MsgBox.Show("InvalidLogin", HttpContext);
                return View("frmLogin");
            }
            catch (Exception err)
            {
                Logger.LogError(string.Format("Error Description:{0}/r/n Stack:{1}", err.Message.ToString(), err.StackTrace.ToString()));
                msgBuilder theBuilder = new msgBuilder();
                theBuilder.DataElements["MessageText"] = string.Format("Error Description:{0} /r/n Stack:{1}", err.Message.ToString(), err.StackTrace.ToString());
                MsgBox.Show("#C1", theBuilder, HttpContext);
                return View("frmLogin");
            }
        }

        public JsonResult ValidateEmail(string email = "")
        {
            string theParameter = string.Format("?email={0}", email);
            string theResult = ObjectFactory.GetStringAsync("Profile/Profile/GetUserEmailValidated", theParameter, "ProfileController");
            DataSet theDS = clsDataOperations.ConvertToDataset(theResult);
            if (theDS.Tables[0].Rows.Count > 0 && theDS.Tables[0].Rows[0]["EmailId"] != "")
                return Json("Email already registered with another user", JsonRequestBehavior.AllowGet);
            else
                return Json("success", JsonRequestBehavior.AllowGet);

            //return Json(new { message = "success" });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(Presentation.Areas.Profile.Models.Profile theModel)
        {
            try
            {

                var UserId = "0";
               
                string thePass = clsEncryptDecrypt.Get8CharacterRandomString();
                string thePassword = clsEncryptDecrypt.Encrypt(thePass);

                if (!string.IsNullOrEmpty(theModel.UserId))
                {
                    UserId = Convert.ToString(clsEncryptDecrypt.Decrypt(theModel.UserId));
                }
                var ProfileId = "0";
                if (!string.IsNullOrEmpty(theModel.Id))
                {
                    ProfileId = clsEncryptDecrypt.Decrypt(theModel.Id);
                }
                //if (!Convert.ToDateTime(theModel.DOB)==)
                //{
                //    theModel.DOB = DateTime.Now.AddYears(-50);
                //}
                var theParameter = new FormUrlEncodedContent(new[]{
                new KeyValuePair<string,string>("FirstName",theModel.FirstName),
                new KeyValuePair<string,string>("LastName",theModel.LastName),
                new KeyValuePair<string,string>("Email",theModel.Email),
                new KeyValuePair<string,string>("Phone",theModel.Phone),
                new KeyValuePair<string,string>("Password",thePassword)
                });

                var theResult = ObjectFactory.PostOnServerAsync("Profile/Profile/ProfileRegistration", theParameter, "ProfileController", HttpContext);

                MailTemplates mailTemplates = new MailTemplates();
                mailTemplates.SendProfileRegistrationMail(theModel.FirstName, theModel.LastName, theModel.Email, thePass, theModel.Email, "");
                mailTemplates.SendProfileRegistrationMail_Admin(theModel.FirstName, theModel.LastName, theModel.Email, thePass, theModel.Email, "");

                ViewBag.Message = "Congratulations on joining NDFriends. Your login credentials were sent on registered email";

                return View("frmLogin");

            }
            catch (Exception err)
            {
                Logger.LogError(string.Format("Error Description:{0}", err.Message.ToString() + err.StackTrace.ToString()));
                msgBuilder theBuilder = new msgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString() + err.StackTrace.ToString();
                MsgBox.Show("#C1", theBuilder, HttpContext);
                ViewBag.Message = err.Message.ToString();
                return View("frmLogin",theModel);
            }
        }

        public ActionResult SignOut()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToActionPermanent("frmLogin", "Login");
        }
    }
}