using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Threading.Tasks;
using System.Net.Http;
using Presentation.Models;
using Application.Common;
using Application.Presentation;

namespace Presentation.Controllers
{
    public class ChangePasswordController : BaseController
    {
        // GET: ChangePassword
        public ActionResult frmChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordModels theChangePassword)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View("frmChangePassword");
                string theMessage = ValidateData(theChangePassword);
                if (theMessage != "")
                {
                    msgBuilder theBuilder = new msgBuilder();
                    theBuilder.DataElements["MessageText"] = theMessage;
                    MsgBox.Show("#C1", theBuilder, HttpContext);
                    return View("frmChangePassword");
                }
                DataSet theDS = (DataSet)Session["User"];
                var theParameter = new FormUrlEncodedContent(new[]{
                new KeyValuePair<string,string>("theUserId",theDS.Tables[0].Rows[0]["Id"].ToString()),
                new KeyValuePair<string,string>("thePassword", clsEncryptDecrypt.Encrypt(theChangePassword.theNewPassword))});
                Task.Run(() => ObjectFactory.PostOnServerAsync("Security/Login/ChangePassword", theParameter, "LoginController", HttpContext));
                MsgBox.Show("PasswordChange", HttpContext);
                //return View("frmChangePassword");
                return RedirectToActionPermanent("frmLogin", "Login");
            }
            catch (Exception err)
            {
                msgBuilder theBuilder = new msgBuilder();
                theBuilder.DataElements["MessageText"] = err.InnerException.ToString();
                MsgBox.Show("#C1", theBuilder, HttpContext);
                return View("frmChangePassword");
            }
        }
        public string ValidateData(ChangePasswordModels theModel)
        {
            string theEncryptPass = clsEncryptDecrypt.Encrypt(theModel.theOldPassword);
            DataSet theDS = (DataSet)Session["User"];

            if (theEncryptPass != theDS.Tables[0].Rows[0]["Password"].ToString())
                return "Current password don't match. Please retry.";
            if (theModel.theNewPassword != theModel.theConfirmPassword)
                return "New Password not matches with confirm password. Retry.";
            return "";
        }
    }
}