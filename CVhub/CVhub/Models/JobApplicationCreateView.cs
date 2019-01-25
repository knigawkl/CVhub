using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CVhub.Models
{
    public class JobApplicationCreateView : JobApplication
    {
        public string JobTitle { get; set; }
        [BindProperty]
        public IFormFile Photo { get; set; }
        [BindProperty]
        public IFormFile CV { get; set; }
    }
}
