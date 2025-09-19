using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using Application.Common;
using Application.Presentation;
using Presentation.Areas.Profile.Models;


namespace Presentation.Areas.Profile.Controllers
{
    public class ProfileController : Controller
    {
        public JsonResult SetEditProfileActiveTab(string theActiveTab = "0")
        {
            if (theActiveTab == null || theActiveTab == "0")
            {
                theActiveTab = "1";
            }
            Session["EditProfileActiveTab"] = theActiveTab;
            return Json(theActiveTab, JsonRequestBehavior.AllowGet);
        }
        public ActionResult frmEditProfile()
        {
            var model = new Profile.Models.NDUserProfile();
            FillDropDowns();
            Init_Form(Session["UserId"].ToString(), model);            
            return View(model);
        }

        private void FillDropDowns()
        {
            clsBindFunctions theBindManager = new clsBindFunctions();
            DataTable theCountryDT = (DataTable)CacheMgr.GetFromCache(CacheMgr.CacheKeys.Mst_Country.ToString());
            ViewData["Country"] = theBindManager.DropDownBindingList(theCountryDT);

            DataTable theMaritalStatusDT = (DataTable)CacheMgr.GetFromCache(CacheMgr.CacheKeys.Mst_MaritalStatus.ToString());
            ViewData["Marital_Status"] = theBindManager.DropDownBindingList(theMaritalStatusDT);

            DataTable theRaceDT = (DataTable)CacheMgr.GetFromCache(CacheMgr.CacheKeys.Mst_Race.ToString());
            ViewData["RaceList"] = theBindManager.DropDownBindingList(theRaceDT);

            DataTable theGenderDT = (DataTable)CacheMgr.GetFromCache(CacheMgr.CacheKeys.Mst_Gender.ToString());
            ViewData["GenderList"] = theBindManager.DropDownBindingList(theGenderDT);

            DataTable theHightFeetsDT = theHeightTableFeets();
            ViewData["HeightFeets"] = theBindManager.DropDownBindingList(theHightFeetsDT);

            DataTable theHightIncDT = theHeightTableInc();
            ViewData["HeightInc"] = theBindManager.DropDownBindingList(theHightIncDT);

            DataTable theProfileSectionsDT = (DataTable)CacheMgr.GetFromCache(CacheMgr.CacheKeys.Mst_ProfileSections.ToString());
            DataView theDV = new DataView(theProfileSectionsDT);
            theDV.RowFilter = "Id <> 10";
            DataTable theProfileSections = theDV.ToTable();

            DataTable theProfileSection1 = theProfileSections.Copy();
            ViewData["ProfileSection1"] = theBindManager.DropDownBindingList(theProfileSection1);

            DataTable theProfileSection2 = theProfileSections.Copy();
            ViewData["ProfileSection2"] = theBindManager.DropDownBindingList(theProfileSection2);

            DataTable theProfileSection3 = theProfileSections.Copy();
            ViewData["ProfileSection3"] = theBindManager.DropDownBindingList(theProfileSection3);

            DataTable theProfileSection4 = theProfileSections.Copy();
            ViewData["ProfileSection4"] = theBindManager.DropDownBindingList(theProfileSection4);

            DataTable theProfileSection5 = theProfileSections.Copy();
            ViewData["ProfileSection5"] = theBindManager.DropDownBindingList(theProfileSection5);

            DataTable theProfileSection6 = theProfileSections.Copy();
            ViewData["ProfileSection6"] = theBindManager.DropDownBindingList(theProfileSection6);
                        
            DataTable theMusicDT = (DataTable)CacheMgr.GetFromCache(CacheMgr.CacheKeys.Mst_Music.ToString());
            ViewData["Music"] = theBindManager.DropDownBindingList(theMusicDT);

            DataTable theMovieDT = (DataTable)CacheMgr.GetFromCache(CacheMgr.CacheKeys.Mst_MovieGenre.ToString());
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (DataRow row in theMovieDT.Rows)
            {
                if (Convert.ToInt32(row["Id"].ToString()) > 0)
                {
                    items.Add(new SelectListItem
                    {
                        Text = row["Name"].ToString(),
                        Value= row["Id"].ToString()
                    });
                }
            }

            ViewBag.Movie = items;

            DataTable theCuisineDT = (DataTable)CacheMgr.GetFromCache(CacheMgr.CacheKeys.Mst_FoodPreference.ToString());
            List<SelectListItem> itemsc = new List<SelectListItem>();
            foreach (DataRow row in theCuisineDT.Rows)
            {
                if (Convert.ToInt32(row["Id"].ToString()) > 0)
                {
                    itemsc.Add(new SelectListItem
                    {
                        Text = row["Name"].ToString(),
                        Value = row["Id"].ToString()
                    });
                }
            }

            ViewBag.Cuisine = itemsc;
            //DataTable theCuisineDT = (DataTable)CacheMgr.GetFromCache(CacheMgr.CacheKeys.Mst_FoodPreference.ToString());
            //ViewData["Cuisine"] = theBindManager.DropDownBindingList(theCuisineDT);
        }

