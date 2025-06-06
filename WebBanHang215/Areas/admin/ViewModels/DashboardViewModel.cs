namespace WebBanHang215.Areas.admin.ViewModels
{
    public class DashboardViewModel
    {
        public int TongSanPham { get; set; }
        public int DonHangMoi { get; set; }
        public int SanPhamSapHet { get; set; }
        public decimal DoanhThuThang { get; set; }
        
        // Recent orders
        public List<DonHangGanDayItem> DonHangGanDay { get; set; } = new List<DonHangGanDayItem>();
        
        // Recent activities
        public List<HoatDongGanDayItem> HoatDongGanDay { get; set; } = new List<HoatDongGanDayItem>();
    }

    public class DonHangGanDayItem
    {
        public string MaDonHang { get; set; } = "";
        public string TenKhachHang { get; set; } = "";
        public decimal TongTien { get; set; }
        public string TrangThai { get; set; } = "";
        public DateTime NgayDat { get; set; }
        public string MauTrangThai { get; set; } = "";
    }

    public class HoatDongGanDayItem
    {
        public string TieuDe { get; set; } = "";
        public string ThoiGian { get; set; } = "";
        public string Icon { get; set; } = "";
        public string MauIcon { get; set; } = "";
    }
}
