using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WebBanHang215.Models;
using X.PagedList;

namespace WebBanHang215.Areas.admin.ViewModels
{
    public class SanPhamFilterViewModel
    {
        // ----- Các tiêu chí lọc -----
        public string? TuKhoa { get; set; }
        public int? MaLoai { get; set; }
        public decimal? GiaTu { get; set; }
        public decimal? GiaDen { get; set; }

        public bool LocTuKhoa { get; set; }
        public bool LocLoai { get; set; }
        public bool LocGia { get; set; }

        // ----- Dữ liệu để render dropdown -----
        public List<SelectListItem> LoaiSanPhams { get; set; } = new();

        // ----- Kết quả sau khi lọc (kèm phân trang) -----
        public IPagedList<SanPham> KetQua { get; set; } = new PagedList<SanPham>(new List<SanPham>(), 1, 1);
    }
}
