using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GymPro.Models
{
    public class GoiTap
    {
        [Key]
        public string goitap_ma { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Vui lòng nhập độ dài ký tự từ {0} đến {2}")]
        [DisplayName("Tên gói tập")]
        public string goitap_tengoi { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống.")]

        [DisplayName("Giá (VNĐ)")]
        public decimal goitap_gia { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống.")]
        [DisplayName("Thời hạn (tháng)")]
        public int goitap_thoihan { get; set; } // in months

        [DisplayName("Hình ảnh")]
        public string goitap_hinhanh { get; set; }

        [DisplayName("Mô tả")]
        public string goitap_motagoi { get; set; }

        [DefaultValue(false)]
        public bool isDelete { get; set; }

    }
}