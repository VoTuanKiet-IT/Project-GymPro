using GymPro.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace GymPro.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HoaDonController : Controller
    {
        private Models.ApplicationDbContext db;
        public HoaDonController()
        {
            db = new Models.ApplicationDbContext();
        }

        // GET: HoaDon List View   
        public ActionResult HoaDonListView()
        {
            var fillHoaDons = db.HoaDons.Where(hd => hd.isDeleted == false).ToList();
            var viewModel = new HoaDonViewModel
            {
                HoaDons = fillHoaDons.Select(hdd => new HoaDonTableRow
                {
                    hoadon = hdd,
                    hoivien = db.HoiViens.FirstOrDefault(hv => hv.hoivien_ma == hdd.hoivien_ma),
                    ngaylap = DateTimeOffset.FromUnixTimeSeconds(hdd.hoadon_ngaylap).LocalDateTime
                }).ToList()
            };
            return View(viewModel);
        }

        //Get: Tạo HoaDon
        public ActionResult TaoHoaDon()
        {

            var sanPhams = db.SanPhams.Where(sp => sp.isDeleted == false).ToList();
            var goiTaps = db.GoiTaps.Where(gt => gt.isDelete == false).ToList();

            var sanPhamItems = sanPhams.Select(sp => new ItemHoaDon
            {
                Ma = sp.sanpham_ma,
                TenHienThi = "SP - " + sp.sanpham_ten,
                DonGia = sp.sanpham_gia
            });
            var goiTapItems = goiTaps.Select(gt => new ItemHoaDon
            {
                Ma = gt.goitap_ma,
                TenHienThi = "GT - " + gt.goitap_tengoi,
                DonGia = gt.goitap_gia
            });
            var tatCaMatHang = sanPhamItems.Union(goiTapItems).OrderBy(item => item.TenHienThi).ToList();

            var viewModel = new SetHoaDonViewModel
            {
                HoiViens = db.HoiViens.Where(hv => hv.isDeleted == false).ToList(),
                TatCaMatHangs = tatCaMatHang,
                ngaylap = System.DateTime.Now
            };



            return View(viewModel);
        }

        //Post: Tạo HoaDon
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TaoHoaDon(SetHoaDonViewModel model)
        {
            // Cần kiểm tra xem có chi tiết hóa đơn nào được gửi lên không
            if (model.ChiTietHoaDons == null || !model.ChiTietHoaDons.Any())
            {
                ModelState.AddModelError("", "Vui lòng thêm ít nhất một sản phẩm hoặc gói tập vào hóa đơn.");
            }

            if (ModelState.IsValid)
            {
                var lastHoaDon = db.HoaDons
                                 .OrderByDescending(hd => hd.hoadon_ma)
                                 .FirstOrDefault();
                string newMaHoaDon;

                if (lastHoaDon != null)
                {
                    int lastNumber = int.Parse(lastHoaDon.hoadon_ma.Substring(4));
                    int newNumber = lastNumber + 1;
                    newMaHoaDon = "HDGT" + newNumber.ToString("D4");
                }
                else
                {
                    newMaHoaDon = "HDGT0001";
                }

                // 2. LƯU HÓA ĐƠN CHÍNH
                var newHoaDon = new HoaDon
                {
                    hoadon_ma = newMaHoaDon,
                    hoivien_ma = model.selected_hoivien_ma,
                    hoadon_ngaylap = new DateTimeOffset(model.ngaylap).ToUnixTimeSeconds(),
                    hoadon_sotien = model.ChiTietHoaDons.Sum(ct => ct.cthoadon_thanhtien)
                };
                db.HoaDons.Add(newHoaDon);

                // 3. LƯU CHI TIẾT HÓA ĐƠN
                int chiTietCounter = 1;
                foreach (var itemRow in model.ChiTietHoaDons)
                {
                    string selectedMa = itemRow.chihoa_items.Ma;

                    var newChiTiet = new ChiTietHoaDon
                    {
                        // Tạo mã chi tiết (ví dụ: HDGT000101, HDGT000102)
                        cthoadon_ma = newMaHoaDon + chiTietCounter.ToString("D2"),
                        hoadon_ma = newMaHoaDon,

                        // Xác định loại mặt hàng và lưu vào cột tương ứng
                        sanpham_ma = selectedMa.StartsWith("SP") ? selectedMa : null,
                        goitap_ma = selectedMa.StartsWith("GT") ? selectedMa : null,

                        cthoadon_dongia = itemRow.chihoa_items.DonGia,
                        cthoadon_soluong = itemRow.cthoadon_soluong
                    };

                    db.ChiTietHoaDons.Add(newChiTiet);
                    chiTietCounter++;
                }

                // 4. GHI NHẬN TẤT CẢ THAY ĐỔI
                db.SaveChanges();
                return RedirectToAction("HoaDonListView");
            }

            // Nếu có lỗi, load lại dữ liệu cho DropDownList và trả về View
            model.HoiViens = db.HoiViens.Where(hv => hv.isDeleted == false).ToList();
            model.TatCaMatHangs = db.SanPhams
                                    .Where(sp => sp.isDeleted == false)
                                    .Select(sp => new ItemHoaDon { Ma = sp.sanpham_ma, TenHienThi = "SP - " + sp.sanpham_ten, DonGia = sp.sanpham_gia })
                                    .Union(db.GoiTaps
                                            .Where(gt => gt.isDelete == false)
                                            .Select(gt => new ItemHoaDon { Ma = gt.goitap_ma, TenHienThi = "GT - " + gt.goitap_tengoi, DonGia = gt.goitap_gia }))
                                    .OrderBy(item => item.TenHienThi)
                                    .ToList();

            return View(model);
        }




    }
}