using Microsoft.AspNetCore.Mvc;
using CVhub.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using System.Net;

namespace CVhub.Controllers
{
    [Route("[controller]/[action]")]
    public class JobOfferController : Controller
    {
        private static List<Company> companies = new List<Company>
        {
            new Company() { Id = 0, Name = "Lekseek"},
            new Company() { Id = 1, Name = "KMD Poland"},
            new Company() { Id = 1, Name = "IBM"}
        };

        private static List<JobOffer> jobOffers = new List<JobOffer>
        {
            new JobOffer{
                Id = 0,
                JobTitle = "Django Developer",
                Company = companies.FirstOrDefault(x => x.Name.Equals("Lekseek")),
                Created = DateTime.Now,
                Description = "Vestibulum sit amet nisi nec erat laoreet viverra. Vivamus sed metus risus. Suspendisse lacinia amet.",
                Location = "Poland, Warsaw",
                SalaryFrom = 4000,
                SalaryTo = 7000,
                ValidUntil = DateTime.Now.AddDays(123)
            },
            new JobOffer{
                Id = 2,
                JobTitle = "Vue.js Developer",
                Company = companies.FirstOrDefault(x => x.Name.Equals("KMD Poland")),
                Created = DateTime.Now,
                Description = "Integer vitae rutrum leo, vel vulputate orci. Nullam eget imperdiet sapien. Sed euismod lorem nullam.",
                Location = "Poland, Warsaw",
                SalaryFrom = 5000,
                SalaryTo = 8000,
                ValidUntil = DateTime.Now.AddDays(18)
            }
        };

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var offer = jobOffers.Find(x => x.Id == id);
            if (offer == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(offer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(JobOffer model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var offer = jobOffers.Find(x => x.Id == model.Id);
            offer.JobTitle = model.JobTitle;
            return RedirectToAction("Details", new { id = model.Id });
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            jobOffers.RemoveAll(x => x.Id == id);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Create()
        {
            var model = new JobOfferCreateView
            {
                Companies = companies
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(JobOfferCreateView model)
        {
            if (!ModelState.IsValid)
            {
                model.Companies = companies;
                return View(model);
            }
            var id = jobOffers.Max(x => x.Id) + 1;
            jobOffers.Add(new JobOffer
            {
                Id = id,
                CompanyId = model.CompanyId,
                Company = companies.FirstOrDefault(c => c.Id == model.CompanyId),
                Description = model.Description,
                JobTitle = model.JobTitle,
                Location = model.Location,
                SalaryFrom = model.SalaryFrom,
                SalaryTo = model.SalaryTo,
                ValidUntil = model.ValidUntil,
                Created = DateTime.Now
            }
            );
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Index([FromQuery(Name = "search")] string searchString)
        {
            if (String.IsNullOrEmpty(searchString))
            {
                return View(jobOffers);
            }
            List<JobOffer> searchResult = jobOffers.FindAll(x => x.JobTitle.Contains(searchString));
            return View(searchResult);
        }

        public IActionResult Details(int id)
        {
            var offer = jobOffers.FirstOrDefault(x => x.Id == id);
            return View(offer);
        }
    }
}
