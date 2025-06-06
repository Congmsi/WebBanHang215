using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using WebBanHang215.Areas.admin.ViewModels;
using WebBanHang215.Models;
using WebBanHang215.ViewModels;
using X.PagedList;
using X.PagedList.Extensions;

namespace WebBanHang215.Areas.admin.Controllers
{
    [Area("admin")]
    [Route("admin/homeadmin")]
    public class HomeAdminController : Controller
    {
        private readonly WebBanHangCong215Context _context;

        public HomeAdminController()
        {
            _context = new WebBanHangCong215Context();
        }        [HttpGet]
        [Route("")]
        [Route("admin")]
        public IActionResult Index()
        {
            var viewModel = new DashboardViewModel();

            // 1. Tổng số sản phẩm
            viewModel.TongSanPham = _context.SanPhams.Count();

            // 2. Đơn hàng mới (trạng thái "Đang xử lý")
            viewModel.DonHangMoi = _context.DonHangs
                .Where(dh => dh.TrangThai == "Đang xử lý")
                .Count();

            // 3. Sản phẩm sắp hết (tổng số lượng tồn < 10)
            viewModel.SanPhamSapHet = _context.ChiTietSanPhams
                .GroupBy(ct => ct.MaSp)
                .Where(g => g.Sum(x => x.SoLuongTon ?? 0) < 10)
                .Count();

            // 4. Doanh thu tháng hiện tại
            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;
            viewModel.DoanhThuThang = _context.DonHangs
                .Where(dh => dh.NgayDatHang.HasValue &&
                            dh.NgayDatHang.Value.Month == currentMonth &&
                            dh.NgayDatHang.Value.Year == currentYear &&
                            dh.TrangThai != "Hủy")
                .Sum(dh => dh.TongTien ?? 0);            // 5. Đơn hàng gần đây (5 đơn mới nhất)
            var donHangQuery = _context.DonHangs
                .Include(dh => dh.MaNdNavigation)
                .OrderByDescending(dh => dh.NgayDatHang)
                .Take(5)
                .Select(dh => new
                {
                    MaDonHang = dh.MaDh.ToString(),
                    TenKhachHang = dh.MaNdNavigation.TenDangNhap ?? "Không có tên",
                    TongTien = dh.TongTien ?? 0,
                    TrangThai = dh.TrangThai ?? "Không xác định",
                    NgayDat = dh.NgayDatHang ?? DateTime.Now
                })
                .ToList();

            viewModel.DonHangGanDay = donHangQuery
                .Select(dh => new DonHangGanDayItem
                {
                    MaDonHang = dh.MaDonHang,
                    TenKhachHang = dh.TenKhachHang,
                    TongTien = dh.TongTien,
                    TrangThai = dh.TrangThai,
                    NgayDat = dh.NgayDat,
                    MauTrangThai = GetStatusColor(dh.TrangThai)
                })
                .ToList();

            // 6. Hoạt động gần đây
            viewModel.HoatDongGanDay = new List<HoatDongGanDayItem>
            {
                new HoatDongGanDayItem
                {
                    TieuDe = $"Có {viewModel.DonHangMoi} đơn hàng mới cần xử lý",
                    ThoiGian = "Vừa xong",
                    Icon = "fas fa-shopping-cart",
                    MauIcon = "text-success"
                },
                new HoatDongGanDayItem
                {
                    TieuDe = $"Cảnh báo: {viewModel.SanPhamSapHet} sản phẩm sắp hết hàng",
                    ThoiGian = "5 phút trước",
                    Icon = "fas fa-exclamation-triangle",
                    MauIcon = "text-warning"
                },
                new HoatDongGanDayItem
                {
                    TieuDe = "Doanh thu tháng đạt " + viewModel.DoanhThuThang.ToString("N0") + " VNĐ",
                    ThoiGian = "Hôm nay",
                    Icon = "fas fa-chart-line",
                    MauIcon = "text-info"
                }
            };

            return View(viewModel);
        }        private static string GetStatusColor(string? status)
        {
            return status switch
            {
                "Đang xử lý" => "warning",
                "Đã xử lý" => "info", 
                "Hoàn thành" => "success",
                "Hủy" => "danger",
                _ => "secondary"
            };
        }

        [HttpGet]
        [Route("danhmucsanpham")]
        public IActionResult DanhMucSanPham(int? page)
        {
            int pageSize = 8;
            int pageNumber = page ?? 1;

            var lstSanPham = _context.SanPhams
                .Include(sp => sp.AnhSanPhams)
                .OrderBy(sp => sp.MaSp)
                .ToPagedList(pageNumber, pageSize);

            return View(lstSanPham);
        }

