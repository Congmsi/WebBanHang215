/* using System;
using System.Collections.Generic;

namespace WebBanHang215.Models;

public partial class DonHang
{
    public int MaDh { get; set; }

    public int MaNd { get; set; }

    public DateTime? NgayDatHang { get; set; }

    public decimal? TongTien { get; set; }

    public string? TrangThai { get; set; }

    public int? MaKm { get; set; }

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();

    public virtual KhuyenMai? MaKmNavigation { get; set; }

    public virtual NguoiDung MaNdNavigation { get; set; } = null!;
}*/

using System;
using System.Collections.Generic;

namespace WebBanHang215.Models
{
    public partial class DonHang
    {
        public int MaDh { get; set; }

        public int MaNd { get; set; }

        public DateTime? NgayDatHang { get; set; }

        public decimal? TongTien { get; set; }

        public string? TrangThai { get; set; } // VD: "Chờ xác nhận", "Đang vận chuyển", ...

        public int? MaKm { get; set; }

        // Thêm mới: Giao nhận
        public string? DiaChiGiaoHang { get; set; }

        public DateTime? NgayGiaoDuKien { get; set; }

        public DateTime? NgayGiaoThucTe { get; set; }

        //  Thêm mới: Thanh toán
        public string? HinhThucThanhToan { get; set; } 

        public string? TrangThaiThanhToan { get; set; } 

        public string? MaGiaoDich { get; set; } 

        // Navigation properties
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();

        public virtual KhuyenMai? MaKmNavigation { get; set; }

        public virtual NguoiDung MaNdNavigation { get; set; } = null!;
    }
}
