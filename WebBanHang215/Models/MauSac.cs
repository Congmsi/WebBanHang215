using System;
using System.Collections.Generic;

namespace WebBanHang215.Models;

public partial class MauSac
{
    public int MaMau { get; set; }

    public string? TenMau { get; set; }

    public string? MaMauHex { get; set; }

    public virtual ICollection<ChiTietSanPham> ChiTietSanPhams { get; set; } = new List<ChiTietSanPham>();
}