        [HttpGet]
        [Route("ThemSanPhamMoi")]
        public IActionResult ThemSanPhamMoi()
        {
            ViewBag.MaLoai = new SelectList(_context.LoaiSanPhams, "MaLoai", "TenLoai");
            ViewBag.MaHang = new SelectList(_context.HangSanXuats, "MaHang", "TenHang");
            ViewBag.MaNcc = new SelectList(_context.NhaCungCaps, "MaNcc", "TenNhaCungCap");
            ViewBag.MaTrangThai = new SelectList(_context.TrangThaiSanPhams, "MaTrangThai", "TenTrangThai");
            ViewBag.MaGioiTinh = new SelectList(_context.GioiTinhs, "MaGioiTinh", "TenGioiTinh");

            return View();
        }

        // Xử lý thêm mới
        [HttpPost]
        [Route("ThemSanPhamMoi")]
        [ValidateAntiForgeryToken]
        public IActionResult ThemSanPhamMoi(SanPhamCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.MaLoai = new SelectList(_context.LoaiSanPhams, "MaLoai", "TenLoai");
                ViewBag.MaHang = new SelectList(_context.HangSanXuats, "MaHang", "TenHang");
                ViewBag.MaNcc = new SelectList(_context.NhaCungCaps, "MaNcc", "TenNhaCungCap");
                ViewBag.MaTrangThai = new SelectList(_context.TrangThaiSanPhams, "MaTrangThai", "TenTrangThai");
                ViewBag.MaGioiTinh = new SelectList(_context.GioiTinhs, "MaGioiTinh", "TenGioiTinh");

                return View(vm);
            }

            var sp = new SanPham
            {
                TenSp = vm.TenSp,
                MoTa = vm.MoTa,
                Gia = vm.Gia,
                GiaNhap = vm.GiaNhap,
                GiaKhuyenMai = vm.GiaKhuyenMai,
                SoLuongNhap = vm.SoLuongNhap,
                MaLoai = vm.MaLoai,
                MaHang = vm.MaHang,
                MaNcc = vm.MaNcc,
                MaTrangThai = vm.MaTrangThai,
                MaGioiTinh = vm.MaGioiTinh
            };

            _context.SanPhams.Add(sp);
            _context.SaveChanges();

            return RedirectToAction("DanhMucSanPham");
        }

        // GET: /admin/homeadmin/SuaSanPham?id=1
        [HttpGet]
        [Route("admin/homeadmin/SuaSanPham")]
        public IActionResult SuaSanPham(int id)
        {
            var sp = _context.SanPhams.Find(id);
            if (sp == null) return NotFound();

            var viewModel = new SanPhamEditViewModel
            {
                MaSp = sp.MaSp,
                TenSp = sp.TenSp,
                MoTa = sp.MoTa,
                Gia = sp.Gia,
                GiaNhap = sp.GiaNhap,
                GiaKhuyenMai = sp.GiaKhuyenMai,
                SoLuongNhap = sp.SoLuongNhap,
                MaLoai = sp.MaLoai,
                MaHang = sp.MaHang,
                MaNcc = sp.MaNcc,
                MaTrangThai = sp.MaTrangThai,
                MaGioiTinh = sp.MaGioiTinh
            };

            // Đổ dữ liệu cho dropdown
            ViewBag.MaLoai = new SelectList(_context.LoaiSanPhams, "MaLoai", "TenLoai");
            ViewBag.MaHang = new SelectList(_context.HangSanXuats, "MaHang", "TenHang");
            ViewBag.MaNcc = new SelectList(_context.NhaCungCaps, "MaNcc", "TenNhaCungCap");
            ViewBag.MaTrangThai = new SelectList(_context.TrangThaiSanPhams, "MaTrangThai", "TenTrangThai");
            ViewBag.MaGioiTinh = new SelectList(_context.GioiTinhs, "MaGioiTinh", "TenGioiTinh");

            return View("SuaSanPham", viewModel);
        }

        // POST: /admin/homeadmin/SuaSanPham
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/homeadmin/SuaSanPham")]
        public IActionResult SuaSanPham(SanPhamEditViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                // Load lại dropdown nếu có lỗi
                ViewBag.MaLoai = new SelectList(_context.LoaiSanPhams, "MaLoai", "TenLoai");
                ViewBag.MaHang = new SelectList(_context.HangSanXuats, "MaHang", "TenHang");
                ViewBag.MaNcc = new SelectList(_context.NhaCungCaps, "MaNcc", "TenNhaCungCap");
                ViewBag.MaTrangThai = new SelectList(_context.TrangThaiSanPhams, "MaTrangThai", "TenTrangThai");
                ViewBag.MaGioiTinh = new SelectList(_context.GioiTinhs, "MaGioiTinh", "TenGioiTinh");
                return View(vm);
            }

            var sp = _context.SanPhams.Find(vm.MaSp);
            if (sp == null) return NotFound();

