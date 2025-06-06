using Microsoft.AspNetCore.Mvc.Rendering;
using WebBanHang215.Models;
using X.PagedList;

namespace WebBanHang215.Areas.admin.ViewModels
{
    public class PhieuNhapFilterViewModel
    {
        public string? MaPhieuNhap { get; set; }
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
        public int? MaNcc { get; set; }
        public decimal? TongTienTu { get; set; }
        public decimal? TongTienDen { get; set; }
        public string? TieuChiLoc { get; set; }
        public List<SelectListItem>? DanhSachNhaCungCap { get; set; }

        // Kết quả
        public IPagedList<NhapKho>? KetQua { get; set; }
    }
}
