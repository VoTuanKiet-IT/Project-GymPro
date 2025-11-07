using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GymPro.Models
{
    public class HopDongHoiVien
    {
        [Key]
        [DisplayName("Mã hợp đồng")]
        public string hdhoivien_ma { get; set; }

        [DisplayName("Mã hội viên")]
        public string hoivien_ma { get; set; }
        public virtual HoiVien HoiViens { get; set; }

        [DisplayName("Mã gói tập")]
        public string goitap_ma { get; set; }
        public virtual GoiTap GoiTaps { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn thời gian")]
        [DisplayName("Ngày bắt đầu")]
        public long hdhoivien_ngaybatdau { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn thời gian")]
        [DisplayName("Ngày kết thúc")]
        public long hdhoivien_ngayketthuc { get; set; }

        [DisplayName("Trạng thái")]
        public string hdhoivien_trangthai { get; set; } // Active, Inactive, Cancelled
    }
}