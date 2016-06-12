using System.Web.Mvc;

namespace SySInventory.Web.Controllers
{
    [RoutePrefix("")]
    public class HomeController: Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}