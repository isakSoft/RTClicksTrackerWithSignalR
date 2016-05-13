using System.Web.Mvc;

using com.ai.ext.upwork.test1.Models;

namespace com.ai.ext.upwork.test1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("Clicks");
        }
    }       
}