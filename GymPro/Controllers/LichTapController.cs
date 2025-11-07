using GymPro.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace GymPro.Controllers
{
    [Authorize(Roles = "Admin,PT,User")]
    public class LichTapController : Controller
    {
        private Models.ApplicationDbContext db;
        public LichTapController()
        {
            db = new Models.ApplicationDbContext();
        }



        //Get Đăng ký lịch tập với PT
        public ActionResult DangKyLichVoiPT(string id)
        {

            var viewModel = new SetLichTapVoiPTViewModel
            {
                hoivien_ma = id,
                CaTaps = db.CaTaps.ToList(),
                HuanLuyenViens = db.HuanLuyenViens.Where(hlv => hlv.isDeleted == false).ToList(),
                ngaytap = System.DateTime.Now
            };

            return View(viewModel);
        }

        //Post Đăng ký lịch tập với PT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKyLichVoiPT(SetLichTapVoiPTViewModel model)
        {
            if (ModelState.IsValid)
            {
                var lastLichTapVoiPT = db.LichTapVoiPTs
                         .OrderByDescending(lt => lt.lichtap_ma)
                         .FirstOrDefault();
                string newMaLichTapVoiPT;
                if (lastLichTapVoiPT != null)
                {
                    int lastNumber = int.Parse(lastLichTapVoiPT.lichtap_ma.Substring(4));
                    newMaLichTapVoiPT = "LTPT" + (lastNumber + 1).ToString("D4");
                }
                else
                {
                    newMaLichTapVoiPT = "LTPT0001";
                }

                var lichTapVoiPT = new LichTapVoiPT
                {
                    lichtap_ma = newMaLichTapVoiPT,
                    hoivien_ma = model.hoivien_ma,
                    hlv_ma = model.selected_hlv_ma,
                    catap_ma = model.selected_catap_ma,
                    lichtap_ngaytap = ((DateTimeOffset)model.ngaytap).ToUnixTimeSeconds()
                };
                db.LichTapVoiPTs.Add(lichTapVoiPT);
                db.SaveChanges();
                return RedirectToAction("ChiTietHoiVien", "HoiVien", new { id = model.hoivien_ma });
            }
            // Nếu có lỗi, tải lại danh sách cho DropDownList và trả về View
            model.CaTaps = db.CaTaps.ToList();
            model.HuanLuyenViens = db.HuanLuyenViens.Where(hlv => hlv.isDeleted == false).ToList();
            return View(model);
        }

    }
}