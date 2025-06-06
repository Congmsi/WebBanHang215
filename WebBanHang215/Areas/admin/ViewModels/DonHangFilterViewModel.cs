using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;
using X.PagedList.Extensions;

namespace WebBanHang215.Areas.admin.ViewModels
{
    public class DonHangFilterViewModel
    {
        //  Mã đơn hàng
        public int? MaDh { get; set; }
        public bool LocMaDh { get; set; }

        //  Mã sản phẩm
        public int? MaSp { get; set; }
        public string? TenSp { get; set; }
        public bool LocSanPham { get; set; }
        public IEnumerable<SelectListItem>? SanPhamList { get; set; }

        //  Từ khóa (tên người dùng, SDT, email… nếu mở rộng)
        public string? TuKhoa { get; set; }
        public bool LocTuKhoa { get; set; }

        //  Trạng thái đơn hàng
        public string? TrangThai { get; set; }
        public bool LocTrangThai { get; set; }
        public IEnumerable<SelectListItem>? TrangThaiList { get; set; }

        //  Khoảng ngày đặt
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
        public bool LocNgay { get; set; }

        //  Tổng tiền
        public decimal? TongTienTu { get; set; }
        public decimal? TongTienDen { get; set; }
        public bool LocTien { get; set; }

        public Dictionary<string, int> ThongKeTheoTrangThai { get; set; } = new();

        //  Kết quả phân trang
        public IPagedList<WebBanHang215.Models.DonHang> KetQua { get; set; }
     = new List<WebBanHang215.Models.DonHang>().ToPagedList(1, 1);
    }
}
