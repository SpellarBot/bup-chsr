
using Microsoft.AspNetCore.Mvc;

namespace CHSR.Web.Controllers
{
    public class AdmissionApplicationController : Controller
    {
        public IActionResult Index()
        {
            return View("BasicInfo");
        }

        [HttpGet]
        public IActionResult BasicInfo()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BasicInfo(string abc)
        {
            return RedirectToAction("AttachDocuments");
        }


        [HttpGet]
        public IActionResult AttachDocuments(string abc)
        {
            return View();
        }
    }
}