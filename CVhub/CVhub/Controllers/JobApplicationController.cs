using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CVhub.EntityFramework;
using CVhub.Models;
using Microsoft.AspNetCore.Mvc;

namespace CVhub.Controllers
{
    public class JobApplicationController : Controller
    {
        private readonly DataContext _context;

        public JobApplicationController(DataContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> Create(int jobOfferId)
        {
            JobApplicationCreateView model = new JobApplicationCreateView();
            var jobOffer = _context.JobOffers.FirstOrDefault(x => x.Id == 1);
            model.JobTitle = jobOffer.JobTitle;
            model.JobOfferId = jobOfferId;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(JobApplicationCreateView model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var application = new JobApplication
            {
                OfferId = model.OfferId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                EmailAddress = model.EmailAddress,
                CoverLetter = model.CoverLetter
            };

            await _context.JobApplications.AddAsync(application);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "JobOffer", new { id = model.JobOfferId });
        }
    }
}
