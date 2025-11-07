using GymPro.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;


namespace GymPro.Controllers
{
    [Authorize(Roles = "Admin,PT")]
    public class HopDongHoiVienController : Controller
    {
        private Models.ApplicationDbContext db;
        public HopDongHoiVienController()
        {
            db = new Models.ApplicationDbContext();
        }

        // GET: HopDongHoiVienListView
        public ActionResult HopDongHoiVienListView()
        {
            var filteredHopDongHoiViens = db.HopDongHoiViens
                                                    .Include(hd => hd.HoiViens) // Tải thông tin Hội Viên
                                                    .Include(hd => hd.GoiTaps) // Tải thông tin Gói Tập

                                                    .ToList();
            var viewModel = new HopDongHoiVienViewModel
            {
                HopDongHoiViens = filteredHopDongHoiViens.Select(hdd => new HopDongHoiVienTableRow
                {
                    hopdonghoivien = hdd,

                    hoivien = hdd.HoiViens,

                    goitap = hdd.GoiTaps,

                    // Chuyển đổi Unix timestamp sang DateTime
                    ngaybatdau = DateTimeOffset.FromUnixTimeSeconds(hdd.hdhoivien_ngaybatdau).LocalDateTime,
                    ngayketthuc = DateTimeOffset.FromUnixTimeSeconds(hdd.hdhoivien_ngayketthuc).LocalDateTime

                }).ToList()
            };
            return View(viewModel);
        }

        //Get: Create HopDongHoiVien
        public ActionResult ThemHopDongHoiVien()
        {
            ViewBag.HoiViens = new SelectList(db.HoiViens.Where(hv => hv.isDeleted == false).ToList(), "hoivien_ma", "hoivien_ten");
            ViewBag.GoiTaps = new SelectList(db.GoiTaps.Where(gt => gt.isDelete == false).ToList(), "goitap_ma", "goitap_tengoi");
            return View();
        }

        //Post: Create HopDongHoiVien
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemHopDongHoiVien(SetHopDongHoiVienViewModel model)
        {
            if (ModelState.IsValid)
            {
                var lastHopDongHoiVien = db.HopDongHoiViens
                         .OrderByDescending(hd => hd.hdhoivien_ma)
                         .FirstOrDefault();
                string newMaHopDongHoiVien;
                if (lastHopDongHoiVien != null)
                {
                    int lastNumber = int.Parse(lastHopDongHoiVien.hdhoivien_ma.Substring(4));
                    int newNumber = lastNumber + 1;
                    newMaHopDongHoiVien = "HDHV" + newNumber.ToString("D4");
                }
                else
                {
                    newMaHopDongHoiVien = "HDHV0001";
                }
                if (model.HopDongHoiViens != null)
                {
                    model.HopDongHoiViens.hdhoivien_ma = newMaHopDongHoiVien;
                    var ngaybatdau = model.ngaybatdau;
                    model.HopDongHoiViens.hdhoivien_ngaybatdau = ((DateTimeOffset)ngaybatdau).ToUnixTimeSeconds();
                    var thoihan = db.GoiTaps
                                    .Where(gt => gt.goitap_ma == model.HopDongHoiViens.goitap_ma)
                                    .Select(gt => gt.goitap_thoihan)
                                    .FirstOrDefault();
                    model.HopDongHoiViens.hdhoivien_ngayketthuc = ((DateTimeOffset)ngaybatdau.AddMonths(thoihan)).ToUnixTimeSeconds();
                    model.HopDongHoiViens.hdhoivien_trangthai = "Hết hạn";
                    db.HopDongHoiViens.Add(model.HopDongHoiViens);
                    db.SaveChanges();
                    return RedirectToAction("HopDongHoiVienListView");
                }
                else
                {
                    // Xử lý khi đối tượng HopDongHoiViens bị null
                    ModelState.AddModelError("", "Dữ liệu hợp đồng hội viên không được cung cấp.");
                }
            }
            ViewBag.HoiViens = new SelectList(db.HoiViens.Where(hv => hv.isDeleted == false).ToList(), "hoivien_ma", "hoivien_ten");
            ViewBag.GoiTaps = new SelectList(db.GoiTaps.Where(gt => gt.isDelete == false).ToList(), "goitap_ma", "goitap_ten");
            return View(model);
        }



    }
}