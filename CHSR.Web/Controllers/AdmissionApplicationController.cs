
using CHSR.ValidatorService;
using CHSR.DataCrudService;
using CHSR.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CHSR.Web.Controllers
{
    public class AdmissionApplicationController : Controller
    {
        private readonly AdmissionFormDataCrudService _service;

        private readonly AdmissionApplicationValidator _validator;
        public AdmissionApplicationController(AdmissionFormDataCrudService admissionFormDataCrudService)
        {
            _validator = new AdmissionApplicationValidator();
            _service = admissionFormDataCrudService;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(AdmissionApplication admissionApplication)
        {
            //if (ModelState.IsValid)
            //{
            //    return View();
            //}
           
            if (!_validator.Validate(admissionApplication).IsValid)
            {
                return View();
            }

            await _service.Insert(admissionApplication);
            return RedirectToAction("FormSubmissionStatus");
        }

        [HttpGet]
        public IActionResult FormSubmissionStatus()
        {
            return View();
        }
    }
}