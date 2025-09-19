using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.Profile;
using Application.Common;
using System.Data.SqlClient;

namespace BusinessProcess.Profile
{
    public class BProfile : ProcessBase, IProfile
    {
        public BProfile()
        { }

        public void ProfileRegistration(string theFirstName, string theLastName, string theEmail, string thePhone, string thePassword)
        {
            this.Connection = DataMgr.GetConnection();
            this.Transaction = DataMgr.BeginTransaction(this.Connection);
            clsObject ProfileManager = new clsObject();
            ProfileManager.Connection = this.Connection;
            ProfileManager.Transaction = this.Transaction;
            try
            {
                clsUtility.Init_Hashtable();
                clsUtility.AddParameters("@FirstName", SqlDbType.VarChar, theFirstName);
                clsUtility.AddParameters("@LastName", SqlDbType.VarChar, theLastName);
                clsUtility.AddParameters("@Email", SqlDbType.VarChar, theEmail);
                clsUtility.AddParameters("@Phone", SqlDbType.VarChar, thePhone);
                clsUtility.AddParameters("@Password", SqlDbType.VarChar, thePassword);
                clsUtility.AddParameters("@DBKey", SqlDbType.Int, appAccess.theDBKey);

                Int32 theRows = (Int32)ProfileManager.ReturnObject(clsUtility.theParams, "Pr_Profile_UserRegistration", clsUtility.ObjectEnum.ExecuteNonQuery);
                DataMgr.CommitTransaction(this.Transaction);
            }
            catch (Exception err)
            {
                DataMgr.RollbackTransaction(this.Transaction);
                throw err;
            }

        }


        public void SaveUpdateProfile(int id, string theFirstName, string theLastName, string theNickName, string theEmail, string thePhone,
            string theCity, Int32 theStateId, Int32 theCountryId, string theAddress, DateTime theDOB, string thePinCode, string thePassword, Int32 theGender, Int32 theMaritalStatus,
            Int32 theRace, Int32 theHeightFeets, Int32 theHeightInc, string theUserId, Int32 theOperatorId, Int32 ProfileId)
        {
            this.Connection = DataMgr.GetConnection();
            this.Transaction = DataMgr.BeginTransaction(this.Connection);
            clsObject ProfileManager = new clsObject();
            ProfileManager.Connection = this.Connection;
            ProfileManager.Transaction = this.Transaction;
            try
            {
                clsUtility.Init_Hashtable();
                clsUtility.AddParameters("@Id", SqlDbType.Int, id.ToString());
                clsUtility.AddParameters("@FirstName", SqlDbType.VarChar, theFirstName);
                clsUtility.AddParameters("@LastName", SqlDbType.VarChar, theLastName);
                clsUtility.AddParameters("@NickName", SqlDbType.VarChar, theNickName);
                clsUtility.AddParameters("@Email", SqlDbType.VarChar, theEmail);
                clsUtility.AddParameters("@Phone", SqlDbType.VarChar, thePhone);
                clsUtility.AddParameters("@City", SqlDbType.VarChar, theCity);
                clsUtility.AddParameters("@StateId", SqlDbType.VarChar, theStateId.ToString());
                clsUtility.AddParameters("@CountryId", SqlDbType.Int, theCountryId.ToString());
                clsUtility.AddParameters("@Address", SqlDbType.VarChar, theAddress);
                clsUtility.AddParameters("@DOB", SqlDbType.DateTime, theDOB.ToString("yyyy-MM-dd"));
                clsUtility.AddParameters("@PinCode", SqlDbType.VarChar, thePinCode);
                clsUtility.AddParameters("@GenderId", SqlDbType.VarChar, theGender.ToString());
                clsUtility.AddParameters("@MaritalStatus", SqlDbType.Int, theMaritalStatus.ToString());
                clsUtility.AddParameters("@Race", SqlDbType.Int, theRace.ToString());
                clsUtility.AddParameters("@Height_Feets", SqlDbType.Int, theHeightFeets.ToString());
                clsUtility.AddParameters("@Height_Inc", SqlDbType.Int, theHeightInc.ToString());
                clsUtility.AddParameters("@Password", SqlDbType.VarChar, thePassword);
                clsUtility.AddParameters("@UserId", SqlDbType.VarChar, theUserId);
                clsUtility.AddParameters("@OperatorId", SqlDbType.Int, theOperatorId.ToString());
                clsUtility.AddParameters("@ProfileId", SqlDbType.Int, ProfileId.ToString());
                clsUtility.AddParameters("@DBKey", SqlDbType.Int, appAccess.theDBKey);
                

                Int32 theRows = (Int32)ProfileManager.ReturnObject(clsUtility.theParams, "Pr_Profile_UserRegistration", clsUtility.ObjectEnum.ExecuteNonQuery);
                DataMgr.CommitTransaction(this.Transaction);
            }
            catch (Exception err)
            {
                DataMgr.RollbackTransaction(this.Transaction);
                throw err;
            }
        }

