namespace WebBanHang215.ViewModels
{
    public class DanhSachAnhSanPhamViewModel
    {
        public int MaSp { get; set; }
        public string TenSp { get; set; } = string.Empty;

        public List<AnhSanPhamItem> UrlAnh { get; set; } = new();
    }

    public class AnhSanPhamItem
    {
        public int MaAnh { get; set; }
        public string Url { get; set; } = string.Empty;
    }
}
