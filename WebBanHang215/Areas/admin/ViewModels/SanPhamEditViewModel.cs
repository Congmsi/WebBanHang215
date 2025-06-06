using System.ComponentModel.DataAnnotations;

namespace WebBanHang215.ViewModels
{
    public class SanPhamEditViewModel
    {
        [Required]
        public int MaSp { get; set; }

        [Required]
        public string TenSp { get; set; } = null!;

        public string? MoTa { get; set; }

        public decimal? Gia { get; set; }

        public decimal? GiaNhap { get; set; }

        public decimal? GiaKhuyenMai { get; set; }

        public int? SoLuongNhap { get; set; }

        [Required]
        public int MaLoai { get; set; }

        [Required]
        public int MaHang { get; set; }

        [Required]
        public int MaNcc { get; set; }

        [Required]
        public int MaTrangThai { get; set; }

        [Required]
        public int MaGioiTinh { get; set; }
    }
}
