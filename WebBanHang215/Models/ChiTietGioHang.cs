﻿using System;
using System.Collections.Generic;

namespace WebBanHang215.Models;

public partial class ChiTietGioHang
{
    public int MaChiTietGh { get; set; }

    public int MaGioHang { get; set; }

    public int MaSp { get; set; }

    public int SoLuong { get; set; }

    public virtual GioHang MaGioHangNavigation { get; set; } = null!;

    public virtual SanPham MaSpNavigation { get; set; } = null!;
}
