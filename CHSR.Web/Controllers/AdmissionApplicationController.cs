
using CHSR.DataCrudService;
using CHSR.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CHSR.Web.Controllers
{
    public class AdmissionApplicationController : Controller
    {
        private readonly AdmissionFormDataCrudService _service;

        public AdmissionApplicationController(AdmissionFormDataCrudService admissionFormDataCrudService)
        {
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

            await _service.Insert(admissionApplication);
            //return View();
            return RedirectToAction("FormSubmissionStatus");
        }

        [HttpGet]
        public IActionResult FormSubmissionStatus()
        {
            return View();
        }
    }
}