        private void Init_Form(string theProfileId, Models.NDUserProfile model)
        {
            int theProfileCompletion = 10;
            string theParameter = string.Format("?theUserId={0}", theProfileId);
            string theResult = ObjectFactory.GetStringAsync("Profile/Profile/GetUserProfile", theParameter, "ProfileController");
            DataSet theDS = clsDataOperations.ConvertToDataset(theResult);

            model.theProfile = new Models.Profile();
            Session.Add("ProfileId", theDS.Tables[0].Rows[0]["Id"].ToString());
            model.theProfile.FirstName = theDS.Tables[0].Rows[0]["FirstName"].ToString();
            model.theProfile.LastName = theDS.Tables[0].Rows[0]["LastName"].ToString();
            model.theProfile.Email = theDS.Tables[0].Rows[0]["Email"].ToString();
            model.theProfile.Phone = theDS.Tables[0].Rows[0]["Phone"].ToString();
            model.theProfile.City = theDS.Tables[0].Rows[0]["City"].ToString();
            model.theProfile.PinCode = Convert.ToInt32(theDS.Tables[0].Rows[0]["PinCode"]);
            model.theProfile.StateId = Convert.ToInt32(theDS.Tables[0].Rows[0]["StateId"]);
            model.theProfile.CountryId = Convert.ToInt32(theDS.Tables[0].Rows[0]["CountryId"]);
            model.theProfile.Address = theDS.Tables[0].Rows[0]["Address"].ToString();
            model.theProfile.PinCode = Convert.ToInt32(theDS.Tables[0].Rows[0]["PinCode"]);
            model.theProfile.DOB = Convert.ToDateTime(theDS.Tables[0].Rows[0]["DOB"]);
            model.theProfile.Gender = Convert.ToInt32(theDS.Tables[0].Rows[0]["Gender"]);
            model.theProfile.MaritalStatus = Convert.ToInt32(theDS.Tables[0].Rows[0]["MaritalStatus"]);
            model.theProfile.Race = Convert.ToInt32(theDS.Tables[0].Rows[0]["Race"]);
            model.theProfile.Height_Feets = Convert.ToInt32(theDS.Tables[0].Rows[0]["Height_Feets"]);
            model.theProfile.Height_Inc = Convert.ToInt32(theDS.Tables[0].Rows[0]["Height_Inc"]);
            ViewBag.Email = model.theProfile.Email;
            var thePath = ConfigurationManager.AppSettings["ProfilePics"].ToString() + "Profile" + @Session["ProfileId"];
            ViewBag.PicPath = thePath;
            ViewBag.ProfilePicDT = theDS.Tables[1].AsEnumerable();

            ////Profile Pic in Session////
            DataView theProfileDV = new DataView(theDS.Tables[1]);
            theProfileDV.RowFilter = "SectionId = 10";
            ViewBag.DivCounter = theProfileDV.Count + 1;
            theProfileDV.RowFilter = "isProfile = 1";
            if (theProfileDV.Count > 0)
            {
                theProfileCompletion += 10;
                Session["ProfilePic"] = "..\\.." + ConfigurationManager.AppSettings["ProfilePics"].ToString() + "Profile" + @Session["ProfileId"] + "\\" + theProfileDV[0]["PicName"].ToString();
            }
            else
            {
                Session["ProfilePic"] = "..\\..\\Content\\assets\\images\\blank-user.png";
            }
            //////////////////////////////
            ///LifeInPics/////////////////

            DataView theLifeInPicsDV = new DataView(theDS.Tables[1]);
            theLifeInPicsDV.RowFilter = "SectionId<>10 ";
            DataTable theLifeInPicDT = theLifeInPicsDV.ToTable();
            if (theLifeInPicDT.Rows.Count > 0)
                theProfileCompletion += 20;

            LoadLifeInPics(theLifeInPicDT);

            /////////////////////////////////
            
            ///Personal Habits/////////////////

            DataTable dtPersonalHabits = theDS.Tables[2];
            DataTable dtPersonalHabits_SelectedMusics = theDS.Tables[3];
            model.thePersonalHabits = new Models.PersonalHabits();
            
            if((dtPersonalHabits.Rows.Count > 0 && Convert.ToInt32(dtPersonalHabits.Rows[0]["ProfileId"].ToString()) > 0) || (dtPersonalHabits_SelectedMusics.Rows.Count > 0 && Convert.ToInt32(dtPersonalHabits_SelectedMusics.Rows[0]["MusicId"].ToString()) > 0))
            {
                theProfileCompletion += 30;
            }
            
            if (dtPersonalHabits.Rows.Count > 0)
            {
                if (Convert.ToInt32(dtPersonalHabits.Rows[0]["ProfileId"].ToString()) > 0)
                {                   

                    model.thePersonalHabits.Q1_are_you_a_night_owl_or_a_morning_person = dtPersonalHabits.Rows[0]["Q1_are_you_a_night_owl_or_a_morning_person"].ToString();
                    model.thePersonalHabits.Q2_are_you_someone_who_enjoys_physical_activity_or_more_of_a_couch_potato = dtPersonalHabits.Rows[0]["Q2_are_you_someone_who_enjoys_physical_activity_or_more_of_a_couch_potato"].ToString();
                    model.thePersonalHabits.Q3_do_you_like_to_travel_or_are_you_a_homebody = dtPersonalHabits.Rows[0]["Q3_do_you_like_to_travel_or_are_you_a_homebody"].ToString();
                    model.thePersonalHabits.Q4_do_you_drink_alcohol = dtPersonalHabits.Rows[0]["Q4_do_you_drink_alcohol"].ToString();
                    model.thePersonalHabits.Q5_do_you_smoke_tobacco_products = dtPersonalHabits.Rows[0]["Q5_do_you_smoke_tobacco_products"].ToString();
                    model.thePersonalHabits.Q6_do_you_use_recreational_drugs = dtPersonalHabits.Rows[0]["Q6_do_you_use_recreational_drugs"].ToString();
                    model.thePersonalHabits.Q7_is_there_anything_you_watch_regularly_on_television = dtPersonalHabits.Rows[0]["Q7_is_there_anything_you_watch_regularly_on_television"].ToString();
                    model.thePersonalHabits.Q8_do_you_have_pets = dtPersonalHabits.Rows[0]["Q8_do_you_have_pets"].ToString();                    
                }
            }

            ///Selected Musics/////////////////
            model.thePersonalHabits.SelectedMultiMusicId = new List<int> { 0 };
            if (dtPersonalHabits_SelectedMusics.Rows.Count > 0)
            {
                List<int> ids = new List<int>(dtPersonalHabits_SelectedMusics.Rows.Count);
                foreach (DataRow row in dtPersonalHabits_SelectedMusics.Rows)
                {
                    ids.Add(Convert.ToInt32(row["MusicId"]));
                }
                model.thePersonalHabits.SelectedMultiMusicId = ids;
            }
            /////////////////////////////////
            model.thePersonalHabits.Q10_do_you_have_strong_political_affiliations = dtPersonalHabits.Rows[0]["Q10_do_you_have_strong_political_affiliations"].ToString();
            model.thePersonalHabits.Q11_would_you_consider_relocating = dtPersonalHabits.Rows[0]["Q11_would_you_consider_relocating"].ToString();

            //////////////////////////////


            ///Activity Preferences/////////////////
            DataTable dtActivityPreferences = theDS.Tables[4];
            DataTable dtActivityPreferences_SelectedMovies = theDS.Tables[5];
            DataTable dtActivityPreferences_SelectedCuisines = theDS.Tables[6];

            model.theActivityPreferences = new Models.ActivityPreferences();

            if ((dtActivityPreferences.Rows.Count > 0 && Convert.ToInt32(dtActivityPreferences.Rows[0]["ProfileId"].ToString()) > 0))
            {
                theProfileCompletion += 30;
            }

            if (dtActivityPreferences.Rows.Count > 0)
            {
                if (Convert.ToInt32(dtActivityPreferences.Rows[0]["ProfileId"].ToString()) > 0)
                {

                    model.theActivityPreferences.Q2_Having_Tea_Coffee= dtActivityPreferences.Rows[0]["Q2_Having_Tea_Coffee"].ToString();
                    model.theActivityPreferences.Q3_Going_To_a_bar_sports_bar= dtActivityPreferences.Rows[0]["Q3_Going_To_a_bar_sports_bar"].ToString();
                    model.theActivityPreferences.Q4_Going_To_a_Concert= dtActivityPreferences.Rows[0]["Q4_Going_To_a_Concert"].ToString();
                    model.theActivityPreferences.Q5_Watching_a_Live_Play= dtActivityPreferences.Rows[0]["Q5_Watching_a_Live_Play"].ToString();
                    model.theActivityPreferences.Q6_Watching_a_musical_live_on_TV= dtActivityPreferences.Rows[0]["Q6_Watching_a_musical_live_on_TV"].ToString();
                    model.theActivityPreferences.Q7_Fishing_Hunting= dtActivityPreferences.Rows[0]["Q7_Fishing_Hunting"].ToString();
                    model.theActivityPreferences.Q8_Participating_In_Water_Sports_in_an_natural_Body_Of_Water= dtActivityPreferences.Rows[0]["Q8_Participating_In_Water_Sports_in_an_natural_Body_Of_Water"].ToString();
                    model.theActivityPreferences.Q9_Going_for_a_motorcycle_ride= dtActivityPreferences.Rows[0]["Q9_Going_for_a_motorcycle_ride"].ToString();
                    model.theActivityPreferences.Q10_Go_on_a_hike= dtActivityPreferences.Rows[0]["Q10_Go_on_a_hike"].ToString();
                    model.theActivityPreferences.Q11_Going_for_a_bike_ride= dtActivityPreferences.Rows[0]["Q11_Going_for_a_bike_ride"].ToString();
                    model.theActivityPreferences.Q12_Going_for_a_run = dtActivityPreferences.Rows[0]["Q12_Going_for_a_run"].ToString();
                    model.theActivityPreferences.Q13_Going_for_a_leisurely_walk= dtActivityPreferences.Rows[0]["Q13_Going_for_a_leisurely_walk"].ToString();
                    model.theActivityPreferences.Q14_Going_on_a_road_trip_vacation= dtActivityPreferences.Rows[0]["Q14_Going_on_a_road_trip_vacation"].ToString();
                    model.theActivityPreferences.Q15_Going_on_a_picnic= dtActivityPreferences.Rows[0]["Q15_Going_on_a_picnic"].ToString();
                    model.theActivityPreferences.Q16_Bowling= dtActivityPreferences.Rows[0]["Q16_Bowling"].ToString();
                    model.theActivityPreferences.Q17_Going_To_a_museum= dtActivityPreferences.Rows[0]["Q17_Going_To_a_museum"].ToString();
                    model.theActivityPreferences.Q18_Going_shopping= dtActivityPreferences.Rows[0]["Q18_Going_shopping"].ToString();
                    model.theActivityPreferences.Q20_Going_dancing= dtActivityPreferences.Rows[0]["Q20_Going_dancing"].ToString();
                    

                }
            }

            ///Selected Movies/////////////////
            model.theActivityPreferences.SelectedMultiMovieId = new List<int> { };
            if (dtActivityPreferences_SelectedMovies.Rows.Count > 0)
            {
                List<int> ids = new List<int>(dtActivityPreferences_SelectedMovies.Rows.Count);
                foreach (DataRow row in dtActivityPreferences_SelectedMovies.Rows)
                {
                    ids.Add(Convert.ToInt32(row["MovieId"]));
                }
                model.theActivityPreferences.SelectedMultiMovieId = ids;
            }
            //////////////////////////////



            ///Selected Cuisines/////////////////
            model.theActivityPreferences.SelectedMultiCuisineId= new List<int> {  };
            if (dtActivityPreferences_SelectedCuisines.Rows.Count > 0)
            {
                List<int> idsc = new List<int>(dtActivityPreferences_SelectedCuisines.Rows.Count);
                foreach (DataRow row in dtActivityPreferences_SelectedCuisines.Rows)
                {
                    idsc.Add(Convert.ToInt32(row["CuisineId"]));
                }
                model.theActivityPreferences.SelectedMultiCuisineId = idsc;
            }

            //////////////////////////////


            clsBindFunctions theBindManager = new clsBindFunctions();
            List<SelectListItem> SList = new List<SelectListItem>();
            DataTable theStateDT = (DataTable)CacheMgr.GetFromCache(CacheMgr.CacheKeys.Mst_State.ToString());
            DataView theDV = new DataView(theStateDT);
            theDV.RowFilter = "CountryId=" + model.theProfile.CountryId;
            theBindManager = new clsBindFunctions();
            SList = theBindManager.DropDownBindingList(theDV.ToTable());
            ViewData["State"] = SList;

            ///////Profile Completion///////////////
            ViewBag.Complete = theProfileCompletion;
            Session["ProfileComplete"] = theProfileCompletion;
            ////////////////////////////////////////

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

        private DataTable theHeightTableFeets()
        {
            DataTable theDT = new DataTable();
            theDT.Columns.Add("Id", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("Name", System.Type.GetType("System.String"));

            DataRow theDR;
            for (Int32 i = 3; i < 9; i++)
            {
                theDR = theDT.NewRow();
                theDR["Id"] = i;
                theDR["Name"] = i.ToString() + "'";
                theDT.Rows.Add(theDR);
            }
            return theDT;

        }

        private DataTable theHeightTableInc()
        {
            DataTable theDT = new DataTable();
            theDT.Columns.Add("Id", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("Name", System.Type.GetType("System.String"));

            DataRow theDR;
            for (Int32 i = 3; i < 13; i++)
            {
                theDR = theDT.NewRow();
                theDR["Id"] = i;
                theDR["Name"] = i.ToString();
                theDT.Rows.Add(theDR);
            }
            return theDT;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveUpdateDemographic(Profile.Models.NDUserProfile theModel)
        {
            
            try
            {
                //if (!ModelState.IsValid)
                //{
                //    return View("frmEditProfile", theModel);
                //}
                var theParameter = new FormUrlEncodedContent(new[]{
                new KeyValuePair<string,string>("FirstName",theModel.theProfile.FirstName),
                new KeyValuePair<string,string>("LastName",theModel.theProfile.LastName),
                new KeyValuePair<string,string>("NickName",""),
                new KeyValuePair<string,string>("Email",theModel.theProfile.Email),
                new KeyValuePair<string,string>("Phone",theModel.theProfile.Phone),
                new KeyValuePair<string,string>("City",theModel.theProfile.City),
                new KeyValuePair<string,string>("StateId",theModel.theProfile.StateId.ToString()),
                new KeyValuePair<string,string>("CountryId",theModel.theProfile.CountryId.ToString()),
                new KeyValuePair<string,string>("Address",theModel.theProfile.Address),
                new KeyValuePair<string,string>("DOB",Convert.ToDateTime(theModel.theProfile.DOB).ToString("yyyy-MM-dd")),
                new KeyValuePair<string,string>("PinCode",theModel.theProfile.PinCode.ToString()),
                new KeyValuePair<string,string>("Gender",theModel.theProfile.Gender.ToString()),
                new KeyValuePair<string,string>("MaritalStatus",theModel.theProfile.MaritalStatus.ToString()),
                new KeyValuePair<string,string>("Race",theModel.theProfile.Race.ToString()),
                new KeyValuePair<string,string>("Height_Feets",theModel.theProfile.Height_Feets.ToString()),
                new KeyValuePair<string,string>("Height_Inc",theModel.theProfile.Height_Inc.ToString()),
                new KeyValuePair<string,string>("Password",""),
                new KeyValuePair<string,string>("UserId", Session["UserId"].ToString()),
                new KeyValuePair<string,string>("OprId", Session["UserId"].ToString()),
                new KeyValuePair<string, string>("ProfileId", Session["ProfileId"].ToString()),
                

                });
                ObjectFactory.PostOnServerAsync("Profile/Profile/SaveUpdateProfiles", theParameter, "ProfileController", HttpContext);

                Session["EditProfileActiveTab"] = "2";

                //msgBuilder theBuilder = new msgBuilder();
                //theBuilder.DataElements["MessageText"] = "Demographic Detail Updated Successfully!";
                //MsgBox.Show("#C1", theBuilder, HttpContext);
            }
            catch (Exception err)
            {
                Logger.LogError(string.Format("Error Description:{0}", err.Message.ToString() + err.StackTrace.ToString()));
                msgBuilder theBuilder = new msgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString() + err.StackTrace.ToString();
                MsgBox.Show("#C1", theBuilder, HttpContext);
            }
            return RedirectToAction("frmEditProfile", "Profile");
        }

        [HttpPost]
        public ActionResult Profile_UserGallerySave(FormCollection theData)
        {
            try
            {
                var theOutput = theData["ProfileArray"];
                string[] theProfileFiles = theOutput.ToString().Split(',');

                DataTable theProfileDT = theProfilePicsTable();
                DataRow theDR;
                Int32 theCount = 1;
                var thePath = Server.MapPath(ConfigurationManager.AppSettings["ProfilePics"].ToString() + "Profile" + Session["ProfileId"].ToString());
                if (!Directory.Exists(thePath))
                {
                    Directory.CreateDirectory(thePath);
                }

                for (int i = 1; i < theProfileFiles.Length; i++)
                {
                    string theBase64 = theProfileFiles[i];
                    string thePicName = theProfileFiles[i + 1];
                    string theProfilePicFlag = theProfileFiles[i + 2];
                    string theProfilePicPrefix = "ProfileId_" + Session["ProfileId"].ToString();

                    theDR = theProfileDT.NewRow();
                    theDR["ProfileSectionId"] = "10";

                    if (thePicName.Contains(theProfilePicPrefix) == true)
                    {
                        theDR["PicName"] = thePicName;
                        theDR["PicDescription"] = thePicName;
                    }
                    else
                    {
                        theDR["PicName"] = theProfilePicPrefix + "_" + thePicName;
                        theDR["PicDescription"] = theProfilePicPrefix + "_" + thePicName;
                    }

                    theDR["IsProfile"] = theProfilePicFlag;
                    theProfileDT.Rows.Add(theDR);

                    i = i + 3;
                }

                DataSet theDS = new DataSet();
                theDS.Tables.Add(theProfileDT);
                string theTableXML = theDS.GetXml();

                var theParameter = new FormUrlEncodedContent(new[]{
                new KeyValuePair<string,string>("theProfileId",Session["ProfileId"].ToString()),
                new KeyValuePair<string,string>("theUserId",Session["UserId"].ToString()),
                new KeyValuePair<string,string>("ProfilPicTable",theTableXML)
                });

                ObjectFactory.PostOnServerAsync("Profile/Profile/SaveProfilePics", theParameter, "ProfileController", HttpContext);

                for (int i = 1; i < theProfileFiles.Length; i++)
                {
                    string theBase64 = theProfileFiles[i];
                    string theProfilePicPrefix = "ProfileId_" + Session["ProfileId"].ToString();
                    string theFileName = theProfileFiles[i + 1];
                    if (theFileName.Contains(theProfilePicPrefix) == true)
                    {
                        theFileName = theFileName;
                    }
                    else
                    {
                        theFileName = theProfilePicPrefix + "_" + theFileName;
                    }
                    string theServerPath = Path.Combine(thePath + "\\" + theFileName);
                    System.IO.File.WriteAllBytes(theServerPath, Convert.FromBase64String(theBase64));                

                    i = i + 3;
                }
                //foreach (HttpPostedFileBase theFile in theProfileFiles.theProfilePics.theProfilePic)
                //{
                //    if(theFile!=null)
                //    {
                //        theDR = theProfileDT.NewRow();
                //        theDR["ProfileSectionId"] = "10";
                //        theDR["PicName"] = "Profile"+ Session["ProfileId"]+"\\ProfileId_"+Session["ProfileId"].ToString()+"_" + theFile.FileName;
                //        theDR["PicDescription"] = "ProfileId_" + Session["ProfileId"].ToString() + "_" + theFile.FileName;
                //        theDR["IsProfile"] = "0";
                //        theProfileDT.Rows.Add(theDR);
                //        var theServerPath = Path.Combine(thePath + "\\ProfileId_" + Session["ProfileId"].ToString() + "_" + theFile.FileName);
                //        theFile.SaveAs(theServerPath);
                //    }
                //}
                //ViewBag.theDirPath = "~/Uploads/Profiles/Profile" + @Session["ProfileId"].ToString() + "/";
                //ViewBag.ProfilePicDT = theProfileDT.AsEnumerable();
            }
            catch (Exception err)
            {
                Logger.LogError(string.Format("Error Description:{0}", err.Message.ToString() + err.StackTrace.ToString()));
                msgBuilder theBuilder = new msgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString() + err.StackTrace.ToString();
                MsgBox.Show("#C1", theBuilder, HttpContext);
            }
            return RedirectToActionPermanent("frmEditProfile", "Profile");
        }

        private DataTable theProfilePicsTable()
        {
            DataTable theDT = new DataTable();
            theDT.Columns.Add("ProfileSectionId", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("PicName", System.Type.GetType("System.String"));
            theDT.Columns.Add("PicDescription", System.Type.GetType("System.String"));
            theDT.Columns.Add("IsProfile", System.Type.GetType("System.Int32"));
            return theDT;
        }

        private void LoadLifeInPics(DataTable theLifeInPicDT)
        {
            ViewBag.LifePic1 = "../../Content/assets/images/empty-img.png";
            ViewBag.LifePic1About = "";
            ViewBag.LifePic2 = "../../Content/assets/images/empty-img.png";
            ViewBag.LifePic2About = "";
            ViewBag.LifePic3 = "../../Content/assets/images/empty-img.png";
            ViewBag.LifePic3About = "";
            ViewBag.LifePic4 = "../../Content/assets/images/empty-img.png";
            ViewBag.LifePic4About = "";
            ViewBag.LifePic5 = "../../Content/assets/images/empty-img.png";
            ViewBag.LifePic5About = "";
            ViewBag.LifePic6 = "../../Content/assets/images/empty-img.png";
            ViewBag.LifePic6About = "";

            int theCounter = 1;
            if (theLifeInPicDT.Rows.Count > 0)
            {
                foreach (DataRow theDR in theLifeInPicDT.Rows)
                {
                    switch (theCounter)
                    {
                        case 1:
                            ViewBag.LifePic1 = ("..\\.." + ConfigurationManager.AppSettings["ProfilePics"].ToString() + "Profile" + @Session["ProfileId"] + "\\" + theDR["PicName"].ToString()).Replace("\\","/");
                            ViewBag.LifePic1Name= theDR["PicName"].ToString();
                            ViewBag.LifePic1Section = theDR["ProfileSectionId"].ToString();
                            ViewBag.LifePic1About = theDR["PicDescription"].ToString();
                            break;
                        case 2:
                            ViewBag.LifePic2 = ("..\\.." + ConfigurationManager.AppSettings["ProfilePics"].ToString() + "Profile" + @Session["ProfileId"] + "\\" + theDR["PicName"].ToString()).Replace("\\", "/");
                            ViewBag.LifePic2Name = theDR["PicName"].ToString(); 
                            ViewBag.LifePic2Section = theDR["ProfileSectionId"].ToString();
                            ViewBag.LifePic2About = theDR["PicDescription"].ToString();
                            break;
                        case 3:
                            ViewBag.LifePic3 = ("..\\.." + ConfigurationManager.AppSettings["ProfilePics"].ToString() + "Profile" + @Session["ProfileId"] + "\\" + theDR["PicName"].ToString()).Replace("\\", "/");
                            ViewBag.LifePic3Name = theDR["PicName"].ToString(); 
                            ViewBag.LifePic3Section = theDR["ProfileSectionId"].ToString(); 
                            ViewBag.LifePic3About = theDR["PicDescription"].ToString();
                            break;
                        case 4:
                            ViewBag.LifePic4 = ("..\\.." + ConfigurationManager.AppSettings["ProfilePics"].ToString() + "Profile" + @Session["ProfileId"] + "\\" + theDR["PicName"].ToString()).Replace("\\", "/");
                            ViewBag.LifePic4Name = theDR["PicName"].ToString(); 
                            ViewBag.LifePic4Section = theDR["ProfileSectionId"].ToString(); 
                            ViewBag.LifePic4About = theDR["PicDescription"].ToString();
                            break;
                        case 5:
                            ViewBag.LifePic5 = ("..\\.." + ConfigurationManager.AppSettings["ProfilePics"].ToString() + "Profile" + @Session["ProfileId"] + "\\" + theDR["PicName"].ToString()).Replace("\\", "/");
                            ViewBag.LifePic5Name = theDR["PicName"].ToString(); 
                            ViewBag.LifePic5Section = theDR["ProfileSectionId"].ToString(); 
                            ViewBag.LifePic5About = theDR["PicDescription"].ToString();
                            break;
                        case 6:
                            ViewBag.LifePic6 = ("..\\.." + ConfigurationManager.AppSettings["ProfilePics"].ToString() + "Profile" + @Session["ProfileId"] + "\\" + theDR["PicName"].ToString()).Replace("\\", "/");
                            ViewBag.LifePic6Name = theDR["PicName"].ToString(); 
                            ViewBag.LifePic6Section = theDR["ProfileSectionId"].ToString(); 
                            ViewBag.LifePic6About = theDR["PicDescription"].ToString();
                            break;
                    }
                    theCounter++;
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveUpdateLifeInPic(HttpPostedFileBase FUSectionSelector1, HttpPostedFileBase FUSectionSelector2, HttpPostedFileBase FUSectionSelector3, HttpPostedFileBase FUSectionSelector4, HttpPostedFileBase FUSectionSelector5, HttpPostedFileBase FUSectionSelector6)
        {
            try
            {

                var thePath = Server.MapPath(ConfigurationManager.AppSettings["ProfilePics"].ToString() + "Profile" + Session["ProfileId"].ToString());
                if (!Directory.Exists(thePath))
                {
                    Directory.CreateDirectory(thePath);
                }

                DataTable theProfileDT = theProfilePicsTable();
                DataRow theDR;
                string ddlProfileSection1 = Request.Form["ddlProfileSection1"].Replace(",", "");
                string ddlProfileSection2 = Request.Form["ddlProfileSection2"].Replace(",", "");
                string ddlProfileSection3 = Request.Form["ddlProfileSection3"].Replace(",", "");
                string ddlProfileSection4 = Request.Form["ddlProfileSection4"].Replace(",", "");
                string ddlProfileSection5 = Request.Form["ddlProfileSection5"].Replace(",", "");
                string ddlProfileSection6 = Request.Form["ddlProfileSection6"].Replace(",", "");

                if (FUSectionSelector1 != null && FUSectionSelector1.ContentLength > 0 && !string.IsNullOrEmpty(ddlProfileSection1) && !string.IsNullOrEmpty(Request.Form["SectionAbout1"]))
                {
                    string ddlProfileSection = ddlProfileSection1;
                    string SectionAbout = Request.Form["SectionAbout1"];

                    string theProfilePicPrefix = "LifeInPic_Section_" + ddlProfileSection + "_";
                    string thePicName = Path.GetFileName(FUSectionSelector1.FileName);
                    if (thePicName.Contains(theProfilePicPrefix) == true)
                    {
                        thePicName = thePicName;
                    }
                    else
                    {
                        thePicName = theProfilePicPrefix + thePicName;
                    }
                    string theProfilePicFlag = "0";
                    string theServerPath = Path.Combine(thePath + "\\" + thePicName);
                    FUSectionSelector1.SaveAs(theServerPath);

                    theDR = theProfileDT.NewRow();
                    theDR["ProfileSectionId"] = ddlProfileSection;
                    theDR["PicName"] = thePicName;
                    theDR["PicDescription"] = SectionAbout;
                    theDR["IsProfile"] = "0";
                    theProfileDT.Rows.Add(theDR);
                }
                else if (!string.IsNullOrEmpty(Request.Form["LifePic1Name"]) && !string.IsNullOrEmpty(ddlProfileSection1) && !string.IsNullOrEmpty(Request.Form["SectionAbout1"]))
                {
                    string ddlProfileSection = ddlProfileSection1;
                    string SectionAbout = Request.Form["SectionAbout1"];
                    string thePicName = Request.Form["LifePic1Name"];

                    theDR = theProfileDT.NewRow();
                    theDR["ProfileSectionId"] = ddlProfileSection;
                    theDR["PicName"] = thePicName;
                    theDR["PicDescription"] = SectionAbout;
                    theDR["IsProfile"] = "0";
                    theProfileDT.Rows.Add(theDR);
                }

                if (FUSectionSelector2 != null && FUSectionSelector2.ContentLength > 0 && !string.IsNullOrEmpty(ddlProfileSection2) && !string.IsNullOrEmpty(Request.Form["SectionAbout2"]))
                {
                    string ddlProfileSection = ddlProfileSection2;
                    string SectionAbout = Request.Form["SectionAbout2"];

                    string theProfilePicPrefix = "LifeInPic_Section_" + ddlProfileSection + "_";
                    string thePicName = Path.GetFileName(FUSectionSelector2.FileName);
                    if (thePicName.Contains(theProfilePicPrefix) == true)
                    {
                        thePicName = thePicName;
                    }
                    else
                    {
                        thePicName = theProfilePicPrefix + thePicName;
                    }
                    string theProfilePicFlag = "0";
                    string theServerPath = Path.Combine(thePath + "\\" + thePicName);
                    FUSectionSelector2.SaveAs(theServerPath);

                    theDR = theProfileDT.NewRow();
                    theDR["ProfileSectionId"] = ddlProfileSection;
                    theDR["PicName"] = thePicName;
                    theDR["PicDescription"] = SectionAbout;
                    theDR["IsProfile"] = "0";
                    theProfileDT.Rows.Add(theDR);
                }
                else if (!string.IsNullOrEmpty(Request.Form["LifePic2Name"]) && !string.IsNullOrEmpty(ddlProfileSection2) && !string.IsNullOrEmpty(Request.Form["SectionAbout2"]))
                {
                    string ddlProfileSection = ddlProfileSection2;
                    string SectionAbout = Request.Form["SectionAbout2"];
                    string thePicName = Request.Form["LifePic2Name"];

                    theDR = theProfileDT.NewRow();
                    theDR["ProfileSectionId"] = ddlProfileSection;
                    theDR["PicName"] = thePicName;
                    theDR["PicDescription"] = SectionAbout;
                    theDR["IsProfile"] = "0";
                    theProfileDT.Rows.Add(theDR);
                }

                if (FUSectionSelector3 != null && FUSectionSelector3.ContentLength > 0 && !string.IsNullOrEmpty(ddlProfileSection3) && !string.IsNullOrEmpty(Request.Form["SectionAbout3"]))
                {
                    string ddlProfileSection = ddlProfileSection3;
                    string SectionAbout = Request.Form["SectionAbout3"];

                    string theProfilePicPrefix = "LifeInPic_Section_" + ddlProfileSection + "_";
                    string thePicName = Path.GetFileName(FUSectionSelector3.FileName);
                    if (thePicName.Contains(theProfilePicPrefix) == true)
                    {
                        thePicName = thePicName;
                    }
                    else
                    {
                        thePicName = theProfilePicPrefix + thePicName;
                    }
                    string theProfilePicFlag = "0";
                    string theServerPath = Path.Combine(thePath + "\\" + thePicName);
                    FUSectionSelector3.SaveAs(theServerPath);

                    theDR = theProfileDT.NewRow();
                    theDR["ProfileSectionId"] = ddlProfileSection;
                    theDR["PicName"] = thePicName;
                    theDR["PicDescription"] = SectionAbout;
                    theDR["IsProfile"] = "0";
                    theProfileDT.Rows.Add(theDR);
                }
                else if (!string.IsNullOrEmpty(Request.Form["LifePic3Name"]) && !string.IsNullOrEmpty(ddlProfileSection3) && !string.IsNullOrEmpty(Request.Form["SectionAbout3"]))
                {
                    string ddlProfileSection = ddlProfileSection3;
                    string SectionAbout = Request.Form["SectionAbout3"];
                    string thePicName = Request.Form["LifePic3Name"];

                    theDR = theProfileDT.NewRow();
                    theDR["ProfileSectionId"] = ddlProfileSection;
                    theDR["PicName"] = thePicName;
                    theDR["PicDescription"] = SectionAbout;
                    theDR["IsProfile"] = "0";
                    theProfileDT.Rows.Add(theDR);
                }

                if (FUSectionSelector4 != null && FUSectionSelector4.ContentLength > 0 && !string.IsNullOrEmpty(ddlProfileSection4) && !string.IsNullOrEmpty(Request.Form["SectionAbout4"]))
                {
                    string ddlProfileSection = ddlProfileSection4;
                    string SectionAbout = Request.Form["SectionAbout4"];

                    string theProfilePicPrefix = "LifeInPic_Section_" + ddlProfileSection + "_";
                    string thePicName = Path.GetFileName(FUSectionSelector4.FileName);
                    if (thePicName.Contains(theProfilePicPrefix) == true)
                    {
                        thePicName = thePicName;
                    }
                    else
                    {
                        thePicName = theProfilePicPrefix + thePicName;
                    }
                    string theProfilePicFlag = "0";
                    string theServerPath = Path.Combine(thePath + "\\" + thePicName);
                    FUSectionSelector4.SaveAs(theServerPath);

                    theDR = theProfileDT.NewRow();
                    theDR["ProfileSectionId"] = ddlProfileSection;
                    theDR["PicName"] = thePicName;
                    theDR["PicDescription"] = SectionAbout;
                    theDR["IsProfile"] = "0";
                    theProfileDT.Rows.Add(theDR);
                }
                else if (!string.IsNullOrEmpty(Request.Form["LifePic4Name"]) && !string.IsNullOrEmpty(ddlProfileSection4) && !string.IsNullOrEmpty(Request.Form["SectionAbout4"]))
                {
                    string ddlProfileSection = ddlProfileSection4;
                    string SectionAbout = Request.Form["SectionAbout4"];
                    string thePicName = Request.Form["LifePic4Name"];

                    theDR = theProfileDT.NewRow();
                    theDR["ProfileSectionId"] = ddlProfileSection;
                    theDR["PicName"] = thePicName;
                    theDR["PicDescription"] = SectionAbout;
                    theDR["IsProfile"] = "0";
                    theProfileDT.Rows.Add(theDR);
                }

                if (FUSectionSelector5 != null && FUSectionSelector5.ContentLength > 0 && !string.IsNullOrEmpty(ddlProfileSection5) && !string.IsNullOrEmpty(Request.Form["SectionAbout5"]))
                {
                    string ddlProfileSection = ddlProfileSection5;
                    string SectionAbout = Request.Form["SectionAbout5"];

                    string theProfilePicPrefix = "LifeInPic_Section_" + ddlProfileSection + "_";
                    string thePicName = Path.GetFileName(FUSectionSelector5.FileName);
                    if (thePicName.Contains(theProfilePicPrefix) == true)
                    {
                        thePicName = thePicName;
                    }
                    else
                    {
                        thePicName = theProfilePicPrefix + thePicName;
                    }
                    string theProfilePicFlag = "0";
                    string theServerPath = Path.Combine(thePath + "\\" + thePicName);
                    FUSectionSelector5.SaveAs(theServerPath);

                    theDR = theProfileDT.NewRow();
                    theDR["ProfileSectionId"] = ddlProfileSection;
                    theDR["PicName"] = thePicName;
                    theDR["PicDescription"] = SectionAbout;
                    theDR["IsProfile"] = "0";
                    theProfileDT.Rows.Add(theDR);
                }
                else if (!string.IsNullOrEmpty(Request.Form["LifePic5Name"]) && !string.IsNullOrEmpty(ddlProfileSection5) && !string.IsNullOrEmpty(Request.Form["SectionAbout5"]))
                {
                    string ddlProfileSection = ddlProfileSection5;
                    string SectionAbout = Request.Form["SectionAbout5"];
                    string thePicName = Request.Form["LifePic5Name"];

                    theDR = theProfileDT.NewRow();
                    theDR["ProfileSectionId"] = ddlProfileSection;
                    theDR["PicName"] = thePicName;
                    theDR["PicDescription"] = SectionAbout;
                    theDR["IsProfile"] = "0";
                    theProfileDT.Rows.Add(theDR);
                }

                if (FUSectionSelector6 != null && FUSectionSelector6.ContentLength > 0 && !string.IsNullOrEmpty(ddlProfileSection6) && !string.IsNullOrEmpty(Request.Form["SectionAbout6"]))
                {
                    string ddlProfileSection = ddlProfileSection6;
                    string SectionAbout = Request.Form["SectionAbout6"];

                    string theProfilePicPrefix = "LifeInPic_Section_" + ddlProfileSection + "_";
                    string thePicName = Path.GetFileName(FUSectionSelector6.FileName);
                    if (thePicName.Contains(theProfilePicPrefix) == true)
                    {
                        thePicName = thePicName;
                    }
                    else
                    {
                        thePicName = theProfilePicPrefix + thePicName;
                    }
                    string theProfilePicFlag = "0";
                    string theServerPath = Path.Combine(thePath + "\\" + thePicName);
                    FUSectionSelector6.SaveAs(theServerPath);

                    theDR = theProfileDT.NewRow();
                    theDR["ProfileSectionId"] = ddlProfileSection;
                    theDR["PicName"] = thePicName;
                    theDR["PicDescription"] = SectionAbout;
                    theDR["IsProfile"] = "0";
                    theProfileDT.Rows.Add(theDR);
                }
                else if (!string.IsNullOrEmpty(Request.Form["LifePic6Name"]) && !string.IsNullOrEmpty(ddlProfileSection6) && !string.IsNullOrEmpty(Request.Form["SectionAbout6"]))
                {
                    string ddlProfileSection = ddlProfileSection6;
                    string SectionAbout = Request.Form["SectionAbout6"];
                    string thePicName = Request.Form["LifePic6Name"];

                    theDR = theProfileDT.NewRow();
                    theDR["ProfileSectionId"] = ddlProfileSection;
                    theDR["PicName"] = thePicName;
                    theDR["PicDescription"] = SectionAbout;
                    theDR["IsProfile"] = "0";
                    theProfileDT.Rows.Add(theDR);
                }

                if (theProfileDT.Rows.Count > 0)
                {
                    DataSet theDS = new DataSet();
                    theDS.Tables.Add(theProfileDT);
                    string theTableXML = theDS.GetXml();

                    var theParameter = new FormUrlEncodedContent(new[]{
                new KeyValuePair<string,string>("theProfileId",Session["ProfileId"].ToString()),
                new KeyValuePair<string,string>("theUserId",Session["UserId"].ToString()),
                new KeyValuePair<string,string>("ProfilPicTable",theTableXML)
                });

                    ObjectFactory.PostOnServerAsync("Profile/Profile/SaveProfilePics", theParameter, "ProfileController", HttpContext);
                    
                    Session["EditProfileActiveTab"] = "6";
                    
                    //msgBuilder theBuilder = new msgBuilder();
                    //theBuilder.DataElements["MessageText"] = "Life in Picture Updated Successfully!";
                    //MsgBox.Show("#C1", theBuilder, HttpContext);
                }
                else
                {
                    msgBuilder theBuilder = new msgBuilder();
                    theBuilder.DataElements["MessageText"] = "Life in Picture not Found!";
                    MsgBox.Show("#C1", theBuilder, HttpContext);
                }

            }
            catch (Exception err)
            {
                Logger.LogError(string.Format("Error Description:{0}", err.Message.ToString() + err.StackTrace.ToString()));
                msgBuilder theBuilder = new msgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString() + err.StackTrace.ToString();
                MsgBox.Show("#C1", theBuilder, HttpContext);
            }
            return RedirectToAction("frmEditProfile", "Profile");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveUpdatePersonalHabits(Profile.Models.NDUserProfile theModel)
        {

            try
            {
                //if (!ModelState.IsValid)
                //{
                //    return View("frmEditProfile", theModel);
                //}

                theModel.thePersonalHabits.SelectedMultiMusicId = theModel.thePersonalHabits.SelectedMultiMusicId == null ? new List<int> { 0 } : theModel.thePersonalHabits.SelectedMultiMusicId;



                DataTable theSelectedMusic = SelectedMusicDT();
                for (int i = 0; i < theModel.thePersonalHabits.SelectedMultiMusicId.Count; i++)
                {
                    DataRow theDR = theSelectedMusic.NewRow();
                    theDR["ProfileId"] = Convert.ToInt32(Session["ProfileId"].ToString());
                    theDR["MusicId"] = Convert.ToInt32(theModel.thePersonalHabits.SelectedMultiMusicId[i].ToString());
                    theSelectedMusic.Rows.Add(theDR);
                }
                

                DataSet theTmpDSSelectedMusic = new DataSet();
                theTmpDSSelectedMusic.Tables.Add(theSelectedMusic);
                string theSelectedMusicXML = theTmpDSSelectedMusic.GetXml();

                var theParameter = new FormUrlEncodedContent(new[]{
                    new KeyValuePair<string,string>("Id","0"),
                new KeyValuePair<string,string>("Q1_are_you_a_night_owl_or_a_morning_person",theModel.thePersonalHabits.Q1_are_you_a_night_owl_or_a_morning_person),
                new KeyValuePair<string,string>("Q2_are_you_someone_who_enjoys_physical_activity_or_more_of_a_couch_potato",theModel.thePersonalHabits.Q2_are_you_someone_who_enjoys_physical_activity_or_more_of_a_couch_potato),
                new KeyValuePair<string,string>("Q3_do_you_like_to_travel_or_are_you_a_homebody",theModel.thePersonalHabits.Q3_do_you_like_to_travel_or_are_you_a_homebody),
                new KeyValuePair<string,string>("Q4_do_you_drink_alcohol",theModel.thePersonalHabits.Q4_do_you_drink_alcohol),
                new KeyValuePair<string,string>("Q5_do_you_smoke_tobacco_products",theModel.thePersonalHabits.Q5_do_you_smoke_tobacco_products),
                new KeyValuePair<string,string>("Q6_do_you_use_recreational_drugs",theModel.thePersonalHabits.Q6_do_you_use_recreational_drugs),
                new KeyValuePair<string,string>("Q7_is_there_anything_you_watch_regularly_on_television",theModel.thePersonalHabits.Q7_is_there_anything_you_watch_regularly_on_television),
                new KeyValuePair<string,string>("Q8_do_you_have_pets",theModel.thePersonalHabits.Q8_do_you_have_pets),
                new KeyValuePair<string,string>("Q10_do_you_have_strong_political_affiliations",theModel.thePersonalHabits.Q10_do_you_have_strong_political_affiliations),
                new KeyValuePair<string,string>("Q11_would_you_consider_relocating",theModel.thePersonalHabits.Q11_would_you_consider_relocating),
                new KeyValuePair<string,string>("UserId", Session["UserId"].ToString()),
                new KeyValuePair<string, string>("ProfileId", Session["ProfileId"].ToString()),
                new KeyValuePair<string,string>("MusicTable",theTmpDSSelectedMusic.GetXml())
                });
                ObjectFactory.PostOnServerAsync("Profile/Profile/SaveUpdateProfilesPersonalHabits", theParameter, "ProfileController", HttpContext);

                Session["EditProfileActiveTab"] = "7";

                //msgBuilder theBuilder = new msgBuilder();
                //theBuilder.DataElements["MessageText"] = "Personal Habits Updated Successfully!";
                //MsgBox.Show("#C1", theBuilder, HttpContext);
            }
            catch (Exception err)
            {
                Logger.LogError(string.Format("Error Description:{0}", err.Message.ToString() + err.StackTrace.ToString()));
                msgBuilder theBuilder = new msgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString() + err.StackTrace.ToString();
                MsgBox.Show("#C1", theBuilder, HttpContext);
            }
            return RedirectToAction("frmEditProfile", "Profile");
        }

        private DataTable SelectedMusicDT()
        {
            DataTable theDT = new DataTable();
            theDT.Columns.Add("ProfileId", typeof(Int32));
            theDT.Columns.Add("MusicId", typeof(Int32));
            return theDT;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveUpdateActivityPreferences(Profile.Models.NDUserProfile theModel)
        {

            try
            {
                string[] SelectedMultiMovieId = Request.Form["hfSelectedMultiMovieId"] == "" ? new string[] { "0" } : Request.Form["hfSelectedMultiMovieId"].Split(',');

                string[] SelectedMultiCuisineId = Request.Form["hfSelectedMultiCuisineId"]==""?new string[] {"0" } : Request.Form["hfSelectedMultiCuisineId"].Split(',');

                DataTable theSelectedMovie = SelectedMovieDT();
                for (int i = 0; i < SelectedMultiMovieId.Length; i++)
                {
                    DataRow theDR = theSelectedMovie.NewRow();
                    theDR["ProfileId"] = Convert.ToInt32(Session["ProfileId"].ToString());
                    theDR["MovieId"] = Convert.ToInt32(SelectedMultiMovieId[i].ToString());
                    theSelectedMovie.Rows.Add(theDR);
                }

                DataTable theSelectedCuisine= SelectedCuisineDT();
                for (int i = 0; i < SelectedMultiCuisineId.Length; i++)
                {
                    DataRow theDR = theSelectedCuisine.NewRow();
                    theDR["ProfileId"] = Convert.ToInt32(Session["ProfileId"].ToString());
                    theDR["CuisineId"] = Convert.ToInt32(SelectedMultiCuisineId[i].ToString());
                    theSelectedCuisine.Rows.Add(theDR);
                }

                DataSet theTmpDSSelectedMovie = new DataSet();
                theTmpDSSelectedMovie.Tables.Add(theSelectedMovie);
                string theSelectedMovieXML = theTmpDSSelectedMovie.GetXml();

                DataSet theTmpDSSelectedCuisine = new DataSet();
                theTmpDSSelectedCuisine.Tables.Add(theSelectedCuisine );
                string theSelectedCuisineXML = theTmpDSSelectedCuisine.GetXml();

                var theParameter = new FormUrlEncodedContent(new[]{
                    new KeyValuePair<string,string>("Id","0"),
                new KeyValuePair<string,string>("Q2_Having_Tea_Coffee",theModel.theActivityPreferences.Q2_Having_Tea_Coffee),
                new KeyValuePair<string,string>("Q3_Going_To_a_bar_sports_bar",theModel.theActivityPreferences.Q3_Going_To_a_bar_sports_bar),
                new KeyValuePair<string,string>("Q4_Going_To_a_Concert",theModel.theActivityPreferences.Q4_Going_To_a_Concert),
                new KeyValuePair<string,string>("Q5_Watching_a_Live_Play",theModel.theActivityPreferences.Q5_Watching_a_Live_Play),
                new KeyValuePair<string,string>("Q6_Watching_a_musical_live_on_TV",theModel.theActivityPreferences.Q6_Watching_a_musical_live_on_TV),
                new KeyValuePair<string,string>("Q7_Fishing_Hunting",theModel.theActivityPreferences.Q7_Fishing_Hunting),
                new KeyValuePair<string,string>("Q8_Participating_In_Water_Sports_in_an_natural_Body_Of_Water",theModel.theActivityPreferences.Q8_Participating_In_Water_Sports_in_an_natural_Body_Of_Water),
                new KeyValuePair<string,string>("Q9_Going_for_a_motorcycle_ride",theModel.theActivityPreferences.Q9_Going_for_a_motorcycle_ride),
                new KeyValuePair<string,string>("Q10_Go_on_a_hike",theModel.theActivityPreferences.Q10_Go_on_a_hike),
                new KeyValuePair<string,string>("Q11_Going_for_a_bike_ride",theModel.theActivityPreferences.Q11_Going_for_a_bike_ride),
                new KeyValuePair<string,string>("Q12_Going_for_a_run",theModel.theActivityPreferences.Q12_Going_for_a_run),
                new KeyValuePair<string,string>("Q13_Going_for_a_leisurely_walk",theModel.theActivityPreferences.Q13_Going_for_a_leisurely_walk),
                new KeyValuePair<string,string>("Q14_Going_on_a_road_trip_vacation",theModel.theActivityPreferences.Q14_Going_on_a_road_trip_vacation),
                new KeyValuePair<string,string>("Q15_Going_on_a_picnic",theModel.theActivityPreferences.Q15_Going_on_a_picnic),
                new KeyValuePair<string,string>("Q16_Bowling",theModel.theActivityPreferences.Q16_Bowling),
                new KeyValuePair<string,string>("Q17_Going_To_a_museum",theModel.theActivityPreferences.Q17_Going_To_a_museum),
                new KeyValuePair<string,string>("Q18_Going_shopping",theModel.theActivityPreferences.Q18_Going_shopping),
                new KeyValuePair<string,string>("Q20_Going_dancing",theModel.theActivityPreferences.Q20_Going_dancing),
                new KeyValuePair<string,string>("UserId", Session["UserId"].ToString()),
                new KeyValuePair<string, string>("ProfileId", Session["ProfileId"].ToString()),
                new KeyValuePair<string,string>("MovieTable",theTmpDSSelectedMovie.GetXml()),
                new KeyValuePair<string,string>("CuisineTable",theTmpDSSelectedCuisine.GetXml())
                });
                ObjectFactory.PostOnServerAsync("Profile/Profile/SaveUpdateProfilesActivityPreferences", theParameter, "ProfileController", HttpContext);

                Session["EditProfileActiveTab"] = "7";

                //msgBuilder theBuilder = new msgBuilder();
                //theBuilder.DataElements["MessageText"] = "Personal Habits Updated Successfully!";
                //MsgBox.Show("#C1", theBuilder, HttpContext);
            }
            catch (Exception err)
            {
                Logger.LogError(string.Format("Error Description:{0}", err.Message.ToString() + err.StackTrace.ToString()));
                msgBuilder theBuilder = new msgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString() + err.StackTrace.ToString();
                MsgBox.Show("#C1", theBuilder, HttpContext);
            }
            return RedirectToAction("frmEditProfile", "Profile");
        }

        private DataTable SelectedCuisineDT()
        {
            DataTable theDT = new DataTable();
            theDT.Columns.Add("ProfileId", typeof(Int32));
            theDT.Columns.Add("CuisineId", typeof(Int32));
            return theDT;
        }

        private DataTable SelectedMovieDT()
        {
            DataTable theDT = new DataTable();
            theDT.Columns.Add("ProfileId", typeof(Int32));
            theDT.Columns.Add("MovieId", typeof(Int32));
            return theDT;
        }
    }
}