using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.Http.Formatting;
using Interface.Administration;

namespace WebAPI.Controllers.Administration
{
    public class SystemUtilityController : ApiController
    {
        private readonly ISystemUtility _theObject;

        public SystemUtilityController(ISystemUtility theSystemUtility)
        {
            _theObject = theSystemUtility;
        }

        [HttpGet]
        public string RefreshSystemCache(string theKey)
        {
            try
            {
                return _theObject.RefreshSystemCache(theKey);
            }
            catch (Exception err)
            {
                HttpResponseMessage theRes = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                theRes.ReasonPhrase = err.Message.Replace("\r\n", "");
                throw new HttpResponseException(theRes);
            }
        }

        [HttpGet]
        public string GetSchedulerMailList(Int32 theStatus)
        {
            try
            {
                return _theObject.GetSchedulerMailList(theStatus);
            }
            catch (Exception err)
            {
                HttpResponseMessage theRes = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                theRes.ReasonPhrase = err.Message.Replace("\r\n", "");
                throw new HttpResponseException(theRes);
            }
        }

        [HttpPost]
        public string SaveApplicationLog(FormDataCollection theData)
        {
            try
            {
                return _theObject.SaveApplicationLog(Convert.ToInt32(theData.Get("theUserId")), theData.Get("theUserName"), theData.Get("theSessionId"), theData.Get("theIPAddress"), theData.Get("theComputerName"), theData.Get("thePageAccessed"), theData.Get("theControllerName"), theData.Get("theActionName"), theData.Get("theActionParameter"), Convert.ToDateTime(theData.Get("theLogOutTime")));
            }
            catch (Exception err)
            {
                HttpResponseMessage theRes = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                theRes.ReasonPhrase = err.Message.Replace("\r\n", "");
                throw new HttpResponseException(theRes);
            }
        }

        [HttpPost]
        public string SaveUpdateApplicationEmailLog(FormDataCollection theData)
        {
            try
            {
                return _theObject.SaveUpdateApplicationEmailLog(Convert.ToInt32(theData.Get("theId")), theData.Get("theToMail"), theData.Get("theSubject"), theData.Get("theMessage"), theData.Get("theAttachementFile"), Convert.ToInt32(theData.Get("theStatus")), theData.Get("theErrorMessage"), Convert.ToInt32(theData.Get("theEmailCategoryId")), Convert.ToInt32(theData.Get("theOrganizationId")), Convert.ToInt32(theData.Get("theRoleId")));
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
