using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using WebBanHang215.Areas.admin.ViewModels;
using WebBanHang215.Models;
using X.PagedList;
using X.PagedList.Extensions;

namespace WebBanHang215.Areas.admin.Controllers
{
    
    [Area("admin")]
    public class DonHangController : Controller
    {
        private readonly WebBanHangCong215Context _context;
        public DonHangController(WebBanHangCong215Context context)
        {
            _context = context;
        }


        /* public IActionResult Index()
         {
             // Lấy danh sách tất cả đơn hàng từ cơ sở dữ liệu
             var danhSachDonHang = _context.DonHangs.Include(d => d.MaNdNavigation)  // include bảng NguoiDung để có thể bổ sung thông tin bảng người dùng vào bảng đơn hàng theo truy vấn trong view
         .ToList();


             // Truyền danh sách đơn hàng vào view
             return View(danhSachDonHang);
         }*/

        public IActionResult Index(int? page)
        {
            int pageSize = 5; // số đơn hàng mỗi trang
            int pageNumber = page ?? 1;

            var donHangs = _context.DonHangs
                .Include(d => d.MaNdNavigation)
                .OrderByDescending(d => d.NgayDatHang)
                .ToPagedList(pageNumber, pageSize);

            return View(donHangs);
        }



        // GET: Hiển thị form cập nhật trạng thái đơn hàng
        [HttpGet]
      
        public IActionResult SuaTrangThai(int id)
        {
            var donHang = _context.DonHangs.Find(id);
            if (donHang == null)
            {
                return NotFound();
            }

            // Danh sách tất cả trạng thái có thể
            List<string> tatCaTrangThai = new List<string>
        {
            "Đang xử lý", "Đã xử lý", "Hoàn thành", "Hủy"
        };

            // Nếu trạng thái hiện tại là "Đã xử lý" hoặc "Hoàn thành" thì loại bỏ "Hủy" khỏi danh sách lựa chọn
            if (donHang.TrangThai == "Đã xử lý" || donHang.TrangThai == "Hoàn thành")
            {
                tatCaTrangThai.Remove("Hủy");
            }

            // Truyền danh sách trạng thái hợp lệ sang View (sử dụng ViewBag hoặc Model tùy ý)
            ViewBag.TrangThaiList = new SelectList(tatCaTrangThai, donHang.TrangThai);

            return View(donHang);
        }

        [HttpPost]
      
        public IActionResult SuaTrangThai(int id, string trangThaiMoi)
        {
            var donHang = _context.DonHangs.Find(id);

            if (donHang == null)
            {
                return NotFound();
            }

            if ((donHang.TrangThai == "Đã xử lý" || donHang.TrangThai == "Hoàn thành") && trangThaiMoi == "Hủy")
            {
                ModelState.AddModelError("", "Không thể hủy đơn hàng đã được xử lý hoặc hoàn thành.");

                // Tạo lại danh sách trạng thái
                List<string> trangThaiList = new List<string> { "Đang xử lý", "Đã xử lý", "Hoàn thành", "Hủy" };
                if (donHang.TrangThai == "Đã xử lý" || donHang.TrangThai == "Hoàn thành")
                {
                    trangThaiList.Remove("Hủy");
                }
                ViewBag.TrangThaiList = new SelectList(trangThaiList, donHang.TrangThai);
                return View(donHang);
            }

            // Nếu hợp lệ, cập nhật
            donHang.TrangThai = trangThaiMoi;
            _context.SaveChanges();
            return RedirectToAction("Index", "DonHang");
        }



        [HttpGet]
        [Route("XemChiTietDonHang")]
        public IActionResult XemChiTietDonHang(int id)
        {
            var donHang = _context.DonHangs
                .Include(d => d.MaNdNavigation)
                .Include(d => d.ChiTietDonHangs)
                    .ThenInclude(ct => ct.MaSpNavigation)
                .FirstOrDefault(d => d.MaDh == id);

            if (donHang == null)
                return NotFound();

            var viewModel = new DonHangChiTietViewModel
            {
                MaDh = donHang.MaDh,
                TenDangNhap = donHang.MaNdNavigation.TenDangNhap,
                NgayDatHang = donHang.NgayDatHang,
                TongTien = donHang.TongTien,
                TrangThai = donHang.TrangThai,

                // ✅ Đây là phần QUAN TRỌNG cần sửa
                ChiTietSanPhams = donHang.ChiTietDonHangs.Select(ct => new ChiTietSanPhamDonHangViewModel
                {
                    MaChiTiet = ct.MaChiTietDh,                     // ⚠️ Gán đúng ID để sửa/xóa hoạt động
                    TenSanPham = ct.MaSpNavigation.TenSp,
                    SoLuong = ct.SoLuong ?? 0,
                    DonGia = ct.DonGia ?? 0
                }).ToList()
            };

            return View(viewModel);
        }




       


