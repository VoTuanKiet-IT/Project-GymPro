using GymPro.Models;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace GymPro.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HuanLuyenVienController : Controller
    {
        private Models.ApplicationDbContext db;
        public HuanLuyenVienController()
        {
            db = new Models.ApplicationDbContext();
        }
        // GET: HuanLuyenVien
        public ActionResult HLVListView()
        {
            var fillteredHLV = db.HuanLuyenViens
                                        .Where(hlv => hlv.isDeleted == false)
                                        .ToList();
            var viewModel = new HuanLuyenVienViewModel
            {
                huanluyenvien = fillteredHLV.Select(hv => new HuanLuyenVienTableRow
                {
                    HuyenLuanViens = new HuanLuyenVien
                    {
                        hlv_ma = hv.hlv_ma,
                        hlv_hodem = hv.hlv_hodem,
                        hlv_ten = hv.hlv_ten,
                        hlv_email = hv.hlv_email,
                        hlv_dienthoai = hv.hlv_dienthoai,
                        hlv_gioitinh = hv.hlv_gioitinh,
                        hlv_diachi = hv.hlv_diachi,
                        hlv_ngaysinh = hv.hlv_ngaysinh,
                        hlv_ngaynhanviec = hv.hlv_ngaynhanviec,
                        hlv_hinhanh = hv.hlv_hinhanh
                    },
                    gioitinh = hv.hlv_gioitinh ? "Nam" : "Nữ",
                    ngaysinh = DateTimeOffset.FromUnixTimeSeconds(hv.hlv_ngaysinh).LocalDateTime,
                    ngaynhanviec = DateTimeOffset.FromUnixTimeSeconds(hv.hlv_ngaynhanviec).LocalDateTime
                }).ToList()
            };



            return View(viewModel);
        }

        //get 
        public ActionResult ChiTietHuanLuyenVien(string id)
        {
            var HuanLuyenVien = db.HuanLuyenViens.Find(id);
            var viewModel = new ChiTietHuanLuyenVienViewModel();
            if (HuanLuyenVien != null)
            {
                viewModel.HuanLuyenViens = HuanLuyenVien;
                viewModel.ngaysinh = DateTimeOffset.FromUnixTimeSeconds(HuanLuyenVien.hlv_ngaysinh).LocalDateTime;
                viewModel.ngaynhanviec = DateTimeOffset.FromUnixTimeSeconds(HuanLuyenVien.hlv_ngaynhanviec).LocalDateTime;
                viewModel.gioitinh = HuanLuyenVien.hlv_gioitinh ? "Nam" : "Nữ";
                viewModel.DSChuyenMons = db.HLVChuyenMons
                                  .Where(cm => cm.hlv_ma == id)
                                  .Select(cm => cm.ChuyenMon)
                                  .ToList();
            }
            else
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        public ActionResult ThemHuanLuyenVien()
        {
            var viewModel = new SetHuanLuyenVienViewModel
            {
                HuanLuyenViens = new HuanLuyenVien(),
                ChuyenMons = db.ChuyenMons.ToList()
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemHuanLuyenVien(SetHuanLuyenVienViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.huanluyenvien_anhdaidien_file != null)
                {
                    var fileName = Path.GetFileName(model.huanluyenvien_anhdaidien_file.FileName);
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;

                    var path = Path.Combine(Server.MapPath("~/Content/Images"), uniqueFileName);
                    model.huanluyenvien_anhdaidien_file.SaveAs(path);

                    // Lưu đường dẫn tương đối vào database
                    model.HuanLuyenViens.hlv_hinhanh = Url.Content("~/Content/Images/") + uniqueFileName;
                }
                else
                {
                    // Nếu không có file, gán null cho trường sanpham_hinhanhchinh
                    model.HuanLuyenViens.hlv_hinhanh = null;
                }
                // 1. Thêm Huấn Luyện Viên vào database
                var lastHLV = db.HuanLuyenViens
                         .OrderByDescending(gt => gt.hlv_ma)
                         .FirstOrDefault();
                string newMaHLV;
                if (lastHLV != null)
                {
                    int lastNumber = int.Parse(lastHLV.hlv_ma.Substring(3));
                    int newNumber = lastNumber + 1;
                    newMaHLV = "HLV" + newNumber.ToString("D2");
                }
                else
                {
                    newMaHLV = "HLV01";
                }

                if (model.HuanLuyenViens != null)
                {
                    model.HuanLuyenViens.hlv_ma = newMaHLV;
                    model.HuanLuyenViens.hlv_ngaysinh = new DateTimeOffset(model.ngaysinh).ToUnixTimeSeconds();
                    model.HuanLuyenViens.hlv_ngaynhanviec = new DateTimeOffset(model.ngaynhanviec).ToUnixTimeSeconds();
                    db.HuanLuyenViens.Add(model.HuanLuyenViens);
                    db.SaveChanges();
                }
                else
                {
                    ModelState.AddModelError("", "Dữ liệu gói tập không được cung cấp.");
                    return View(model);
                }
                // 2. Thêm các mối quan hệ nhiều-nhiều vào bảng trung gian
                if (model.ChuyenMonDaChon != null)
                {
                    foreach (var chuyenMonMa in model.ChuyenMonDaChon)
                    {
                        var hlvChuyenMon = new HLVChuyenMon
                        {
                            hlv_ma = model.HuanLuyenViens.hlv_ma,
                            chuyenmon_ma = chuyenMonMa
                        };
                        db.HLVChuyenMons.Add(hlvChuyenMon);
                    }
                }
                db.SaveChanges();

                return RedirectToAction("HLVListView"); // Chuyển hướng về trang danh sách
            }

            // Nếu dữ liệu không hợp lệ, tải lại danh sách chuyên môn và trả về view
            model.ChuyenMons = db.ChuyenMons.ToList();
            return View(model);
        }

        //Get: Sua Huan Luyen Vien
        public ActionResult SuaHuanLuyenVien(string id)
        {
            var fillteredHLV = db.HuanLuyenViens
                                        .Where(hv => hv.hlv_ma == id)
                                        .FirstOrDefault();
            if (fillteredHLV == null)
            {
                return HttpNotFound();
            }
            var viewModel = new SetHuanLuyenVienViewModel
            {
                HuanLuyenViens = fillteredHLV,
                ngaysinh = DateTimeOffset.FromUnixTimeSeconds(fillteredHLV.hlv_ngaysinh).LocalDateTime,
                ngaynhanviec = DateTimeOffset.FromUnixTimeSeconds(fillteredHLV.hlv_ngaynhanviec).LocalDateTime,
                ChuyenMons = db.ChuyenMons.ToList(),
                ChuyenMonDaChon = db.HLVChuyenMons
                                  .Where(cm => cm.hlv_ma == id)
                                  .Select(cm => cm.chuyenmon_ma)
                                  .ToList()
            };
            return View(viewModel);
        }
        //Post: Sua Huan Luyen Vien
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuaHuanLuyenVien(SetHuanLuyenVienViewModel model)
        {
            if (ModelState.IsValid)
            {
                var suaHuanLuyenVien = db.HuanLuyenViens.Find(model.HuanLuyenViens.hlv_ma);
                if (suaHuanLuyenVien == null)
                {
                    return HttpNotFound();
                }
                // Cập nhật thông tin Huấn Luyện Viên
                suaHuanLuyenVien.hlv_ten = model.HuanLuyenViens.hlv_ten;
                suaHuanLuyenVien.hlv_email = model.HuanLuyenViens.hlv_email;
                suaHuanLuyenVien.hlv_dienthoai = model.HuanLuyenViens.hlv_dienthoai;
                suaHuanLuyenVien.hlv_ngaynhanviec = model.HuanLuyenViens.hlv_ngaynhanviec;
                // Xóa các mối quan hệ hiện tại trong bảng trung gian
                var suaHLVChuyenMon = db.HLVChuyenMons.Where(cm => cm.hlv_ma == model.HuanLuyenViens.hlv_ma).ToList();
                foreach (var relation in suaHLVChuyenMon)
                {
                    db.HLVChuyenMons.Remove(relation);
                }
                // Thêm các mối quan hệ mới vào bảng trung gian
                if (model.ChuyenMonDaChon != null)
                {
                    foreach (var chuyenMonMa in model.ChuyenMonDaChon)
                    {
                        var hlvChuyenMon = new HLVChuyenMon
                        {
                            hlv_ma = model.HuanLuyenViens.hlv_ma,
                            chuyenmon_ma = chuyenMonMa
                        };
                        db.HLVChuyenMons.Add(hlvChuyenMon);
                    }
                }
                db.SaveChanges();
                return RedirectToAction("HLVListView");
            }
            // Nếu dữ liệu không hợp lệ, tải lại danh sách chuyên môn và trả về view
            model.ChuyenMons = db.ChuyenMons.ToList();
            return View(model);
        }



        //Get: ChuyenMon
        public ActionResult ChuyenMonListView()
        {
            var listviewChuyenMon = db.ChuyenMons
                                    .ToList();
            return View(listviewChuyenMon);
        }
        //Get: ThemChuyenMon
        public ActionResult ThemChuyenMon()
        {
            return View();
        }

        //Post: ThemChuyenMon
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemChuyenMon(ThemChuyenMonViewModel model)
        {
            if (ModelState.IsValid)
            {
                var lastCM = db.ChuyenMons
                         .OrderByDescending(lsp => lsp.chuyenmon_ma)
                         .FirstOrDefault();

                string newMaCM;
                if (lastCM != null)
                {
                    // Tăng số thứ tự
                    int lastNumber = int.Parse(lastCM.chuyenmon_ma.Substring(3));
                    int newNumber = lastNumber + 1;
                    newMaCM = "CM" + newNumber.ToString("D2");
                }
                else
                {
                    newMaCM = "CM01";
                }

                var themChuyenMons = new ChuyenMon
                {
                    chuyenmon_ma = newMaCM,
                    chuyenmon_ten = model.ChuyenMons.chuyenmon_ten,
                    chuyenmon_mota = model.ChuyenMons.chuyenmon_mota,

                };

                db.ChuyenMons.Add(themChuyenMons);
                db.SaveChanges();
                return RedirectToAction("ChuyenMonListView");
            }
            return View(model);
        }




    }
}