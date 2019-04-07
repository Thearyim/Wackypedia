using Microsoft.AspNetCore.Mvc;

namespace Wackypedia.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public ActionResult Index()
        {
            return View();
        }
    }
}