            // Cập nhật lại thông tin sản phẩm
            sp.TenSp = vm.TenSp;
            sp.MoTa = vm.MoTa;
            sp.Gia = vm.Gia;
            sp.GiaNhap = vm.GiaNhap;
            sp.GiaKhuyenMai = vm.GiaKhuyenMai;
            sp.SoLuongNhap = vm.SoLuongNhap;
            sp.MaLoai = vm.MaLoai;
            sp.MaHang = vm.MaHang;
            sp.MaNcc = vm.MaNcc;
            sp.MaTrangThai = vm.MaTrangThai;
            sp.MaGioiTinh = vm.MaGioiTinh;

            _context.SaveChanges();

            return RedirectToAction("DanhMucSanPham");
        }
        // Hiển thị xác nhận
        [HttpGet]
        [Route("XoaSanPham")]
        public IActionResult XoaSanPham(int id)
        {
            var sp = _context.SanPhams.FirstOrDefault(x => x.MaSp == id);
            if (sp == null) return NotFound();

            var vm = new SanPhamDeleteViewModel
            {
                MaSp = sp.MaSp,
                TenSp = sp.TenSp,
                MoTa = sp.MoTa,
                Gia = sp.Gia,
                SoLuongNhap = sp.SoLuongNhap
            };

            return View(vm);
        }

        // Thực hiện xóa
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("XoaSanPham")]
        public IActionResult XoaSanPham(SanPhamDeleteViewModel vm)
        {
            var sp = _context.SanPhams.Find(vm.MaSp);
            if (sp == null) return NotFound();

            _context.SanPhams.Remove(sp);
            _context.SaveChanges();

            return RedirectToAction("DanhMucSanPham");
        }


        [HttpGet]
        [Route("ChiTietSanPham")]
        public IActionResult ChiTietSanPham(int id)
        {
            var sp = _context.SanPhams
                .Include(s => s.MaLoaiNavigation)
                .Include(s => s.MaHangNavigation)
                .Include(s => s.MaGioiTinhNavigation)
                .Include(s => s.MaTrangThaiNavigation)
                .Include(s => s.MaNccNavigation)
                .Include(s => s.AnhSanPhams)
                .Include(s => s.ChiTietSanPhams)
                    .ThenInclude(ct => ct.MaSizeNavigation)
                .Include(s => s.ChiTietSanPhams)
                    .ThenInclude(ct => ct.MaMauNavigation)
                .FirstOrDefault(s => s.MaSp == id);

            if (sp == null) return NotFound();

            var viewModel = new SanPhamChiTietViewModel
            {
                MaSp = sp.MaSp,
                TenSp = sp.TenSp,
                MoTa = sp.MoTa,
                Gia = sp.Gia,
                GiaKhuyenMai = sp.GiaKhuyenMai,
                TenLoai = sp.MaLoaiNavigation.TenLoai,
                TenHang = sp.MaHangNavigation.TenHang,
                TenGioiTinh = sp.MaGioiTinhNavigation.TenGioiTinh??"Không rõ",
                TenTrangThai = sp.MaTrangThaiNavigation.TenTrangThai,
                TenNcc = sp.MaNccNavigation.TenNhaCungCap??"Không rõ",
                UrlAnh = sp.AnhSanPhams.Select(a => a.Url).ToList(),
                ChiTietSanPham = sp.ChiTietSanPhams.Select(ct => new ChiTietSPItem
                {
                    TenSize = ct.MaSizeNavigation.TenSize?? "Không rõ",
                    TenMau = ct.MaMauNavigation.TenMau?? "không rõ",
                    SoLuongTon = ct.SoLuongTon,
                    NgayNhap = (ct as dynamic)?.NgayNhap // nếu có thuộc tính này
                }).ToList()
            };

            return View(viewModel);
        }
       [HttpGet]
[Route("ThemAnhSanPham")]
public IActionResult ThemAnhSanPham(int id)
{
    var vm = new SanPhamAnhViewModel { MaSp = id };
    return View(vm);
}

[HttpPost]
[Route("ThemAnhSanPham")]
[ValidateAntiForgeryToken]
public async Task<IActionResult> ThemAnhSanPham(SanPhamAnhViewModel vm)
{
    if (vm.AnhMoi != null && vm.AnhMoi.Any())
    {
        foreach (var file in vm.AnhMoi)
        {
            if (file.Length > 0)
            {
                var fileName = Path.GetFileName(file.FileName);

                // Đường dẫn lưu ảnh vật lý trong wwwroot/productsImages
                var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "productsImages", fileName);

                // Tạo thư mục nếu chưa có
                var directory = Path.GetDirectoryName(savePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory!);
                }

                // Ghi file
                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Lưu URL vào DB (dùng trong <img src="...">)
                var anh = new AnhSanPham
                {
                    MaSp = vm.MaSp,
                    Url = "/productsImages/" + fileName
                };

                _context.AnhSanPhams.Add(anh);
            }
        }

        await _context.SaveChangesAsync();
    }

    return RedirectToAction("DanhMucSanPham", new { id = vm.MaSp });
}

        

      [HttpGet]
