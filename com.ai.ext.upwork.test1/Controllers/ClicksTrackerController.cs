using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using com.ai.ext.upwork.test1.Models;

namespace com.ai.ext.upwork.test1.Controllers
{    
    public class ClicksTrackerController : ApiController
    {        
        public ClicksTrackerController()
        {
            Repository = new ClicksTrackerRepository();
        }

        public IEnumerable<ClicksTracker> GetClicks()
        {
            return Repository.ClicksTrackers;
        }

        public IHttpActionResult GetClick(int Id)
        {
            ClicksTracker item = Repository.ClicksTrackers.Where(c => c.ID == Id).SingleOrDefault();
            return item == null ? (IHttpActionResult)BadRequest("No record found") : Ok(item);            
        }

        public IHttpActionResult PostClick(ClicksTracker item)
        {
            if (ModelState.IsValid)
            {
                Repository.SaveClicksTracker(item);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        
        public void DeleteClick(int Id)
        {
            Repository.DeleteClicksTracker(Id);
        }

        private IRepository Repository { get; set; }
    }
}
