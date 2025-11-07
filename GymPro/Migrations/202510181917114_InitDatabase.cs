namespace GymPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CaTaps",
                c => new
                    {
                        catap_ma = c.String(nullable: false, maxLength: 128),
                        catap_ten = c.String(nullable: false),
                        catap_giobatdau = c.Int(nullable: false),
                        catap_sogio = c.Int(nullable: false),
                        catap_mota = c.String(nullable: false),
                        isDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.catap_ma);
            
            CreateTable(
                "dbo.ChiTietAnhHLVs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        hlv_ma = c.String(maxLength: 128),
                        linkanh = c.String(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.HuanLuyenViens", t => t.hlv_ma)
                .Index(t => t.hlv_ma);
            
            CreateTable(
                "dbo.HuanLuyenViens",
                c => new
                    {
                        hlv_ma = c.String(nullable: false, maxLength: 128),
                        hlv_hodem = c.String(nullable: false, maxLength: 100),
                        hlv_ten = c.String(nullable: false, maxLength: 100),
                        hlv_email = c.String(nullable: false),
                        hlv_dienthoai = c.String(nullable: false, maxLength: 15),
                        hlv_gioitinh = c.Boolean(nullable: false),
                        hlv_ngaysinh = c.Long(nullable: false),
                        hlv_diachi = c.String(maxLength: 255),
                        hlv_hinhanh = c.String(),
                        hlv_ngaynhanviec = c.Long(nullable: false),
                        isDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.hlv_ma);
            
            CreateTable(
                "dbo.HLVChuyenMons",
                c => new
                    {
                        hlv_ma = c.String(nullable: false, maxLength: 128),
                        chuyenmon_ma = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.hlv_ma, t.chuyenmon_ma })
                .ForeignKey("dbo.ChuyenMons", t => t.chuyenmon_ma, cascadeDelete: true)
                .ForeignKey("dbo.HuanLuyenViens", t => t.hlv_ma, cascadeDelete: true)
                .Index(t => t.hlv_ma)
                .Index(t => t.chuyenmon_ma);
            
            CreateTable(
                "dbo.ChuyenMons",
                c => new
                    {
                        chuyenmon_ma = c.String(nullable: false, maxLength: 128),
                        chuyenmon_ten = c.String(nullable: false),
                        chuyenmon_mota = c.String(),
                    })
                .PrimaryKey(t => t.chuyenmon_ma);
            
            CreateTable(
                "dbo.ChiTietAnhPhongs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        phong_ma = c.String(maxLength: 128),
                        linkanh = c.String(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Phongs", t => t.phong_ma)
                .Index(t => t.phong_ma);
            
            CreateTable(
                "dbo.Phongs",
                c => new
                    {
                        phong_ma = c.String(nullable: false, maxLength: 128),
                        phong_ten = c.String(nullable: false),
                        phong_mota = c.String(),
                        phong_succhuatoida = c.Int(nullable: false),
                        phong_anhchinh = c.String(),
                        isDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.phong_ma);
            
            CreateTable(
                "dbo.ChiTietHoaDons",
                c => new
                    {
                        cthoadon_ma = c.String(nullable: false, maxLength: 128),
                        hoadon_ma = c.String(nullable: false, maxLength: 128),
                        sanpham_ma = c.String(maxLength: 128),
                        goitap_ma = c.String(maxLength: 128),
                        cthoadon_dongia = c.Decimal(nullable: false, precision: 18, scale: 2),
                        cthoadon_soluong = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.cthoadon_ma)
                .ForeignKey("dbo.GoiTaps", t => t.goitap_ma)
                .ForeignKey("dbo.HoaDons", t => t.hoadon_ma, cascadeDelete: true)
                .ForeignKey("dbo.SanPhams", t => t.sanpham_ma)
                .Index(t => t.hoadon_ma)
                .Index(t => t.sanpham_ma)
                .Index(t => t.goitap_ma);
            
            CreateTable(
                "dbo.GoiTaps",
                c => new
                    {
                        goitap_ma = c.String(nullable: false, maxLength: 128),
                        goitap_tengoi = c.String(nullable: false, maxLength: 100),
                        goitap_gia = c.Decimal(nullable: false, precision: 18, scale: 2),
                        goitap_thoihan = c.Int(nullable: false),
                        goitap_hinhanh = c.String(),
                        goitap_motagoi = c.String(),
                        isDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.goitap_ma);
            
            CreateTable(
                "dbo.HoaDons",
                c => new
                    {
                        hoadon_ma = c.String(nullable: false, maxLength: 128),
                        hoivien_ma = c.String(maxLength: 128),
                        hoadon_sotien = c.Decimal(nullable: false, precision: 18, scale: 2),
                        hoadon_ngaylap = c.Long(nullable: false),
                        hoadon_hinhthucthanhtoan = c.String(),
                        isDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.hoadon_ma)
                .ForeignKey("dbo.HoiViens", t => t.hoivien_ma)
                .Index(t => t.hoivien_ma);
            
            CreateTable(
                "dbo.HoiViens",
                c => new
                    {
                        hoivien_ma = c.String(nullable: false, maxLength: 128),
                        hoivien_hodem = c.String(nullable: false, maxLength: 100),
                        hoivien_ten = c.String(nullable: false, maxLength: 100),
                        hoivien_email = c.String(nullable: false),
                        hoivien_dienthoai = c.String(nullable: false, maxLength: 15),
                        hoivien_gioitinh = c.Boolean(nullable: false),
                        hoivien_ngaysinh = c.Long(nullable: false),
                        hoivien_diachi = c.String(maxLength: 255),
                        hoivien_ngaydangky = c.Long(nullable: false),
                        hoivien_hinhanh = c.String(),
                        isDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.hoivien_ma);
            
            CreateTable(
                "dbo.SanPhams",
                c => new
                    {
                        sanpham_ma = c.String(nullable: false, maxLength: 128),
                        sanpham_ten = c.String(nullable: false, maxLength: 100),
                        sanpham_gia = c.Int(nullable: false),
                        sanpham_hinhanhchinh = c.String(),
                        loaisanpham_ma = c.String(maxLength: 128),
                        sanpham_mota = c.String(),
                        isDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.sanpham_ma)
                .ForeignKey("dbo.LoaiSanPhams", t => t.loaisanpham_ma)
                .Index(t => t.loaisanpham_ma);
            
            CreateTable(
                "dbo.LoaiSanPhams",
                c => new
                    {
                        loaisanpham_ma = c.String(nullable: false, maxLength: 128),
                        loaisanpham_ten = c.String(nullable: false, maxLength: 100),
                        loaisanpham_mota = c.String(),
                    })
                .PrimaryKey(t => t.loaisanpham_ma);
            
            CreateTable(
                "dbo.HopDongHoiViens",
                c => new
                    {
                        hdhoivien_ma = c.String(nullable: false, maxLength: 128),
                        hoivien_ma = c.String(maxLength: 128),
                        goitap_ma = c.String(maxLength: 128),
                        hdhoivien_ngaybatdau = c.Long(nullable: false),
                        hdhoivien_ngayketthuc = c.Long(nullable: false),
                        hdhoivien_trangthai = c.String(),
                    })
                .PrimaryKey(t => t.hdhoivien_ma)
                .ForeignKey("dbo.GoiTaps", t => t.goitap_ma)
                .ForeignKey("dbo.HoiViens", t => t.hoivien_ma)
                .Index(t => t.hoivien_ma)
                .Index(t => t.goitap_ma);
            
            CreateTable(
                "dbo.LichTapVoiPTs",
                c => new
                    {
                        lichtap_ma = c.String(nullable: false, maxLength: 128),
                        hoivien_ma = c.String(maxLength: 128),
                        hlv_ma = c.String(maxLength: 128),
                        lichtap_ngaytap = c.Long(nullable: false),
                        catap_ma = c.String(nullable: false, maxLength: 128),
                        lichtap_trangthai = c.String(),
                    })
                .PrimaryKey(t => t.lichtap_ma)
                .ForeignKey("dbo.CaTaps", t => t.catap_ma, cascadeDelete: true)
                .ForeignKey("dbo.HoiViens", t => t.hoivien_ma)
                .ForeignKey("dbo.HuanLuyenViens", t => t.hlv_ma)
                .Index(t => t.hoivien_ma)
                .Index(t => t.hlv_ma)
                .Index(t => t.catap_ma);
            
            CreateTable(
                "dbo.Lops",
                c => new
                    {
                        lop_ma = c.String(nullable: false, maxLength: 128),
                        lop_ten = c.String(nullable: false),
                        lop_mota = c.String(),
                        lop_anh = c.String(),
                        hlv_ma = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.lop_ma)
                .ForeignKey("dbo.HuanLuyenViens", t => t.hlv_ma, cascadeDelete: true)
                .Index(t => t.hlv_ma);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ThietBis",
                c => new
                    {
                        thietbi_ma = c.String(nullable: false, maxLength: 128),
                        thietbi_ten = c.String(nullable: false, maxLength: 100),
                        thietbi_ngaymua = c.String(nullable: false),
                        thietbi_ngaybaotrigannhat = c.String(nullable: false),
                        phong_ma = c.String(maxLength: 128),
                        isDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.thietbi_ma)
                .ForeignKey("dbo.Phongs", t => t.phong_ma)
                .Index(t => t.phong_ma);
            
            CreateTable(
                "dbo.TrangThais",
                c => new
                    {
                        trangthai_ma = c.String(nullable: false, maxLength: 128),
                        trangthai_ten = c.String(nullable: false),
                        trangthai_mota = c.String(nullable: false),
                        trangthai_mabang = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.trangthai_ma);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ThietBis", "phong_ma", "dbo.Phongs");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Lops", "hlv_ma", "dbo.HuanLuyenViens");
            DropForeignKey("dbo.LichTapVoiPTs", "hlv_ma", "dbo.HuanLuyenViens");
            DropForeignKey("dbo.LichTapVoiPTs", "hoivien_ma", "dbo.HoiViens");
            DropForeignKey("dbo.LichTapVoiPTs", "catap_ma", "dbo.CaTaps");
            DropForeignKey("dbo.HopDongHoiViens", "hoivien_ma", "dbo.HoiViens");
            DropForeignKey("dbo.HopDongHoiViens", "goitap_ma", "dbo.GoiTaps");
            DropForeignKey("dbo.ChiTietHoaDons", "sanpham_ma", "dbo.SanPhams");
            DropForeignKey("dbo.SanPhams", "loaisanpham_ma", "dbo.LoaiSanPhams");
            DropForeignKey("dbo.ChiTietHoaDons", "hoadon_ma", "dbo.HoaDons");
            DropForeignKey("dbo.HoaDons", "hoivien_ma", "dbo.HoiViens");
            DropForeignKey("dbo.ChiTietHoaDons", "goitap_ma", "dbo.GoiTaps");
            DropForeignKey("dbo.ChiTietAnhPhongs", "phong_ma", "dbo.Phongs");
            DropForeignKey("dbo.HLVChuyenMons", "hlv_ma", "dbo.HuanLuyenViens");
            DropForeignKey("dbo.HLVChuyenMons", "chuyenmon_ma", "dbo.ChuyenMons");
            DropForeignKey("dbo.ChiTietAnhHLVs", "hlv_ma", "dbo.HuanLuyenViens");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.ThietBis", new[] { "phong_ma" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Lops", new[] { "hlv_ma" });
            DropIndex("dbo.LichTapVoiPTs", new[] { "catap_ma" });
            DropIndex("dbo.LichTapVoiPTs", new[] { "hlv_ma" });
            DropIndex("dbo.LichTapVoiPTs", new[] { "hoivien_ma" });
            DropIndex("dbo.HopDongHoiViens", new[] { "goitap_ma" });
            DropIndex("dbo.HopDongHoiViens", new[] { "hoivien_ma" });
            DropIndex("dbo.SanPhams", new[] { "loaisanpham_ma" });
            DropIndex("dbo.HoaDons", new[] { "hoivien_ma" });
            DropIndex("dbo.ChiTietHoaDons", new[] { "goitap_ma" });
            DropIndex("dbo.ChiTietHoaDons", new[] { "sanpham_ma" });
            DropIndex("dbo.ChiTietHoaDons", new[] { "hoadon_ma" });
            DropIndex("dbo.ChiTietAnhPhongs", new[] { "phong_ma" });
            DropIndex("dbo.HLVChuyenMons", new[] { "chuyenmon_ma" });
            DropIndex("dbo.HLVChuyenMons", new[] { "hlv_ma" });
            DropIndex("dbo.ChiTietAnhHLVs", new[] { "hlv_ma" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.TrangThais");
            DropTable("dbo.ThietBis");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Lops");
            DropTable("dbo.LichTapVoiPTs");
            DropTable("dbo.HopDongHoiViens");
            DropTable("dbo.LoaiSanPhams");
            DropTable("dbo.SanPhams");
            DropTable("dbo.HoiViens");
            DropTable("dbo.HoaDons");
            DropTable("dbo.GoiTaps");
            DropTable("dbo.ChiTietHoaDons");
            DropTable("dbo.Phongs");
            DropTable("dbo.ChiTietAnhPhongs");
            DropTable("dbo.ChuyenMons");
            DropTable("dbo.HLVChuyenMons");
            DropTable("dbo.HuanLuyenViens");
            DropTable("dbo.ChiTietAnhHLVs");
            DropTable("dbo.CaTaps");
        }
    }
}
