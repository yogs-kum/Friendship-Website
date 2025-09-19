using Presentation.Areas.Administration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Threading;
using System.IO;
using System.Configuration;

using Application.Presentation;
using System.Web.UI;
using Application.Common;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Presentation.Areas.Administration.Controllers
{
    public class OrganizationController : BaseController
    {
        public ActionResult frmOrganizationList()
        {
            try
            {
                List<OrganizationModel> theModel = FillGrid();
                return View(theModel);
            }
            catch (Exception err)
            {
                msgBuilder theBuilder = new msgBuilder();
                Logger.LogError(string.Format("Error Description:{0}", err.StackTrace.ToString()));
                theBuilder.DataElements["MessageText"] = string.Format("Error Description:{0}", err.Message.ToString());
                MsgBox.Show("#C1", theBuilder, HttpContext);
                return View("frmOrganization");
            }

        }

        private List<OrganizationModel> FillGrid()
        {
            DataSet theUserDS = (DataSet)Session["User"];
            string theParameter = "";
            if (theUserDS.Tables[0].Rows[0]["Role"].ToString() == "2")
                theParameter = string.Format("?theOrganizationId={0}", Session["OrganizationId"].ToString());
            else
                theParameter = string.Format("?theOrganizationId={0}", "0");

            string theResult = ObjectFactory.GetStringAsync("Administration/Organization/GetOrganizationInfo", theParameter, "OrganizationController");
            DataSet theDS = clsDataOperations.ConvertToDataset(theResult);
            List<OrganizationModel> theModel = new List<OrganizationModel>();
            foreach (DataRow theDR in theDS.Tables[0].Rows)
            {
                if (Convert.ToInt32(theDR["Id"]) > 0)
                {
                    theModel.Add(new OrganizationModel
                    {
                        Code = theDR["Code"].ToString(),
                        Name = theDR["Name"].ToString(),
                        ShortName = theDR["ShortName"].ToString(),
                        Token = theDR["Token"].ToString(),
                        Address1 = theDR["Address1"].ToString(),
                        Address2 = theDR["Address2"].ToString(),
                        City = theDR["City"].ToString(),
                        StateId = Convert.ToInt32(theDR["StateId"]),
                        CountryId = Convert.ToInt32(theDR["CountryId"]),
                        Pin = theDR["Pin"].ToString(),
                        Phone = theDR["Phone"].ToString(),
                        Email = theDR["Email"].ToString(),
                        Website = theDR["Website"].ToString(),
                        LogoFile = theDR["LogoFile"].ToString(),
                        Deleted = Convert.ToInt32(theDR["Deleted"]),
                        Id = Convert.ToInt32(theDR["Id"])
                    });
                }
            }
            return theModel;
        }


        // GET: Administration/Organization
        public ActionResult frmOrganization(string theParameter)
        {
            try
            {
                Session.Remove("OrgId");
                ModelState.Clear();
                Init_form();
                if (theParameter != null)
                {
                    string thePara = clsEncryptDecrypt.Decrypt(theParameter);
                    FillDetails(thePara);
                    Session.Add("OrgId", thePara);
                }

                return View();
            }
            catch (Exception err)
            {
                msgBuilder theBuilder = new msgBuilder();
                Logger.LogError(string.Format("Error Description:{0}", err.StackTrace.ToString()));
                theBuilder.DataElements["MessageText"] = string.Format("Error Description:{0}", err.Message.ToString());
                MsgBox.Show("#C1", theBuilder, HttpContext);
                return View("frmOrganization");
            }
        }

        private void Init_form()
        {
            DataSet theDS = (DataSet)Session["User"];
            ViewBag.Save = AccessManager.HasFeatureAccess(appAccess.Organization, FeatureAccess.Save, theDS.Tables[2]);
            ViewBag.Save = AccessManager.HasFeatureAccess(appAccess.Organization, FeatureAccess.Update, theDS.Tables[2]);

            clsBindFunctions theBindManager = new clsBindFunctions();
            DataTable theCountryDT = (DataTable)CacheMgr.GetFromCache(CacheMgr.CacheKeys.Mst_Country.ToString());
            ViewData["Country"] = theBindManager.DropDownBindingList(theCountryDT);

            List<SelectListItem> SList = new List<SelectListItem>();
            ViewData["State"] = SList;

        }

        [HttpGet]
        public JsonResult GetCountryState(string theCountryId)
        {
            List<SelectListItem> SList = new List<SelectListItem>();
            DataTable theStateDT = (DataTable)CacheMgr.GetFromCache(CacheMgr.CacheKeys.Mst_State.ToString());
            DataView theDV = new DataView(theStateDT);
            theDV.RowFilter = "CountryId=" + theCountryId;
            clsBindFunctions theBindManager = new clsBindFunctions();
            SList = theBindManager.DropDownBindingList(theDV.ToTable());
            ViewData["State"] = SList;
            return Json(SList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PagePost(string theParameter, OrganizationModel theModel, HttpPostedFileBase fileupload)
        {
            try
            {
                switch (theParameter)
                {
                    case "Save":
                        //if (!ModelState.IsValid)
                        //{
                        //    Init_form();
                        //    return View("frmOrganization", theModel);
                        //}
                        return (SaveRecord(theModel, fileupload));

                    case "Close":
                        return RedirectToAction("frmOrganizationList", "Organization");
                }

            }
            catch (Exception err)
            {
                msgBuilder theBuilder = new msgBuilder();
                Logger.LogError(string.Format("Error Description:{0}", err.StackTrace.ToString()));
                theBuilder.DataElements["MessageText"] = string.Format("Error Description:{0}", err.Message.ToString());
                MsgBox.Show("#C1", theBuilder, HttpContext);
                return View("frmOrganization", theModel);
            }
            return View("frmOrganization",theModel);
        }

        public ActionResult FillDetails(string theOrganizationId)
        {
            string theParameter = string.Format("?theOrganizationId={0}",theOrganizationId);
            string theResult = ObjectFactory.GetStringAsync("Administration/Organization/GetOrganizationInfo", theParameter, "OrganizationController");
            DataSet theDS = clsDataOperations.ConvertToDataset(theResult);
            OrganizationModel theModel = new OrganizationModel();
            theModel.Code = theDS.Tables[0].Rows[0]["Code"].ToString();
            theModel.Name = theDS.Tables[0].Rows[0]["Name"].ToString();
            theModel.ShortName = theDS.Tables[0].Rows[0]["ShortName"].ToString();
            theModel.Token = theDS.Tables[0].Rows[0]["Token"].ToString();
            theModel.Address1 = theDS.Tables[0].Rows[0]["Address1"].ToString();
            theModel.Address2 = theDS.Tables[0].Rows[0]["Address2"].ToString();
            theModel.City = theDS.Tables[0].Rows[0]["City"].ToString();
            theModel.StateId = Convert.ToInt32(theDS.Tables[0].Rows[0]["StateId"]);
            ViewBag.StateId = theDS.Tables[0].Rows[0]["StateId"].ToString();
            theModel.CountryId = Convert.ToInt32(theDS.Tables[0].Rows[0]["CountryId"]);
            theModel.Pin = theDS.Tables[0].Rows[0]["Pin"].ToString();
            theModel.Phone = theDS.Tables[0].Rows[0]["Phone"].ToString();
            theModel.Email = theDS.Tables[0].Rows[0]["Email"].ToString();
            theModel.Website = theDS.Tables[0].Rows[0]["Website"].ToString();
            theModel.LogoFile = theDS.Tables[0].Rows[0]["LogoFile"].ToString();
            theModel.Deleted = Convert.ToInt32(theDS.Tables[0].Rows[0]["Deleted"]);
            return View(theModel);
        }

        public ActionResult SaveRecord(OrganizationModel theModel, HttpPostedFileBase theUploadFile)
        {
            string theOrganizationId = "0";
            string theLogoFileName = "";
            if (Session["OrgId"] != null)
            {
                theOrganizationId = Session["OrgId"].ToString();
            }

            theModel.Code = theModel.Code == null ? "" : theModel.Code;
            string strToken = "";
            if (theModel.Name != null && theModel.Name != "" && (theModel.Token == null || theModel.Token == ""))
            {
                theModel.Name.Split(' ').ToList().ForEach(i => strToken += i[0].ToString());
            }
            strToken = strToken.ToUpper();
            theModel.Token = theModel.Token == null ? strToken : theModel.Token;
            theModel.ShortName = theModel.ShortName == null ? "" : theModel.ShortName;

            theModel.Email = theModel.Email == null ? "" : theModel.Email;

            if (theUploadFile != null)
            {
                theLogoFileName = theModel.Token.ToString() + "_" + Path.GetFileName(theUploadFile.FileName);
            }

            if (theModel.LogoFile != "" && theLogoFileName == "")
                theLogoFileName = theModel.LogoFile;

            string thePass = clsEncryptDecrypt.Get8CharacterRandomString();
            string thePassword = clsEncryptDecrypt.Encrypt(thePass);

            var theParameter = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,string>("OrganizationId",theOrganizationId),
                new KeyValuePair<string, string>("Code",(theModel.Code == null ? "" : theModel.Code.Trim())),
                new KeyValuePair<string, string>("Name",(theModel.Name == null ? "" : theModel.Name.Trim())),
                new KeyValuePair<string, string>("ShortName",(theModel.ShortName == null ? "" : theModel.ShortName.Trim())),
                new KeyValuePair<string, string>("Token",(theModel.Token == null ? "" : theModel.Token.Trim())),
                new KeyValuePair<string, string>("Address1",(theModel.Address1 == null ? "" : theModel.Address1.Trim())),
                new KeyValuePair<string, string>("Address2",(theModel.Address2 == null ? "" : theModel.Address2.Trim())),
                new KeyValuePair<string, string>("City",(theModel.City == null ? "" : theModel.City.Trim())),
                new KeyValuePair<string, string>("StateId",(theModel.StateId == null ? "0" : theModel.StateId.ToString())),
                new KeyValuePair<string, string>("CountryId",(theModel.CountryId == null ? "0" : theModel.CountryId.ToString())),
                new KeyValuePair<string, string>("Pin",(theModel.Pin == null ? "" : theModel.Pin.Trim())),
                new KeyValuePair<string, string>("Phone",(theModel.Phone == null ? "" : theModel.Phone.Trim())),
                new KeyValuePair<string, string>("Email",(theModel.Email == null ? "" : theModel.Email.Trim())),
                new KeyValuePair<string, string>("CFirstName",(theModel.ContactFirstName == null ? "" : theModel.ContactFirstName.Trim())),
                new KeyValuePair<string, string>("CLastName",(theModel.ContactLastName == null ? "" : theModel.ContactLastName.Trim())),
                new KeyValuePair<string, string>("Password",thePassword),
                new KeyValuePair<string, string>("WebSite",(theModel.Website == null ? "" : theModel.Website.Trim())),
                new KeyValuePair<string, string>("LogoFile",theLogoFileName),
                new KeyValuePair<string, string>("Deleted",(theModel.Deleted == null ? "0" : theModel.Deleted.ToString())),
                new KeyValuePair<string, string>("UserId",Session["UserId"].ToString()),
            });
            var theResult = ObjectFactory.PostOnServerAsync("Administration/Organization/SaveUpdateOrganization", theParameter, "OrganizationController", HttpContext);
            Session.Remove("OrgId");
            if (theLogoFileName != "" && theUploadFile != null)
            {
                string theLogoPath = Server.MapPath(ConfigurationManager.AppSettings["OrganizationUploads"].ToString());
                theUploadFile.SaveAs(theLogoPath + "/" + theLogoFileName);
            }
            CacheMgr.ClearCache();
            if (theOrganizationId == "0")
            {
                if (Session["LoginRole"] != null && Session["LoginRole"].ToString() == "2")
                {
                    if (string.IsNullOrEmpty(theLogoFileName))
                    {
                        Session["LogoPath"] = "/Content/images/blank-user.png";
                    }
                    else
                    {
                        Session["LogoPath"] = ConfigurationManager.AppSettings["OrganizationUploads"].ToString() + "\\" + theLogoFileName;
                    }
                }
                SendMail(theModel, thePass, theModel.Token.Trim());
                MsgBox.Show("Saved", HttpContext);
            }
            else
            {
                if (Session["LoginRole"] != null && Session["LoginRole"].ToString() == "2")
                {
                    if (string.IsNullOrEmpty(theLogoFileName))
                    {
                        Session["LogoPath"] = "/Content/images/blank-user.png";
                    }
                    else
                    {
                        Session["LogoPath"] = ConfigurationManager.AppSettings["OrganizationUploads"].ToString() + "\\" + theLogoFileName;
                    }
                }
                MsgBox.Show("Updated", HttpContext);
            }

            return RedirectToAction("frmOrganizationList", "Organization");
        }

        private void SendMail(OrganizationModel theModel, string thePassword, string theToken)
        {
            DataSet theDS = (DataSet)Session["User"];
            MailTemplates theMailTemplate = new MailTemplates();
            theMailTemplate.SendUserRegistrationMail(theModel.Name, theModel.Email.Trim(), thePassword, theModel.Email.Trim(), theToken.Trim());
        }
    }
}