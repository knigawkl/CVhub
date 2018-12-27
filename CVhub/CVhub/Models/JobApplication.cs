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
        public int OfferId { get; set; }

        [Required(ErrorMessage = "First Name required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Phone number required")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email required")]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
        public bool ContactAgreement { get; set; }
        public string CvUrl { get; set; }
    }
}
