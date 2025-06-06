using System;
using System.Collections.Generic;

namespace WebBanHang215.Models;

public partial class GioiTinh
{
    public int MaGioiTinh { get; set; }

    public string? TenGioiTinh { get; set; }

    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
