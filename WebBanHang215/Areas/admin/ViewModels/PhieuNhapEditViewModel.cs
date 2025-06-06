using System.ComponentModel.DataAnnotations;

namespace WebBanHang215.Areas.admin.ViewModels
{
    public class PhieuNhapEditViewModel
    {
        public string MaPhieuNhap { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime NgayNhap { get; set; }

        public string? GhiChu { get; set; }
    }
}