        public void SaveProfilePics(int theProfileId, int theUserId, DataTable thePicTable)
        {
            this.Connection = DataMgr.GetConnection();
            this.Transaction = DataMgr.BeginTransaction(this.Connection);
            clsObject ProfileManager = new clsObject();
            ProfileManager.Connection = this.Connection;
            ProfileManager.Transaction = this.Transaction;
            try
            {
                clsUtility.Init_Hashtable();
                clsUtility.AddParameters("@ProfileId", SqlDbType.Int, theProfileId.ToString());
                clsUtility.AddParameters("@UserId", SqlDbType.Int, theUserId.ToString());
                clsUtility.AddParameters("@DBKey", SqlDbType.VarChar, appAccess.theDBKey);
                clsUtility.AddTableParameters("@ProfilePicTable",  thePicTable);

                Int32 theRows = (Int32)ProfileManager.ReturnObject(clsUtility.theParams, "Pr_Profile_SaveProfilePics", clsUtility.ObjectEnum.ExecuteNonQuery);
                DataMgr.CommitTransaction(this.Transaction);
            }
            catch (Exception err)
            {
                DataMgr.RollbackTransaction(this.Transaction);
                throw err;
            }
        }

        public void SaveUpdateProfilesPersonalHabits(Int32 Id, string Q1_are_you_a_night_owl_or_a_morning_person, string Q2_are_you_someone_who_enjoys_physical_activity_or_more_of_a_couch_potato, string Q3_do_you_like_to_travel_or_are_you_a_homebody, string Q4_do_you_drink_alcohol, string Q5_do_you_smoke_tobacco_products, string Q6_do_you_use_recreational_drugs, string Q7_is_there_anything_you_watch_regularly_on_television, string Q8_do_you_have_pets, string Q10_do_you_have_strong_political_affiliations, string Q11_would_you_consider_relocating, Int32 UserId, Int32 ProfileId, DataTable MusicTable)
        {
            this.Connection = DataMgr.GetConnection();
            this.Transaction = DataMgr.BeginTransaction(this.Connection);
            clsObject ProfileManager = new clsObject();
            ProfileManager.Connection = this.Connection;
            ProfileManager.Transaction = this.Transaction;
            try
            {
                clsUtility.Init_Hashtable();
                clsUtility.AddParameters("@Id", SqlDbType.Int, Id.ToString());
                clsUtility.AddParameters("@Q1_are_you_a_night_owl_or_a_morning_person", SqlDbType.VarChar, Q1_are_you_a_night_owl_or_a_morning_person);
                clsUtility.AddParameters("@Q2_are_you_someone_who_enjoys_physical_activity_or_more_of_a_couch_potato", SqlDbType.VarChar, Q2_are_you_someone_who_enjoys_physical_activity_or_more_of_a_couch_potato);
                clsUtility.AddParameters("@Q3_do_you_like_to_travel_or_are_you_a_homebody", SqlDbType.VarChar, Q3_do_you_like_to_travel_or_are_you_a_homebody);
                clsUtility.AddParameters("@Q4_do_you_drink_alcohol", SqlDbType.VarChar, Q4_do_you_drink_alcohol);
                clsUtility.AddParameters("@Q5_do_you_smoke_tobacco_products", SqlDbType.VarChar, Q5_do_you_smoke_tobacco_products);
                clsUtility.AddParameters("@Q6_do_you_use_recreational_drugs", SqlDbType.VarChar, Q6_do_you_use_recreational_drugs);
                clsUtility.AddParameters("@Q7_is_there_anything_you_watch_regularly_on_television", SqlDbType.VarChar, Q7_is_there_anything_you_watch_regularly_on_television);
                clsUtility.AddParameters("@Q8_do_you_have_pets", SqlDbType.Int, Q8_do_you_have_pets);
                clsUtility.AddParameters("@Q10_do_you_have_strong_political_affiliations", SqlDbType.VarChar, Q10_do_you_have_strong_political_affiliations);
                clsUtility.AddParameters("@Q11_would_you_consider_relocating", SqlDbType.Int, Q11_would_you_consider_relocating);
                clsUtility.AddParameters("@UserId", SqlDbType.VarChar, UserId.ToString());
                clsUtility.AddParameters("@ProfileId", SqlDbType.Int, ProfileId.ToString());
                clsUtility.AddTableParameters("@MusicTable", MusicTable);

                Int32 theRows = (Int32)ProfileManager.ReturnObject(clsUtility.theParams, "Pr_Profile_SaveUpdatePersonalHabits", clsUtility.ObjectEnum.ExecuteNonQuery);
                DataMgr.CommitTransaction(this.Transaction);
            }
            catch (Exception err)
            {
                DataMgr.RollbackTransaction(this.Transaction);
                throw err;
            }
        }


