using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GymPro.Models
{
    public class LoaiSanPham
    {
        [Key]
        [DisplayName("Mã loại sản phẩm")]

        public string loaisanpham_ma { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thông tin")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Vui lòng nhập độ dài ký tự từ {0} đến {2}")]
        [DisplayName("Tên loại sản phẩm")]
        public string loaisanpham_ten { get; set; }


        [DisplayName("Mô tả")]
        public string loaisanpham_mota { get; set; }

    }

}