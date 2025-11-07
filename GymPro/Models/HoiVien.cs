using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GymPro.Models
{
    public class HoiVien
    {

        [Key]
        [DisplayName("Mã hội viên")]
        public string hoivien_ma { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống.")]
        [DisplayName("Họ đệm")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Vui lòng nhập độ dài ký tự từ {0} đến {2}")]
        public string hoivien_hodem { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống.")]
        [DisplayName("Tên hội viên")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Vui lòng nhập độ dài ký tự từ {0} đến {2}")]
        public string hoivien_ten { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống.")]
        [EmailAddress]
        [DisplayName("Email")]
        public string hoivien_email { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống.")]
        [StringLength(15, MinimumLength = 9, ErrorMessage = "Vui lòng nhập độ dài ký tự từ {0} đến {2}")]
        [DisplayName("Số điện thoại")]
        public string hoivien_dienthoai { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống.")]
        [DisplayName("Giới tính")]
        public bool hoivien_gioitinh { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống.")]
        [DisplayName("Ngày sinh")]
        public long hoivien_ngaysinh { get; set; }

        [StringLength(255)]
        [DisplayName("Địa chỉ")]
        public string hoivien_diachi { get; set; }

        [DisplayName("Ngày đăng ký")]
        public long hoivien_ngaydangky { get; set; }

        [DisplayName("Hình đại diện")]
        public string hoivien_hinhanh { get; set; }

        [DefaultValue(false)]
        public bool isDeleted { get; set; }

        public string TenDayDu
        {
            get { return hoivien_hodem + " " + hoivien_ten; }
        }


    }
}