using System;

namespace WebBanHang215.ViewModels
{
    public class SanPhamDetailViewModel
    {
        public int MaSp { get; set; }
        public string TenSp { get; set; } = null!;
        public string? MoTa { get; set; }
        public decimal? Gia { get; set; }
        public decimal? GiaNhap { get; set; }
        public decimal? GiaKhuyenMai { get; set; }
        public int? SoLuongNhap { get; set; }

        // Thông tin mô tả
        public string TenLoai { get; set; } = null!;
        public string TenHang { get; set; } = null!;
        public string TenNcc { get; set; } = null!;
        public string TenTrangThai { get; set; } = null!;
        public string TenGioiTinh { get; set; } = null!;

        // Ảnh đại diện
        public string? AnhDaiDien { get; set; }
    }
}
