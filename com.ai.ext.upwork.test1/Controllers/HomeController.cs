using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using com.ai.ext.upwork.test1.Models;

namespace com.ai.ext.upwork.test1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetClicks()
        {
            ClicksTrackerRepository repository = new ClicksTrackerRepository();
            return PartialView("_clicksList", repository.GetAll());
        }
    }
}