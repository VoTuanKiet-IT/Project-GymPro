using GymPro.Models;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace GymPro.Controllers
{
    [Authorize(Roles = "Admin,PT")]
    public class HoiVienController : Controller
    {
        private Models.ApplicationDbContext db;
        public HoiVienController()
        {
            db = new Models.ApplicationDbContext();
        }
        // GET: HoiVien
        public ActionResult HoiVienListView()
        {
            var fillteredHoiVien = db.HoiViens
                                        .Where(hv => hv.isDeleted == false)
                                        .ToList();
            var viewModel = new HoiVienViewModel
            {
                hoivien = fillteredHoiVien.Select(hv => new HoiVienTableRow
                {
                    hoiviens = new HoiVien
                    {
                        hoivien_ma = hv.hoivien_ma,
                        hoivien_hodem = hv.hoivien_hodem,
                        hoivien_ten = hv.hoivien_ten,
                        hoivien_email = hv.hoivien_email,
                        hoivien_dienthoai = hv.hoivien_dienthoai,
                        hoivien_gioitinh = hv.hoivien_gioitinh,
                        hoivien_diachi = hv.hoivien_diachi,
                        hoivien_ngaysinh = hv.hoivien_ngaysinh,
                        hoivien_ngaydangky = hv.hoivien_ngaydangky,
                        hoivien_hinhanh = hv.hoivien_hinhanh
                    },
                    gioitinh = hv.hoivien_gioitinh ? "Nam" : "Nữ",
                    ngaysinh = DateTimeOffset.FromUnixTimeSeconds(hv.hoivien_ngaysinh).LocalDateTime,
                    ngaydangky = DateTimeOffset.FromUnixTimeSeconds(hv.hoivien_ngaydangky).LocalDateTime
                }).ToList()
            };

            return View(viewModel);
        }

        //Get ChiTietHoiVien
        public ActionResult ChiTietHoiVien(string id)
        {
            var fillHV = db.HoiViens
                                .Where(hv => hv.hoivien_ma == id)
                                .FirstOrDefault();
            var fillHopDong = db.HopDongHoiViens
                                        .Include(hd => hd.GoiTaps)
                                        .Where(hd => hd.hoivien_ma == id)
                                        .ToList();
            var fillLichTap = db.LichTapVoiPTs
                                        .Include(lt => lt.CaTaps)
                                        .Include(lt => lt.HuanLuyenViens)
                                        .Where(lt => lt.hoivien_ma == id)
                                        .ToList();
            var viewModel = new ChiTietHoiVienViewModel
            {
                hoivien = new HoiVienTableRow
                {
                    hoiviens = fillHV,
                    gioitinh = fillHV.hoivien_gioitinh ? "Nam" : "Nữ",
                    ngaysinh = DateTimeOffset.FromUnixTimeSeconds(fillHV.hoivien_ngaysinh).LocalDateTime,
                    ngaydangky = DateTimeOffset.FromUnixTimeSeconds(fillHV.hoivien_ngaydangky).LocalDateTime
                },
                listhopdong = fillHopDong.Select(hdd => new HopDongHoiVienTableRow
                {
                    hopdonghoivien = hdd,
                    goitap = hdd.GoiTaps,
                    ngaybatdau = DateTimeOffset.FromUnixTimeSeconds(hdd.hdhoivien_ngaybatdau).LocalDateTime,
                    ngayketthuc = DateTimeOffset.FromUnixTimeSeconds(hdd.hdhoivien_ngayketthuc).LocalDateTime
                }).OrderByDescending(item => item.ngaybatdau).ToList(),
                listlichtap = fillLichTap.Select(ltt => new LichTapVoiPTTableRow
                {
                    lichtapvoipt = ltt,
                    catap = ltt.CaTaps,
                    huanluyenvien = ltt.HuanLuyenViens,
                    ngaytap = DateTimeOffset.FromUnixTimeSeconds(ltt.lichtap_ngaytap).LocalDateTime,
                }).OrderByDescending(item => item.ngaytap).ToList()

            };

            return View(viewModel);
        }




        //get: Create HoiVien
        public ActionResult ThemHoiVien()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemHoiVien(SetHoiVienViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.hoivien_anhdaidien_file != null)
                {
                    var fileName = Path.GetFileName(model.hoivien_anhdaidien_file.FileName);
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;

                    var path = Path.Combine(Server.MapPath("~/Content/Images"), uniqueFileName);
                    model.hoivien_anhdaidien_file.SaveAs(path);

                    // Lưu đường dẫn tương đối vào database
                    model.HoiViens.hoivien_hinhanh = Url.Content("~/Content/Images/") + uniqueFileName;
                }
                else
                {
                    // Nếu không có file, gán null cho trường sanpham_hinhanhchinh
                    model.HoiViens.hoivien_hinhanh = null;
                }
                var lastHoiVien = db.HoiViens
                         .OrderByDescending(hv => hv.hoivien_ma)
                         .FirstOrDefault();
                string newMaHoiVien;
                if (lastHoiVien != null)
                {
                    int lastNumber = int.Parse(lastHoiVien.hoivien_ma.Substring(3));
                    int newNumber = lastNumber + 1;
                    newMaHoiVien = "HV" + newNumber.ToString("D3");
                }
                else
                {
                    newMaHoiVien = "HV001";
                }
                // 3. Chuyển datetime sang unix timestamp
                model.HoiViens.hoivien_ngaysinh = new DateTimeOffset(model.ngaysinh).ToUnixTimeSeconds();
                var ngaydangky = DateTime.Now;
                model.HoiViens.hoivien_ngaydangky = new DateTimeOffset(ngaydangky).ToUnixTimeSeconds();
                if (model.HoiViens != null)
                {
                    model.HoiViens.hoivien_ma = newMaHoiVien;
                    db.HoiViens.Add(model.HoiViens);
                    db.SaveChanges();
                    return RedirectToAction("HoiVienListView");
                }
                else
                {
                    ModelState.AddModelError("", "Dữ liệu không hợp lệ.");
                }
            }
            return View(model);
        }

        //Get SuaHoiVien
        public ActionResult SuaHoiVien(string id)
        {
            var fillteredHoiVien = db.HoiViens
                                        .Where(hv => hv.hoivien_ma == id)
                                        .FirstOrDefault();
            var viewModel = new HoiVienTableRow
            {
                hoiviens = fillteredHoiVien,
                gioitinh = fillteredHoiVien.hoivien_gioitinh ? "Nam" : "Nữ",
                ngaysinh = DateTimeOffset.FromUnixTimeSeconds(fillteredHoiVien.hoivien_ngaysinh).LocalDateTime,
                ngaydangky = DateTimeOffset.FromUnixTimeSeconds(fillteredHoiVien.hoivien_ngaydangky).LocalDateTime
            };


            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuaHoiVien(HoiVienTableRow model)
        {
            if (ModelState.IsValid)
            {
                var timHoiVien = db.HoiViens.Find(model.hoiviens.hoivien_ma);
                if (timHoiVien == null)
                {
                    return HttpNotFound();
                }
                // Cập nhật các trường thông tin
                if (model.hoivien_anhdaidien_file != null && model.hoivien_anhdaidien_file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(model.hoivien_anhdaidien_file.FileName);
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;

                    var path = Path.Combine(Server.MapPath("~/Content/Images"), uniqueFileName);
                    model.hoivien_anhdaidien_file.SaveAs(path);

                    // Lưu đường dẫn tương đối vào database
                    timHoiVien.hoivien_hinhanh = Url.Content("~/Content/Images/") + uniqueFileName;
                }
                timHoiVien.hoivien_hodem = model.hoiviens.hoivien_hodem;
                timHoiVien.hoivien_ten = model.hoiviens.hoivien_ten;
                timHoiVien.hoivien_dienthoai = model.hoiviens.hoivien_dienthoai;
                timHoiVien.hoivien_email = model.hoiviens.hoivien_email;
                timHoiVien.hoivien_gioitinh = model.hoiviens.hoivien_gioitinh;
                timHoiVien.hoivien_diachi = model.hoiviens.hoivien_diachi;
                timHoiVien.hoivien_ngaysinh = new DateTimeOffset(model.ngaysinh).ToUnixTimeSeconds();
                timHoiVien.hoivien_ngaydangky = new DateTimeOffset(model.ngaydangky).ToUnixTimeSeconds();
                db.SaveChanges();
                return RedirectToAction("HoiVienListView");
            }
            return View("SuaHoiVien", model);
        }
    }
}