        public void SaveUpdateProfilesActivityPreferences(Int32 Id, string Q2_Having_Tea_Coffee, string Q3_Going_To_a_bar_sports_bar, string Q4_Going_To_a_Concert, string Q5_Watching_a_Live_Play, string Q6_Watching_a_musical_live_on_TV, string Q7_Fishing_Hunting, string Q8_Participating_In_Water_Sports_in_an_natural_Body_Of_Water, string Q9_Going_for_a_motorcycle_ride, string Q10_Go_on_a_hike, string Q11_Going_for_a_bike_ride, string Q12_Going_for_a_run, string Q13_Going_for_a_leisurely_walk, string Q14_Going_on_a_road_trip_vacation, string Q15_Going_on_a_picnic, string Q16_Bowling, string Q17_Going_To_a_museum, string Q18_Going_shopping, string Q20_Going_dancing, Int32 UserId, Int32 ProfileId, DataTable MovieTable, DataTable CuisineTable)
        {
            this.Connection = DataMgr.GetConnection();
            this.Transaction = DataMgr.BeginTransaction(this.Connection);
            clsObject ProfileManager = new clsObject();
            ProfileManager.Connection = this.Connection;
            ProfileManager.Transaction = this.Transaction;
            try
            {
                clsUtility.Init_Hashtable();
                clsUtility.AddParameters("@Id", SqlDbType.Int, Id.ToString());
                clsUtility.AddParameters("@Q2_Having_Tea_Coffee", SqlDbType.VarChar, Q2_Having_Tea_Coffee);
                clsUtility.AddParameters("@Q3_Going_To_a_bar_sports_bar", SqlDbType.VarChar, Q3_Going_To_a_bar_sports_bar);
                clsUtility.AddParameters("@Q4_Going_To_a_Concert", SqlDbType.VarChar, Q4_Going_To_a_Concert);
                clsUtility.AddParameters("@Q5_Watching_a_Live_Play", SqlDbType.VarChar, Q5_Watching_a_Live_Play);
                clsUtility.AddParameters("@Q6_Watching_a_musical_live_on_TV", SqlDbType.VarChar, Q6_Watching_a_musical_live_on_TV);
                clsUtility.AddParameters("@Q7_Fishing_Hunting", SqlDbType.VarChar, Q7_Fishing_Hunting);
                clsUtility.AddParameters("@Q8_Participating_In_Water_Sports_in_an_natural_Body_Of_Water", SqlDbType.VarChar, Q8_Participating_In_Water_Sports_in_an_natural_Body_Of_Water);
                clsUtility.AddParameters("@Q9_Going_for_a_motorcycle_ride", SqlDbType.Int, Q9_Going_for_a_motorcycle_ride);
                clsUtility.AddParameters("@Q10_Go_on_a_hike", SqlDbType.VarChar, Q10_Go_on_a_hike);
                clsUtility.AddParameters("@Q11_Going_for_a_bike_ride", SqlDbType.Int, Q11_Going_for_a_bike_ride);
                clsUtility.AddParameters("@Q12_Going_for_a_run", SqlDbType.Int, Q12_Going_for_a_run);
                clsUtility.AddParameters("@Q13_Going_for_a_leisurely_walk", SqlDbType.Int, Q13_Going_for_a_leisurely_walk);
                clsUtility.AddParameters("@Q14_Going_on_a_road_trip_vacation", SqlDbType.Int, Q14_Going_on_a_road_trip_vacation);
                clsUtility.AddParameters("@Q15_Going_on_a_picnic", SqlDbType.Int, Q15_Going_on_a_picnic);
                clsUtility.AddParameters("@Q16_Bowling", SqlDbType.Int, Q16_Bowling);
                clsUtility.AddParameters("@Q17_Going_To_a_museum", SqlDbType.Int, Q17_Going_To_a_museum);
                clsUtility.AddParameters("@Q18_Going_shopping", SqlDbType.Int, Q18_Going_shopping);
                clsUtility.AddParameters("@Q20_Going_dancing", SqlDbType.Int, Q20_Going_dancing);
                clsUtility.AddParameters("@UserId", SqlDbType.VarChar, UserId.ToString());
                clsUtility.AddParameters("@ProfileId", SqlDbType.Int, ProfileId.ToString());
                clsUtility.AddTableParameters("@MovieTable", MovieTable);
                clsUtility.AddTableParameters("@CuisineTable", CuisineTable);

                Int32 theRows = (Int32)ProfileManager.ReturnObject(clsUtility.theParams, "Pr_Profile_SaveUpdateActivityPreferences", clsUtility.ObjectEnum.ExecuteNonQuery);
                DataMgr.CommitTransaction(this.Transaction);
            }
            catch (Exception err)
            {
                DataMgr.RollbackTransaction(this.Transaction);
                throw err;
            }
        }




