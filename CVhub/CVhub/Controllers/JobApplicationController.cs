using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CVhub.EntityFramework;
using CVhub.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Apply(JobApplicationCreateView model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string photoURI = null;
            if (model.Photo != null) 
            {
                var photoGUID = Guid.NewGuid().ToString(); 
                var extension = Path.GetExtension(model.Photo.FileName);
                var photoName = photoGUID + extension;

                photoURI = await UploadPhotoToBlobStorageAsync(model.Photo, photoName);
            }

            string CVURI = null;
            if (model.CV != null)
            {
                var CVGUID = Guid.NewGuid().ToString();
                var extension = Path.GetExtension(model.CV.FileName);
                var fileName = CVGUID + extension;

                CVURI = await UploadCVToBlobStorageAsync(model.CV, fileName);
            }

            var newApplication = new JobApplication()
            {
                JobOfferId = model.JobOfferId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                EmailAddress = model.EmailAddress,
                CoverLetter = model.CoverLetter,
                DateOfBirth = model.DateOfBirth,
                CvUrl = CVURI,
                PhotoFileName = photoURI,
            };

            _context.JobApplications.Add(newApplication);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "JobOffer", new { id = model.JobOfferId });
        }

        private static async Task<string> UploadFileToBlobStorageAsync(IFormFile file, string fileName, string containerName)
        {
            string connectionString = @"DefaultEndpointsProtocol=https;AccountName=cvhubblob;AccountKey=3N9jQyMaVrMvmQU7L/b2O1g7H8b8WXgFcOHeXTCkYQTnTE2e0lGh5ehSaOswyspOTHL7hKlQ5/3B9kMGJ6LseQ==;EndpointSuffix=core.windows.net";

            if (CloudStorageAccount.TryParse(connectionString, out var storageAccount))
            {
                CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

                CloudBlobContainer container = cloudBlobClient.GetContainerReference(containerName);

                CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);

                using (var memoryStream = new MemoryStream())
                {
                    file.OpenReadStream().CopyTo(memoryStream);
                    var bytes = memoryStream.ToArray();

                    await blockBlob.UploadFromByteArrayAsync(bytes, 0, bytes.Length);
                }

                return blockBlob.Uri.AbsoluteUri;
            }

            return null;
        }

        private static async Task<string> UploadCVToBlobStorageAsync(IFormFile modelCv, string fileName)
        {
            return await UploadFileToBlobStorageAsync(modelCv, fileName, "applications");
        }

        private static async Task<string> UploadPhotoToBlobStorageAsync(IFormFile file, string fileName)
        {
            return await UploadFileToBlobStorageAsync(file, fileName, "applications");
        }

        private async Task<ActionResult> GetPicture(string name)
        {
            string connectionString = @"DefaultEndpointsProtocol=https;AccountName=cvhubblob;AccountKey=3N9jQyMaVrMvmQU7L/b2O1g7H8b8WXgFcOHeXTCkYQTnTE2e0lGh5ehSaOswyspOTHL7hKlQ5/3B9kMGJ6LseQ==;EndpointSuffix=core.windows.net";

            if (CloudStorageAccount.TryParse(connectionString, out var storageAccount))
            {
                CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

                // Get reference to the blob container by passing the name by reading the value from the configuration (appsettings.json)
                CloudBlobContainer container = cloudBlobClient.GetContainerReference("applications");
                //await container.CreateIfNotExistsAsync();

                // Get the reference to the block blob from the container
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(name);
                if (!await blockBlob.ExistsAsync())
                    return NotFound();

                var url = blockBlob.StorageUri;
                var url2 = blockBlob.Uri;

                using (var stream = new MemoryStream())
                {
                    await blockBlob.DownloadToStreamAsync(stream);
                    return File(stream.ToArray(), "image/png"); 
                }
            }
            else
            {
                return NotFound();
            }
        }
    }
}
