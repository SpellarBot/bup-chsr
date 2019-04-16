using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CHSR.Models;
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
            //await _context.AdmissionApplications.AddAsync(admissionApplication);
            await _context.SaveChangesAsync();
            return View();
        }
    }
}