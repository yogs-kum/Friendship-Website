using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Text;
using System.Data;
using Interface.Profile;


namespace WebAPI.Controllers.Profile
{
    public class ProfileController : ApiController
    {
        private readonly IProfile _theObject;
        // GET: Signup
        public ProfileController(IProfile theProfile)
        {
            _theObject = theProfile;
        }

        [HttpPost]
        public void ProfileRegistration(FormDataCollection theData)
        {
            _theObject.ProfileRegistration(theData.Get("FirstName"), theData.Get("LastName"), theData.Get("Email"),theData.Get("Phone"), theData.Get("Password"));
        }

        [HttpPost]
        public void SaveUpdateProfiles(FormDataCollection theData)
        {
            var date = theData.Get("DOB");
            _theObject.SaveUpdateProfile(id: 0, theData.Get("FirstName"), theData.Get("LastName"),theData.Get("NickName"), theData.Get("Email"),
                theData.Get("Phone"), theData.Get("City"), Convert.ToInt32(theData.Get("StateId")),
                Convert.ToInt32(theData.Get("CountryId")), theData.Get("Address"),Convert.ToDateTime(theData.Get("DOB")), theData.Get("PinCode"), theData.Get("Password"),
                Convert.ToInt32(theData.Get("Gender")),Convert.ToInt32(theData.Get("MaritalStatus")),Convert.ToInt32(theData.Get("Race")),
                Convert.ToInt32(theData.Get("Height_Feets")),Convert.ToInt32(theData.Get("Height_Inc")), theData.Get("UserId"), 
                Convert.ToInt32(theData.Get("OprId")), Convert.ToInt32(theData.Get("ProfileId")));
        }

        [HttpPost]
        public void SaveProfilePics(FormDataCollection theData)
        {
            byte[] theByte = Encoding.UTF8.GetBytes(theData.Get("ProfilPicTable"));
            System.IO.MemoryStream theSteam = new System.IO.MemoryStream(theByte, 0, theByte.Length);
            DataSet theDS = new DataSet();
            theDS.ReadXml(theSteam);
            _theObject.SaveProfilePics(Convert.ToInt32(theData.Get("theProfileId")), Convert.ToInt32(theData.Get("theUserId")), theDS.Tables[0]);
        }

        [HttpPost]
        public void SaveUpdateProfilesPersonalHabits(FormDataCollection theData)
        {
            byte[] theByte = Encoding.UTF8.GetBytes(theData.Get("MusicTable"));
            System.IO.MemoryStream theSteam = new System.IO.MemoryStream(theByte, 0, theByte.Length);
            DataSet theDS = new DataSet();
            theDS.ReadXml(theSteam);
            _theObject.SaveUpdateProfilesPersonalHabits(Convert.ToInt32(theData.Get("Id")), theData.Get("Q1_are_you_a_night_owl_or_a_morning_person"), theData.Get("Q2_are_you_someone_who_enjoys_physical_activity_or_more_of_a_couch_potato"), theData.Get("Q3_do_you_like_to_travel_or_are_you_a_homebody"), theData.Get("Q4_do_you_drink_alcohol"), theData.Get("Q5_do_you_smoke_tobacco_products"), theData.Get("Q6_do_you_use_recreational_drugs"), theData.Get("Q7_is_there_anything_you_watch_regularly_on_television"), theData.Get("Q8_do_you_have_pets"), theData.Get("Q10_do_you_have_strong_political_affiliations"), theData.Get("Q11_would_you_consider_relocating"), Convert.ToInt32(theData.Get("UserId")), Convert.ToInt32(theData.Get("ProfileId")), theDS.Tables[0]);
        }

        [HttpPost]
        public void SaveUpdateProfilesActivityPreferences(FormDataCollection theData)
        {
            byte[] theByte = Encoding.UTF8.GetBytes(theData.Get("MovieTable"));
            System.IO.MemoryStream theSteam = new System.IO.MemoryStream(theByte, 0, theByte.Length);
            DataSet theDS = new DataSet();
            //theDS.Tables.Add();//Yogesh 07-06
            theDS.ReadXml(theSteam);

            byte[] theBytes = Encoding.UTF8.GetBytes(theData.Get("CuisineTable"));
            System.IO.MemoryStream theSteams = new System.IO.MemoryStream(theBytes, 0, theBytes.Length);
            DataSet theDSS = new DataSet();
            //theDSS.Tables.Add();//Yogesh 07-06
            theDSS.ReadXml(theSteams);
            _theObject.SaveUpdateProfilesActivityPreferences(Convert.ToInt32(theData.Get("Id")), theData.Get("Q2_Having_Tea_Coffee"), theData.Get("Q3_Going_To_a_bar_sports_bar"), theData.Get("Q4_Going_To_a_Concert"), theData.Get("Q5_Watching_a_Live_Play"), theData.Get("Q6_Watching_a_musical_live_on_TV"), theData.Get("Q7_Fishing_Hunting"), theData.Get("Q8_Participating_In_Water_Sports_in_an_natural_Body_Of_Water"), theData.Get("Q9_Going_for_a_motorcycle_ride"), theData.Get("Q10_Go_on_a_hike"), theData.Get("Q11_Going_for_a_bike_ride"), theData.Get("Q12_Going_for_a_run"), theData.Get("Q13_Going_for_a_leisurely_walk"), theData.Get("Q14_Going_on_a_road_trip_vacation"), theData.Get("Q15_Going_on_a_picnic"), theData.Get("Q16_Bowling"), theData.Get("Q17_Going_To_a_museum"), theData.Get("Q18_Going_shopping"), theData.Get("Q20_Going_dancing"), Convert.ToInt32(theData.Get("UserId")), Convert.ToInt32(theData.Get("ProfileId")), theDS.Tables[0],theDSS.Tables[0]);
        }

