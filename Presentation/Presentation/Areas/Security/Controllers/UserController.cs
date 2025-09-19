using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Net.Http;
using System.Threading.Tasks;

using Application.Presentation;
using Application.Common;
using Presentation.Areas.Security.Models;

namespace Presentation.Areas.Security.Controllers
{
    public class UserController : BaseController
    {
        // GET: Security/User
        public ActionResult frmUserList()
        {
            List<UserModel> theModel = Fill_Grid();
            return View(theModel);
        }

        public List<UserModel> Fill_Grid()
        {
            DataSet theUserDS = (DataSet)Session["User"];

            string strUserId = "0";
            string theParameter = string.Format("?theUserId={0}", strUserId);
            string theResult = ObjectFactory.GetStringAsync("Security/User/GetUserDetails", theParameter, "UserController");
            DataSet theDS = clsDataOperations.ConvertToDataset(theResult);

           
            DataTable theDT = theDS.Tables[0];
            DataView dv = new DataView(theDT);
            string theFilterString = "";
            if (theUserDS.Tables[0].Rows[0]["Role"].ToString() == "2")
            {
                theFilterString = "1=1 and OrganizationId = " + Session["OrganizationId"].ToString();
            }            
            else
            {
                theFilterString = "1=1";
            }
            dv.RowFilter = theFilterString;
            theDT = dv.ToTable();

            List<UserModel> theModel = new List<UserModel>();
            foreach (DataRow theDR in theDT.Rows)
            {
                if (Convert.ToInt32(theDR["ID"]) > 0)
                {
                    theModel.Add(new UserModel
                    {
                        ID = Convert.ToInt32(theDR["ID"]),
                        UserId = theDR["UserId"].ToString(),
                        UserName = theDR["UserName"].ToString(),
                        EmailId = theDR["EmailId"].ToString(),
                        Deleted = Convert.ToInt32(theDR["Deleted"]),
                        Status = theDR["Status"].ToString(),
                        OrganizationName=theDR["OrganizationName"].ToString()
                    });
                }
            }
            return theModel;
        }
        
        public ActionResult frmUser(string theParameter)
        {
            Session.Remove("UId");
            Init_Form();
            if (theParameter != null)
                GetRecordDetails(theParameter);
            return View();
        }

        private void Init_Form()
        {
            DataSet theUserDS = (DataSet)Session["User"];
            
            DataTable theGroupDT = (DataTable)CacheMgr.GetFromCache(CacheMgr.CacheKeys.Mst_Groups.ToString());
            DataView theDV = new DataView(theGroupDT);
            string theFilterString = "";
            if (theUserDS.Tables[0].Rows[0]["Role"].ToString() == "2")
            {
                theFilterString = "Id Not In (1,5)";
            }
            else
            {
                theFilterString = "Id<>1";
            }
            theDV.RowFilter = theFilterString;
            List<dynamic> theGroupList = clsDataOperations.DataTableToList(theDV.ToTable());
            ViewBag.GroupList = theGroupList;

            DataTable theLocationDT = (DataTable)CacheMgr.GetFromCache(CacheMgr.CacheKeys.Mst_Location.ToString());
            DataView dvLocation = new DataView(theLocationDT);
            dvLocation.RowFilter = "OrganizationId = " + Session["OrganizationId"].ToString();
            theLocationDT = dvLocation.ToTable();
            List<dynamic> theLocationList = clsDataOperations.DataTableToList(theLocationDT);
            ViewBag.LocationList = theLocationList;

        }
                
        private ActionResult GetRecordDetails(string theId)
        {
            string theRecordId = clsEncryptDecrypt.Decrypt(theId);
            Session.Add("UId", theRecordId);
            string theParameter = string.Format("?theUserId={0}", theRecordId);
            string theResult = ObjectFactory.GetStringAsync("Security/User/GetUserDetails", theParameter, "UserController");
            DataSet theDS = clsDataOperations.ConvertToDataset(theResult);

            UserModel theModel = new UserModel();
            theModel.UserFirstName = theDS.Tables[0].Rows[0]["UserFirstName"].ToString();
            theModel.UserLastName = theDS.Tables[0].Rows[0]["UserLastName"].ToString();
            theModel.UserId = theDS.Tables[0].Rows[0]["UserId"].ToString();
            theModel.EmailId = theDS.Tables[0].Rows[0]["EmailId"].ToString();
            theModel.Deleted = Convert.ToInt32(theDS.Tables[0].Rows[0]["Deleted"]);

            /////Array for Checkboxes//////
            string[] theGroupArr = new string[theDS.Tables[1].Rows.Count];
            Int32 theKey = 0;
            foreach (DataRow theDR in theDS.Tables[1].Rows)
            {
                theGroupArr[theKey] = theDR["GroupId"].ToString();
                theKey++;
            }
            ViewData["GroupArr"] = theGroupArr;
            
            string[] theLocArr = new string[theDS.Tables[2].Rows.Count];
            theKey = 0;
            foreach (DataRow theDR in theDS.Tables[2].Rows)
            {
                theLocArr[theKey] = theDR["LocationId"].ToString();
                theKey++;
            }
            ViewData["LocationArr"] = theLocArr;

            return View(theModel);
        }

