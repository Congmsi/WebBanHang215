using Microsoft.AspNetCore.Mvc.Rendering;
using WebBanHang215.Models;
using X.PagedList;
using X.PagedList.Extensions;

namespace WebBanHang215.Areas.admin.ViewModels
{
    public class TonKhoFilterViewModel
    {
        // ===== Tiêu chí lọc =====

        // Mã sản phẩm
        public string? MaSp { get; set; }

        // Tên sản phẩm
        public string? TenSp { get; set; }

        // Số lượng tồn kho từ...
        public int? TonKhoTu { get; set; }

        // Số lượng tồn kho đến...
        public int? TonKhoDen { get; set; }

        // ===== Kết quả phân trang =====
        public IPagedList<TonKhoViewModel> KetQua { get; set; } = new List<TonKhoViewModel>().ToPagedList(1, 1);
    }

    
}

