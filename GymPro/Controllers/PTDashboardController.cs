using System.Web.Mvc;
using GymPro.Filters;

namespace GymPro.Controllers
{
    [CustomAuthorize(Roles = "PT")]
    public class PTDashboardController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
