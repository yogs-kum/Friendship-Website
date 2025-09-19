using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Net.Http;
using System.Threading.Tasks;

using Application.Common;
using Application.Presentation;
using Presentation.Areas.Administration.Models;

namespace Presentation.Areas.Administration.Controllers
{
    public class LocationController : BaseController
    {
        // GET: Administration/Location
        public ActionResult frmLocationList()
        {
            List<LocationModel> theModel = Fill_Grid();
            return View(theModel);
        }

        public List<LocationModel> Fill_Grid()
        {
            string theParameter = string.Format("?theLocationId={0}", "0");
            string theResult = ObjectFactory.GetStringAsync("Administration/Location/GetLocationInfo", theParameter, "LocationController");
            DataSet theDS = clsDataOperations.ConvertToDataset(theResult);
            List<LocationModel> theModel = new List<LocationModel>();
            foreach (DataRow theDR in theDS.Tables[0].Rows)
            {
                theModel.Add(new LocationModel
                {
                    Code = theDR["Code"].ToString(),
                    Name = theDR["Name"].ToString(),
                    OrganizationName = theDR["OrganizationName"].ToString(),
                    Id = Convert.ToInt32(theDR["Id"])
                });
            }
            return theModel;
        }

        public ActionResult frmLocation(string theParameter)
        {
            try
            {
                ModelState.Clear();
                Init_form();
                if (theParameter != null)
                {
                    string thePara = clsEncryptDecrypt.Decrypt(theParameter);
                    FillDetails(thePara);
                    Session.Add("LocId", thePara);
                }

                return View();
            }
            catch (Exception err)
            {
                msgBuilder theBuilder = new msgBuilder();
                Logger.LogError(string.Format("Error Description:{0}", err.InnerException.ToString()));
                theBuilder.DataElements["MessageText"] = err.InnerException.ToString();
                MsgBox.Show("#C1", theBuilder, HttpContext);
                return View("frmOrganization");
            }
        }

        private void Init_form()
        {
            DataSet theDS = (DataSet)Session["User"];
            ViewBag.Save = AccessManager.HasFeatureAccess(appAccess.Location, FeatureAccess.Save, theDS.Tables[2]);
            ViewBag.Save = AccessManager.HasFeatureAccess(appAccess.Location, FeatureAccess.Update, theDS.Tables[2]);

            clsBindFunctions theBindManager = new clsBindFunctions();
            DataTable theCountryDT = (DataTable)CacheMgr.GetFromCache(CacheMgr.CacheKeys.Mst_Country.ToString());
            ViewData["Country"] = theBindManager.DropDownBindingList(theCountryDT);

            List<SelectListItem> SList = new List<SelectListItem>();
            ViewData["State"] = SList;

        }

        public ActionResult FillDetails(string theOrganizationId)
        {
            string theParameter = string.Format("?theLocationId={0}", theOrganizationId);
            string theResult = ObjectFactory.GetStringAsync("Administration/Location/GetLocationInfo", theParameter, "LocationController");
            DataSet theDS = clsDataOperations.ConvertToDataset(theResult);
            LocationModel theModel = new LocationModel();
            theModel.Code = theDS.Tables[0].Rows[0]["Code"].ToString();
            theModel.Name = theDS.Tables[0].Rows[0]["Name"].ToString();
            theModel.Address1 = theDS.Tables[0].Rows[0]["Address1"].ToString();
            theModel.Address2 = theDS.Tables[0].Rows[0]["Address2"].ToString();
            theModel.City = theDS.Tables[0].Rows[0]["City"].ToString();
            theModel.StateId = Convert.ToInt32(theDS.Tables[0].Rows[0]["StateId"]);
            ViewBag.StateId = theDS.Tables[0].Rows[0]["StateId"].ToString();
            theModel.CountryId = Convert.ToInt32(theDS.Tables[0].Rows[0]["CountryId"]);
            theModel.Pin = theDS.Tables[0].Rows[0]["Pin"].ToString();
            theModel.Phone = theDS.Tables[0].Rows[0]["Phone"].ToString();
            theModel.Email = theDS.Tables[0].Rows[0]["Email"].ToString();
            theModel.OrganizationId = Convert.ToInt32(theDS.Tables[0].Rows[0]["OrganizationId"]);
            theModel.Deleted = Convert.ToInt32(theDS.Tables[0].Rows[0]["Deleted"]);
            return View(theModel);
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
        public ActionResult PagePost(string theParameter, LocationModel theModel)
        {
            try
            {
                switch (theParameter)
                {
                    case "Save":
                        if (!ModelState.IsValid)
                        {
                            Init_form();
                            return View("frmLocation", theModel);
                        }
                        return (SaveRecord(theModel));

                    case "Close":
                        return RedirectToAction("frmLocationList", "Location");
                }

            }
            catch (Exception err)
            {
                msgBuilder theBuilder = new msgBuilder();
                Logger.LogError(string.Format("Error Description:{0}", err.InnerException.ToString()));
                theBuilder.DataElements["MessageText"] = err.InnerException.ToString();
                MsgBox.Show("#C1", theBuilder, HttpContext);
                return View("frmOrganization");
            }
            return View("frmOrganization");
        }

        public ActionResult SaveRecord(LocationModel theModel)
        {
            string theLocationId = "0";
            if (Session["LocId"] != null)
                theLocationId = Session["LocId"].ToString();

            var theParameter = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,string>("LocationId",theLocationId),
                new KeyValuePair<string, string>("Code",theModel.Code),
                new KeyValuePair<string, string>("Name",theModel.Name),
                new KeyValuePair<string, string>("OrganizationId",Session["OrganizationId"].ToString()),
                new KeyValuePair<string, string>("Address1",theModel.Address1),
                new KeyValuePair<string, string>("Address2",theModel.Address2),
                new KeyValuePair<string, string>("City",theModel.City),
                new KeyValuePair<string, string>("StateId",theModel.StateId.ToString()),
                new KeyValuePair<string, string>("CountryId",theModel.CountryId.ToString()),
                new KeyValuePair<string, string>("Pin",theModel.Pin),
                new KeyValuePair<string, string>("Phone",theModel.Phone),
                new KeyValuePair<string, string>("Email",theModel.Email),
                new KeyValuePair<string, string>("Deleted",theModel.Deleted.ToString()),
                new KeyValuePair<string, string>("UserId",Session["UserId"].ToString())
            });
            Task.Run(() => ObjectFactory.PostOnServerAsync("Administration/Location/SaveUpdateLocation", theParameter, "LocationController", HttpContext));
            Session.Remove("OrgId");

            return RedirectToAction("frmLocationList", "Location");
        }
    }
}