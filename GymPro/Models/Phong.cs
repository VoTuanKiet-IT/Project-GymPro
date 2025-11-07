using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GymPro.Models
{
    public class Phong
    {
        [Key]
        [Required]
        [DisplayName("Mã phòng")]
        public string phong_ma { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên phòng")]
        [DisplayName("Tên phòng")]
        public string phong_ten { get; set; }

        [DisplayName("Mô tả")]
        public string phong_mota { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập sức chứa tối đa")]
        [DisplayName("Sức chứa tối đa")]
        public int phong_succhuatoida { get; set; }

        [DisplayName("Ảnh chính")]
        public string phong_anhchinh { get; set; }

        [DefaultValue(false)]
        public bool isDeleted { get; set; }
        public virtual ICollection<ChiTietAnhPhong> ChiTietAnhPhongs { get; set; }

    }
}