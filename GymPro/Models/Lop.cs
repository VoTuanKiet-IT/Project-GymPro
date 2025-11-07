using System.ComponentModel.DataAnnotations;

namespace GymPro.Models
{
    public class Lop
    {
        [Key]
        [Required]
        [Display(Name = "Mã lớp")]
        public string lop_ma { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống...")]
        [Display(Name = "Tên lớp")]
        public string lop_ten { get; set; }

        [Display(Name = "Mô tả")]
        public string lop_mota { get; set; }

        [Display(Name = "Ảnh lớp")]
        public string lop_anh { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn huấn luyện viên")]
        [Display(Name = "Mã huấn luyện viên")]
        public string hlv_ma { get; set; }
        public HuanLuyenVien HuanLuyenVien { get; set; }
    }
}