        public void PostData(string[] theGroupArr, string[] theLocArr, UserModel theModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return;
                int UserId = 0;
                if (Session["UId"] != null)
                    UserId = Convert.ToInt32(Session["UId"]);

                DataTable theUserGroup = UserGroupDT();
                for (int i = 0; i < theGroupArr.Length; i++)
                {
                    if (theGroupArr[i].ToString() != "")
                    {
                        DataRow theDR = theUserGroup.NewRow();
                        theDR["UserId"] = UserId;
                        theDR["GroupId"] = theGroupArr[i];
                        theUserGroup.Rows.Add(theDR);
                    }
                }

                DataTable theUserLocation = UserLocationDT();
                for (int i = 0; i < theLocArr.Length; i++)
                {
                    if (theLocArr[i].ToString() != "")
                    {
                        DataRow theDR = theUserLocation.NewRow();
                        theDR["UserId"] = UserId;
                        theDR["LocationId"] = theLocArr[i];
                        theUserLocation.Rows.Add(theDR);
                    }
                }

                DataSet theTmpDS = new DataSet();
                theTmpDS.Tables.Add(theUserGroup);
                string theUserGroupXML = theTmpDS.GetXml();

                theTmpDS = new DataSet();
                theTmpDS.Tables.Add(theUserLocation);
                string theUserLocationXML = theTmpDS.GetXml();

                string thePass = clsEncryptDecrypt.Get8CharacterRandomString();
                string thePassword = clsEncryptDecrypt.Encrypt(thePass);

                var theParameter = new FormUrlEncodedContent(new[]{
                new KeyValuePair<string,string>("UId",UserId.ToString()),
                new KeyValuePair<string,string>("UserFirstName",theModel.UserFirstName),
                new KeyValuePair<string,string>("UserLastName",theModel.UserLastName),
                new KeyValuePair<string,string>("UserId",theModel.UserId),
                new KeyValuePair<string,string>("Email",theModel.EmailId),
                new KeyValuePair<string,string>("Password",thePassword),
                new KeyValuePair<string,string>("OrganizationId",Session["OrganizationId"].ToString()),
                new KeyValuePair<string,string>("DeletedFlag",theModel.Deleted.ToString()),
                new KeyValuePair<string,string>("OprId", Session["UserId"].ToString()),
                new KeyValuePair<string,string>("UserGroup",theUserGroupXML),
                new KeyValuePair<string, string>("UserLocation", theUserLocationXML)});
                var theResult = ObjectFactory.PostOnServerAsync("Security/User/SaveUpdateUsers", theParameter, "UserController", HttpContext);
                Session.Remove("UId");
                if (UserId == 0)
                {
                    //Task.Run(()=> SendUserMail(theModel.UserFirstName + " " + theModel.UserLastName, theModel.UserId, thePass, theModel.EmailId));
                    SendUserMail(theModel.UserFirstName + " " + theModel.UserLastName, theModel.UserId, thePass, theModel.EmailId);
                    MsgBox.Show("Saved", HttpContext);
                }
                else
                {
                    MsgBox.Show("Updated", HttpContext);
                }
            }
            catch (Exception err)
            {
                Logger.LogError(string.Format("Error Description:{0}", err.StackTrace.ToString()));
                msgBuilder theBuilder = new msgBuilder();
                theBuilder.DataElements["MessageText"] = string.Format("Error Description:{0}", err.Message.ToString());
                MsgBox.Show("#C1", theBuilder, HttpContext);
            }
        }
                
        private void SendUserMail(string theUserName, string theUserId, string thePassword, string theMailId)
        {
            DataSet theDS = GetOrganization(Session["OrganizationId"].ToString());
            MailTemplates theTemplate = new MailTemplates();
            theTemplate.SendUserRegistrationMail(theUserName, theUserId, thePassword,theMailId, theDS.Tables[0].Rows[0]["Token"].ToString());
            
        }

        private DataSet GetOrganization(string OrganizationId)
        {
            DataSet theUserDS = (DataSet)Session["User"];
            string theParameter = "";
            
                theParameter = string.Format("?theOrganizationId={0}", OrganizationId);
            
            string theResult = ObjectFactory.GetStringAsync("Administration/Organization/GetOrganizationInfo", theParameter, "OrganizationController");
            DataSet theDS = clsDataOperations.ConvertToDataset(theResult);
            
            return theDS;
        }

        private DataTable UserGroupDT()
        {
            DataTable theDT = new DataTable();
            theDT.Columns.Add("UserId", typeof(Int32));
            theDT.Columns.Add("GroupId", typeof(Int32));
            return theDT;
        }

        private DataTable UserLocationDT()
        {
            DataTable theDT = new DataTable();
            theDT.Columns.Add("UserId", typeof(Int32));
            theDT.Columns.Add("LocationId", typeof(Int32));
            return theDT;
        }
    }
}