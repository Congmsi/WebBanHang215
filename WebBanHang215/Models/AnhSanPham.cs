using System;
using System.Collections.Generic;

namespace WebBanHang215.Models;

public partial class AnhSanPham
{
    public int MaAnh { get; set; }

    public int MaSp { get; set; }

    public string? Url { get; set; }

    public string? MoTa { get; set; }

    public virtual SanPham MaSpNavigation { get; set; } = null!;
}
