/*using System.ComponentModel.DataAnnotations;

namespace WebBanHang215.Areas.admin.ViewModels
{
    public class DonHangDeleteViewModel
    {
        public int MaDh { get; set; }

        [Display(Name = "Người đặt hàng")]
        
        public  string TenDangNhap { get; set; } 

        [Display(Name = "Tổng tiền")]
        public decimal? TongTien { get; set; }

        [Display(Name = "Ngày đặt")]
        public DateTime? NgayDatHang { get; set; }

        [Display(Name = "Trạng thái")]
        public string? TrangThai { get; set; }
    }
}*/


using System.ComponentModel.DataAnnotations;

namespace WebBanHang215.Areas.admin.ViewModels
{
    public class DonHangDeleteViewModel
    {
        [Display(Name = "Mã đơn hàng")]
        public int MaDh { get; set; }

        [Display(Name = "Người đặt hàng")]
        [Required]
        public string TenDangNhap { get; set; } = string.Empty;

        [Display(Name = "Tổng tiền")]
        [DataType(DataType.Currency)]
        public decimal? TongTien { get; set; }

        [Display(Name = "Ngày đặt hàng")]
        [DataType(DataType.DateTime)]
        public DateTime? NgayDatHang { get; set; }

        [Display(Name = "Trạng thái đơn hàng")]
        public string? TrangThai { get; set; }
    }
}
