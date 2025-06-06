using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanHang215.Models;
using WebBanHang215.ViewModels;
using X.PagedList;
using X.PagedList.Extensions;
using WebBanHang215.Models.Athentication;

namespace WebBanHang215.Controllers
{
    public class HomeController : Controller
    {
        private readonly WebBanHangCong215Context _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(WebBanHangCong215Context context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [Authentication]

        // Phan trang chung index 8sp/page khong phan biet chung loai sp
        public IActionResult Index(int? page)
        {
            int pageSize = 8;
            int pageNumber = page ?? 1;

            var lstSanPham = _context.SanPhams
                .Include(sp => sp.AnhSanPhams)
                .OrderBy(sp => sp.MaSp)
                .ToPagedList(pageNumber, pageSize);

            return View(lstSanPham);
        }

        public IActionResult SanPhamTheoLoai(int maLoai, int? page)
        {
            int pageSize = 8;
            int pageNumber = page ?? 1;

            var lstSanPham = _context.SanPhams
                                     .Include(sp => sp.AnhSanPhams)
                                     .Where(sp => sp.MaLoai == maLoai)
                                     .OrderBy(sp => sp.TenSp) // Tuy chon thu tu
                                     .ToList();

            if (lstSanPham == null || lstSanPham.Count == 0)
            {
                _logger.LogWarning($"Không tìm thấy sản phẩm cho mã loại: {maLoai}");
                TempData["ThongBao"] = "Không có sản phẩm phù hợp.";
                return RedirectToAction("Index", "Home");
            }
            ViewBag.MaLoai = maLoai; // Bo sung dong nay khi phan trang vi phuong thuc dau vao nhan hai tham so  public IActionResult SanPhamTheoLoai(int maLoai, int? page)
            // Tao danh sach phan trang
            var pagedList = new X.PagedList.PagedList<SanPham>(lstSanPham, pageNumber, pageSize);

            return View(pagedList);
        }

        public IActionResult TraVeChiTietSanPhamNguoiDung(int maSP)
        {
            
            var sp = _context.SanPhams
                .Include(sp=>sp.MaLoaiNavigation)
                .Include(sp=>sp.MaHangNavigation)
                .Include(sp=>sp.MaNccNavigation)
                .SingleOrDefault(x=>x.MaSp == maSP);
            if (sp == null)
            {
                return NotFound();  // hoac chuyen ve trang Index: return RedirectToAction("Index")
            }

            var anhSanPham = _context.AnhSanPhams.Where(x => x.MaSp == maSP).ToList();

            ViewBag.TenLoai = sp.MaLoaiNavigation.TenLoai;
            ViewBag.TenHang = sp.MaHangNavigation.TenHang;
            ViewBag.TenNcc = sp.MaNccNavigation.TenNhaCungCap;
            ViewBag.anhSanPham = anhSanPham;
            return View(sp);
        }

        public IActionResult ProductDetail(int maSP)
        {
            var sp = _context.SanPhams
                .Include(sp => sp.MaLoaiNavigation)
                .Include(sp => sp.MaHangNavigation)
                .Include(sp => sp.MaNccNavigation)
                .SingleOrDefault(x => x.MaSp == maSP);

            if (sp == null)
            {
                return NotFound();
            }

            var anhSanPham = _context.AnhSanPhams.Where(x => x.MaSp == maSP).ToList();

            var homeProductDetailViewModel = new HomeProductDetailViewModel
            {
                sanpham = sp,
                anhSanPhams = anhSanPham
            };

            return View(homeProductDetailViewModel);
        }




    }
}
