using System;
using System.Collections.Generic;

namespace WebBanHang215.Models;

public partial class ChiTietSanPham
{
    public int MaCtsp { get; set; }

    public int MaSp { get; set; }

    public int MaSize { get; set; }

    public int MaMau { get; set; }

    public int? SoLuongTon { get; set; }

    public virtual MauSac MaMauNavigation { get; set; } = null!;

    public virtual Size MaSizeNavigation { get; set; } = null!;

    public virtual SanPham MaSpNavigation { get; set; } = null!;
}
