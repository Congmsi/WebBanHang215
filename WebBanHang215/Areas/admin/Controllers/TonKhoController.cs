using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanHang215.Areas.admin.ViewModels;
using WebBanHang215.Models;
using X.PagedList.Extensions;

namespace WebBanHang215.Areas.admin.Controllers
{
    [Area("admin")]
    public class TonKhoController : Controller
    {
        private readonly WebBanHangCong215Context _context;

        public TonKhoController(WebBanHangCong215Context context)
        {
            _context = context;
        }

        public IActionResult ThongKeTonKho(int? page)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;

            var tonKhoList = _context.SanPhams
                .Select(sp => new TonKhoViewModel
                {
                    MaSp = sp.MaSp.ToString(),
                    TenSp = sp.TenSp,
                    TongNhap = _context.ChiTietNhapKhos
                                .Where(ct => ct.MaSp == sp.MaSp)
                                .Sum(ct => (int?)ct.SoLuong) ?? 0,
                    TongBan = _context.ChiTietDonHangs
                                .Where(ct => ct.MaSp == sp.MaSp)
                                .Sum(ct => (int?)ct.SoLuong) ?? 0
                })
                .ToPagedList(pageNumber, pageSize); // ✅

            return View("ThongKeTonKho", tonKhoList);
        }




        public IActionResult ThongKeTonKhoFilter(string? maSp, string? tenSp, int? tonKhoTu, int? tonKhoDen, int? page)
        {
            var danhSachTonKho = _context.SanPhams
                .Select(sp => new TonKhoViewModel
                {
                    MaSp = sp.MaSp.ToString(),
                    TenSp = sp.TenSp,
                    TongNhap = _context.ChiTietNhapKhos
                                .Where(ct => ct.MaSp == sp.MaSp)
                                .Sum(ct => (int?)ct.SoLuong) ?? 0,
                    TongBan = _context.ChiTietDonHangs
                                .Where(ct => ct.MaSp == sp.MaSp)
                                .Sum(ct => (int?)ct.SoLuong) ?? 0
                });

            // ====== Lọc theo tiêu chí ======

            if (!string.IsNullOrEmpty(maSp))
            {
                danhSachTonKho = danhSachTonKho.Where(tk => tk.MaSp.Contains(maSp));
            }

            if (!string.IsNullOrEmpty(tenSp))
            {
                danhSachTonKho = danhSachTonKho.Where(tk => tk.TenSp.Contains(tenSp));
            }

            if (tonKhoTu.HasValue)
            {
                danhSachTonKho = danhSachTonKho.Where(tk => (tk.TongNhap - tk.TongBan) >= tonKhoTu.Value);
            }

            if (tonKhoDen.HasValue)
            {
                danhSachTonKho = danhSachTonKho.Where(tk => (tk.TongNhap - tk.TongBan) <= tonKhoDen.Value);
            }

            // ====== Phân trang ======
            int pageSize = 5;
            int pageNumber = page ?? 1;

            var ketQuaPhanTrang = danhSachTonKho.OrderBy(t => t.TenSp).ToPagedList(pageNumber, pageSize);

            var viewModel = new TonKhoFilterViewModel
            {
                MaSp = maSp,
                TenSp = tenSp,
                TonKhoTu = tonKhoTu,
                TonKhoDen = tonKhoDen,
                KetQua = ketQuaPhanTrang
            };

            return View("ThongKeTonKhoFilter", viewModel);
        }



    }
}
