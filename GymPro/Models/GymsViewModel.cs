using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace GymPro.Models
{
    public class ItemHoaDon
    {
        public string Ma { get; set; }
        public string TenHienThi { get; set; }
        public decimal DonGia { get; set; }
    }

    //ViewModel for HoaDon
    public class HoaDonViewModel
    {
        public List<HoaDonTableRow> HoaDons { get; set; }
    }
    public class HoaDonTableRow
    {
        public HoaDon hoadon { get; set; }
        public HoiVien hoivien { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ngaylap { get; set; }
    }
    public class SetHoaDonViewModel
    {
        public HoaDon HoaDons { get; set; }
        public List<HoiVien> HoiViens { get; set; }
        public List<ItemHoaDon> TatCaMatHangs { get; set; }

        public string selected_hoivien_ma { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ngaylap { get; set; }

        public List<ChiTietHoaDonRowAdd> ChiTietHoaDons { get; set; }
    }

    public class ChiTietHoaDonRowAdd
    {
        public ItemHoaDon chihoa_items { get; set; }
        public int cthoadon_soluong { get; set; }
        public decimal cthoadon_thanhtien
        {
            get { return chihoa_items.DonGia * cthoadon_soluong; }
        }
    }

    //ViewModel for LichTapVoiPT
    public class LichTapVoiPTViewModel
    {
        public List<LichTapVoiPTTableRow> LichTapVoiPTs { get; set; }
    }
    public class LichTapVoiPTTableRow
    {
        public LichTapVoiPT lichtapvoipt { get; set; }
        public HuanLuyenVien huanluyenvien { get; set; }
        public CaTap catap { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ngaytap { get; set; }
    }
    public class SetLichTapVoiPTViewModel
    {
        // Dữ liệu được truyền từ GET Action
        public string hoivien_ma { get; set; }
        public List<CaTap> CaTaps { get; set; }
        public List<HuanLuyenVien> HuanLuyenViens { get; set; }

        // Dữ liệu sẽ được POST lên
        public string selected_catap_ma { get; set; } // Dùng cho DropDownListFor
        public string selected_hlv_ma { get; set; } // Dùng cho DropDownListFor

        public LichTapVoiPT LichTapVoiPTs { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ngaytap { get; set; }
    }

    //ViewModel for HopDongHoiVien
    public class HopDongHoiVienViewModel
    {
        public List<HopDongHoiVienTableRow> HopDongHoiViens { get; set; }
    }
    public class HopDongHoiVienTableRow
    {
        public HopDongHoiVien hopdonghoivien { get; set; }
        public HoiVien hoivien { get; set; }
        public GoiTap goitap { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ngaybatdau { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ngayketthuc { get; set; }
    }
    public class SetHopDongHoiVienViewModel
    {
        public HopDongHoiVien HopDongHoiViens { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ngaybatdau { get; set; }

    }
    public class HopDongChoHoiVienViewModel
    {
        public HopDongHoiVien HopDongHoiViens { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ngaybatdau { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ngayketthuc { get; set; }
        public GoiTap GoiTaps { get; set; }
    }



    // ViewModel for SanPham with related LoaiSanPham
    public class ThemSanPhamViewModel
    {
        public SanPham SanPhams { get; set; }
        public IEnumerable<LoaiSanPham> LoaiSanPhams { get; set; }
        public HttpPostedFileBase sanpham_hinhanhchinh_file { get; set; }

    }
    public class SuaSanPhamViewModel
    {
        public SanPham SanPhams { get; set; }
        public IEnumerable<LoaiSanPham> LoaiSanPhams { get; set; }
        public HttpPostedFileBase sanpham_hinhanhchinh_file { get; set; }

    }
    // ViewModel for LoaiSanPham
    public class LoaiSanPhamViewModel
    {
        public LoaiSanPham LoaiSanPhams { get; set; }

    }
    public class ThemLoaiSanPhamViewModel
    {
        public LoaiSanPham LoaiSanPhams { get; set; }
    }

    // ViewModel for ThietBi with related Phong
    public class ThietBiViewModel
    {
        public ThietBi thietbis { get; set; }
        public IEnumerable<Phong> phong { get; set; }
    }
    public class ThemThietBiViewModel
    {
        public ThietBi thietbis { get; set; }
        public IEnumerable<Phong> phong { get; set; }
    }
    public class ThemPhongViewModel
    {
        public Phong phongs { get; set; }
        public IEnumerable<ChiTietAnhPhong> ChiTietAnhPhongs { get; set; }
        public HttpPostedFileBase phong_linkanhchinh_file { get; set; }

        public List<HttpPostedFileBase> linkanh_files { get; set; }

    }
    public class ChiTietPhongViewModel
    {
        public Phong phongs { get; set; }
        public IEnumerable<ChiTietAnhPhong> ChiTietAnhPhongs { get; set; }
        public List<ChiTietAnhPhong> ListAnhPhong { get; set; }
    }



    // ViewModel for HuanLuyenVien with related ChuyenMon
    public class HuanLuyenVienTableRow
    {
        public HuanLuyenVien HuyenLuanViens { get; set; }

        [DisplayName("Giới tính")]
        public string gioitinh { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Ngày sinh")]
        public DateTime ngaysinh { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Ngày nhận việc")]
        public DateTime ngaynhanviec { get; set; }
        [DisplayName("Hình ảnh")]

        public HttpPostedFileBase huanluyenvien_anhdaidien_file { get; set; }

    }
    public class HuanLuyenVienViewModel
    {
        public List<HuanLuyenVienTableRow> huanluyenvien { get; set; }
    }
    //public class HHuanLuyenVienViewModel
    //{
    //    public HuanLuyenVien HuanLuyenViens { get; set; }
    //    public IEnumerable<ChuyenMon> ChuyenMons { get; set; }
    //    public IEnumerable<HLVChuyenMon> HLVChuyenMons { get; set; }
    //    public string[] SelectedChuyenMonIds { get; set; }
    //}
    public class SetHuanLuyenVienViewModel
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ngaysinh { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ngaynhanviec { get; set; }

        public HuanLuyenVien HuanLuyenViens { get; set; }

        public IEnumerable<ChuyenMon> ChuyenMons { get; set; }

        public List<string> ChuyenMonDaChon { get; set; }
        public HttpPostedFileBase huanluyenvien_anhdaidien_file { get; set; }
    }
    public class ChiTietHuanLuyenVienViewModel
    {
        public HuanLuyenVien HuanLuyenViens { get; set; }
        public List<ChuyenMon> DSChuyenMons { get; set; }

        [DisplayName("Giới tính")]
        public string gioitinh { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Ngày sinh")]
        public DateTime ngaysinh { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Ngày nhận việc")]
        public DateTime ngaynhanviec { get; set; }

    }

    // ViewModel for GoiTap with related ChiTietGoiTap
    public class GoiTapViewModel
    {
        [DisplayName("STT")]
        public int STT { get; set; }
        public List<GoiTap> GoiTaps { get; set; }

    }
    public class SetGoiTapViewModel
    {
        public GoiTap GoiTaps { get; set; }
    }

    //ViewModel for HoiVien
    public class HoiVienTableRow
    {
        public HoiVien hoiviens { get; set; }

        [DisplayName("Giới tính")]
        public string gioitinh { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Ngày sinh")]
        public DateTime ngaysinh { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Ngày đăng ký")]
        public DateTime ngaydangky { get; set; }
        [DisplayName("Hình ảnh")]

        public HttpPostedFileBase hoivien_anhdaidien_file { get; set; }

    }
    public class HoiVienViewModel
    {
        public List<HoiVienTableRow> hoivien { get; set; }
    }
    public class SetHoiVienViewModel
    {
        public HoiVien HoiViens { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Ngày sinh")]
        public DateTime ngaysinh { get; set; }


        public HttpPostedFileBase hoivien_anhdaidien_file { get; set; }
    }
    public class ChiTietHoiVienViewModel
    {
        public HoiVienTableRow hoivien { get; set; }

        public List<HopDongHoiVienTableRow> listhopdong { get; set; }

        public List<LichTapVoiPTTableRow> listlichtap { get; set; }


    }





}