using Microsoft.EntityFrameworkCore;
using NVSP.Models;

namespace NVSP.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<TaiKhoan> TaiKhoans { get; set; }
        public DbSet<SinhVien> SinhViens { get; set; }
        public DbSet<GiangVien> GiangViens { get; set; }
        public DbSet<BcnKhoa> BcnKhoas { get; set; }
        public DbSet<BanToChuc> BanToChucs { get; set; }
        public DbSet<CanBoLop> CanBoLops { get; set; }
        public DbSet<DsRubrics> DsRubrics { get; set; }
        public DbSet<ChiTietRubric> ChiTietRubrics { get; set; }
        public DbSet<SuKien> SuKiens { get; set; }
        public DbSet<HoatDongThi> HoatDongThis { get; set; }
        public DbSet<BanGiamKhao> BanGiamKhaos { get; set; }
        public DbSet<HoatDongThamDu> HoatDongThamDus { get; set; }
        public DbSet<HoatDongHoTro> HoatDongHoTros { get; set; }
        public DbSet<DangKyCaNhan> DangKyCaNhans { get; set; }
        public DbSet<DangKyNhom> DangKyNhoms { get; set; }
        public DbSet<ThanhVienNhom> ThanhVienNhoms { get; set; }
        public DbSet<DangKyThamDu> DangKyThamDus { get; set; }
        public DbSet<DiemDanh> DiemDanhs { get; set; }
        public DbSet<ChamDiem> ChamDiems { get; set; }
        public DbSet<KetQua> KetQuas { get; set; }
        public DbSet<ThongTinGCN> ThongTinGCNs { get; set; }
        public DbSet<LogGCN> LogGCNs { get; set; }
        public DbSet<LogTraCuu> LogTraCuus { get; set; }
        public DbSet<DiemRenLuyen> DiemRenLuyens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaiKhoan>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.MaCaNhan).IsUnique();
            });

            modelBuilder.Entity<SinhVien>(entity =>
            {
                entity.HasKey(e => e.MaSinhVien);
                entity.HasOne(e => e.TaiKhoan)
                      .WithOne(e => e.SinhVien)
                      .HasForeignKey<SinhVien>(e => e.MaSinhVien)
                      .HasPrincipalKey<TaiKhoan>(e => e.MaCaNhan)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<GiangVien>(entity =>
            {
                entity.HasKey(e => e.MaGiangVien);
                entity.HasOne(e => e.TaiKhoan)
                      .WithOne(e => e.GiangVien)
                      .HasForeignKey<GiangVien>(e => e.MaGiangVien)
                      .HasPrincipalKey<TaiKhoan>(e => e.MaCaNhan)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<BcnKhoa>(entity =>
            {
                entity.HasKey(e => e.MaGiangVien);
                entity.HasOne(e => e.GiangVien)
                      .WithMany(e => e.BcnKhoas)
                      .HasForeignKey(e => e.MaGiangVien)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<BanToChuc>(entity =>
            {
                entity.HasKey(e => e.MaGiangVien);
                entity.HasOne(e => e.GiangVien)
                      .WithMany(e => e.BanToChucs)
                      .HasForeignKey(e => e.MaGiangVien)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<CanBoLop>(entity =>
            {
                entity.HasKey(e => e.MaSinhVien);
                entity.HasOne(e => e.SinhVien)
                      .WithMany(e => e.CanBoLops)
                      .HasForeignKey(e => e.MaSinhVien)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<DsRubrics>(entity =>
            {
                entity.HasKey(e => e.IdRubric);
            });

            modelBuilder.Entity<ChiTietRubric>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.TieuChi });
                entity.HasOne(e => e.DsRubrics)
                      .WithMany(e => e.ChiTietRubrics)
                      .HasForeignKey(e => e.Id)
                      .HasPrincipalKey(e => e.IdRubric)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<SuKien>(entity =>
            {
                entity.HasKey(e => e.IdSk);
                entity.HasIndex(e => new { e.TenSk, e.NamHoc }).IsUnique();
                entity.HasOne(e => e.BanToChuc)
                      .WithMany(e => e.SuKiens)
                      .HasForeignKey(e => e.MaBtc)
                      .HasPrincipalKey(e => e.MaGiangVien)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<HoatDongThi>(entity =>
            {
                entity.HasKey(e => e.IdHd);
                entity.HasIndex(e => new { e.TenHd, e.IdSk }).IsUnique();
                entity.HasOne(e => e.DsRubrics)
                      .WithMany(e => e.HoatDongThis)
                      .HasForeignKey(e => e.IdRubric)
                      .OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(e => e.SuKien)
                      .WithMany(e => e.HoatDongThis)
                      .HasForeignKey(e => e.IdSk)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<BanGiamKhao>(entity =>
            {
                entity.HasKey(e => new { e.IdHd, e.MaGv });
                entity.HasOne(e => e.HoatDongThi)
                      .WithMany(e => e.BanGiamKhaos)
                      .HasForeignKey(e => e.IdHd)
                      .OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(e => e.GiangVien)
                      .WithMany(e => e.BanGiamKhaos)
                      .HasForeignKey(e => e.MaGv)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<HoatDongThamDu>(entity =>
            {
                entity.HasKey(e => e.IdHdThamDu);
                entity.HasOne(e => e.HoatDongThi)
                      .WithMany(e => e.HoatDongThamDus)
                      .HasForeignKey(e => e.IdHd)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<HoatDongHoTro>(entity =>
            {
                entity.HasKey(e => e.IdHdHoTro);
                entity.HasOne(e => e.HoatDongThi)
                      .WithMany(e => e.HoatDongHoTros)
                      .HasForeignKey(e => e.IdHd)
                      .OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(e => e.GiangVien)
                      .WithMany(e => e.HoatDongHoTros)
                      .HasForeignKey(e => e.MaGv)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<DangKyCaNhan>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.HoatDongThi)
                      .WithMany(e => e.DangKyCaNhans)
                      .HasForeignKey(e => e.IdHd)
                      .OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(e => e.SinhVien)
                      .WithMany(e => e.DangKyCaNhans)
                      .HasForeignKey(e => e.MaSv)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<DangKyNhom>(entity =>
            {
                entity.HasKey(e => e.IdNhom);
                entity.HasIndex(e => e.MaThamGia).IsUnique();
                entity.HasOne(e => e.HoatDongThi)
                      .WithMany(e => e.DangKyNhoms)
                      .HasForeignKey(e => e.IdHd)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<ThanhVienNhom>(entity =>
            {
                entity.HasKey(e => new { e.IdNhom, e.MaSv });
                entity.HasOne(e => e.DangKyNhom)
                      .WithMany(e => e.ThanhVienNhoms)
                      .HasForeignKey(e => e.IdNhom)
                      .OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(e => e.SinhVien)
                      .WithMany(e => e.ThanhVienNhoms)
                      .HasForeignKey(e => e.MaSv)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<DangKyThamDu>(entity =>
            {
                entity.HasKey(e => new { e.MaSv, e.IdHd });
                entity.HasOne(e => e.SinhVien)
                      .WithMany(e => e.DangKyThamDus)
                      .HasForeignKey(e => e.MaSv)
                      .OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(e => e.HoatDongThamDu)
                      .WithMany(e => e.DangKyThamDus)
                      .HasForeignKey(e => e.IdHd)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<DiemDanh>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<ChamDiem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Diem)
                      .HasPrecision(5, 2);
                entity.HasOne(e => e.HoatDongThi)
                      .WithMany(e => e.ChamDiems)
                      .HasForeignKey(e => e.IdHdThi)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<KetQua>(entity =>
            {
                entity.HasKey(e => e.IdChamDiem);
                entity.HasOne(e => e.ChamDiem)
                      .WithOne(e => e.KetQua)
                      .HasForeignKey<KetQua>(e => e.IdChamDiem)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<ThongTinGCN>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.HoatDongThi)
                      .WithMany(e => e.ThongTinGCNs)
                      .HasForeignKey(e => e.IdHd)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<LogGCN>(entity =>
            {
                entity.HasKey(e => e.IdGcn);
                entity.HasOne(e => e.ThongTinGCN)
                      .WithMany(e => e.LogGCNs)
                      .HasForeignKey(e => e.IdGcn)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<LogTraCuu>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.ThongTinGCN)
                      .WithMany(e => e.LogTraCuus)
                      .HasForeignKey(e => e.IdGcn)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<DiemRenLuyen>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.SinhVien)
                      .WithMany(e => e.DiemRenLuyens)
                      .HasForeignKey(e => e.MaSv)
                      .OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}