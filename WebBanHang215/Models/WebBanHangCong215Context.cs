using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebBanHang215.Models;

public partial class WebBanHangCong215Context : DbContext
{
    public WebBanHangCong215Context()
    {
    }

    public WebBanHangCong215Context(DbContextOptions<WebBanHangCong215Context> options)
        : base(options)
    {
    }

    public virtual DbSet<AnhSanPham> AnhSanPhams { get; set; }

    public virtual DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }

    public virtual DbSet<ChiTietGioHang> ChiTietGioHangs { get; set; }

    public virtual DbSet<ChiTietSanPham> ChiTietSanPhams { get; set; }

    public virtual DbSet<DonHang> DonHangs { get; set; }

    public virtual DbSet<GioHang> GioHangs { get; set; }

    public virtual DbSet<GioiTinh> GioiTinhs { get; set; }

    public virtual DbSet<HangSanXuat> HangSanXuats { get; set; }

    public virtual DbSet<KhuyenMai> KhuyenMais { get; set; }

    public virtual DbSet<KhuyenMaiSanPham> KhuyenMaiSanPhams { get; set; }

    public virtual DbSet<LoaiSanPham> LoaiSanPhams { get; set; }

    public virtual DbSet<MauSac> MauSacs { get; set; }

    public virtual DbSet<NguoiDung> NguoiDungs { get; set; }

    public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }

    public virtual DbSet<SanPham> SanPhams { get; set; }

    public virtual DbSet<Size> Sizes { get; set; }

    public virtual DbSet<TrangThaiSanPham> TrangThaiSanPhams { get; set; }

 

    public virtual DbSet<NhapKho> NhapKhos { get; set; } = null!;
    public virtual DbSet<ChiTietNhapKho> ChiTietNhapKhos { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer("Data Source=MACDUONG;Initial Catalog=WebBanHangCong215;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnhSanPham>(entity =>
        {
            entity.HasKey(e => e.MaAnh).HasName("PK__AnhSanPh__356240DFD406E406");

            entity.ToTable("AnhSanPham");

            entity.Property(e => e.MoTa).HasMaxLength(255);
            entity.Property(e => e.Url)
                .HasMaxLength(255)
                .HasColumnName("URL");

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.AnhSanPhams)
                .HasForeignKey(d => d.MaSp)
                .HasConstraintName("FK_AnhSanPham_SanPham");
        });

        modelBuilder.Entity<ChiTietDonHang>(entity =>
        {
            entity.HasKey(e => e.MaChiTietDh).HasName("PK__ChiTietD__651E6E6AF0163EB8");

            entity.ToTable("ChiTietDonHang");

            entity.Property(e => e.MaChiTietDh).HasColumnName("MaChiTietDH");
            entity.Property(e => e.DonGia).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.MaDhNavigation).WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.MaDh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CTDonHang_DonHang");

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.MaSp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CTDonHang_SanPham");
        });

        modelBuilder.Entity<ChiTietGioHang>(entity =>
        {
            entity.HasKey(e => e.MaChiTietGh).HasName("PK__ChiTietG__651E71CECF02EB26");

            entity.ToTable("ChiTietGioHang");

            entity.Property(e => e.MaChiTietGh).HasColumnName("MaChiTietGH");

            entity.HasOne(d => d.MaGioHangNavigation).WithMany(p => p.ChiTietGioHangs)
                .HasForeignKey(d => d.MaGioHang)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CTGioHang_GioHang");

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.ChiTietGioHangs)
                .HasForeignKey(d => d.MaSp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CTGioHang_SanPham");
        });

        modelBuilder.Entity<ChiTietSanPham>(entity =>
        {
            entity.HasKey(e => e.MaCtsp).HasName("PK__ChiTietS__368D46B3F49A3762");

            entity.ToTable("ChiTietSanPham");

            entity.HasOne(d => d.MaMauNavigation).WithMany(p => p.ChiTietSanPhams)
                .HasForeignKey(d => d.MaMau)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CTSP_Mau");

            entity.HasOne(d => d.MaSizeNavigation).WithMany(p => p.ChiTietSanPhams)
                .HasForeignKey(d => d.MaSize)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CTSP_Size");

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.ChiTietSanPhams)
                .HasForeignKey(d => d.MaSp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CTSP_SanPham");
        });

        modelBuilder.Entity<DonHang>(entity =>
        {
            entity.HasKey(e => e.MaDh).HasName("PK__DonHang__27258641201D96E5");

            entity.ToTable("DonHang");

            entity.Property(e => e.NgayDatHang)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TongTien).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TrangThai).HasMaxLength(50);

            entity.HasOne(d => d.MaKmNavigation).WithMany(p => p.DonHangs)
                .HasForeignKey(d => d.MaKm)
                .HasConstraintName("FK_DonHang_KhuyenMai");

            entity.HasOne(d => d.MaNdNavigation).WithMany(p => p.DonHangs)
                .HasForeignKey(d => d.MaNd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DonHang_NguoiDung");
        });

        modelBuilder.Entity<GioHang>(entity =>
        {
            entity.HasKey(e => e.MaGioHang).HasName("PK__GioHang__F5001DA3CE6ED011");

            entity.ToTable("GioHang");

            entity.Property(e => e.NgayTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MaNdNavigation).WithMany(p => p.GioHangs)
                .HasForeignKey(d => d.MaNd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GioHang_NguoiDung");
        });

        modelBuilder.Entity<GioiTinh>(entity =>
        {
            entity.HasKey(e => e.MaGioiTinh).HasName("PK__GioiTinh__46AF33FAA3D4A0C2");

            entity.ToTable("GioiTinh");

            entity.Property(e => e.TenGioiTinh).HasMaxLength(50);
        });

        modelBuilder.Entity<HangSanXuat>(entity =>
        {
            entity.HasKey(e => e.MaHang).HasName("PK__HangSanX__19C0DB1D9931CE2C");

            entity.ToTable("HangSanXuat");

            entity.Property(e => e.TenHang).HasMaxLength(100);
        });

        modelBuilder.Entity<KhuyenMai>(entity =>
        {
            entity.HasKey(e => e.MaKm).HasName("PK__KhuyenMa__2725CF75552DF546");

            entity.ToTable("KhuyenMai");

            entity.Property(e => e.PhanTramGiam).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.TenKhuyenMai).HasMaxLength(100);
        });

        modelBuilder.Entity<KhuyenMaiSanPham>(entity =>
        {
            entity.HasKey(e => new { e.MaKm, e.MaSp });

            entity.ToTable("KhuyenMai_SanPham");

            entity.HasOne(d => d.MaKmNavigation).WithMany(p => p.KhuyenMaiSanPhams)
                .HasForeignKey(d => d.MaKm)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_KM_SP_KM");

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.KhuyenMaiSanPhams)
                .HasForeignKey(d => d.MaSp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_KM_SP_SP");
        });

        modelBuilder.Entity<LoaiSanPham>(entity =>
        {
            entity.HasKey(e => e.MaLoai).HasName("PK__LoaiSanP__730A575941BF782D");

            entity.ToTable("LoaiSanPham");

            entity.Property(e => e.TenLoai).HasMaxLength(100);
        });

        modelBuilder.Entity<MauSac>(entity =>
        {
            entity.HasKey(e => e.MaMau).HasName("PK__MauSac__3A5BBB7D4863F143");

            entity.ToTable("MauSac");

            entity.Property(e => e.MaMauHex)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.TenMau).HasMaxLength(50);
        });

        modelBuilder.Entity<NguoiDung>(entity =>
        {
            entity.HasKey(e => e.MaNd).HasName("PK__NguoiDun__2725D7048DBFE1DE");

            entity.ToTable("NguoiDung");

            entity.Property(e => e.DiaChi).HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.HoVaTen).HasMaxLength(100);
            entity.Property(e => e.MatKhau).HasMaxLength(100);
            entity.Property(e => e.SoDienThoai).HasMaxLength(20);
            entity.Property(e => e.TenDangNhap).HasMaxLength(50);
            entity.Property(e => e.VaiTro).HasMaxLength(20);
        });

        modelBuilder.Entity<NhaCungCap>(entity =>
        {
            entity.HasKey(e => e.MaNcc).HasName("PK__NhaCungC__3A1951E314162D6D");

            entity.ToTable("NhaCungCap");

            entity.Property(e => e.DiaChi).HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.SoDienThoai).HasMaxLength(20);
            entity.Property(e => e.TenNhaCungCap).HasMaxLength(100);
        });

        modelBuilder.Entity<SanPham>(entity =>
        {
            entity.HasKey(e => e.MaSp).HasName("PK__SanPham__2725087C19D07A26");

            entity.ToTable("SanPham");

            entity.Property(e => e.Gia).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.GiaKhuyenMai).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.GiaNhap).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TenSp).HasMaxLength(100);

            entity.HasOne(d => d.MaGioiTinhNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaGioiTinh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SanPham_GioiTinh");

            entity.HasOne(d => d.MaHangNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaHang)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SanPham_HangSanXuat");

            entity.HasOne(d => d.MaLoaiNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaLoai)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SanPham_LoaiSanPham");

            entity.HasOne(d => d.MaNccNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaNcc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SanPham_NhaCungCap");

            entity.HasOne(d => d.MaTrangThaiNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaTrangThai)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SanPham_TrangThai");
        });

        modelBuilder.Entity<Size>(entity =>
        {
            entity.HasKey(e => e.MaSize).HasName("PK__Size__A787E7ED6018E466");

            entity.ToTable("Size");

            entity.Property(e => e.LoaiSize).HasMaxLength(50);
            entity.Property(e => e.MoTa).HasMaxLength(250);
        });

        modelBuilder.Entity<TrangThaiSanPham>(entity =>
        {
            entity.HasKey(e => e.MaTrangThai).HasName("PK__TrangTha__AADE41382D6E30CA");

            entity.ToTable("TrangThaiSanPham");

            entity.Property(e => e.TenTrangThai).HasMaxLength(100);
        });

        modelBuilder.Entity<NhapKho>(entity =>
        {
            entity.HasKey(e => e.MaPhieuNhap);
            entity.Property(e => e.MaPhieuNhap).HasMaxLength(20);
            entity.Property(e => e.NgayNhap).IsRequired();
        });


        modelBuilder.Entity<ChiTietNhapKho>(entity =>
        {
            entity.HasKey(e => e.MaChiTietNk);
            entity.Property(e => e.MaChiTietNk).HasMaxLength(30);
            entity.Property(e => e.DonGiaNhap).HasColumnType("decimal(18,2)");

            entity.HasOne(e => e.SanPham)
                  .WithMany(sp => sp.ChiTietNhapKhos)
                  .HasForeignKey(e => e.MaSp)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.NhapKho)
                  .WithMany(nk => nk.ChiTietNhapKhos)
                  .HasForeignKey(e => e.MaPhieuNhap)
                  .OnDelete(DeleteBehavior.Cascade);
        });









        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
