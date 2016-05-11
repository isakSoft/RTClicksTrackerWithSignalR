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
        private readonly ClicksTrackerRepository _repository = new ClicksTrackerRepository();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetClicks()
        {
            return PartialView("_clicksList", _repository.GetAll());
        }

        public ActionResult AddClick()
        {
            return PartialView("_clickAdd");
        }

        [HttpPost]
        public void AddClick(ClicksTracker item)
        {
            // str = Convert.ToString(jsonOfClick);
            //After successful addition the SignalR broadcast the data
            //No need to return view
            _repository.Add(item);
            return;
        }
    }       
}