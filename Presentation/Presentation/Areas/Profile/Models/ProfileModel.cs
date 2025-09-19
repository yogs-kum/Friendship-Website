//using Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Presentation.Areas.Profile.Models
{
    public class Profile
    {
        public string Id { get; set; }
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please enter first name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please enter last name")]
        public string LastName { get; set; }

        [Display(Name = "Date Of Birth")]
        [Required(ErrorMessage = "Please select DOB")]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage ="Please select Gender")]
        public int? Gender { get; set; }

        [Display(Name = "Zip Code")]
        public int PinCode { get; set; }

        public string City { get; set; }

        [Display(Name = "State")]
        public int StateId { get; set; }

        [Display(Name = "Country")]
        public int CountryId { get; set; }

        [Display(Name = "Email")]
        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        [Display(Name = "Phone No.(i.e. cell)")]
        [Required]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string Phone { get; set; }
        public string UserId { get; set; }

        public int? Deleted { get; set; }

        public int? OperatorId { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        [Display(Name = "StateName")]
        public string StateName { get; set; }

        [Display(Name = "CountryName")]
        public string CountryName { get; set; }
        [Display(Name = "Nick Name")]
        public string NickName { get; set; }
        public int? IsFreind { get; set; }
        public int? MaritalStatus { get; set; } 
        public int? Race { get; set; }
        public int? Height_Feets { get; set; }
        public int? Height_Inc { get; set; }
        public string Address { get; set; }
    }

    public class LifeInPics
    {
        public HttpPostedFileBase[] LifePic1 { get; set; }
        public HttpPostedFileBase[] LifePic2 { get; set; }
        public HttpPostedFileBase[] LifePic3 { get; set; }
        public HttpPostedFileBase[] LifePic4 { get; set; }
        public HttpPostedFileBase[] LifePic5 { get; set; }
        public HttpPostedFileBase[] LifePic6 { get; set; }

    }

    public class PersonalHabits
    {
        public int Id { get; set; }
        public string ProfileId { get; set; }        
        public string Q1_are_you_a_night_owl_or_a_morning_person { get; set; }
        public string Q2_are_you_someone_who_enjoys_physical_activity_or_more_of_a_couch_potato { get; set; }
        public string Q3_do_you_like_to_travel_or_are_you_a_homebody { get; set; }
        public string Q4_do_you_drink_alcohol { get; set; }
        public string Q5_do_you_smoke_tobacco_products { get; set; }
        public string Q6_do_you_use_recreational_drugs { get; set; }
        public string Q7_is_there_anything_you_watch_regularly_on_television { get; set; }
        public string Q8_do_you_have_pets { get; set; }
        public List<PersonalHabits_Q9_what_kind_of_music_do_you_like> PersonalHabits_Q9_what_kind_of_music_do_you_like { get; set; }

        [Required]
        [Display(Name = "Choose Multiple Music")]
        public List<int> SelectedMultiMusicId { get; set; }

        public List<PersonalHabits_Q9_what_kind_of_music_do_you_like> SelectedMusicLst { get; set; }

        public string Q10_do_you_have_strong_political_affiliations { get; set; }
        public string Q11_would_you_consider_relocating { get; set; }

        public int? UserId { get; set; }

        public int? Deleted { get; set; }
    }
    public class PersonalHabits_Q9_what_kind_of_music_do_you_like
    {
        public int Id { get; set; }
        public string ProfileId { get; set; }
        public string MusicId { get; set; }
        public int? UserId { get; set; }
        public int? Deleted { get; set; }
    }

    public class ActivityPreferences
    {
        public int Id { get; set; }

        public string ProfileId { get; set; }

        public string Q2_Having_Tea_Coffee { get; set; }

        public string Q3_Going_To_a_bar_sports_bar { get; set; }

        public string Q4_Going_To_a_Concert { get; set; }
        public string Q5_Watching_a_Live_Play { get; set; }

        public string Q6_Watching_a_musical_live_on_TV { get; set; }

        public string Q7_Fishing_Hunting { get; set; }

        public string Q8_Participating_In_Water_Sports_in_an_natural_Body_Of_Water { get; set; }

        public string Q9_Going_for_a_motorcycle_ride { get; set; }
        public string Q10_Go_on_a_hike { get; set; }

       
        public string Q11_Going_for_a_bike_ride { get; set; }
        public string Q12_Going_for_a_run { get; set; }
        public string Q13_Going_for_a_leisurely_walk { get; set; }
        public string Q14_Going_on_a_road_trip_vacation { get; set; }
        public string Q15_Going_on_a_picnic { get; set; }
        public string Q16_Bowling { get; set; }
        public string Q17_Going_To_a_museum { get; set; }

        public string Q18_Going_shopping { get; set; }
        public string Q20_Going_dancing { get; set; }

        public List<ActivityPreferences_Q1_watching_a_movie> ActivityPreferences_Q1_watching_a_movie { get; set; }

        public List<ActivityPreferences_Q19_going_out_to_dinner> ActivityPreferences_Q19_going_out_to_dinner { get; set; }

        [Required]
        [Display(Name = "Choose Multiple Movie")]
        public List<int> SelectedMultiMovieId { get; set; }

        [Required]
        [Display(Name = "Choose Multiple Cuisine")]
        public List<int> SelectedMultiCuisineId { get; set; }

    }    

    public class ActivityPreferences_Q1_watching_a_movie
    {
        public int Id { get; set; }
        public string ProfileId { get; set; }
        public string MovieId { get; set; }
        public int? UserId { get; set; }
        public int? Deleted { get; set; }
    }

    public class ActivityPreferences_Q19_going_out_to_dinner
    {
        public int Id { get; set; }
        public string ProfileId { get; set; }
        public string FoodId { get; set; }
        public int? UserId { get; set; }
        public int? Deleted { get; set; }
    }


    public class NDUserProfile
    {
        public Profile theProfile { get; set; }
        public LifeInPics theLifePics { get; set; }

        public PersonalHabits thePersonalHabits { get; set; }

        public ActivityPreferences theActivityPreferences { get; set; }
    }

    public class ProfilePhotos
    {
        public int Id { get; set; }
        public int ProfileSectionId { get; set; }
        public string PicName { get; set; }
        public string PicDescription { get; set; }
        //public HttpPostedFileBase[] theProfilePic { get; set; }
    }


    public class LnkProfileData
    {
        public string Name { get; set; }

        public string ProfileID { get; set; }

        public string ProfileSectionId { get; set; }

        public string UserID { get; set; }

        public string SectionImage { get; set; }

        public string Id { get; set; }

        public string PicName { get; set; }

        public string LinkID { get; set; }

        public string PicDescription { get; set; }
        public string SectionID { get; set; }
    }

    public class Mst_ProfileQuestions
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Response { get; set; }
        public string UserId { get; set; }
        public int ProfileID { get; set; }
    }

    public class Mst_ProfileInterests
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int ProfileID { get; set; }

        public int InterestId { get; set; }
    }

    public class Mst_Strengths
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int ProfileID { get; set; }

        public int StrengthTypeID { get; set; }
        public int StrengthId { get; set; }
    }

    public class Mst_ProfileTraits
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public string UserId { get; set; }
        public int ProfileID { get; set; }
        public int TraitId { get; set; }

    }

    //public class LnkProfilePhotos
    //{
    //    public List<LnkProfileData> LinkPhotos { get; set; }
    //    public ProfilePhotos profilePhoto { get; set; }
    //}

    public class FriendModel
    {
        public int Id { get; set; }
        public int CurrentUserId { get; set; }
        public int FriendUserId { get; set; }
        public int FriendProfileId { get; set; }
        public int Status { get; set; }
        public string FriendFirstName { get; set; }
        public string FriendLastName { get; set; }
        public string FriendEmail { get; set; }
        public string FriendProfilePic { get; set; }
        public string FullName { get { return $"{FriendFirstName.Trim()} {FriendLastName.Trim()}".Trim(); } }
    }
}