        public void SaveProfilePhotos(int id, int theProfileId, int theProfileSectionId, string thePicName, string PicDescription, int theUserId)
        {
            this.Connection = DataMgr.GetConnection();
            this.Transaction = DataMgr.BeginTransaction(this.Connection);
            clsObject ProfileManager = new clsObject();
            ProfileManager.Connection = this.Connection;
            ProfileManager.Transaction = this.Transaction;
            //DataMgr.BeginTransaction(this.Connection);
            try
            {
                clsUtility.Init_Hashtable();
                clsUtility.AddParameters("@Id", SqlDbType.Int, id.ToString());
                clsUtility.AddParameters("@ProfileId", SqlDbType.Int, theProfileId.ToString());
                clsUtility.AddParameters("@ProfileSectionId", SqlDbType.Int, theProfileSectionId.ToString());
                clsUtility.AddParameters("@PicName", SqlDbType.VarChar, thePicName);
                clsUtility.AddParameters("@PicDescription", SqlDbType.VarChar, PicDescription);
                clsUtility.AddParameters("@UserId", SqlDbType.Int, theUserId.ToString());
                clsUtility.AddParameters("@DBKey", SqlDbType.VarChar, appAccess.theDBKey);
                clsObject theUserProfilePhotos = new clsObject();
                Int32 theRows = (Int32)theUserProfilePhotos.ReturnObject(clsUtility.theParams, "Pr_Profile_ProfilePhotos", clsUtility.ObjectEnum.ExecuteNonQuery);
                // DataMgr.CommitTransaction(this.Transaction);
            }
            catch (Exception err)
            {
                //DataMgr.RollbackTransaction(this.Transaction);
                throw err;
            }
        }


