using CVhub.CustomDataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVhub.Models
{
    public class JobOffer
    {
        public int Id { get; set; }

        [Display(Name = "Job title")]
        [Required(ErrorMessage = "Job title required")]
        public string JobTitle { get; set; }

        public virtual Company Company { get; set; }

        [Required(ErrorMessage = "Company name required")]
        public virtual string CompanyName { get; set; }

        public virtual int CompanyId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Input correct min salary")]
        [Required(ErrorMessage = "Min salary required")]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Salary from")]
        public decimal? SalaryFrom { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Input correct max salary")]
        [Required(ErrorMessage = "Max salary required")]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Salary to")]
        public decimal? SalaryTo { get; set; }

        public DateTime Created { get; set; }

        [Required(ErrorMessage = "Location required")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Description required")]
        [MinLength(100)]
        public string Description { get; set; }

        [CurrentDate(ErrorMessage = "Validation date must be after or equal to current date")]
        [Required(ErrorMessage = "Validation date required")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyy-MM-dd}")]
        [Display(Name = "Valid until")]
        public DateTime? ValidUntil { get; set; }

        public List<JobApplication> JobApplications { get; set; } = new List<JobApplication>();
    }
}
