using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GymPro.Models
{
    public class TrangThai
    {
        [Key]
        public string trangthai_ma { get; set; }

        [Required]
        [DisplayName("Trạng thái")]
        public string trangthai_ten { get; set; }

        [Required]
        [DisplayName("Mô tả")]
        public string trangthai_mota { get; set; }

        [Required]
        [DisplayName("Bảng")]
        public string trangthai_mabang { get; set; }
    }
}