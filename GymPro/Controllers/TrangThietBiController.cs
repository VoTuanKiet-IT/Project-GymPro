using GymPro.Models;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace GymPro.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TrangThietBiController : Controller
    {

        private Models.ApplicationDbContext db;
        public TrangThietBiController()
        {
            db = new Models.ApplicationDbContext();
        }
        // GET: TrangThietBi
        public ActionResult PhongListView()
        {
            var listviewPhong = db.Phongs.ToList();
            return View(listviewPhong);
        }

        //Get ChiTiet Phong
        public ActionResult ChiTietPhong(string id)
        {
            var phong = db.Phongs.Find(id);
            if (phong == null)
            {
                return HttpNotFound();
            }
            // Lấy danh sách thiết bị liên quan đến phòng

            // Lấy danh sách ảnh liên quan đến phòng
            var anhPhongs = db.ChiTietAnhPhongs.Where(a => a.phong_ma == id).ToList();
            // Tạo một ViewModel để truyền dữ liệu đến View
            var viewModel = new ChiTietPhongViewModel
            {
                phongs = phong,
                ListAnhPhong = anhPhongs
            };

            return View(viewModel);
        }

        //get ThemPhong
        public ActionResult ThemPhong()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemPhong(ThemPhongViewModel model)
        {
            //if (ModelState.IsValid)
            //{
            var lastLoai = db.Phongs
                 .OrderByDescending(lsp => lsp.phong_ma)
                 .FirstOrDefault();

            string newMaPhong;
            if (lastLoai != null)
            {
                int lastNumber = int.Parse(lastLoai.phong_ma.Substring(3));
                int newNumber = lastNumber + 1;
                newMaPhong = "PHG" + newNumber.ToString("D2");
            }
            else
            {
                newMaPhong = "PHG01";
            }

            if (model.phong_linkanhchinh_file != null)
            {
                var fileName = Path.GetFileName(model.phong_linkanhchinh_file.FileName);
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;

                var path = Path.Combine(Server.MapPath("~/Content/Images"), uniqueFileName);
                model.phong_linkanhchinh_file.SaveAs(path);

                // Lưu đường dẫn tương đối vào database
                model.phongs.phong_anhchinh = Url.Content("~/Content/Images/") + uniqueFileName;
            }
            else
            {
                // Nếu không có file, gán null cho trường sanpham_hinhanhchinh
                model.phongs.phong_anhchinh = null;
            }
            // Thêm phòng vào database
            model.phongs.phong_ma = newMaPhong;
            db.Phongs.Add(model.phongs);
            db.SaveChanges(); // Lưu để có được phong_ma

            // Xử lý lưu các ảnh liên quan đến phòng
            if (model.linkanh_files != null && model.linkanh_files.Count > 0)
            {
                foreach (var file in model.linkanh_files)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = System.IO.Path.GetFileName(file.FileName);
                        var path = System.IO.Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                        file.SaveAs(path);
                        // Tạo một bản ghi ChiTietAnhPhong mới
                        var chiTietAnhPhong = new ChiTietAnhPhong
                        {
                            phong_ma = model.phongs.phong_ma,
                            linkanh = "~/Content/Images" + fileName
                        };
                        db.ChiTietAnhPhongs.Add(chiTietAnhPhong);
                    }
                }
                db.SaveChanges(); // Lưu các ảnh vào database
            }
            return RedirectToAction("PhongListView");
            //}
            // Nếu có lỗi, trả về lại view với dữ liệu hiện tại
            //model.ChiTietAnhPhongs = db.ChiTietAnhPhongs.ToList();
            //return View(model);
        }
    }
}