        public string GetUserProfilePhotos(Int32 theUserId)
        {
            clsObject theOrganizationManager = new clsObject();
            clsUtility.Init_Hashtable();
            clsUtility.AddParameters("@UserID", SqlDbType.Int, theUserId.ToString());
            DataSet theDS = (DataSet)theOrganizationManager.ReturnObject(clsUtility.theParams, "Pr_Profile_GetUserProfilePhotos", clsUtility.ObjectEnum.DataSet);
            return theDS.GetXml();
        }
        public string GetUserEmailValidated(string email = "")
        {
            clsObject theOrganizationManager = new clsObject();
            clsUtility.Init_Hashtable();
            clsUtility.AddParameters("@Email", SqlDbType.NVarChar, email.ToString());
            DataSet theDS = (DataSet)theOrganizationManager.ReturnObject(clsUtility.theParams, "[Pr_Profile_GetUserEmailValidated]", clsUtility.ObjectEnum.DataSet);
            return theDS.GetXml();
        }


        public string GetUserProfile(Int32 theUserId)
        {
            clsObject theOrganizationManager = new clsObject();
            clsUtility.Init_Hashtable();
            clsUtility.AddParameters("@UserID", SqlDbType.Int, theUserId.ToString());
            clsUtility.AddParameters("@DBKey", SqlDbType.VarChar, appAccess.theDBKey);
            DataSet theDS = (DataSet)theOrganizationManager.ReturnObject(clsUtility.theParams, "Pr_Profile_GetUserProfile", clsUtility.ObjectEnum.DataSet);
            return theDS.GetXml();
        }

        public string GetSearhedProfile(Int32 theUserId)
        {
            clsObject theOrganizationManager = new clsObject();
            clsUtility.Init_Hashtable();
            clsUtility.AddParameters("@UserID", SqlDbType.Int, theUserId.ToString());
            clsUtility.AddParameters("@DBKey", SqlDbType.VarChar, appAccess.theDBKey);
            DataSet theDS = (DataSet)theOrganizationManager.ReturnObject(clsUtility.theParams, "Pr_Profile_GetUserSearchProfile", clsUtility.ObjectEnum.DataSet);
            return theDS.GetXml();
        }
        public string GetChatUser(Int32 theUserId)
        {
            clsObject theOrganizationManager = new clsObject();
            clsUtility.Init_Hashtable();
            clsUtility.AddParameters("@UserID", SqlDbType.Int, theUserId.ToString());
            clsUtility.AddParameters("@DBKey", SqlDbType.VarChar, appAccess.theDBKey);
            DataSet theDS = (DataSet)theOrganizationManager.ReturnObject(clsUtility.theParams, "Pr_Profile_GetChatUser", clsUtility.ObjectEnum.DataSet);
            return theDS.GetXml();
        }

        public string GetUserProfileQuestions(Int32 theUserId)
        {
            clsObject theOrganizationManager = new clsObject();
            clsUtility.Init_Hashtable();
            clsUtility.AddParameters("@UserID", SqlDbType.Int, theUserId.ToString());
            clsUtility.AddParameters("@DBKey", SqlDbType.VarChar, appAccess.theDBKey);
            DataSet theDS = (DataSet)theOrganizationManager.ReturnObject(clsUtility.theParams, "Pr_Profile_GetUserProfileQuestions", clsUtility.ObjectEnum.DataSet);
            return theDS.GetXml();
        }
        public string GetUserSpecificInterests(Int32 theUserId)
        {
            clsObject theOrganizationManager = new clsObject();
            clsUtility.Init_Hashtable();
            clsUtility.AddParameters("@UserID", SqlDbType.Int, theUserId.ToString());
            clsUtility.AddParameters("@DBKey", SqlDbType.VarChar, appAccess.theDBKey);
            DataSet theDS = (DataSet)theOrganizationManager.ReturnObject(clsUtility.theParams, "Pr_Profile_GetUserSpecificInterests", clsUtility.ObjectEnum.DataSet);
            return theDS.GetXml();
        }

