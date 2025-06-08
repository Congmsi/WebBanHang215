using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace WebBanHang215.ViewModels
{
    public class SanPhamCreateViewModel
    {
        [Required(ErrorMessage = "Tên sản phẩm không được bỏ trống")]
        public string TenSp { get; set; }

        public string? MoTa { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Giá phải >= 0")]
        public decimal? Gia { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Giá nhập phải >= 0")]
        public decimal? GiaNhap { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Giá khuyến mãi phải >= 0")]
        public decimal? GiaKhuyenMai { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Số lượng phải >= 0")]
        public int? SoLuongNhap { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn loại sản phẩm")]
        public int MaLoai { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn hãng sản xuất")]
        public int MaHang { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn nhà cung cấp")]
        public int MaNcc { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn trạng thái")]
        public int MaTrangThai { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn giới tính")]
        public int MaGioiTinh { get; set; }

        // Image upload properties
        public List<IFormFile> AnhSanPham { get; set; } = new();
    }
}
