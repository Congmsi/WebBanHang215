using System;
using System.Collections.Generic;

namespace WebBanHang215.Models;

public partial class KhuyenMai
{
    public int MaKm { get; set; }

    public string? TenKhuyenMai { get; set; }

    public decimal? PhanTramGiam { get; set; }

    public DateOnly? NgayBatDau { get; set; }

    public DateOnly? NgayKetThuc { get; set; }

    public virtual ICollection<DonHang> DonHangs { get; set; } = new List<DonHang>();

    public virtual ICollection<KhuyenMaiSanPham> KhuyenMaiSanPhams { get; set; } = new List<KhuyenMaiSanPham>();
}
