using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebBanHang215.Models;

public partial class SanPham
{
    public int MaSp { get; set; }

    public string TenSp { get; set; } = null!;

    public string? MoTa { get; set; }

    public decimal? Gia { get; set; }

    public decimal? GiaNhap { get; set; }

    public decimal? GiaKhuyenMai { get; set; }

    public int? SoLuongNhap { get; set; }

    public int MaLoai { get; set; }

    public int MaHang { get; set; }

    public int MaNcc { get; set; }

    public int MaTrangThai { get; set; }

    public int MaGioiTinh { get; set; }

    public string? AnhDaiDien
    {
        get => AnhSanPhams.FirstOrDefault()?.Url;
    }

    public virtual ICollection<AnhSanPham> AnhSanPhams { get; set; } = new List<AnhSanPham>();

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();

    public virtual ICollection<ChiTietGioHang> ChiTietGioHangs { get; set; } = new List<ChiTietGioHang>();

    public virtual ICollection<ChiTietSanPham> ChiTietSanPhams { get; set; } = new List<ChiTietSanPham>();

    public virtual ICollection<KhuyenMaiSanPham> KhuyenMaiSanPhams { get; set; } = new List<KhuyenMaiSanPham>();

    [BindNever]
    public virtual GioiTinh MaGioiTinhNavigation { get; set; } = null!;

    [BindNever]
    public virtual HangSanXuat MaHangNavigation { get; set; } = null!;

    [BindNever]
    public virtual LoaiSanPham MaLoaiNavigation { get; set; } = null!;

    [BindNever]
    public virtual NhaCungCap MaNccNavigation { get; set; } = null!;

    [BindNever]
    public virtual TrangThaiSanPham MaTrangThaiNavigation { get; set; } = null!;

    public virtual ICollection<ChiTietNhapKho> ChiTietNhapKhos { get; set; } = new List<ChiTietNhapKho>();
}
