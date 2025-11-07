using GymPro.Models;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace GymPro.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SanPhamController : Controller
    {
        private Models.ApplicationDbContext db;
        public SanPhamController()
        {
            db = new Models.ApplicationDbContext();
        }


        // GET: DanhSachSanPham
        public ActionResult SanPhamListView()
        {
            var listviewSanPham = db.SanPhams
                                    .Include("LoaiSanPham")
                                    .ToList();
            return View(listviewSanPham);
        }

        public ActionResult ChiTietSanPham(int id)
        {
            var sanpham = db.SanPhams
                            .Include("LoaiSanPham")
                            .FirstOrDefault(sp => sp.sanpham_ma == id.ToString());
            if (sanpham == null)
            {
                return HttpNotFound();
            }
            return View(sanpham);
        }

        public ActionResult ThemSanPham()
        {

            var viewModel = new ThemSanPhamViewModel
            {
                LoaiSanPhams = db.LoaiSanPhams.ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemSanPham(ThemSanPhamViewModel model)
        {
            if (ModelState.IsValid)
            {
                // 1. Lưu file nếu file không null
                if (model.sanpham_hinhanhchinh_file != null)
                {
                    var fileName = Path.GetFileName(model.sanpham_hinhanhchinh_file.FileName);
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;

                    var path = Path.Combine(Server.MapPath("~/Content/Images"), uniqueFileName);
                    model.sanpham_hinhanhchinh_file.SaveAs(path);

                    // Lưu đường dẫn tương đối vào database
                    model.SanPhams.sanpham_hinhanhchinh = Url.Content("~/Content/Images/") + uniqueFileName;
                }
                else
                {
                    // Nếu không có file, gán null cho trường sanpham_hinhanhchinh
                    model.SanPhams.sanpham_hinhanhchinh = null;
                }

                var lastSanPham = db.SanPhams
                         .OrderByDescending(lsp => lsp.sanpham_ma)
                         .FirstOrDefault();

                string newMaSanPham;
                if (lastSanPham != null)
                {
                    // Tăng số thứ tự
                    int lastNumber = int.Parse(lastSanPham.sanpham_ma.Substring(3));
                    int newNumber = lastNumber + 1;
                    newMaSanPham = "SP" + newNumber.ToString("D2");
                }
                else
                {
                    newMaSanPham = "SP01";
                }

                var themSanPhams = new SanPham
                {
                    sanpham_ma = newMaSanPham,
                    sanpham_ten = model.SanPhams.sanpham_ten,
                    sanpham_gia = model.SanPhams.sanpham_gia,
                    sanpham_hinhanhchinh = model.SanPhams.sanpham_hinhanhchinh,
                    loaisanpham_ma = model.SanPhams.loaisanpham_ma,
                    sanpham_mota = model.SanPhams.sanpham_mota
                };


                // 2. Lưu sản phẩm vào database
                db.SanPhams.Add(themSanPhams);
                db.SaveChanges();

                return RedirectToAction("SanPhamListView");
            }

            // Nếu dữ liệu không hợp lệ, tải lại ViewModel
            model.LoaiSanPhams = db.LoaiSanPhams.ToList();
            return View("ThemSanPham", model);
        }

        [HttpGet]
        public ActionResult SuaSanPham(int id)
        {
            var sanpham = db.SanPhams.Find(id);
            if (sanpham == null)
            {
                return HttpNotFound();
            }
            var viewModel = new SuaSanPhamViewModel
            {
                SanPhams = sanpham,
                LoaiSanPhams = db.LoaiSanPhams.ToList()
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuaSanPham(SuaSanPhamViewModel model)
        {
            if (ModelState.IsValid)
            {
                var sanpham = db.SanPhams.Find(model.SanPhams.sanpham_ma);
                if (sanpham == null)
                {
                    return HttpNotFound();
                }

                // Cập nhật các thuộc tính khác của sản phẩm
                sanpham.sanpham_ten = model.SanPhams.sanpham_ten;
                sanpham.sanpham_gia = model.SanPhams.sanpham_gia;
                sanpham.loaisanpham_ma = model.SanPhams.loaisanpham_ma;
                sanpham.sanpham_mota = model.SanPhams.sanpham_mota;

                // Xử lý upload ảnh
                if (model.sanpham_hinhanhchinh_file != null)
                {
                    // Tạo tên file duy nhất và lưu file mới vào server
                    var fileName = Path.GetFileName(model.sanpham_hinhanhchinh_file.FileName);
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
                    var path = Path.Combine(Server.MapPath("~/Content/Images"), uniqueFileName);
                    model.sanpham_hinhanhchinh_file.SaveAs(path);

                    // Cập nhật đường dẫn ảnh trong database
                    sanpham.sanpham_hinhanhchinh = Url.Content("~/Content/Images/") + uniqueFileName;
                }
                // Nếu không có file mới được upload, đường dẫn ảnh cũ sẽ được giữ lại

                db.SaveChanges();
                return RedirectToAction("SanPhamListView");
            }

            // Nếu model không hợp lệ, tải lại ViewModel và hiển thị lỗi
            model.LoaiSanPhams = db.LoaiSanPhams.ToList();
            return View("SuaSanPham", model);
        }



        //Get: LoaiSanPham
        public ActionResult LoaiSanphamListView()
        {
            // Lấy danh sách Loại Sản Phẩm từ database
            var loaiSanPhams = db.LoaiSanPhams.ToList();

            // Trả về view với danh sách này
            return View(loaiSanPhams);
        }

        //Get: ThemLoaiSanPham  
        public ActionResult ThemLoaiSanPham()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemLoaiSanPham(ThemLoaiSanPhamViewModel model)
        {
            if (ModelState.IsValid)
            {
                var lastLoai = db.LoaiSanPhams
                         .OrderByDescending(lsp => lsp.loaisanpham_ma)
                         .FirstOrDefault();

                string newMaLoai;
                if (lastLoai != null)
                {
                    // Tăng số thứ tự
                    int lastNumber = int.Parse(lastLoai.loaisanpham_ma.Substring(3));
                    int newNumber = lastNumber + 1;
                    newMaLoai = "LSP" + newNumber.ToString("D2");
                }
                else
                {
                    newMaLoai = "LSP01";
                }

                var themLoaiSanPhams = new LoaiSanPham
                {
                    loaisanpham_ma = newMaLoai,
                    loaisanpham_ten = model.LoaiSanPhams.loaisanpham_ten,
                    loaisanpham_mota = model.LoaiSanPhams.loaisanpham_mota
                };

                db.LoaiSanPhams.Add(themLoaiSanPhams);
                db.SaveChanges();
                return RedirectToAction("LoaiSanphamListView");
            }
            return View(model);
        }

    }
}