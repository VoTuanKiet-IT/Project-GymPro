using System.Web.Mvc;
using GymPro.Filters; //dùng CustomAuthorize

namespace GymPro.Controllers
{
    [CustomAuthorize(Roles = "Admin")]
    public class AdminDashboardController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
