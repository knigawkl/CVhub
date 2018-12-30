using System.Linq;
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

        public async Task<ActionResult> Create(int id)
        {
            JobApplicationCreateView model = new JobApplicationCreateView();
            var jobOffer = _context.JobOffers.FirstOrDefault(x => x.Id == id);
            model.JobTitle = jobOffer.JobTitle;
            model.JobOfferId = id;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(JobApplicationCreateView model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var application = new JobApplication
            {
                JobOfferId = model.JobOfferId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                EmailAddress = model.EmailAddress,
                CoverLetter = model.CoverLetter,
                DateOfBirth = model.DateOfBirth
            };

            await _context.JobApplications.AddAsync(application);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "JobOffer");
        }
    }
}
