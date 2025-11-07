using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GymPro.Models
{
    public class CaTap
    {
        [Key]
        public string catap_ma { get; set; }

        [Required]
        [DisplayName("Ca tập")]
        public string catap_ten { get; set; }

        [Required]
        [DisplayName("Thời gian(buổi)")]
        public int catap_giobatdau { get; set; }

        [Required]
        [DisplayName("Thời gian(giờ)")]
        public int catap_sogio { get; set; }

        [Required]
        [DisplayName("Mô tả")]
        public string catap_mota { get; set; }

        [DefaultValue(false)]
        public bool isDeleted { get; set; }

        public string catap_DropdownDisplay
        {
            get { return catap_ten + " " + catap_mota; }
        }
    }
}