        public string GetUserProfileCharacterStrength(Int32 theUserId)
        {
            clsObject theOrganizationManager = new clsObject();
            clsUtility.Init_Hashtable();
            clsUtility.AddParameters("@UserID", SqlDbType.Int, theUserId.ToString());
            clsUtility.AddParameters("@DBKey", SqlDbType.VarChar, appAccess.theDBKey);
            DataSet theDS = (DataSet)theOrganizationManager.ReturnObject(clsUtility.theParams, "Pr_Profile_GetUserProfileCharacterStrength", clsUtility.ObjectEnum.DataSet);
            return theDS.GetXml();
        }

        public string GetUserProfileNeurodiverseStrengths(Int32 theUserId)
        {
            clsObject theOrganizationManager = new clsObject();
            clsUtility.Init_Hashtable();
            clsUtility.AddParameters("@UserID", SqlDbType.Int, theUserId.ToString());
            clsUtility.AddParameters("@DBKey", SqlDbType.VarChar, appAccess.theDBKey);
            DataSet theDS = (DataSet)theOrganizationManager.ReturnObject(clsUtility.theParams, "Pr_Profile_GetUserProfileNeurodiverseTraits", clsUtility.ObjectEnum.DataSet);
            return theDS.GetXml();
        }
        public void SaveProfileQuestions(int id, int theProfileId, int theQuestionId, string theResponse, int theUserId)
        {
            this.Connection = DataMgr.GetConnection();
            this.Transaction = DataMgr.BeginTransaction(this.Connection);
            clsObject ProfileManager = new clsObject();
            ProfileManager.Connection = this.Connection;
            ProfileManager.Transaction = this.Transaction;
            try
            {
                clsUtility.Init_Hashtable();
                clsUtility.AddParameters("@Id", SqlDbType.Int, id.ToString());
                clsUtility.AddParameters("@ProfileId", SqlDbType.Int, theProfileId.ToString());
                clsUtility.AddParameters("@QuestionId", SqlDbType.Int, theQuestionId.ToString());
                clsUtility.AddParameters("@Response", SqlDbType.VarChar, theResponse);
                clsUtility.AddParameters("@UserId", SqlDbType.Int, theUserId.ToString());
                clsUtility.AddParameters("@DBKey", SqlDbType.VarChar, appAccess.theDBKey);
                clsObject theUserProfilePhotos = new clsObject();
                Int32 theRows = (Int32)theUserProfilePhotos.ReturnObject(clsUtility.theParams, "Pr_Profile_ProfileQuestions", clsUtility.ObjectEnum.ExecuteNonQuery);
                DataMgr.CommitTransaction(this.Transaction);
            }
            catch (Exception err)
            {
                DataMgr.RollbackTransaction(this.Transaction);
                throw err;
            }
        }

        public void SaveProfileSpecificInterests(int id, int theProfileId, int theInterestId, int theUserId)
        {
            this.Connection = DataMgr.GetConnection();
            this.Transaction = DataMgr.BeginTransaction(this.Connection);
            clsObject ProfileManager = new clsObject();
            ProfileManager.Connection = this.Connection;
            ProfileManager.Transaction = this.Transaction;
            try
            {
                clsUtility.Init_Hashtable();
                clsUtility.AddParameters("@Id", SqlDbType.Int, id.ToString());
                clsUtility.AddParameters("@ProfileId", SqlDbType.Int, theProfileId.ToString());
                clsUtility.AddParameters("@InterestId", SqlDbType.Int, theInterestId.ToString());
                clsUtility.AddParameters("@Deleted", SqlDbType.Int, "0");
                clsUtility.AddParameters("@UserId", SqlDbType.Int, theUserId.ToString());
                clsUtility.AddParameters("@DBKey", SqlDbType.VarChar, appAccess.theDBKey);
                clsObject theUserProfilePhotos = new clsObject();
                Int32 theRows = (Int32)theUserProfilePhotos.ReturnObject(clsUtility.theParams, "Pr_Profile_Lnk_ProfileInterests", clsUtility.ObjectEnum.ExecuteNonQuery);
                DataMgr.CommitTransaction(this.Transaction);
            }
            catch (Exception err)
            {
                DataMgr.RollbackTransaction(this.Transaction);
                throw err;
            }
        }

