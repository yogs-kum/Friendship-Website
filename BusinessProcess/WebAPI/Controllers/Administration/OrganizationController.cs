using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.Http.Formatting;
using Interface.Administration;
using System.Data;
using System.Text;

namespace WebAPI.Controllers
{
    public class OrganizationController : ApiController
    {
        private readonly IOrganization _theObject;

        public OrganizationController(IOrganization theOrganization)
        {
            _theObject = theOrganization;
        }

        [HttpGet]
        public string GetOrganizationInfo(Int32 theOrganizationId)
        {
            try
            {
                return _theObject.GetOrganizationInfo(theOrganizationId);
            }
            catch (Exception err)
            {
                HttpResponseMessage theRes = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                theRes.ReasonPhrase = err.Message.Replace("\r\n", "");
                throw new HttpResponseException(theRes);
            }
        }

        [HttpPost]
        public string SaveUpdateOrganization(FormDataCollection theData)
        {
            try
            {
                return _theObject.SaveUpdateOrganization(Convert.ToInt32(theData.Get("OrganizationId")), theData.Get("Code"), theData.Get("Name"), theData.Get("ShortName"),
                      theData.Get("Token"), theData.Get("Address1"), theData.Get("Address2"), theData.Get("City"), Convert.ToInt32(theData.Get("StateId")), Convert.ToInt32(theData.Get("CountryId")),
                      theData.Get("Pin"), theData.Get("Phone"), theData.Get("Email"), theData.Get("CFirstName"), theData.Get("CLastName"), theData.Get("Password"), theData.Get("WebSite"),
                      theData.Get("LogoFile"), Convert.ToInt32(theData.Get("Deleted")), Convert.ToInt32(theData.Get("UserId")));

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
