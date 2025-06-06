using System;
using System.Collections.Generic;

namespace WebBanHang215.Models;

public partial class Size
{
    public int MaSize { get; set; }

    public string? LoaiSize { get; set; }

    public string? TenSize { get; set; }

    public string? MoTa { get; set; }

    public virtual ICollection<ChiTietSanPham> ChiTietSanPhams { get; set; } = new List<ChiTietSanPham>();
}
