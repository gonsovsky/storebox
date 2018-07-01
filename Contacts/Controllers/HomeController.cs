using System.Web.Mvc;

namespace Contacts.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Домашнее задание. Гонсовский К.";
            return View();
        }
    }
}