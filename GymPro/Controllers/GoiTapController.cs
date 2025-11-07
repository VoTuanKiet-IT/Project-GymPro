using GymPro.Models;
using System.Linq;
using System.Web.Mvc;


namespace GymPro.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GoiTapController : Controller
    {
        private Models.ApplicationDbContext db;
        public GoiTapController()
        {
            db = new Models.ApplicationDbContext();
        }

        // GET: ManagerGym
        public ActionResult GoiTapListView()
        {
            var filteredGoiTaps = db.GoiTaps
                                        .Where(gt => gt.isDelete == false)
                                        .ToList();

            var viewModel = new GoiTapViewModel
            {
                GoiTaps = filteredGoiTaps
            };

            return View(viewModel);
        }
        //GEt: Create GoiTap
        public ActionResult CreateGoiTap()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateGoiTap(SetGoiTapViewModel model)
        {
            if (ModelState.IsValid)
            {
                var lastGoiTap = db.GoiTaps
                         .OrderByDescending(gt => gt.goitap_ma)
                         .FirstOrDefault();
                string newMaGoiTap;
                if (lastGoiTap != null)
                {
                    int lastNumber = int.Parse(lastGoiTap.goitap_ma.Substring(3));
                    int newNumber = lastNumber + 1;
                    newMaGoiTap = "GT" + newNumber.ToString("D2");
                }
                else
                {
                    newMaGoiTap = "GT01";
                }
                if (model.GoiTaps != null)
                {
                    model.GoiTaps.goitap_ma = newMaGoiTap;
                    db.GoiTaps.Add(model.GoiTaps);
                    db.SaveChanges();
                    return RedirectToAction("GoiTapListView");
                }
                else
                {
                    // Xử lý khi đối tượng GoiTaps bị null
                    ModelState.AddModelError("", "Dữ liệu gói tập không được cung cấp.");
                    return View(model);
                }

            }
            return View(model);
        }
        //get   Sua GoiTap
        public ActionResult SuaGoiTap(string id)
        {
            var goiTap = db.GoiTaps.Find(id);
            if (goiTap == null)
            {
                return HttpNotFound();
            }
            var viewModel = new SetGoiTapViewModel
            {
                GoiTaps = goiTap
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuaGoiTap(SetGoiTapViewModel model)
        {
            if (ModelState.IsValid)
            {
                var suaGoTap = db.GoiTaps.Find(model.GoiTaps.goitap_ma);
                if (suaGoTap == null)
                {
                    return HttpNotFound();
                }
                suaGoTap.goitap_tengoi = model.GoiTaps.goitap_tengoi;
                suaGoTap.goitap_gia = model.GoiTaps.goitap_gia;
                suaGoTap.goitap_thoihan = model.GoiTaps.goitap_thoihan;
                suaGoTap.goitap_motagoi = model.GoiTaps.goitap_motagoi;
                db.SaveChanges();
                return RedirectToAction("GoiTapListView");
            }
            return View(model);
        }
    }
}