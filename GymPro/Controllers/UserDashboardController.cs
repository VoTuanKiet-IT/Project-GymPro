using System.Web.Mvc;
using GymPro.Filters;

namespace GymPro.Controllers
{
    [CustomAuthorize(Roles = "User")]
    public class UserDashboardController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
