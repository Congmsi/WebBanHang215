using X.PagedList;

namespace WebBanHang215.Areas.admin.ViewModels
{
    public class ChiTietPhieuNhapListViewModel
    {
        public string MaPhieuNhap { get; set; } = null!;

        // ✅ Đổi từ List thành IPagedList để hỗ trợ phân trang
        public IPagedList<ChiTietPhieuNhapItemViewModel> DanhSachChiTiet { get; set; } = default!;
    }

    public class ChiTietPhieuNhapItemViewModel
    {
        public string MaChiTietNk { get; set; } = null!;

        public string MaPhieuNhap { get; set; } = null!;
        public int MaSp { get; set; }
        public string TenSp { get; set; } = null!;
        public int SoLuong { get; set; }
        public decimal DonGiaNhap { get; set; }
        public DateTime NgayNhap { get; set; } 
    }
}
