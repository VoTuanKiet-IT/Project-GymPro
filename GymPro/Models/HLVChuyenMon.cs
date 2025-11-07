using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymPro.Models
{
    public class HLVChuyenMon
    {
        [Key]
        [Column(Order = 1)]
        public string hlv_ma { get; set; }

        [Key]
        [Column(Order = 2)]
        public string chuyenmon_ma { get; set; }

        // Navigation properties
        public virtual HuanLuyenVien HuanLuyenVien { get; set; }
        public virtual ChuyenMon ChuyenMon { get; set; }
    }
}