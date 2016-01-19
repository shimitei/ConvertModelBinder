using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ConvertModelBinderSample.Web.Models;

namespace ConvertModelBinderSample.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ConvertModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                return View("Create", model);
            }

            return View("Index", model);
        }
    }
}
