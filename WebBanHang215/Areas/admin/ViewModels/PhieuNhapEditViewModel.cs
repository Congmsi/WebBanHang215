using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebBanHang215.Areas.admin.ViewModels
{
    public class PhieuNhapEditViewModel
    {
        [Display(Name = "Mã phiếu nhập")]
        public string MaPhieuNhap { get; set; } = string.Empty;

        [Display(Name = "Ngày nhập")]
        [Required(ErrorMessage = "Vui lòng chọn ngày nhập")]
        [DataType(DataType.Date)]
        public DateTime NgayNhap { get; set; }

        [Display(Name = "Nhà cung cấp")]
        [Required(ErrorMessage = "Vui lòng chọn nhà cung cấp")]
        public int MaNcc { get; set; }

        [Display(Name = "Ghi chú")]
        [StringLength(500, ErrorMessage = "Ghi chú không được vượt quá 500 ký tự")]
        public string? GhiChu { get; set; }

        // Dropdown lists
        public IEnumerable<SelectListItem>? NhaCungCapList { get; set; }

        // Display properties
        public string? TenNhaCungCap { get; set; }
        public int TongSoMat { get; set; }
        public decimal TongTien { get; set; }
    }
}