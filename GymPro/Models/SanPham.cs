using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GymPro.Models
{
    public class SanPham
    {
        [Key]
        public string sanpham_ma { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thông tin.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Vui lòng nhập độ dài ký tự từ {0} đến {2}")]
        [DisplayName("Tên sản phẩm")]
        public string sanpham_ten { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giá.")]
        [DisplayName("Giá")]
        public int sanpham_gia { get; set; }

        [DisplayName("Hình ảnh")]
        public string sanpham_hinhanhchinh { get; set; }
        public string loaisanpham_ma { get; set; }
        public LoaiSanPham loaisanpham { get; set; }

        [DisplayName("Mô tả")]
        public string sanpham_mota { get; set; }

        [DefaultValue(false)]
        public bool isDeleted { get; set; }

    }

}