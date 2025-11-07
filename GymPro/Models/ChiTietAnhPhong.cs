using System.ComponentModel.DataAnnotations;

namespace GymPro.Models
{
    public class ChiTietAnhPhong
    {
        [Key]
        public int id { get; set; }

        public string phong_ma { get; set; }


        public string linkanh { get; set; }

        // Navigation properties
        public virtual Phong Phongs { get; set; }
    }
}