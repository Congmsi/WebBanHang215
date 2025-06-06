using System;
using System.Collections.Generic;

namespace WebBanHang215.ViewModels
{
    public class SanPhamChiTietViewModel
    {
        public int MaSp { get; set; }
        public string TenSp { get; set; } = string.Empty;
        public string? MoTa { get; set; }
        public decimal? Gia { get; set; }
        public decimal? GiaKhuyenMai { get; set; }
        public string TenLoai { get; set; } = string.Empty;
        public string TenHang { get; set; } = string.Empty;
        public string TenGioiTinh { get; set; } = string.Empty;
        public string TenTrangThai { get; set; } = string.Empty;
        public string TenNcc { get; set; } = string.Empty;

        public List<string?> UrlAnh { get; set; } = new();
        public List<ChiTietSPItem> ChiTietSanPham { get; set; } = new();
    }

    public class ChiTietSPItem
    {
        public string TenSize { get; set; } = string.Empty;
        public string TenMau { get; set; } = string.Empty;
        public int? SoLuongTon { get; set; }
        public DateTime? NgayNhap { get; set; }
    }
}