        public void SaveProfileSpecificInterestsNew(int id, int theProfileId, string theInterestId, string theStrengthId, string theDesiredStrengthId, int theUserId)
        {
            this.Connection = DataMgr.GetConnection();
            this.Transaction = DataMgr.BeginTransaction(this.Connection);
            clsObject ProfileManager = new clsObject();
            ProfileManager.Connection = this.Connection;
            ProfileManager.Transaction = this.Transaction;
            try
            {
                clsUtility.Init_Hashtable();
                clsUtility.AddParameters("@Id", SqlDbType.Int, id.ToString());
                clsUtility.AddParameters("@ProfileId", SqlDbType.Int, theProfileId.ToString());
                clsUtility.AddParameters("@InterestId", SqlDbType.VarChar, theInterestId.ToString());
                clsUtility.AddParameters("@StrengthId", SqlDbType.VarChar, theStrengthId.ToString());
                clsUtility.AddParameters("@DesiredStrengthId", SqlDbType.VarChar, theDesiredStrengthId.ToString());
                clsUtility.AddParameters("@UserId", SqlDbType.Int, theUserId.ToString());
                clsUtility.AddParameters("@DBKey", SqlDbType.VarChar, appAccess.theDBKey);
                clsObject theUserProfilePhotos = new clsObject();
                Int32 theRows = (Int32)theUserProfilePhotos.ReturnObject(clsUtility.theParams, "Pr_Profile_Lnk_ProfileInterestsNew", clsUtility.ObjectEnum.ExecuteNonQuery);
                DataMgr.CommitTransaction(this.Transaction);
            }
            catch (Exception err)
            {
                DataMgr.RollbackTransaction(this.Transaction);
                throw err;
            }
        }
        public void SaveProfileNeurodiverseStrengths(int id, int theProfileId, int theTraitId, int theScore, int theUserId)
        {
            this.Connection = DataMgr.GetConnection();
            this.Transaction = DataMgr.BeginTransaction(this.Connection);
            clsObject ProfileManager = new clsObject();
            ProfileManager.Connection = this.Connection;
            ProfileManager.Transaction = this.Transaction;
            try
            {
                clsUtility.Init_Hashtable();
                clsUtility.AddParameters("@Id", SqlDbType.Int, id.ToString());
                clsUtility.AddParameters("@ProfileId", SqlDbType.Int, theProfileId.ToString());
                clsUtility.AddParameters("@TraitId", SqlDbType.Int, theTraitId.ToString());
                clsUtility.AddParameters("@Score", SqlDbType.Int, theScore.ToString());
                clsUtility.AddParameters("@UserId", SqlDbType.Int, theUserId.ToString());
                clsUtility.AddParameters("@DBKey", SqlDbType.VarChar, appAccess.theDBKey);
                clsObject theUserProfilePhotos = new clsObject();
                Int32 theRows = (Int32)theUserProfilePhotos.ReturnObject(clsUtility.theParams, "Pr_Profile_ProfileTraits", clsUtility.ObjectEnum.ExecuteNonQuery);
                DataMgr.CommitTransaction(this.Transaction);
            }
            catch (Exception err)
            {
                DataMgr.RollbackTransaction(this.Transaction);
                throw err;
            }
        }

