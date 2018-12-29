using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CVhub.Models
{
    public class JobApplication
    {
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int OfferId { get; set; }

        [Required(ErrorMessage = "First Name required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Phone number required")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email required")]
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Invalid email address!")]
        public string EmailAddress { get; set; }
        //public bool ContactAgreement { get; set; }
        //public string CvUrl { get; set; }
        public string CoverLetter { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Date of birth required")]
        [Display(Name = "Date of birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyy-MM-dd}")]
        public DateTime? DateOfBirth { get; set; }
    }
}
