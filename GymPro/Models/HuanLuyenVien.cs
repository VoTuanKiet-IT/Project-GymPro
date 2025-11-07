using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GymPro.Models
{
    public class HuanLuyenVien
    {
        [Key]
        public string hlv_ma { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống.")]
        [DisplayName("Họ đệm")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Vui lòng nhập độ dài ký tự từ {0} đến {2}")]
        public string hlv_hodem { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống.")]
        [DisplayName("Tên")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Vui lòng nhập độ dài ký tự từ {0} đến {2}")]
        public string hlv_ten { get; set; }

        [EmailAddress(ErrorMessage = "Email không đúng định dạng.")]
        [Required(ErrorMessage = "Vui lòng nhập email.")]
        [DisplayName("Email")]
        public string hlv_email { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống.")]
        [StringLength(15, MinimumLength = 9, ErrorMessage = "Vui lòng nhập độ dài ký tự từ {0} đến {2}")]
        [DisplayName("Số điện thoại")]
        public string hlv_dienthoai { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống.")]
        [DisplayName("Giới tính")]
        public bool hlv_gioitinh { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống.")]
        [DisplayName("Ngày sinh")]
        public long hlv_ngaysinh { get; set; }

        [StringLength(255)]
        [DisplayName("Địa chỉ")]
        public string hlv_diachi { get; set; }

        [DisplayName("Hình đại diện")]
        public string hlv_hinhanh { get; set; }

        [DisplayName("Ngày nhận việc")]
        public long hlv_ngaynhanviec { get; set; }

        [DefaultValue(false)]
        public bool isDeleted { get; set; }

        [DisplayName("Tên HLV")]
        public string TenDayDu
        {
            get { return hlv_hodem + " " + hlv_ten; }
        }

        public virtual ICollection<HLVChuyenMon> HLVChuyenMons { get; set; }
        public virtual ICollection<ChiTietAnhHLV> ChiTietAnhHLVs { get; set; }


    }


}