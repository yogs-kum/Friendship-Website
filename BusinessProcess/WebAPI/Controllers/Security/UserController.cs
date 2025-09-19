using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Data;

using Interface.Security;

namespace WebAPI.Controllers.Security
{
    public class UserController : ApiController
    {
        private readonly IUser _theObject;

        public UserController(IUser theUser)
        {
            _theObject = theUser;
        }

        [HttpGet]
        public string GetGroupDetails(Int32 theGroupId)
        {
            try
            {
                return _theObject.GetGroupDetails(theGroupId);
            }
            catch (Exception err)
            {
                HttpResponseMessage theRes = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                theRes.ReasonPhrase = err.Message.Replace("\r\n", "");
                throw new HttpResponseException(theRes);
            }

        }

        [HttpGet]
        public string GetGroupModuleFunction()
        {
            try
            {
                return _theObject.GetGroupModuleFunction();
            }
            catch (Exception err)
            {
                HttpResponseMessage theRes = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                theRes.ReasonPhrase = err.Message.Replace("\r\n", "");
                throw new HttpResponseException(theRes);
            }

        }

        [HttpPost]
        public string SaveUpdateGroups(FormDataCollection theData)
        {
            try
            {
                byte[] theByte = Encoding.UTF8.GetBytes(theData.Get("RoleFeature"));
                System.IO.MemoryStream theSteam = new System.IO.MemoryStream(theByte, 0, theByte.Length);
                DataSet theDS = new DataSet();
                theDS.ReadXml(theSteam);
                return _theObject.SaveUpdateGroups(Convert.ToInt32(theData.Get("GroupId")), theData.Get("GroupName"), Convert.ToInt32(theData.Get("DeletedFlag")),
                      Convert.ToInt32(theData.Get("UserId")), theDS.Tables[0]);
            }
            catch (Exception err)
            {
                HttpResponseMessage theRes = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                theRes.ReasonPhrase = err.Message.Replace("\r\n", "");
                throw new HttpResponseException(theRes);
            }
        }

        [HttpGet]
        public string GetUserDetails(Int32 theUserId)
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
        public string SaveUpdateUsers(FormDataCollection theData)
        {
            try
            {
                byte[] theByte = Encoding.UTF8.GetBytes(theData.Get("UserGroup"));
                System.IO.MemoryStream theSteam = new System.IO.MemoryStream(theByte, 0, theByte.Length);
                DataSet theDSGroup = new DataSet();
                theDSGroup.ReadXml(theSteam);

                theByte = Encoding.UTF8.GetBytes(theData.Get("UserLocation"));
                theSteam = new System.IO.MemoryStream(theByte, 0, theByte.Length);
                DataSet theDSLocation = new DataSet();
                theDSLocation.ReadXml(theSteam);

                return _theObject.SaveUpdateUsers(Convert.ToInt32(theData.Get("UId")), theData.Get("UserFirstName"), theData.Get("UserLastName"), theData.Get("UserId"),
                      theData.Get("Email"), theData.Get("Password"), Convert.ToInt32(theData.Get("OrganizationId")), Convert.ToInt32(theData.Get("DeletedFlag")),
                      Convert.ToInt32(theData.Get("OprId")), theDSGroup.Tables[0], theDSLocation.Tables[0]);
            }
            catch (Exception err)
            {
                HttpResponseMessage theRes = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                theRes.ReasonPhrase = err.Message.Replace("\r\n","");
                throw new HttpResponseException(theRes);
            }
        }
    }
}