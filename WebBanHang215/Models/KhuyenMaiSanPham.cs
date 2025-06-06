using System;
using System.Collections.Generic;

namespace WebBanHang215.Models;

public partial class KhuyenMaiSanPham
{
    public int MaKm { get; set; }

    public int MaSp { get; set; }

    public DateOnly? NgayBatDau { get; set; }

    public DateOnly? NgayKetThuc { get; set; }

    public virtual KhuyenMai MaKmNavigation { get; set; } = null!;

    public virtual SanPham MaSpNavigation { get; set; } = null!;
}
