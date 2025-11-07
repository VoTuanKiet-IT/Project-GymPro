using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GymPro.Models
{
    public class ChuyenMon
    {
        [Key]
        public string chuyenmon_ma { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên chuyên môn")]
        [DisplayName("Tên chuyên môn")]
        public string chuyenmon_ten { get; set; }
        [DisplayName("Mô tả")]
        public string chuyenmon_mota { get; set; }

        public virtual ICollection<HLVChuyenMon> HLVChuyenMons { get; set; }

    }
    public class ThemChuyenMonViewModel
    {
        public ChuyenMon ChuyenMons { get; set; }
    }

}