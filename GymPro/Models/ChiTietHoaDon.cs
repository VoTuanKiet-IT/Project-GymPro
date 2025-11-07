using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GymPro.Models
{
    public class ChiTietHoaDon
    {
        [Key]
        [DisplayName("Mã")]
        public string cthoadon_ma { get; set; }

        [Required]
        [DisplayName("Số hóa đơn")]
        public string hoadon_ma { get; set; }
        public HoaDon HoaDons { get; set; }

        [DisplayName("Sản phẩm")]
        public string sanpham_ma { get; set; }
        public SanPham SanPhams { get; set; }

        [DisplayName("Gói tập")]
        public string goitap_ma { get; set; }
        public GoiTap GoiTaps { get; set; }

        [DisplayName("Đơn giá")]
        public decimal cthoadon_dongia { get; set; }

        [Required]
        [DisplayName("Số lượng")]
        [DefaultValue(0)]
        public int cthoadon_soluong { get; set; }
    }
}