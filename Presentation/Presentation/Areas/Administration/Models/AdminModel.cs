using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Presentation.Areas.Administration.Models
{
	public class AdminModel
	{
	}

	public class OrganizationModel
	{
		[Required]
		[Display(Name = "Code")]
		[StringLength(maximumLength: 10)]
		public string Code { get; set; }

		[Required]
		[Display(Name = "Organization Name")]
		[StringLength(maximumLength: 200)]
		public string Name { get; set; }

		[Display(Name = "Short Name")]
		[StringLength(maximumLength: 100)]
		public string ShortName { get; set; }

		[Required]
		[Display(Name = "Organization Token")]
		[StringLength(maximumLength: 10)]
		public string Token { get; set; }

		[Required]
		[Display(Name = "Address")]
		[StringLength(maximumLength: 150)]
		public string Address1 { get; set; }

		[StringLength(maximumLength: 150)]
		public string Address2 { get; set; }

		[Required]
		[Display(Name = "City")]
		[StringLength(maximumLength: 100)]
		public string City { get; set; }

		[Required]
		[Display(Name = "State")]
		public int StateId { get; set; }

		[Required]
		[Display(Name = "Country")]
		public int CountryId { get; set; }

		[Required]
		[Display(Name = "Pin")]
		[StringLength(maximumLength: 10)]
		public string Pin { get; set; }

		[Required]
		[Display(Name = "Phone")]
		[StringLength(maximumLength: 50)]
		public string Phone { get; set; }

		[Required]
		[Display(Name = "Email")]
		[StringLength(maximumLength: 50)]
		public string Email { get; set; }

		[Display(Name = "Website")]
		[StringLength(maximumLength: 50)]
		public string Website { get; set; }

		[Display(Name = "Logo")]
		[StringLength(maximumLength: 100)]
		public string LogoFile { get; set; }

		[Required]
		[Display(Name = "Status")]
		public int Deleted { get; set; }

		public int Id { get; set; }
		public string OrganizationName { get; set; }
		public string StateName { get; set; }
		public string CountryName { get; set; }
		public string ContactFirstName { get; set; }
		public string ContactLastName { get; set; }
	}

	public class LocationModel
    {
		public int Id { get; set; }

		[Required]
		[Display(Name = "Code")]
		[StringLength(maximumLength: 10)]
		public string Code { get; set; }

		[Required]
		[Display(Name = "Location Name")]
		[StringLength(maximumLength: 200)]
		public string Name { get; set; }

		public int OrganizationId { get; set; }

		[Required]
		[Display(Name = "Address")]
		[StringLength(maximumLength: 150)]
		public string Address1 { get; set; }

		[Display(Name = "Address")]
		[StringLength(maximumLength: 150)]
		public string Address2 { get; set; }

		[Required]
		[Display(Name = "City")]
		[StringLength(maximumLength: 100)]
		public string City { get; set; }

		[Required]
		[Display(Name = "State")]
		public int StateId { get; set; }

		[Required]
		[Display(Name = "Country")]
		public int CountryId { get; set; }

		[Required]
		[Display(Name = "Pin")]
		[StringLength(maximumLength: 10)]
		public string Pin { get; set; }

		[Required]
		[Display(Name = "Phone")]
		[StringLength(maximumLength: 50)]
		public string Phone { get; set; }

		[Required]
		[Display(Name = "Email")]
		[StringLength(maximumLength: 50)]
		public string Email { get; set; }

		[Required]
		[Display(Name = "Status")]
		public int Deleted { get; set; }

		public string OrganizationName { get; set; }
	}

}