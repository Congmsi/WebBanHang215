using System.ComponentModel.DataAnnotations;

namespace WebBanHang215.Areas.admin.ViewModels
{
    public class ChiTietDonHangEditViewModel
    {
        public int MaChiTiet { get; set; }
        public int MaDh { get; set; }

        public string TenSanPham { get; set; } = string.Empty;

        [Required]
        [Range(1, int.MaxValue)]
        public int SoLuong { get; set; }

        public decimal DonGia { get; set; }
        public decimal ThanhTien => DonGia * SoLuong;

        public int MaSp { get; set; } 
    }
}
