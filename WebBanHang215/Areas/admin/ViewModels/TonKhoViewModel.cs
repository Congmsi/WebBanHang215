namespace WebBanHang215.Areas.admin.ViewModels
{
    public class TonKhoViewModel
    {
        public string MaSp { get; set; } = null!;
        public string TenSp { get; set; } = null!;
        public int TongNhap { get; set; }
        public int TongBan { get; set; }
        public int TonKho => TongNhap - TongBan; 
    }
}
