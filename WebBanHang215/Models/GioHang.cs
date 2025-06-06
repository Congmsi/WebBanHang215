using System;
using System.Collections.Generic;

namespace WebBanHang215.Models;

public partial class GioHang
{
    public int MaGioHang { get; set; }

    public int MaNd { get; set; }

    public DateTime? NgayTao { get; set; }

    public virtual ICollection<ChiTietGioHang> ChiTietGioHangs { get; set; } = new List<ChiTietGioHang>();

    public virtual NguoiDung MaNdNavigation { get; set; } = null!;
}
