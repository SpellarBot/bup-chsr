using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CHSR.Web.Controllers
{
    public class StatusCodeController : Controller
    {
        [Route("error/404")]
        public IActionResult Error404()
        {
            return View();
        }

        [Route("error/501")]
        public IActionResult Error501()
        {
            return View();
        }

        // GET: /<controller>/
        [HttpGet("/StatusCode/{statusCode}")]
        public IActionResult Index(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    {
                        return RedirectToAction("Error404", "StatusCode");
                    }
            }

            return View(statusCode);
        }
    }
}