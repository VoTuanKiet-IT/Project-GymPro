using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GymPro.Models
{
    public class ThietBi
    {
        [Key]
        [DisplayName("Mã thiết bị")]
        public string thietbi_ma { get; set; }


        [Required(ErrorMessage = "Vui lòng nhập tên thiết bị")]
        [DisplayName("Tên thiết bị")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Vui lòng nhập độ dài ký tự từ {0} đến {2}")]
        public string thietbi_ten { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập ngày mua thiết bị")]
        [DisplayName("Ngày mua thiết bị")]
        public string thietbi_ngaymua { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập ngày bảo trì gần nhất")]
        [DisplayName("Ngày bảo trì gần")]
        public string thietbi_ngaybaotrigannhat { get; set; }

        public string phong_ma { get; set; }
        public Phong phong { get; set; }

        [DefaultValue(false)]
        public bool isDeleted { get; set; }
    }

}