        [HttpPost]
        public void SaveProfilePhoto(FormDataCollection theData)
        {
            _theObject.SaveProfilePhotos(Convert.ToInt32(theData.Get("Id")), Convert.ToInt32(theData.Get("ProfileId")), Convert.ToInt32(theData.Get("ProfileSectionId")), theData.Get("PicName"), theData.Get("PicDescription"), Convert.ToInt32(theData.Get("UserId")));
        }

        [HttpGet]
        public string GetUserProfilePhotos(Int32 theUserId)
        {
            return _theObject.GetUserProfilePhotos(theUserId);
        }
        [HttpGet]
        public string GetUserEmailValidated(string email)
        {
            return _theObject.GetUserEmailValidated(email);
        }

        [HttpGet]
        public string GetUserProfile(Int32 theUserId)
        {
            return _theObject.GetUserProfile(theUserId);
        }

        [HttpGet]
        public string GetSearhedProfile(Int32 theUserId)
        {
            return _theObject.GetSearhedProfile(theUserId);
        }
        [HttpGet]
        public string GetChatUser(Int32 theUserId)
        {
            return _theObject.GetChatUser(theUserId);
        }
        [HttpGet]
        public string GetUserProfileQuestion(Int32 theUserId)
        {
            return _theObject.GetUserProfileQuestions(theUserId);
        }

        [HttpGet]
        public string GetUserProfileSpecificInterest(Int32 theUserId)
        {
            return _theObject.GetUserSpecificInterests(theUserId);
        }

        [HttpGet]
        public string GetUserProfileCharacterStrengths(Int32 theUserId)
        {
            return _theObject.GetUserProfileCharacterStrength(theUserId);
        }
        [HttpGet]
        public string GetUserProfileNeurodiverseStrengths(Int32 theUserId)
        {
            return _theObject.GetUserProfileNeurodiverseStrengths(theUserId);
        }
        [HttpPost]
        public void SaveProfileQuestions(FormDataCollection theData)
        {
            _theObject.SaveProfileQuestions(Convert.ToInt32(theData.Get("Id")), Convert.ToInt32(theData.Get("ProfileId")), Convert.ToInt32(theData.Get("theQuestionId")), theData.Get("theResponse"), Convert.ToInt32(theData.Get("UserId")));
        }

        [HttpPost]
        public void SaveProfileCharacterStrengths(FormDataCollection theData)
        {
            _theObject.SaveProfileCharacterStrength(Convert.ToInt32(theData.Get("Id")), Convert.ToInt32(theData.Get("ProfileId")), Convert.ToInt32(theData.Get("theStrengthId")), Convert.ToInt32(theData.Get("UserId")));
        }

        [HttpPost]
        public void SaveProfileSpecificInterests(FormDataCollection theData)
        {
            _theObject.SaveProfileSpecificInterests(Convert.ToInt32(theData.Get("Id")), Convert.ToInt32(theData.Get("ProfileId")), Convert.ToInt32(theData.Get("theInterestId")), Convert.ToInt32(theData.Get("UserId")));
        }

        [HttpPost]
        public void SaveProfileSpecificInterestsNew(FormDataCollection theData)
        {
            _theObject.SaveProfileSpecificInterestsNew(Convert.ToInt32(theData.Get("Id")), Convert.ToInt32(theData.Get("ProfileId")), theData.Get("theInterestId"), theData.Get("theStrengthId"), theData.Get("theDesiredStrengthId"), Convert.ToInt32(theData.Get("UserId")));
        }

        [HttpPost]
        public void SaveProfileNeurodiverseStrengths(FormDataCollection theData)
        {
            _theObject.SaveProfileNeurodiverseStrengths(Convert.ToInt32(theData.Get("Id")), Convert.ToInt32(theData.Get("ProfileId")), Convert.ToInt32(theData.Get("theTraitId")), Convert.ToInt32(theData.Get("theScore")), Convert.ToInt32(theData.Get("UserId")));
        }

        [HttpPost]
        public void SaveInterestAndCharacterStrength(FormDataCollection theData)
        {
            _theObject.SaveInterestAndCharacterStrength(Convert.ToInt32(theData.Get("Id")), Convert.ToInt32(theData.Get("ProfileId")), Convert.ToInt32(theData.Get("theStrengthId")), Convert.ToInt32(theData.Get("theStrengthTypeID")), Convert.ToInt32(theData.Get("UserId")));
        }
        #region Notifications

        [HttpGet]
        public string GetUserNotifications(int UserId, int ActiveChatUserId)
        {
            return _theObject.GetUserNotifications(UserId, ActiveChatUserId);
        }
        [HttpGet]
        public void UpdateAlertNotifications(int UserId)
        {
            _theObject.UpdateAlertNotifications(UserId);
        }
        [HttpGet]
        public void UpdateNotificationsRead(int UserId)
        {
            _theObject.UpdateNotificationsRead(UserId);
        }
        #endregion
    }
}