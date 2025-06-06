using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebBanHang215.Areas.admin.ViewModels
{
    public class ChiTietPhieuNhapCreateViewModel
    {
        public string MaPhieuNhap { get; set; } = null!;
        public string? MaChiTietNk { get; set; } 

        [Required]
        public int MaSp { get; set; }
        public string? TenSanPham { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int SoLuong { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Đơn giá phải không âm")]
        public decimal DonGiaNhap { get; set; }
        public DateTime? NgayNhap { get; set; }

        public IEnumerable<SelectListItem>? SanPhamList { get; set; }
    }
}