[Route("XoaAnhSanPham")]
public IActionResult XoaAnhSanPham(int id)
{
    var anh = _context.AnhSanPhams
        .Include(a => a.MaSpNavigation)
        .FirstOrDefault(a => a.MaAnh == id);

    if (anh == null) return NotFound();

    var viewModel = new XoaAnhSanPhamViewModel
    {
        MaAnh = anh.MaAnh,
        MaSp = anh.MaSp,
        TenSp = anh.MaSpNavigation?.TenSp ?? "",
        Url = anh.Url
    };

    return View(viewModel);
}

[HttpPost]
[ValidateAntiForgeryToken]
[Route("XoaAnhSanPham")]
public IActionResult XoaAnhSanPham(XoaAnhSanPhamViewModel vm)
{
    var anh = _context.AnhSanPhams.FirstOrDefault(a => a.MaAnh == vm.MaAnh);
    if (anh == null) return NotFound();

    // Xóa file vật lý nếu có
    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", anh.Url ?? "");
    if (System.IO.File.Exists(path))
    {
        System.IO.File.Delete(path);
    }

    _context.AnhSanPhams.Remove(anh);
    _context.SaveChanges();

    // Sau khi xóa, quay lại trang danh sách ảnh sản phẩm
    return RedirectToAction("DanhSachAnhSanPham", new { id = vm.MaSp });
}





        [HttpGet]
        [Route("DanhSachAnhSanPham")]
        public IActionResult DanhSachAnhSanPham(int id)
        {
            var sp = _context.SanPhams
                .Include(s => s.AnhSanPhams)
                .FirstOrDefault(s => s.MaSp == id);

            if (sp == null) return NotFound();

            var viewModel = new DanhSachAnhSanPhamViewModel
            {
                MaSp = sp.MaSp,
                TenSp = sp.TenSp ?? "",
                UrlAnh = sp.AnhSanPhams.Select(a => new AnhSanPhamItem
                {
                    MaAnh = a.MaAnh,
                    Url = a.Url ?? ""
                }).ToList()
            };

            return View(viewModel);
        }


        // Danh Sách lọc sản phẩm
        [HttpGet]
        [Route("DanhSachLocSanPham")]
        public IActionResult DanhSachLocSanPham(
       string? tuKhoa,
       int? maLoai,
       decimal? giaTu,
       decimal? giaDen,
       bool locTuKhoa = false,
       bool locLoai = false,
       bool locGia = false,
       int? page = 1)
        {
            // Debug kiểm tra dữ liệu đầu vào
            Console.WriteLine($"[DEBUG] TuKhoa={tuKhoa}, locTuKhoa={locTuKhoa}, MaLoai={maLoai}, locLoai={locLoai}, GiaTu={giaTu}, GiaDen={giaDen}, locGia={locGia}");

            int pageSize = 8;
            int pageNumber = page ?? 1;

            var query = _context.SanPhams.AsQueryable();

            if (locTuKhoa && !string.IsNullOrWhiteSpace(tuKhoa))
            {
                query = query.Where(sp =>
            sp.TenSp.Contains(tuKhoa) ||
            sp.MoTa.Contains(tuKhoa) ||
            sp.Gia.ToString().Contains(tuKhoa) ||
            sp.SoLuongNhap.ToString().Contains(tuKhoa) ||
            sp.MaHangNavigation.TenHang.Contains(tuKhoa)) ;
            }

            if (locLoai && maLoai.HasValue)
            {
                query = query.Where(sp => sp.MaLoai == maLoai);
            }

            if (locGia)
            {
                if (giaTu.HasValue)
                    query = query.Where(sp => sp.Gia >= giaTu);
                if (giaDen.HasValue)
                    query = query.Where(sp => sp.Gia <= giaDen);
            }

            var sanPhamPaged = query
                .Include(sp => sp.MaLoaiNavigation)
                .OrderBy(sp => sp.MaSp)
                .ToPagedList(pageNumber, pageSize);

            var vm = new SanPhamFilterViewModel
            {
                TuKhoa = tuKhoa,
                MaLoai = maLoai,
                GiaTu = giaTu,
                GiaDen = giaDen,
                LocTuKhoa = locTuKhoa,
                LocLoai = locLoai,
                LocGia = locGia,
                KetQua = sanPhamPaged,
                LoaiSanPhams = _context.LoaiSanPhams
                    .Select(l => new SelectListItem { Value = l.MaLoai.ToString(), Text = l.TenLoai })
                    .ToList()
            };

            return View(vm);
        }


       



    }
}