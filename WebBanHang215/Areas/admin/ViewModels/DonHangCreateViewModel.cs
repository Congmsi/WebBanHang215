/*using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebBanHang215.Areas.admin.ViewModels
{
    public class DonHangCreateViewModel
    {
        [Required]
        [Display(Name = "Người đặt hàng")]
        public int MaNd { get; set; }

        [Required]
        [Display(Name = "Tổng tiền")]
        public decimal TongTien { get; set; }

        [Required]
        [Display(Name = "Trạng thái")]
        public string TrangThai { get; set; }

        [Display(Name = "Khuyến mãi")]
        public int? MaKm { get; set; }

        public DateTime? NgayDatHang { get; set; }

        // Danh sách dropdown
        public IEnumerable<SelectListItem>? NguoiDungList { get; set; }
        public IEnumerable<SelectListItem>? KhuyenMaiList { get; set; }
    }

}*/

using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebBanHang215.Areas.admin.ViewModels
{
    public class DonHangCreateViewModel
    {
        [Required]
        [Display(Name = "Người đặt hàng")]
        public int MaNd { get; set; }

        [Required]
        [Display(Name = "Tổng tiền")]
        public decimal TongTien { get; set; }

        [Required]
        [Display(Name = "Trạng thái đơn")]
        public string TrangThai { get; set; }

        [Display(Name = "Khuyến mãi")]
        public int? MaKm { get; set; }

        [Display(Name = "Ngày đặt hàng")]
        public DateTime? NgayDatHang { get; set; }

        //Bổ sung trường mở rộng:

        [Display(Name = "Địa chỉ giao hàng")]
        public string? DiaChiGiaoHang { get; set; }

        [Display(Name = "Hình thức thanh toán")]
        public string? HinhThucThanhToan { get; set; }

        [Display(Name = "Trạng thái thanh toán")]
        public string? TrangThaiThanhToan { get; set; }

        [Display(Name = "Mã giao dịch")]
        public string? MaGiaoDich { get; set; }

        [Display(Name = "Ngày giao dự kiến")]
        [DataType(DataType.Date)]
        public DateTime? NgayGiaoDuKien { get; set; }

        [Display(Name = "Ngày giao thực tế")]
        [DataType(DataType.Date)]
        public DateTime? NgayGiaoThucTe { get; set; }

        // Danh sách dropdown
        public IEnumerable<SelectListItem>? NguoiDungList { get; set; }
        public IEnumerable<SelectListItem>? KhuyenMaiList { get; set; }
    }
}

