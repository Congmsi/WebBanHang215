using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace WebBanHang215.Areas.admin.ViewModels;
public class ChiTietDonHangViewModel
{
    public int MaDh { get; set; }

    [Required]
    public int MaSp { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int SoLuong { get; set; }

    public IEnumerable<SelectListItem>? SanPhamList { get; set; }
}
