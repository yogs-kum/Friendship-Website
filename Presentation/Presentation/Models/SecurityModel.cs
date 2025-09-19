using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Models
{
    public class SecurityModel
    {
    }
    public class LoginModel
    {
        [Required]
        [Display(Name = "User Id*")]
        public string theUserId
        { get; set; }

        [Required]
        [Display(Name = "Password*")]
        [DataType(DataType.Password)]
        public string thePassword
        { get; set; }

        //[Required]
        //[Display(Name = "Token")]
        //public string theToken
        //{ get; set; }
    }

    public class ChangePasswordModels
    {
        [Required]
        [Display(Name = "Old Password")]
        [DataType(DataType.Password)]
        public string theOldPassword
        { get; set; }

        [Required]
        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        public string theNewPassword
        { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string theConfirmPassword
        { get; set; }
    }
}