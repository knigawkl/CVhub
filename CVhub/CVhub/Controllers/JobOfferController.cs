using Microsoft.AspNetCore.Mvc;
using CVhub.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using System.Net;
using CVhub.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace CVhub.Controllers
{
    [Route("[controller]/[action]")]
    public class JobOfferController : Controller
    {
        private readonly DataContext _context;

        public JobOfferController(DataContext context)
        {
            _context = context;
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var offer = _context.JobOffers.FirstOrDefault(x => x.Id == id);
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
            var offer = _context.JobOffers.FirstOrDefault(x => x.Id == model.Id);
            if (offer == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            offer.JobTitle = model.JobTitle;
            offer.Description = model.Description;
            offer.CompanyName = model.CompanyName;
            offer.Description = model.Description;
            offer.JobTitle = model.JobTitle;
            offer.Location = model.Location;
            offer.SalaryFrom = model.SalaryFrom;
            offer.SalaryTo = model.SalaryTo;
            offer.ValidUntil = model.ValidUntil;
            _context.SaveChanges();
            return RedirectToAction("Details", new { id = model.Id });
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var offer = _context.JobOffers.FirstOrDefault(x => x.Id == id);
            _context.JobOffers.Remove(offer);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Create()
        {
            var model = new JobOfferCreateView
            {
                Companies = _context.Companies.ToList()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(JobOfferCreateView model)
        {
            var companies = await _context.Companies.ToListAsync();
            var jobOffers = await _context.JobOffers.ToListAsync();

            if (!ModelState.IsValid)
            {
                model.Companies = companies;
                return View(model);
            }

            var offer = new JobOffer {
                CompanyId = companies.First(x => x.Name == model.CompanyName).Id,
                CompanyName = model.CompanyName,
                Description = model.Description,
                JobTitle = model.JobTitle,
                Location = model.Location,
                SalaryFrom = model.SalaryFrom,
                SalaryTo = model.SalaryTo,
                ValidUntil = model.ValidUntil,
                Created = DateTime.Now
            };
            await _context.JobOffers.AddAsync(offer);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Index([FromQuery(Name = "search")] string searchString)
        {
            List<JobOffer> jobOffers = await _context.JobOffers.ToListAsync();

            if (String.IsNullOrEmpty(searchString))
            {
                return View(jobOffers);
            }
            List<JobOffer> searchResult = jobOffers.FindAll(x => x.JobTitle.Contains(searchString));
            return View(searchResult);
        }

        public IActionResult Details(int id)
        {
            var offer = _context.JobOffers.ToList().FirstOrDefault(x => x.Id == id);
            List<JobApplication> applications = _context.JobApplications.Where(x => x.JobOfferId == id).ToList();
            offer.JobApplications = applications;
            return View(offer);
        }
    }
}
