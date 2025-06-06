using System;
using System.Collections.Generic;

namespace WebBanHang215.Models;

public partial class TrangThaiSanPham
{
    public int MaTrangThai { get; set; }

    public string TenTrangThai { get; set; } = null!;

    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