        public void SaveProfileCharacterStrength(int id, int theProfileId, int theStrengthId, int theUserId)
        {
            this.Connection = DataMgr.GetConnection();
            this.Transaction = DataMgr.BeginTransaction(this.Connection);
            clsObject ProfileManager = new clsObject();
            ProfileManager.Connection = this.Connection;
            ProfileManager.Transaction = this.Transaction;
            try
            {
                clsUtility.Init_Hashtable();
                clsUtility.AddParameters("@Id", SqlDbType.Int, id.ToString());
                clsUtility.AddParameters("@ProfileId", SqlDbType.Int, theProfileId.ToString());
                clsUtility.AddParameters("@StrengthId", SqlDbType.Int, theStrengthId.ToString());

                clsUtility.AddParameters("@UserId", SqlDbType.Int, theUserId.ToString());
                clsUtility.AddParameters("@DBKey", SqlDbType.VarChar, appAccess.theDBKey);
                clsObject theUserProfilePhotos = new clsObject();
                Int32 theRows = (Int32)theUserProfilePhotos.ReturnObject(clsUtility.theParams, "Pr_Profile_Lnk_ProfileStrengths", clsUtility.ObjectEnum.ExecuteNonQuery);
                DataMgr.CommitTransaction(this.Transaction);
            }
            catch (Exception err)
            {
                DataMgr.RollbackTransaction(this.Transaction);
                throw err;
            }
        }
        public void SaveInterestAndCharacterStrength(int id, int theProfileId, int theStrengthId, int theStrengthTypeID, int theUserId)
        {
            this.Connection = DataMgr.GetConnection();
            this.Transaction = DataMgr.BeginTransaction(this.Connection);
            clsObject ProfileManager = new clsObject();
            ProfileManager.Connection = this.Connection;
            ProfileManager.Transaction = this.Transaction;
            try
            {
                clsUtility.Init_Hashtable();
                clsUtility.AddParameters("@Id", SqlDbType.Int, id.ToString());
                clsUtility.AddParameters("@ProfileId", SqlDbType.Int, theProfileId.ToString());
                clsUtility.AddParameters("@StrengthId", SqlDbType.Int, theStrengthId.ToString());
                clsUtility.AddParameters("@StrengthTypeId", SqlDbType.Int, theStrengthTypeID.ToString());
                clsUtility.AddParameters("@UserId", SqlDbType.Int, theUserId.ToString());
                clsUtility.AddParameters("@DBKey", SqlDbType.VarChar, appAccess.theDBKey);
                clsObject theUserProfilePhotos = new clsObject();
                Int32 theRows = (Int32)theUserProfilePhotos.ReturnObject(clsUtility.theParams, "Pr_Profile_Lnk_ProfileStrengths", clsUtility.ObjectEnum.ExecuteNonQuery);
                DataMgr.CommitTransaction(this.Transaction);
            }
            catch (Exception err)
            {
                DataMgr.RollbackTransaction(this.Transaction);
                throw err;
            }
        }

        #region Notifications
        public string GetUserNotifications(int theUserId, int theActiveChatUserId)
        {
            clsObject theOrganizationManager = new clsObject();
            var perameters = new[]
                  {new SqlParameter("@UserID", theUserId.ToString()),
                  new SqlParameter("@ActiveChatUserId", theActiveChatUserId.ToString())};
            //clsUtility.Init_Hashtable();
            //clsUtility.AddParameters("@UserID", SqlDbType.Int, theUserId.ToString());
            //clsUtility.AddParameters("@ActiveChatUserId", SqlDbType.Int, theActiveChatUserId.ToString());
            DataSet theDS = (DataSet)theOrganizationManager.ReturnObject(perameters, "Pr_GetNotifications", clsUtility.ObjectEnum.DataSet);
            return theDS.GetXml();
        }
        public void UpdateNotificationsRead(int theUserId)
        {
            clsObject theOrganizationManager = new clsObject();
            var param = new List<SqlParameter>().ToArray();
            var query = $"Update UserFriendRequest Set IsNotificationRead=1 Where RequestedUserId={theUserId} AND IsNotificationRead=0; ";
            query += $"Update Messages Set IsNotificationRead=1 Where SendToUserID={theUserId} AND IsNotificationRead=0";
            theOrganizationManager.ReturnObject(param, query, clsUtility.ObjectEnum.ExecuteNonQuery);
        }
        public void UpdateAlertNotifications(int theUserId)
        {
            clsObject theOrganizationManager = new clsObject();
            var param = new List<SqlParameter>().ToArray();
            var query = $"Update UserFriendRequest Set IsAlertSent=1 Where RequestedUserId={theUserId} AND IsAlertSent=0; ";
            query += $"Update Messages Set IsAlertSent=1 Where SendToUserID={theUserId} AND IsAlertSent=0";
            theOrganizationManager.ReturnObject(param, query, clsUtility.ObjectEnum.ExecuteNonQuery);
        }
        #endregion

    }
}