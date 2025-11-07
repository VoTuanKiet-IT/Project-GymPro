using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GymPro.Models
{
    public class HoaDon
    {
        [Key]
        [DisplayName("Mã hóa đơn")]
        public string hoadon_ma { get; set; }

        [DisplayName("Mã hội viên")]
        public string hoivien_ma { get; set; }
        public HoiVien hoivien { get; set; }

        [DisplayName("Số tiền")]
        public decimal hoadon_sotien { get; set; }

        [DisplayName("Ngày thanh toán")]

        public long hoadon_ngaylap { get; set; }
        [DisplayName("Hình thức thanh toán")]
        public string hoadon_hinhthucthanhtoan { get; set; } // Cash, Credit Card, Bank Transfer

        [DefaultValue(false)]
        public bool isDeleted { get; set; }
    }
}