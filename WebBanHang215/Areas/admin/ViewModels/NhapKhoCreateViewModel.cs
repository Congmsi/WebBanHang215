using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebBanHang215.Areas.admin.ViewModels
{
    public class NhapKhoCreateViewModel
    {
        [Required]
        public string MaPhieuNhap { get; set; } = null!;

        public string? GhiChu { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn nhà cung cấp.")]
        public int? MaNcc { get; set; }
        public List<ChiTietNhapViewModel> ChiTietNhap { get; set; } = new();
        public List<SelectListItem> NhaCungCapList { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> SanPhamList { get; set; } = new List<SelectListItem>();
    }
    public class ChiTietNhapViewModel
    {
        public string MaSp { get; set; } = null!;
        public int SoLuong { get; set; }
        public decimal DonGiaNhap { get; set; }
    }
}
