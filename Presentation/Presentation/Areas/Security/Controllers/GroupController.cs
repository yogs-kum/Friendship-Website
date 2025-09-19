using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Threading.Tasks;
using System.Net.Http;

using Application.Presentation;
using Application.Common;
using Presentation.Areas.Security.Models;

namespace Presentation.Areas.Security.Controllers
{
    public class GroupController : BaseController
    {
        // GET: Security/Group
        public ActionResult frmGroupList()
        {
            List<GroupModel> theModel = Fill_Grid();
            return View(theModel);
        }

        public List<GroupModel> Fill_Grid()
        {
            string theParameter = string.Format("?theGroupId={0}", "0");
            string theResult = ObjectFactory.GetStringAsync("Security/User/GetGroupDetails", theParameter, "UserController");
            DataSet theDS = clsDataOperations.ConvertToDataset(theResult);
            List<GroupModel> theModel = new List<GroupModel>();
            foreach (DataRow theDR in theDS.Tables[0].Rows)
            {
                theModel.Add(new GroupModel
                {
                    Id = Convert.ToInt32(theDR["Id"]),
                    Name = theDR["Name"].ToString(),
                    Deleted = Convert.ToInt32(theDR["Deleted"]),
                    Status = theDR["Status"].ToString()
                });
            }
            return theModel;
        }

        public ActionResult frmGroup(string theParameter)
        {
            Session.Remove("GroupId");
            Init_Form();
            if (theParameter != null)
                GetRecordDetails(theParameter);
            return View();
        }

        private void Init_Form()
        {
            string theResult = ObjectFactory.GetStringAsync("Security/User/GetGroupModuleFunction", "", "UserController");
            DataSet theDS = clsDataOperations.ConvertToDataset(theResult);
            CreateModuleGrids(theDS);
        }

        private ActionResult GetRecordDetails(string theId)
        {
            string theRecordId = clsEncryptDecrypt.Decrypt(theId);
            Session.Add("GroupId", theRecordId);
            string theParameter = string.Format("?theGroupId={0}", theRecordId);
            string theResult = ObjectFactory.GetStringAsync("Security/User/GetGroupDetails", theParameter, "UserController");
            DataSet theDS = clsDataOperations.ConvertToDataset(theResult);

            GroupModel theModel = new GroupModel();
            theModel.Name = theDS.Tables[0].Rows[0]["Name"].ToString();
            theModel.Deleted = Convert.ToInt32(theDS.Tables[0].Rows[0]["Deleted"]);

            /////Array for Checkboxes//////
            string[] theCheckBoxArray = new string[theDS.Tables[1].Rows.Count];
            Int32 theKey = 0;
            foreach (DataRow theDR in theDS.Tables[1].Rows)
            {
                if (theDR["FeatureId"].ToString() == "1")
                {
                    theCheckBoxArray[theKey] = "Save_" + theDR["FunctionId"].ToString();
                }
                else if (theDR["FeatureId"].ToString() == "2")
                {
                    theCheckBoxArray[theKey] = "Update_" + theDR["FunctionId"].ToString();
                }
                else if (theDR["FeatureId"].ToString() == "3")
                {
                    theCheckBoxArray[theKey] = "View_" + theDR["FunctionId"].ToString();
                }
                else if (theDR["FeatureId"].ToString() == "4")
                {
                    theCheckBoxArray[theKey] = "Delete_" + theDR["FunctionId"].ToString();
                }
                theKey++;
            }
            ViewData["CheckBoxArray"] = theCheckBoxArray;
            return View(theModel);
        }

        private void CreateModuleGrids(DataSet theDS)
        {
            List<dynamic> theModuleList = clsDataOperations.DataTableToList(theDS.Tables[0]);
            ViewBag.theModules = theModuleList;
            foreach (DataRow theModuleDR in theDS.Tables[0].Rows)
            {
                DataView theDV = new DataView(theDS.Tables[1]);
                theDV.RowFilter = "ModuleId=" + theModuleDR["Id"].ToString();
                DataTable theFiterTable = theDV.ToTable();
                List<dynamic> theList = clsDataOperations.DataTableToList(theFiterTable);
                string theViewBag = "GridData" + theModuleDR["Id"].ToString();

                if (TempData.Keys.Contains(theViewBag))
                    TempData.Remove(theViewBag);

                TempData.Add(theViewBag, theList);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void PostData(string[] theCheckBoxArray, GroupModel theModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return;
                int GroupId = 0;
                if (Session["GroupId"] != null)
                    GroupId = Convert.ToInt32(Session["GroupId"]);

                DataTable theRoleFunctions = RoleFeatureFunctionTable();
                for (int i = 0; i < theCheckBoxArray.Length; i++)
                {
                    if (theCheckBoxArray[i].ToString() != "")
                    {
                        DataRow theDR = theRoleFunctions.NewRow();
                        theDR["GroupId"] = GroupId;
                        string[] theData = theCheckBoxArray[i].ToString().Split('_');
                        theDR["FunctionId"] = theData[1];
                        switch (theData[0])
                        {
                            case "Save":
                                theDR["FeatureId"] = 1;
                                break;
                            case "Update":
                                theDR["FeatureId"] = 2;
                                break;
                            case "View":
                                theDR["FeatureId"] = 3;
                                break;
                            case "Delete":
                                theDR["FeatureId"] = 4;
                                break;
                        }
                        theRoleFunctions.Rows.Add(theDR);
                    }
                }
                DataSet theTmpDS = new DataSet();
                theTmpDS.Tables.Add(theRoleFunctions);

                var theParameter = new FormUrlEncodedContent(new[]{
                new KeyValuePair<string,string>("GroupId",GroupId.ToString()),
                new KeyValuePair<string,string>("GroupName",theModel.Name),
                new KeyValuePair<string,string>("DeletedFlag",theModel.Deleted.ToString()),
                new KeyValuePair<string,string>("UserId", Session["UserId"].ToString()),
                new KeyValuePair<string,string>("RoleFeature",theTmpDS.GetXml())});
                var theResult = ObjectFactory.PostOnServerAsync("Security/User/SaveUpdateGroups", theParameter, "UserController", HttpContext);
                Session.Remove("GroupId");
                CacheMgr.ClearCache();
                MsgBox.Show("Updated", HttpContext);
            }
            catch (Exception err)
            {
                msgBuilder theBuilder = new msgBuilder();
                theBuilder.DataElements["MessageText"] = string.Format("Error Description:{0}", err.Message.ToString());
                MsgBox.Show("#C1", theBuilder, HttpContext);
            }
        }

        private DataTable RoleFeatureFunctionTable()
        {
            DataTable theDT = new DataTable();
            theDT.Columns.Add("GroupId", typeof(Int32));
            theDT.Columns.Add("FunctionId", typeof(Int32));
            theDT.Columns.Add("FeatureId", typeof(Int32));
            return theDT;
        }
    }
}