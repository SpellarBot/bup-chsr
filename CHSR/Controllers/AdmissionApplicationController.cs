using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CHSR.Models;
using CHSR.Service;
using CHSR.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CHSR.Controllers
{
    public class AdmissionApplicationController : Controller
    {
        private readonly CHSRContext _context;
        private readonly FileAddRemoveService _fileService;
        public AdmissionApplicationController(CHSRContext context, FileAddRemoveService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.AdmissionApplications.ToListAsync());
        }


        [HttpGet]
        public IActionResult Registration(string sessionId, string applicationTraceId)
        {
            
            var countries = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Argentina",
                    Value = "argentina"
                },
                new SelectListItem
                {
                    Text = "Bangladesh",
                    Value = "bangladesh"
                }
            };

            ViewData["CountryList"] = countries;

            if (applicationTraceId != null)
            {
                var admissionApplication = _context.AdmissionApplications.Where(p => p.TraceId == applicationTraceId).FirstOrDefault();
                if (admissionApplication!=null)
                {
                    return View(admissionApplication);
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(AdmissionApplication admissionApplication)
        {
            if (string.IsNullOrEmpty(admissionApplication.TraceId))
            {
                admissionApplication.TraceId = Guid.NewGuid().ToString();
            }

            var rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\documents", admissionApplication.TraceId);

            if (!Directory.Exists(rootPath))
            {
                Directory.CreateDirectory(rootPath);
            }

            //_fileUploaderService.UploadFile(admissionApplication.ProfilePicture, rootPath);
            //admissionApplication.ProfilePictureId = admissionApplication.ProfilePicture.FileName;

            //if (admissionApplication.ProfilePicture != null || admissionApplication.ProfilePicture.Length > 0)
            //{
            //    var profilePicturePath = Path.Combine(rootPath, admissionApplication.ProfilePicture.FileName);

            //    using (var stream = new FileStream(profilePicturePath, FileMode.Create))
            //    {
            //        await admissionApplication.ProfilePicture.CopyToAsync(stream);
            //        admissionApplication.ProfilePictureId = admissionApplication.ProfilePicture.FileName;
            //    }
            //}
            if (ModelState.IsValid|| admissionApplication.IsDraft)
            {
                admissionApplication.IsDraft = true;
                await _context.AdmissionApplications.AddAsync(admissionApplication);
                await _context.SaveChangesAsync();

                //TODO : send mail to applicant

                return RedirectToAction("AttachDocs", new { applicationTraceId = admissionApplication.TraceId });
            }

            return View();
           
        }

        [HttpGet]
        public IActionResult AttachDocs(string applicationTraceId)
        {
            ViewData["applicationTraceId"] = applicationTraceId;

            DirectoryInfo dirInfo = new DirectoryInfo(@"wwwroot\\documents\\"+applicationTraceId);
            List<FileInfo> files = dirInfo.GetFiles().ToList();

            //List<dynamic> data = new List<dynamic>();
            List<string> data = new List<string>();
            foreach(var item in files)
            {
                data.Add(item.Name);
            }

            ViewData["Files"] = data;
            
            //pass the data trough the "View" method
            return View();

            //return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegistrationDocs(AttachmentViewModel attachmentViewModel, string applicationTraceId)
        {
            var fileCategory = Request.Form["FileCategory"];
            var rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\documents", applicationTraceId);

            if (!Directory.Exists(rootPath))
            {
                Directory.CreateDirectory(rootPath);
            }

            

            var admissionApplication = _context.AdmissionApplications.Where(p => p.TraceId == applicationTraceId).Single();
            int i = 0;
            foreach (var attachment in attachmentViewModel.ApplicationAttachmentFiles)
            {
                _fileService.UploadFile(attachment, rootPath);
                admissionApplication.ApplicationAttachments.Add(new ApplicationAttachment { FileName = attachment.FileName, FileCategory = fileCategory[i], AdmissionApplication = admissionApplication });
                i++;
            }
            _context.Update(admissionApplication);
            await _context.SaveChangesAsync();

            return RedirectToAction("AttachDocs", new { applicationTraceId = applicationTraceId });
        }

        [HttpGet]
        public IActionResult DocumentAttachments(string traceId)
        {
            return View();
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admissionApplication = await _context.AdmissionApplications.FindAsync(id);
            if (admissionApplication == null)
            {
                return NotFound();
            }
            return View(admissionApplication);
        }

        // POST: Institutes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AdmissionApplication admissionApplication)
        {
            

            var rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\documents", admissionApplication.TraceId);

            if (!Directory.Exists(rootPath))
            {
                Directory.CreateDirectory(rootPath);
            }

            //_fileUploaderService.UploadFile(admissionApplication.ProfilePicture, rootPath);
            //admissionApplication.ProfilePictureId = admissionApplication.ProfilePicture.FileName;

            //if (admissionApplication.ProfilePicture != null || admissionApplication.ProfilePicture.Length > 0)
            //{
            //    var profilePicturePath = Path.Combine(rootPath, admissionApplication.ProfilePicture.FileName);

            //    using (var stream = new FileStream(profilePicturePath, FileMode.Create))
            //    {
            //        await admissionApplication.ProfilePicture.CopyToAsync(stream);
            //        admissionApplication.ProfilePictureId = admissionApplication.ProfilePicture.FileName;
            //    }
            //}
            if (ModelState.IsValid)
            {
                
                _context.Update(admissionApplication);
                await _context.SaveChangesAsync();

                //TODO : send mail to applicant

                return RedirectToAction("AttachDocs", new { applicationTraceId = admissionApplication.TraceId });
            }

            
            return View(admissionApplication);
        }

        private bool admissionApplicationExists(Guid id)
        {
            return _context.AdmissionApplications.Any(e => e.Id == id);
        }

    }
}