using LMSProfile.ExceptionLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMSProfile.Controllers
{
    public class HomeController : Controller
    {
        [LogExceptions]
        public ActionResult Index()
        {
            return View();
        }
    }
}