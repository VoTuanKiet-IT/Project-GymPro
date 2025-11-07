using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace GymPro.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<CaTap> CaTaps { get; set; }
        public DbSet<ChiTietAnhHLV> ChiTietAnhHLVs { get; set; }
        public DbSet<ChiTietAnhPhong> ChiTietAnhPhongs { get; set; }
        public DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public DbSet<ChuyenMon> ChuyenMons { get; set; }
        public DbSet<GoiTap> GoiTaps { get; set; }
        public DbSet<HLVChuyenMon> HLVChuyenMons { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<HoiVien> HoiViens { get; set; }
        public DbSet<HopDongHoiVien> HopDongHoiViens { get; set; }
        public DbSet<HuanLuyenVien> HuanLuyenViens { get; set; }
        public DbSet<LichTapVoiPT> LichTapVoiPTs { get; set; }
        public DbSet<LoaiSanPham> LoaiSanPhams { get; set; }
        public DbSet<Lop> Lops { get; set; }
        public DbSet<Phong> Phongs { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<ThietBi> ThietBis { get; set; }
        public DbSet<TrangThai> TrangThais { get; set; }










        //Lệnh thêm dữ liệu mẫu vào bảng
        //EntityFramework6\Enable-Migrations


        //EntityFramework6\Add-Migration InitialCreateModel
        //EntityFramework6\Update-Database
        //EntityFramework6\Add-Migration + Tên
        //EntityFramework6\Update-Database





        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}