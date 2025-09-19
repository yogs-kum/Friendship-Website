using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Interface.Profile
{
    public interface IProfile
    {
        void ProfileRegistration(string theFirstName, string theLastName, string theEmail, string thePhone, string thePassword);

        void SaveUpdateProfile(int id, string theFirstName, string theLastName, string theNickName, string theEmail, string thePhone,
            string theCity, Int32 theStateId, Int32 theCountryId, string theAddress, DateTime theDOB, string thePinCode, string thePassword, Int32 theGender, Int32 theMaritalStatus,
            Int32 theRace, Int32 theHeightFeets, Int32 theHeightInc, string theUserId, Int32 theOperatorId, Int32 ProfileId);
        
        void SaveProfilePics(int theProfileId, int theUserId, DataTable thePicTable);
        void SaveUpdateProfilesPersonalHabits(Int32 Id, string Q1_are_you_a_night_owl_or_a_morning_person, string Q2_are_you_someone_who_enjoys_physical_activity_or_more_of_a_couch_potato, string Q3_do_you_like_to_travel_or_are_you_a_homebody, string Q4_do_you_drink_alcohol, string Q5_do_you_smoke_tobacco_products, string Q6_do_you_use_recreational_drugs, string Q7_is_there_anything_you_watch_regularly_on_television, string Q8_do_you_have_pets, string Q10_do_you_have_strong_political_affiliations, string Q11_would_you_consider_relocating, Int32 UserId, Int32 ProfileId, DataTable MusicTable);
        void SaveUpdateProfilesActivityPreferences(Int32 Id, string Q2_Having_Tea_Coffee, string Q3_Going_To_a_bar_sports_bar, string Q4_Going_To_a_Concert, string Q5_Watching_a_Live_Play, string Q6_Watching_a_musical_live_on_TV, string Q7_Fishing_Hunting, string Q8_Participating_In_Water_Sports_in_an_natural_Body_Of_Water, string Q9_Going_for_a_motorcycle_ride, string Q10_Go_on_a_hike, string Q11_Going_for_a_bike_ride, string Q12_Going_for_a_run, string Q13_Going_for_a_leisurely_walk, string Q14_Going_on_a_road_trip_vacation, string Q15_Going_on_a_picnic, string Q16_Bowling, string Q17_Going_To_a_museum, string Q18_Going_shopping, string Q20_Going_dancing, Int32 UserId, Int32 ProfileId, DataTable MovieTable, DataTable CuisineTable);

        void SaveProfilePhotos(int id, int theProfileId, int theProfileSectionId, string thePicName,string PicDescription, int theUserId);
		string GetUserProfilePhotos(Int32 theUserId);
		string GetUserEmailValidated(string email);
        string GetUserProfile(Int32 theUserId);      
        string GetSearhedProfile(Int32 theUserId);
        string GetChatUser(Int32 theUserId);

        string GetUserProfileQuestions(Int32 theUserId);

        string GetUserSpecificInterests(Int32 theUserId);

        string GetUserProfileCharacterStrength(Int32 theUserId);

        //NeurodiverseStrengths
        string GetUserProfileNeurodiverseStrengths(Int32 theUserId);
        void SaveProfileQuestions(int id, int theProfileId,int theQuestionId, string theResponse, int theUserId);

        void SaveProfileSpecificInterests(int id, int theProfileId, int theInterestId, int theUserId);
        void SaveProfileSpecificInterestsNew(int id, int theProfileId, string theInterestId, string theStrengthId, string theDesiredStrengthId, int theUserId);
        void SaveProfileCharacterStrength(int id, int theProfileId, int theStrengthId, int theUserId);

        void SaveProfileNeurodiverseStrengths(int id, int theProfileId, int theTraitId, int theScore, int theUserId);

        //SaveInterestAndCharacterStrength
        void SaveInterestAndCharacterStrength(int id, int theProfileId, int theStrengthId,int theStrengthTypeID, int theUserId);

        #region Notifications
        string GetUserNotifications(int theUserId, int theActiveChatUserId);
        void UpdateNotificationsRead(int theUserId);
        void UpdateAlertNotifications(int theUserId);
        #endregion
    }
}
