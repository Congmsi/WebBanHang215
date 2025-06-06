using System.ComponentModel.DataAnnotations;

namespace WebBanHang215.Areas.admin.ViewModels
{
    public class ChiTietPhieuNhapViewModel
    {
        [Required]
        public string MaPhieuNhap { get; set; } = string.Empty;

        [Required]
        public int MaSp { get; set; } 

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int SoLuongNhap { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Đơn giá phải >= 0")]
        public decimal DonGiaNhap { get; set; }
    }
}