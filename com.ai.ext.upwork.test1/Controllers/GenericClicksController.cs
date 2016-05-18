using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using com.ai.ext.upwork.test1.Models;

namespace com.ai.ext.upwork.test1.Controllers
{
    public class GenericClicksController : ApiController
    {
        private GenericUnitOfWork uow = null;

        public GenericClicksController()
        {
            uow = new GenericUnitOfWork();
        }

        public GenericClicksController(GenericUnitOfWork _uow)
        {
            uow = _uow;
        }

        public IEnumerable<ClicksTracker> GetClicks()
        {
            return uow.Repository<ClicksTracker>().GetAll().ToList();
        }

        public IHttpActionResult GetClick(int Id)
        {
            ClicksTracker item = uow.Repository<ClicksTracker>().Get(c => c.ID == Id);
            return item == null ? (IHttpActionResult)BadRequest("No record found") : Ok(item);            
        }

        public IHttpActionResult AddClick(ClicksTracker item)
        {
            if (ModelState.IsValid)
            {
                uow.Repository<ClicksTracker>().Add(item);
                uow.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        public IHttpActionResult UpdateClick(ClicksTracker item)
        {
            if (ModelState.IsValid)
            {
                uow.Repository<ClicksTracker>().Attach(item);
                uow.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        public void DeleteClick(int Id)
        {
            ClicksTracker item = uow.Repository<ClicksTracker>().Get(c => c.ID == Id);
            uow.Repository<ClicksTracker>().Delete(item);
            uow.SaveChanges();            
        }
    }
}
