using System.ComponentModel.DataAnnotations;

namespace GymPro.Models
{
    public class ChiTietAnhHLV
    {
        [Key]
        public int id { get; set; }

        public string hlv_ma { get; set; }


        public string linkanh { get; set; }

        // Navigation properties
        public virtual HuanLuyenVien HuanLuyenViens { get; set; }
    }
}