        [HttpGet]
        public IActionResult ThemDonHang()
        {
            var viewModel = new DonHangCreateViewModel
            {
                NguoiDungList = _context.NguoiDungs
                    .Select(nd => new SelectListItem { Value = nd.MaNd.ToString(), Text = nd.TenDangNhap }),
                KhuyenMaiList = _context.KhuyenMais
                    .Select(km => new SelectListItem { Value = km.MaKm.ToString(), Text = km.TenKhuyenMai })
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemDonHang(DonHangCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var donHang = new DonHang
                {
                    MaNd = model.MaNd,
                    TongTien = model.TongTien,
                    TrangThai = model.TrangThai,
                    MaKm = model.MaKm,
                    NgayDatHang = model.NgayDatHang ?? DateTime.Now,

                    //  bo sung cac truong moi
                    DiaChiGiaoHang = model.DiaChiGiaoHang,
                    HinhThucThanhToan = model.HinhThucThanhToan,
                    TrangThaiThanhToan = model.TrangThaiThanhToan,
                    MaGiaoDich = model.MaGiaoDich,
                    NgayGiaoDuKien = model.NgayGiaoDuKien,
                    NgayGiaoThucTe = model.NgayGiaoThucTe
                };

                _context.DonHangs.Add(donHang);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            // Neu co loi thi load lai tha danh sach chon dropdown
            model.NguoiDungList = _context.NguoiDungs
                .Select(nd => new SelectListItem { Value = nd.MaNd.ToString(), Text = nd.TenDangNhap });
            model.KhuyenMaiList = _context.KhuyenMais
                .Select(km => new SelectListItem { Value = km.MaKm.ToString(), Text = km.TenKhuyenMai });

            return View(model);
        }

        /*

         [HttpGet]
         public IActionResult SuaDonHang(int id)
         {
             var donHang = _context.DonHangs.FirstOrDefault(d => d.MaDh == id);
             if (donHang == null) return NotFound();

             var vm = new DonHangEditViewModel
             {
                 MaDh = donHang.MaDh,
                 MaNd = donHang.MaNd,
                 TongTien = donHang.TongTien ?? 0,
                 TrangThai = donHang.TrangThai ?? "Đang xử lý",
                 MaKm = donHang.MaKm,
                 NgayDatHang = donHang.NgayDatHang,

                 NguoiDungList = GetNguoiDungList(),
                 KhuyenMaiList = GetKhuyenMaiList()
             };

             return View(vm);
         }

         [HttpPost]
         [ValidateAntiForgeryToken]
         public IActionResult SuaDonHang(int id, DonHangEditViewModel model)
         {
             var donHang = _context.DonHangs.FirstOrDefault(d => d.MaDh == id);
             if (donHang == null) return NotFound();

             var trangThaiHienTai = donHang.TrangThai;
             var trangThaiMoi = model.TrangThai;

             // Ràng buộc trạng thái
             if ((trangThaiHienTai == "Đã xử lý" || trangThaiHienTai == "Hoàn thành") &&
                 (trangThaiMoi == "Đang xử lý" || trangThaiMoi == "Hủy"))
             {
                 ModelState.AddModelError("TrangThai", "Không được chuyển từ trạng thái đã xử lý/hoàn thành về trạng thái trước đó.");
             }

             if (ModelState.IsValid)
             {
                 donHang.MaNd = model.MaNd;
                 donHang.TongTien = model.TongTien;
                 donHang.TrangThai = model.TrangThai;
                 donHang.MaKm = model.MaKm;
                 donHang.NgayDatHang = model.NgayDatHang ?? donHang.NgayDatHang;

                 _context.SaveChanges();
                 return RedirectToAction("Index", "DonHang");
             }

             // Nếu có lỗi, nạp lại dropdown
             model.NguoiDungList = GetNguoiDungList();
             model.KhuyenMaiList = GetKhuyenMaiList();

             return View(model);
         } */

        // Phương thức mới:
        [HttpGet]
        public IActionResult SuaDonHang(int id)
        {
            var donHang = _context.DonHangs.FirstOrDefault(d => d.MaDh == id);
            if (donHang == null) return NotFound();

            var vm = new DonHangEditViewModel
            {
                MaDh = donHang.MaDh,
                MaNd = donHang.MaNd,
                TongTien = donHang.TongTien ?? 0,
                TrangThai = donHang.TrangThai ?? "Đang xử lý",
                MaKm = donHang.MaKm,
                NgayDatHang = donHang.NgayDatHang,

                NguoiDungList = GetNguoiDungList(),
                KhuyenMaiList = GetKhuyenMaiList()
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaDonHang(int id, DonHangEditViewModel model)
        {
            var donHang = _context.DonHangs.FirstOrDefault(d => d.MaDh == id);
            if (donHang == null) return NotFound();

            var trangThaiHienTai = donHang.TrangThai ?? "Đang xử lý";
            var trangThaiMoi = model.TrangThai ?? "Đang xử lý";

            // 1. Không được chuyển trạng thái ngược lại
            if ((trangThaiHienTai == "Đã xử lý" || trangThaiHienTai == "Hoàn thành") &&
                (trangThaiMoi == "Đang xử lý" || trangThaiMoi == "Hủy"))
            {
                ModelState.AddModelError("TrangThai", "Không được chuyển từ trạng thái đã xử lý/hoàn thành về trạng thái trước đó.");
            }

            // 2. Nếu đơn hàng đã xử lý, khóa không cho sửa Mã người dùng, Tổng tiền, KM
            if (trangThaiHienTai == "Đã xử lý" || trangThaiHienTai == "Hoàn thành")
            {
                if (model.MaNd != donHang.MaNd)
                {
                    ModelState.AddModelError("MaNd", "Không được thay đổi người đặt hàng khi đơn hàng đã xử lý hoặc hoàn thành.");
                }

                if (model.TongTien != donHang.TongTien)
                {
                    ModelState.AddModelError("TongTien", "Không được thay đổi tổng tiền khi đơn hàng đã xử lý hoặc hoàn thành.");
                }

                if (model.MaKm != donHang.MaKm)
                {
                    ModelState.AddModelError("MaKm", "Không được thay đổi khuyến mãi khi đơn hàng đã xử lý hoặc hoàn thành.");
                }
            }

            if (ModelState.IsValid)
            {
                donHang.TrangThai = trangThaiMoi;
                donHang.NgayDatHang = model.NgayDatHang ?? donHang.NgayDatHang;

                // Các trường chỉ cập nhật nếu đơn hàng chưa xử lý
                if (trangThaiHienTai != "Đã xử lý" && trangThaiHienTai != "Hoàn thành")
                {
                    donHang.MaNd = model.MaNd;
                    donHang.TongTien = model.TongTien;
                    donHang.MaKm = model.MaKm;
                }

                _context.SaveChanges();
                return RedirectToAction("Index", "DonHang");
            }

            // Nếu có lỗi, nạp lại dropdown
            model.NguoiDungList = GetNguoiDungList();
            model.KhuyenMaiList = GetKhuyenMaiList();

            return View(model);
        }





        // Helper methods để tránh lặp lại
        private IEnumerable<SelectListItem> GetNguoiDungList()
        {
            return _context.NguoiDungs.Select(nd => new SelectListItem
            {
                Value = nd.MaNd.ToString(),
                Text = nd.TenDangNhap
            });
        }

        private IEnumerable<SelectListItem> GetKhuyenMaiList()
        {
            return _context.KhuyenMais.Select(km => new SelectListItem
            {
                Value = km.MaKm.ToString(),
                Text = km.TenKhuyenMai
            });
        }




        [HttpGet]
        public IActionResult XoaDonHang(int id)
        {
            var donHang = _context.DonHangs
                .Include(d => d.MaNdNavigation)
                .FirstOrDefault(d => d.MaDh == id);

            if (donHang == null)
            {
                return NotFound();
            }

            var viewModel = new DonHangDeleteViewModel
            {
                MaDh = donHang.MaDh,
                TenDangNhap = donHang.MaNdNavigation?.TenDangNhap ?? "Không rõ",
                TongTien = donHang.TongTien,
                NgayDatHang = donHang.NgayDatHang,
                TrangThai = donHang.TrangThai
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult XoaDonHangConfirmed(DonHangDeleteViewModel model)
        {
            var donHang = _context.DonHangs
                .Include(d => d.MaNdNavigation)
                .FirstOrDefault(d => d.MaDh == model.MaDh);

            if (donHang == null)
            {
                return NotFound();
            }

            if (donHang.TrangThai == "Đã xử lý" || donHang.TrangThai == "Hoàn thành")
            {
                ModelState.AddModelError(string.Empty, "Không thể xóa đơn hàng đã được xử lý hoặc hoàn thành.");

                // Nạp lại view model đầy đủ để giữ giao diện
                var viewModel = new DonHangDeleteViewModel
                {
                    MaDh = donHang.MaDh,
                    TenDangNhap = donHang.MaNdNavigation?.TenDangNhap ?? "Không rõ",
                    TongTien = donHang.TongTien,
                    NgayDatHang = donHang.NgayDatHang,
                    TrangThai = donHang.TrangThai
                };

                return View("XoaDonHang", viewModel);
            }

            _context.DonHangs.Remove(donHang);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }



        // Thêm ChiTietDonHang


        [HttpGet]
        public IActionResult ThemChiTietDonHang(int maDh)
        {
            var viewModel = new ChiTietDonHangViewModel
            {
                MaDh = maDh,
                SanPhamList = _context.SanPhams.Select(sp => new SelectListItem
                {
                    Value = sp.MaSp.ToString(),
                    Text = sp.TenSp
                })
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemChiTietDonHang(ChiTietDonHangViewModel model)
        {
            if (ModelState.IsValid)
            {
                var sanPham = _context.SanPhams.Find(model.MaSp);
                if (sanPham == null)
                {
                    ModelState.AddModelError("", "Sản phẩm không tồn tại.");
                }
                else
                {
                    // ✅ Tính tồn kho hiện tại
                    int tongNhap = _context.ChiTietNhapKhos
                        .Where(c => c.MaSp == model.MaSp)
                        .Sum(c => (int?)c.SoLuong) ?? 0;

                    int tongBan = _context.ChiTietDonHangs
                        .Where(c => c.MaSp == model.MaSp)
                        .Sum(c => (int?)c.SoLuong) ?? 0;

                    int tonKho = tongNhap - tongBan;

                    // ✅ Kiểm tra nếu vượt quá tồn kho
                    if (model.SoLuong > tonKho)
                    {
                        ModelState.AddModelError("", $"Số lượng tồn kho hiện tại chỉ còn {tonKho}. Không thể bán {model.SoLuong} sản phẩm.");
                    }
                    else
                    {
                        // ✅ Chọn giá ưu tiên
                        var donGia = sanPham.GiaKhuyenMai.HasValue && sanPham.GiaKhuyenMai > 0
                                     ? sanPham.GiaKhuyenMai.Value
                                     : sanPham.Gia;

                        var chiTiet = new ChiTietDonHang
                        {
                            MaDh = model.MaDh,
                            MaSp = model.MaSp,
                            SoLuong = model.SoLuong,
                            DonGia = donGia
                        };

                        _context.ChiTietDonHangs.Add(chiTiet);
                        _context.SaveChanges();

                        // ✅ Cập nhật tổng tiền đơn hàng
                        var tongTien = _context.ChiTietDonHangs
                            .Where(ct => ct.MaDh == model.MaDh)
                            .Sum(ct => ct.SoLuong * ct.DonGia);

                        var donHang = _context.DonHangs.Find(model.MaDh);
                        if (donHang != null)
                        {
                            donHang.TongTien = tongTien;
                            _context.SaveChanges();
                        }

                        return RedirectToAction("XemChiTietDonHang", new { id = model.MaDh });
                    }
                }
            }

            // Nếu có lỗi, nạp lại danh sách sản phẩm
            model.SanPhamList = _context.SanPhams.Select(sp => new SelectListItem
            {
                Value = sp.MaSp.ToString(),
                Text = sp.TenSp
            });

            return View(model);
        }


        // Sửa chi tiết đơn hàng 


        [HttpGet]
        public IActionResult SuaChiTietDonHang(int id)
        {

            ViewBag.DanhSachSanPham = _context.SanPhams
    .Select(sp => new SelectListItem
    {
        Value = sp.MaSp.ToString(),
        Text = sp.TenSp
    }).ToList();
            var chiTiet = _context.ChiTietDonHangs
                .Include(ct => ct.MaSpNavigation)
                .FirstOrDefault(ct => ct.MaChiTietDh == id);

            if (chiTiet == null) return NotFound();

            var model = new ChiTietDonHangEditViewModel
            {
                MaChiTiet = chiTiet.MaChiTietDh,
                MaDh = chiTiet.MaDh,
                TenSanPham = chiTiet.MaSpNavigation.TenSp,
                SoLuong = chiTiet.SoLuong ?? 0,
                DonGia = chiTiet.DonGia ?? 0
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaChiTietDonHang(ChiTietDonHangEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.DanhSachSanPham = _context.SanPhams
                    .Select(sp => new SelectListItem
                    {
                        Value = sp.MaSp.ToString(),
                        Text = sp.TenSp
                    }).ToList();

                return View(model);
            }

            var chiTiet = _context.ChiTietDonHangs.FirstOrDefault(c => c.MaChiTietDh == model.MaChiTiet);
            if (chiTiet == null) return NotFound();

            // Lấy số lượng cũ để tính lại tồn kho cho hợp lý
            int soLuongBanCu = chiTiet.SoLuong ?? 0;

            // Tính lại tồn kho
            int tongNhap = _context.ChiTietNhapKhos
                .Where(c => c.MaSp == model.MaSp)
                .Sum(c => (int?)c.SoLuong) ?? 0;

            int tongBanTruTruoc = _context.ChiTietDonHangs
                .Where(c => c.MaSp == model.MaSp && c.MaChiTietDh != chiTiet.MaChiTietDh)
                .Sum(c => (int?)c.SoLuong) ?? 0;

            int tonKhoConLai = tongNhap - tongBanTruTruoc;

            if (model.SoLuong > tonKhoConLai)
            {
                ModelState.AddModelError("", $"Tồn kho hiện tại chỉ còn {tonKhoConLai}. Không thể cập nhật với số lượng {model.SoLuong}.");

                // Nạp lại dropdown nếu có lỗi
                ViewBag.DanhSachSanPham = _context.SanPhams
                    .Select(sp => new SelectListItem
                    {
                        Value = sp.MaSp.ToString(),
                        Text = sp.TenSp
                    }).ToList();

                return View(model);
            }

            // Cập nhật chi tiết đơn hàng
            chiTiet.MaSp = model.MaSp;
            chiTiet.SoLuong = model.SoLuong;

            // Lấy giá ưu tiên nếu có khuyến mãi
            var sanPham = _context.SanPhams.FirstOrDefault(sp => sp.MaSp == model.MaSp);
            if (sanPham != null)
            {
                chiTiet.DonGia = sanPham.GiaKhuyenMai.HasValue && sanPham.GiaKhuyenMai > 0
                                 ? sanPham.GiaKhuyenMai.Value
                                 : sanPham.Gia;
            }

            _context.SaveChanges();

            // Cập nhật lại tổng tiền đơn hàng
            var tongTien = _context.ChiTietDonHangs
                .Where(ct => ct.MaDh == chiTiet.MaDh)
                .Sum(ct => (ct.SoLuong ?? 0) * (ct.DonGia ?? 0));

            var donHang = _context.DonHangs.Find(chiTiet.MaDh);
            if (donHang != null)
            {
                donHang.TongTien = tongTien;
                _context.SaveChanges();
            }

            return RedirectToAction("XemChiTietDonHang", new { id = chiTiet.MaDh });
        }




        // Lọc đơn hàng

        public IActionResult LocDonHang(int? page, int? maDh, int? maSp, string? tenSp, string? trangThai, DateTime? ngayTu, DateTime? ngayDen, decimal? tongTienTu, decimal? tongTienDen, string? tuKhoa)
        {
            var query = _context.DonHangs
                .Include(d => d.MaNdNavigation)
                 .Include(d => d.ChiTietDonHangs)
                .ThenInclude(ct => ct.MaSpNavigation)
                 .AsQueryable();

            // Lọc theo mã đơn
            if (maDh.HasValue)
            {
                query = query.Where(d => d.MaDh == maDh);
            }

            // Lọc theo trạng thái
            if (!string.IsNullOrEmpty(trangThai))
            {
                query = query.Where(d => d.TrangThai == trangThai);
            }

            // Lọc theo khoảng ngày
            if (ngayTu.HasValue)
            {
                query = query.Where(d => d.NgayDatHang >= ngayTu);
            }
            if (ngayDen.HasValue)
            {
                query = query.Where(d => d.NgayDatHang <= ngayDen);
            }

            // Lọc theo sản phẩm
            if (maSp.HasValue)
            {
                var dsMaDh = _context.ChiTietDonHangs
                    .Where(ct => ct.MaSp == maSp)
                    .Select(ct => ct.MaDh)
                    .Distinct();

                query = query.Where(d => dsMaDh.Contains(d.MaDh));
            }


            if (!string.IsNullOrEmpty(tenSp))
            {
                var maDonHangCoSanPham = _context.ChiTietDonHangs
                    .Include(ct => ct.MaSpNavigation)
                    .Where(ct => ct.MaSpNavigation.TenSp.Contains(tenSp))
                    .Select(ct => ct.MaDh)
                    .Distinct();

                query = query.Where(d => maDonHangCoSanPham.Contains(d.MaDh));
            }
            // Lọc theo tổng tiền
            if (tongTienTu.HasValue)
            {
                query = query.Where(d => d.TongTien >= tongTienTu);
            }
            if (tongTienDen.HasValue)
            {
                query = query.Where(d => d.TongTien <= tongTienDen);
            }

            // Lọc theo từ khóa (tên đăng nhập)
            if (!string.IsNullOrEmpty(tuKhoa))
            {
                query = query.Where(d => d.MaNdNavigation.TenDangNhap.Contains(tuKhoa));
            }

            int pageSize = 5;
            int pageNumber = page ?? 1;

            var donHangs = query.OrderByDescending(d => d.NgayDatHang).ToPagedList(pageNumber, pageSize);

            var viewModel = new DonHangFilterViewModel
            {
                MaDh = maDh,
                MaSp = maSp,
                TrangThai = trangThai,
                TuNgay = ngayTu,
                DenNgay = ngayDen,
                TongTienTu = tongTienTu,
                TongTienDen = tongTienDen,
                TuKhoa = tuKhoa,
                KetQua = donHangs
            };

            var thongKe = query
            .GroupBy(d => d.TrangThai)
         .Select(g => new {
         TrangThai = g.Key,
         SoLuong = g.Count()
         }).ToList();

            var thongKeDict = thongKe.ToDictionary(t => t.TrangThai ?? "", t => t.SoLuong);

            viewModel.ThongKeTheoTrangThai = new Dictionary<string, int>
         {
         { "Đang xử lý", thongKeDict.ContainsKey("Đang xử lý") ? thongKeDict["Đang xử lý"] : 0 },
         { "Đã xử lý", thongKeDict.ContainsKey("Đã xử lý") ? thongKeDict["Đã xử lý"] : 0 },
            { "Hoàn thành", thongKeDict.ContainsKey("Hoàn thành") ? thongKeDict["Hoàn thành"] : 0 },
        { "Hủy", thongKeDict.ContainsKey("Hủy") ? thongKeDict["Hủy"] : 0 }
        };


            return View("LocDonHang", viewModel);
        }

        // Quan ly nhap kho
        /*

        [HttpGet]
        public IActionResult ThemPhieuNhap()
        {
            var model = new NhapKhoCreateViewModel
            {
                MaPhieuNhap = "NK" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                SanPhamList = _context.SanPhams
                    .Select(sp => new SelectListItem
                    {
                        Value = sp.MaSp.ToString(),
                        Text = sp.TenSp
                    }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemPhieuNhap(NhapKhoCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.SanPhamList = _context.SanPhams
                    .Select(sp => new SelectListItem
                    {
                        Value = sp.MaSp.ToString(),
                        Text = sp.TenSp
                    }).ToList();
                return View(model);
            }
            if (model.MaNcc == null || !_context.NhaCungCaps.Any(n => n.MaNcc == model.MaNcc))
            {
                ModelState.AddModelError("MaNcc", "Bạn phải chọn nhà cung cấp hợp lệ.");
                model.SanPhamList = _context.SanPhams
                    .Select(sp => new SelectListItem
                    {
                        Value = sp.MaSp.ToString(),
                        Text = sp.TenSp
                    }).ToList();
                return View(model);
            }
            // 1. Thêm phiếu nhập
            var phieu = new NhapKho
            {
                MaPhieuNhap = model.MaPhieuNhap,
                NgayNhap = DateTime.Now,
                GhiChu = model.GhiChu,



                 MaNcc = model.MaNcc ?? 0 // PHẢI có giá trị hợp lệ > 0
            };
            _context.NhapKhos.Add(phieu);

            // 2. Thêm chi tiết nhập kho
            foreach (var ct in model.ChiTietNhap)
            {
                var maChiTiet = $"CTNK{DateTime.Now.Ticks}";
                var chiTiet = new ChiTietNhapKho
                {
                    MaChiTietNk = maChiTiet,
                    MaPhieuNhap = model.MaPhieuNhap,
                    MaSp = int.Parse(ct.MaSp),
                    SoLuong = ct.SoLuong,
                    DonGiaNhap = ct.DonGiaNhap
                };

                _context.ChiTietNhapKhos.Add(chiTiet);
            }

            _context.SaveChanges();
            return RedirectToAction("DanhSachPhieuNhap");
        }  */

        public IActionResult ThemPhieuNhap()
        {
            var model = new NhapKhoCreateViewModel
            {
                MaPhieuNhap = "NK" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                SanPhamList = _context.SanPhams
                    .Select(sp => new SelectListItem
                    {
                        Value = sp.MaSp.ToString(),
                        Text = sp.TenSp
                    }).ToList(),
                NhaCungCapList = _context.NhaCungCaps
                    .Select(n => new SelectListItem
                    {
                        Value = n.MaNcc.ToString(),
                        Text = n.TenNhaCungCap
                    }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemPhieuNhap(NhapKhoCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.SanPhamList = _context.SanPhams
                    .Select(sp => new SelectListItem
                    {
                        Value = sp.MaSp.ToString(),
                        Text = sp.TenSp
                    }).ToList();

                model.NhaCungCapList = _context.NhaCungCaps
                    .Select(n => new SelectListItem
                    {
                        Value = n.MaNcc.ToString(),
                        Text = n.TenNhaCungCap
                    }).ToList();

                return View(model);
            }

            // Kiểm tra nhà cung cấp có tồn tại không
            if (model.MaNcc == null || !_context.NhaCungCaps.Any(n => n.MaNcc == model.MaNcc))
            {
                ModelState.AddModelError("MaNcc", "Bạn phải chọn nhà cung cấp hợp lệ.");

                model.SanPhamList = _context.SanPhams
                    .Select(sp => new SelectListItem
                    {
                        Value = sp.MaSp.ToString(),
                        Text = sp.TenSp
                    }).ToList();

                model.NhaCungCapList = _context.NhaCungCaps
                    .Select(n => new SelectListItem
                    {
                        Value = n.MaNcc.ToString(),
                        Text = n.TenNhaCungCap
                    }).ToList();

                return View(model);
            }

            // 1. Thêm phiếu nhập
            var phieu = new NhapKho
            {
                MaPhieuNhap = model.MaPhieuNhap,
                NgayNhap = DateTime.Now,
                GhiChu = model.GhiChu,
                MaNcc = model.MaNcc.Value // Đảm bảo đã kiểm tra null ở trên
            };
            _context.NhapKhos.Add(phieu);

            // 2. Thêm chi tiết nhập kho
            for (int i = 0; i < model.ChiTietNhap.Count; i++)
            {
                var ct = model.ChiTietNhap[i];

                var maChiTiet = $"CTNK{DateTime.Now.Ticks}{i}";
                var chiTiet = new ChiTietNhapKho
                {
                    MaChiTietNk = maChiTiet,
                    MaPhieuNhap = model.MaPhieuNhap,
                    MaSp = int.Parse(ct.MaSp),
                    SoLuong = ct.SoLuong,
                    DonGiaNhap = ct.DonGiaNhap
                };

                _context.ChiTietNhapKhos.Add(chiTiet);
            }

            _context.SaveChanges();
            return RedirectToAction("DanhSachPhieuNhap");
        }






        public IActionResult DanhSachPhieuNhap(int? page)
        {
            int pageSize = 5;
            int pageNumber = page ?? 1;

            var danhSach = _context.NhapKhos
                .OrderByDescending(p => p.NgayNhap)
                .ToPagedList(pageNumber, pageSize);

            return View(danhSach);
        }


        [HttpGet]
        public IActionResult SuaPhieuNhap(string id)
        {
            var phieuNhap = _context.NhapKhos.FirstOrDefault(p => p.MaPhieuNhap == id);
            if (phieuNhap == null)
            {
                return NotFound();
            }

            var viewModel = new PhieuNhapEditViewModel
            {
                MaPhieuNhap = phieuNhap.MaPhieuNhap,
                NgayNhap = phieuNhap.NgayNhap,
                GhiChu = phieuNhap.GhiChu
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaPhieuNhap(PhieuNhapEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var phieuNhap = _context.NhapKhos.FirstOrDefault(p => p.MaPhieuNhap == model.MaPhieuNhap);
            if (phieuNhap == null)
            {
                return NotFound();
            }

            phieuNhap.NgayNhap = model.NgayNhap;
            phieuNhap.GhiChu = model.GhiChu;

            _context.SaveChanges();

            return RedirectToAction("DanhSachPhieuNhap");
        }
        [HttpGet]
        public IActionResult ChiTietPhieuNhap(string maPhieuNhap)
        {
            ViewBag.DanhSachSanPham = new SelectList(_context.SanPhams.ToList(), "MaSp", "TenSp");

            var model = new ChiTietPhieuNhapViewModel
            {
                MaPhieuNhap = maPhieuNhap
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChiTietPhieuNhap(ChiTietPhieuNhapViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.DanhSachSanPham = new SelectList(_context.SanPhams.ToList(), "MaSp", "TenSp");
                return View(model);
            }

            var chiTiet = new ChiTietNhapKho
            {
                MaPhieuNhap = model.MaPhieuNhap,
                MaSp = model.MaSp,
                SoLuong = model.SoLuongNhap,
                DonGiaNhap = model.DonGiaNhap
            };

            _context.ChiTietNhapKhos.Add(chiTiet);
            _context.SaveChanges();

            return RedirectToAction("DanhSachPhieuNhap");
        }

       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult XoaPhieuNhap(string id)
        {
            var phieuNhap = _context.NhapKhos.FirstOrDefault(p => p.MaPhieuNhap == id);
            if (phieuNhap == null)
                return NotFound();

            // Xóa các chi tiết nhập trước (nếu có)
            var chiTietList = _context.ChiTietNhapKhos.Where(c => c.MaPhieuNhap == id).ToList();
            if (chiTietList.Any())
            {
                _context.ChiTietNhapKhos.RemoveRange(chiTietList);
            }

            // Xóa phiếu nhập
            _context.NhapKhos.Remove(phieuNhap);
            _context.SaveChanges();

            return RedirectToAction("DanhSachPhieuNhap");
        }





        public IActionResult DanhSachChiTietPhieuNhap(string maPhieuNhap, int? page)
        {
            int pageSize = 5;
            int pageNumber = page ?? 1;

            var danhSach = _context.ChiTietNhapKhos
                .Where(c => c.MaPhieuNhap == maPhieuNhap)
                .Include(c => c.SanPham)
                .Select(c => new ChiTietPhieuNhapItemViewModel
                {
                    MaChiTietNk = c.MaChiTietNk,
                    MaPhieuNhap = c.MaPhieuNhap,
                    MaSp = c.MaSp,
                    TenSp = c.SanPham.TenSp,
                    SoLuong = c.SoLuong,
                    DonGiaNhap = c.DonGiaNhap,
                    NgayNhap = c.NhapKho.NgayNhap

                })
                .OrderByDescending(c => c.MaChiTietNk)
                .ToPagedList(pageNumber, pageSize);

            var viewModel = new ChiTietPhieuNhapListViewModel
            {
                MaPhieuNhap = maPhieuNhap,
                DanhSachChiTiet = danhSach
            };

            return View(viewModel);
        }


        [HttpGet]
        public IActionResult ThemChiTietPhieuNhap(string maPhieuNhap)
        {
            if (string.IsNullOrEmpty(maPhieuNhap))
            {
                return RedirectToAction("DanhSachPhieuNhap");
            }

            var phieuNhap = _context.NhapKhos.FirstOrDefault(p => p.MaPhieuNhap == maPhieuNhap);
            if (phieuNhap == null)
            {
                return NotFound();
            }

            var model = new ChiTietPhieuNhapCreateViewModel
            {
                MaPhieuNhap = maPhieuNhap,
                NgayNhap = phieuNhap.NgayNhap,
                SanPhamList = _context.SanPhams.Select(sp => new SelectListItem
                {
                    Value = sp.MaSp.ToString(),
                    Text = sp.TenSp
                }).ToList()
            };

            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemChiTietPhieuNhap(ChiTietPhieuNhapCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // ✅ Cần load lại danh sách sản phẩm nếu có lỗi nhập
                model.SanPhamList = _context.SanPhams.Select(sp => new SelectListItem
                {
                    Value = sp.MaSp.ToString(),
                    Text = sp.TenSp
                }).ToList();

                // ✅ Load lại ngày nhập nếu cần hiển thị readonly trong View
                var phieuNhap = _context.NhapKhos.FirstOrDefault(p => p.MaPhieuNhap == model.MaPhieuNhap);
                if (phieuNhap != null)
                {
                    model.NgayNhap = phieuNhap.NgayNhap;
                }

                return View(model);
            }

            // Tạo mã chi tiết tự động
            var maChiTiet = "CTNK" + DateTime.Now.Ticks;

            var chiTiet = new ChiTietNhapKho
            {
                MaChiTietNk = maChiTiet,
                MaPhieuNhap = model.MaPhieuNhap,
                MaSp = model.MaSp,
                SoLuong = model.SoLuong,
                DonGiaNhap = model.DonGiaNhap
            };

            _context.ChiTietNhapKhos.Add(chiTiet);
            _context.SaveChanges();

            return RedirectToAction("DanhSachChiTietPhieuNhap", new { maPhieuNhap = model.MaPhieuNhap });
        }

        [HttpGet]
        public IActionResult SuaChiTietPhieuNhap(string id)
        {
            var chiTiet = _context.ChiTietNhapKhos.FirstOrDefault(c => c.MaChiTietNk == id);
            if (chiTiet == null) return NotFound();

            var model = new ChiTietPhieuNhapCreateViewModel
            {
                MaChiTietNk = chiTiet.MaChiTietNk,
                MaPhieuNhap = chiTiet.MaPhieuNhap,
                MaSp = chiTiet.MaSp,
                SoLuong = chiTiet.SoLuong,
                DonGiaNhap = chiTiet.DonGiaNhap,
                NgayNhap = _context.NhapKhos.FirstOrDefault(p => p.MaPhieuNhap == chiTiet.MaPhieuNhap)?.NgayNhap ?? DateTime.Now,
                SanPhamList = _context.SanPhams.Select(sp => new SelectListItem
                {
                    Value = sp.MaSp.ToString(),
                    Text = sp.TenSp
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaChiTietPhieuNhap(ChiTietPhieuNhapCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.SanPhamList = _context.SanPhams.Select(sp => new SelectListItem
                {
                    Value = sp.MaSp.ToString(),
                    Text = sp.TenSp
                }).ToList();
                return View(model);
            }

            var chiTiet = _context.ChiTietNhapKhos.FirstOrDefault(c => c.MaChiTietNk == model.MaChiTietNk);
            if (chiTiet == null) return NotFound();

            chiTiet.MaSp = model.MaSp;
            chiTiet.SoLuong = model.SoLuong;
            chiTiet.DonGiaNhap = model.DonGiaNhap;
            _context.SaveChanges();

            return RedirectToAction("DanhSachChiTietPhieuNhap", new { maPhieuNhap = model.MaPhieuNhap });
        }

        [HttpGet]
        public IActionResult XemChiTietPhieuNhap(string id)
        {
            var chiTiet = _context.ChiTietNhapKhos
                .Include(c => c.SanPham)
                .Include(c => c.NhapKho)
                .FirstOrDefault(c => c.MaChiTietNk == id);

            if (chiTiet == null) return NotFound();

            var model = new ChiTietPhieuNhapItemViewModel
            {
                MaChiTietNk = chiTiet.MaChiTietNk,
                MaPhieuNhap = chiTiet.MaPhieuNhap,
                MaSp = chiTiet.MaSp,
                TenSp = chiTiet.SanPham.TenSp,
                SoLuong = chiTiet.SoLuong,
                DonGiaNhap = chiTiet.DonGiaNhap,
                NgayNhap = chiTiet.NhapKho.NgayNhap
            };

            return View(model);
        }


        [HttpGet]
        public IActionResult XacNhanXoaChiTietPhieuNhap(string id)
        {
            var chiTiet = _context.ChiTietNhapKhos
                .Include(c => c.SanPham)
                .FirstOrDefault(c => c.MaChiTietNk == id);

            if (chiTiet == null)
                return NotFound();

            return View(chiTiet); // Gửi tới view xác nhận xóa (nếu có)
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult XoaChiTietPhieuNhap(string id)
        {
            var chiTiet = _context.ChiTietNhapKhos.FirstOrDefault(c => c.MaChiTietNk == id);
            if (chiTiet == null)
                return NotFound();

            string maPhieuNhap = chiTiet.MaPhieuNhap;

            _context.ChiTietNhapKhos.Remove(chiTiet);
            _context.SaveChanges();

            return RedirectToAction("DanhSachChiTietPhieuNhap", new { maPhieuNhap });
        }




        // GET: admin/DonHang/LocPhieuNhap

        // GET: admin/DonHang/LocPhieuNhap
        // Controller LocPhieuNhap dạng GET, tương tự như LocDonHang
        [HttpGet]
        public IActionResult LocPhieuNhap(
            string? maPhieuNhap,
            int? maNcc,
            DateTime? tuNgay,
            DateTime? denNgay,
            decimal? tongTienTu,
            decimal? tongTienDen,
            int? page)
        {
            var query = _context.NhapKhos
                .Include(p => p.NhaCungCap)
                .Include(p => p.ChiTietNhapKhos)
                .AsQueryable();

            // Lọc theo mã phiếu
            if (!string.IsNullOrEmpty(maPhieuNhap))
            {
                query = query.Where(p => p.MaPhieuNhap.Contains(maPhieuNhap));
            }

            // Lọc theo nhà cung cấp
            if (maNcc.HasValue)
            {
                query = query.Where(p => p.MaNcc == maNcc);
            }

            // Lọc theo khoảng ngày
            if (tuNgay.HasValue)
            {
                query = query.Where(p => p.NgayNhap >= tuNgay);
            }
            if (denNgay.HasValue)
            {
                query = query.Where(p => p.NgayNhap <= denNgay);
            }

            // Lọc theo tổng tiền từ chi tiết
            if (tongTienTu.HasValue)
            {
                query = query.Where(p => p.ChiTietNhapKhos.Sum(c => c.SoLuong * c.DonGiaNhap) >= tongTienTu);
            }
            if (tongTienDen.HasValue)
            {
                query = query.Where(p => p.ChiTietNhapKhos.Sum(c => c.SoLuong * c.DonGiaNhap) <= tongTienDen);
            }

            // Phân trang
            int pageSize = 5;
            int pageNumber = page ?? 1;
            var ketQua = query.OrderByDescending(p => p.NgayNhap).ToPagedList(pageNumber, pageSize);

            var model = new PhieuNhapFilterViewModel
            {
                MaPhieuNhap = maPhieuNhap,
                MaNcc = maNcc,
                TuNgay = tuNgay,
                DenNgay = denNgay,
                TongTienTu = tongTienTu,
                TongTienDen = tongTienDen,
                KetQua = ketQua,
                DanhSachNhaCungCap = _context.NhaCungCaps
                    .Select(n => new SelectListItem
                    {
                        Value = n.MaNcc.ToString(),
                        Text = n.TenNhaCungCap
                    }).ToList()
            };
            model.DanhSachNhaCungCap.Insert(0, new SelectListItem { Value = "", Text = "-- Tất cả --" });

            return View("LocPhieuNhap", model);
        }





    }
}




    

    






    





