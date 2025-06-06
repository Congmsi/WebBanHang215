using System;
using System.Collections.Generic;

namespace WebBanHang215.Models;

public partial class NguoiDung
{
    public int MaNd { get; set; }

    public string TenDangNhap { get; set; } = null!;
    
    public string MatKhau { get; set; } = null!;

    public string? Email { get; set; }

    public string? SoDienThoai { get; set; }

    public string? DiaChi { get; set; }

    public string? VaiTro { get; set; }

    public string? HoVaTen { get; set; }

    public virtual ICollection<DonHang> DonHangs { get; set; } = new List<DonHang>();

    public virtual ICollection<GioHang> GioHangs { get; set; } = new List<GioHang>();
}
