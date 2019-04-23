﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CHSR.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CHSR.Controllers
{
    public class AdmissionApplicationController : Controller
    {
        private readonly CHSRContext _context;
        public AdmissionApplicationController(CHSRContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Registration()
        {
            var programs = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "PhD",
                    Value = "phd"
                },
                new SelectListItem
                {
                    Text = "MPhil",
                    Value = "mphil"
                }
            };

            ViewData["ProgramList"] = programs;

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

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(AdmissionApplication admissionApplication)
        {
            var traceId = Guid.NewGuid().ToString();
            var rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\documents", traceId);

            if (!Directory.Exists(rootPath))
            {
                Directory.CreateDirectory(rootPath);
            }

            if (admissionApplication.ProfilePicture != null || admissionApplication.ProfilePicture.Length > 0)
            {
                var profilePicturePath = Path.Combine(rootPath, admissionApplication.ProfilePicture.FileName);

                using (var stream = new FileStream(profilePicturePath, FileMode.Create))
                {
                    await admissionApplication.ProfilePicture.CopyToAsync(stream);
                    admissionApplication.ProfilePictureId = admissionApplication.ProfilePicture.FileName;
                }
            }

            

            if (admissionApplication.ApplicationAttachments != null || admissionApplication.ApplicationAttachments.Count > 0)
            {
                foreach (IFormFile attachment in admissionApplication.ApplicationAttachments)
                {
                    var applicationAttachmentPath = Path.Combine(rootPath, attachment.FileName);

                    using (var stream = new FileStream(applicationAttachmentPath, FileMode.Create))
                    {
                        await attachment.CopyToAsync(stream);
                        admissionApplication.ApplicationAttachmentIds.Add(attachment.FileName);
                    }
                }
            }

            admissionApplication.IsDraft = true;
            admissionApplication.TraceId = traceId;
            await _context.AdmissionApplications.AddAsync(admissionApplication);
            await _context.SaveChangesAsync();
            return View();
        }

        [HttpGet]
        public IActionResult DocumentAttachments(string traceId)
        {
            return View();
        }
    }
}