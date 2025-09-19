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
    public class LocationController : ApiController
    {
        private readonly ILocation _theObject;

        public LocationController(ILocation theLocation)
        {
            _theObject = theLocation;
        }

        [HttpGet]
        public string GetLocationInfo(Int32 theLocationId)
        {
            try
            {
                return _theObject.GetLocationInfo(theLocationId);
            }
            catch (Exception err)
            {
                HttpResponseMessage theRes = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                theRes.ReasonPhrase = err.Message.Replace("\r\n", "");
                throw new HttpResponseException(theRes);
            }
        }

        public string SaveUpdateLocation(FormDataCollection theData)
        {
            try
            {
                return _theObject.SaveUpdateLocation(Convert.ToInt32(theData.Get("LocationId")), theData.Get("Code"), theData.Get("Name"), Convert.ToInt32(theData.Get("OrganizationId")),
                    theData.Get("Address1"), theData.Get("Address2"), theData.Get("City"), Convert.ToInt32(theData.Get("StateId")), Convert.ToInt32(theData.Get("CountryId")),
                    theData.Get("Pin"), theData.Get("Phone"), theData.Get("Email"), Convert.ToInt32(theData.Get("Deleted")), Convert.ToInt32(theData.Get("UserId")));
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
