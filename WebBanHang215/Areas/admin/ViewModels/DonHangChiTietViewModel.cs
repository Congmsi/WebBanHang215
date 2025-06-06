using System;

namespace WebBanHang215.Areas.admin.ViewModels
{
    public class DonHangChiTietViewModel
    {
        public int MaDh { get; set; }
        public string? TenDangNhap { get; set; }
        public DateTime? NgayDatHang { get; set; }
        public decimal? TongTien { get; set; }
        public string? TrangThai { get; set; }

        // ✅ Bổ sung các trường mở rộng từ DonHang
        public string? DiaChiGiaoHang { get; set; }
        public string? HinhThucThanhToan { get; set; }
        public string? TrangThaiThanhToan { get; set; }
        public string? MaGiaoDich { get; set; }
        public DateTime? NgayGiaoDuKien { get; set; }
        public DateTime? NgayGiaoThucTe { get; set; }

        // Danh sách chi tiết sản phẩm
        public List<ChiTietSanPhamDonHangViewModel> ChiTietSanPhams { get; set; } = new();
    }

    public class ChiTietSanPhamDonHangViewModel
    {
        public int MaChiTiet { get; set; }
        public string TenSanPham { get; set; } = string.Empty;
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien => SoLuong * DonGia;
    }
}
