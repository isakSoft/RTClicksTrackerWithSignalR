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
        public ActionResult AddClick(ClicksTracker item)
        {
            // str = Convert.ToString(jsonOfClick);
            //After successful addition the SignalR broadcast the data
            //No need to return view
            _repository.Add(item);

           return RedirectToAction("GetClicks");            
        }

        public ActionResult EditClick(int Id)
        {
            ClicksTracker item = _repository.Find(Id);
            return PartialView("_clickEdit", item);
        }

        [HttpPost]
        public ActionResult EditClick(ClicksTracker item)
        {
            // str = Convert.ToString(jsonOfClick);
            //After successful addition the SignalR broadcast the data
            //No need to return view
            if(item == null)
            {
                //This might be improved by using HTTP RESPONSE STATUSES
                return null;
            }
            if (!_repository.Update(item)) // if item was not updated
            {
                return null;
            }

            return RedirectToAction("GetClicks");
        }

        public ActionResult ClickDetails(int Id)
        {
           ClicksTracker item = _repository.Find(Id);

            return PartialView("_clickDetails", item);
        }

    }       
}