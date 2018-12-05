using Microsoft.AspNetCore.Mvc;
using CVhub.Models;
using System.Collections.Generic;
using System.Linq;

namespace CVhub.Controllers
{
    [Route("[controller]/[action]")]
    public class JobOfferController : Controller
    {
        private static List<JobOffer> jobOffers = new List<JobOffer>
        {
            new JobOffer{ Id = 0, JobTitle = "Django Developer" },
            new JobOffer{ Id = 1, JobTitle = "Android Xamarin Developer" },
            new JobOffer{ Id = 2, JobTitle = "Vue.js Developer" },
            new JobOffer{ Id = 3, JobTitle = "ASP .NET MVC Developer" }
        };

        public IActionResult Index()
        {
            return View(jobOffers);
        }

        public IActionResult Details(int id)
        {
            var offer = jobOffers.FirstOrDefault(x => x.Id == id);
            return View(offer);
        }
    }
}
