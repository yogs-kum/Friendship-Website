using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Areas.Security.Models
{
    public class SecurityModel
    {

    }

    public class GroupModel
    {
        [Required]
        [Display(Name = "Role Name")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Role Status")]
        public int Deleted { get; set; }

        public int Id { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }
    }

    public class UserModel
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string UserFirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string UserLastName { get; set; }

        [Required]
        [Display(Name = "User Id")]
        public string UserId { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string EmailId { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }

        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }

        public string UserName { get; set; }

        [Display(Name = "Status")]
        public int Deleted { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }
    }
}