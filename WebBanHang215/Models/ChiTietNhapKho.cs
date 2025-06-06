using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebBanHang215.Models;

namespace WebBanHang215.Models
{

    [Table("ChiTietNhapKho")]
    public class ChiTietNhapKho
    {
        [Key]
        [StringLength(30)]
        public string MaChiTietNk { get; set; } = null!;

        [Required]
        [StringLength(20)]
        public string MaPhieuNhap { get; set; } = null!;

        public int MaSp { get; set; }

        public int SoLuong { get; set; }

        public decimal DonGiaNhap { get; set; }

        [ForeignKey("MaPhieuNhap")]
        public NhapKho NhapKho { get; set; } = null!;

        [ForeignKey("MaSp")]
        public SanPham SanPham { get; set; } = null!;
    }
}