using System.Web.Mvc;

namespace RISH.Controllers
{
    [Authorize]
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}
	}
}