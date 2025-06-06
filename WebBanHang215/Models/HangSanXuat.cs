using System;
using System.Collections.Generic;

namespace WebBanHang215.Models;

public partial class HangSanXuat
{
    public int MaHang { get; set; }

    public string TenHang { get; set; } = null!;

    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
