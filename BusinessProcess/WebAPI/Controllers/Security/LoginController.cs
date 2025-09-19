using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.Http.Formatting;

using Interface.Security;

namespace WebAPI.Controllers.Security
{
    public class LoginController : ApiController
    {
        private readonly ILogin _theObject;

        public LoginController(ILogin theLogin)
        {
            _theObject = theLogin;
        }

        [HttpGet]
        public string GetUserDetails(string theUserId)
        {
            try
            {
                return _theObject.GetUserDetails(theUserId);
            }
            catch (Exception err)
            {
                HttpResponseMessage theRes = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                theRes.ReasonPhrase = err.Message.Replace("\r\n", "");
                throw new HttpResponseException(theRes);
            }
        }

        [HttpPost]
        public string ChangePassword(FormDataCollection theData)
        {
            try
            {
                return _theObject.ChangePassword(Convert.ToInt32(theData.Get("theUserId")), theData.Get("thePassword"));
            }
            catch (Exception err)
            {
                HttpResponseMessage theRes = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                theRes.ReasonPhrase = err.Message.Replace("\r\n", "");
                throw new HttpResponseException(theRes);
            }

        }

        [HttpGet]
        public string GetForgetPasswordDetails(string theUserId)
        {
            try
            {
                return _theObject.GetForgetPasswordDetails(theUserId);
            }
            catch (Exception err)
            {
                HttpResponseMessage theRes = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                theRes.ReasonPhrase = err.Message.Replace("\r\n", "");
                throw new HttpResponseException(theRes);
            }

        }

        [HttpPost]
        public string SaveLoginLog(FormDataCollection theData)
        {
            try
            {
                return _theObject.SaveLoginLog(Convert.ToInt32(theData.Get("theUserId")), theData.Get("theUserName"), theData.Get("theSessionId"), theData.Get("theIPAddress"), theData.Get("theComputerName"));
            }
            catch (Exception err)
            {
                HttpResponseMessage theRes = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                theRes.ReasonPhrase = err.Message.Replace("\r\n", "");
                throw new HttpResponseException(theRes);
            }
        }
    }
}
