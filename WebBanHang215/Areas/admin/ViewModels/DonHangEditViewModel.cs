/*using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebBanHang215.Areas.admin.ViewModels
{
    public class DonHangEditViewModel
    {
        public int MaDh { get; set; }

        [Required(ErrorMessage = "Chọn người đặt hàng")]
        public int MaNd { get; set; }

        [Required(ErrorMessage = "Nhập tổng tiền")]
        public decimal TongTien { get; set; }

        [Required(ErrorMessage = "Chọn trạng thái")]
        public string TrangThai { get; set; }

        public int? MaKm { get; set; }

        public DateTime? NgayDatHang { get; set; }

        // Dropdown
        public IEnumerable<SelectListItem>? NguoiDungList { get; set; }
        public IEnumerable<SelectListItem>? KhuyenMaiList { get; set; }
    }
}*/

using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebBanHang215.Areas.admin.ViewModels
{
    public class DonHangEditViewModel
    {
        [Display(Name = "Mã đơn hàng")]
        public int MaDh { get; set; }

        [Display(Name = "Người đặt hàng")]
        [Required(ErrorMessage = "Vui lòng chọn người đặt hàng")]
        public int MaNd { get; set; }

        [Display(Name = "Tổng tiền")]
        [Required(ErrorMessage = "Vui lòng nhập tổng tiền")]
        [Range(0, double.MaxValue, ErrorMessage = "Tổng tiền phải là số không âm")]
        public decimal TongTien { get; set; }

        [Display(Name = "Trạng thái đơn hàng")]
        [Required(ErrorMessage = "Vui lòng chọn trạng thái")]
        public string TrangThai { get; set; } = string.Empty;

        [Display(Name = "Khuyến mãi áp dụng")]
        public int? MaKm { get; set; }

        [Display(Name = "Ngày đặt hàng")]
        [DataType(DataType.DateTime)]
        public DateTime? NgayDatHang { get; set; }

        // Dropdown danh sách
        public IEnumerable<SelectListItem>? NguoiDungList { get; set; }
        public IEnumerable<SelectListItem>? KhuyenMaiList { get; set; }

        // Gợi ý mở rộng thêm nếu có trạng thái dưới dạng dropdown:
        public IEnumerable<SelectListItem>? TrangThaiList { get; set; }
    }
}

