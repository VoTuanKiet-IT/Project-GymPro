using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GymPro.Models
{
    public class LichTapVoiPT
    {
        [Key]
        [DisplayName("Mã")]
        public string lichtap_ma { get; set; }

        [DisplayName("Mã hội viên")]
        public string hoivien_ma { get; set; }
        public HoiVien HoiViens { get; set; }


        [DisplayName("Mã huấn luyện viên")]
        public string hlv_ma { get; set; }
        public HuanLuyenVien HuanLuyenViens { get; set; }

        [Required]
        [DisplayName("Ngày tập")]
        public long lichtap_ngaytap { get; set; }

        [Required]
        [DisplayName("Ca tập")]
        public string catap_ma { get; set; }
        public CaTap CaTaps { get; set; }

        [DisplayName("Trạng thái")]
        public string lichtap_trangthai { get; set; } // Scheduled, Completed, Cancelled
    }
}