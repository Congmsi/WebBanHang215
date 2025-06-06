using Microsoft.AspNetCore.Mvc;
using WebBanHang215.Models;
using Microsoft.AspNetCore.Http;

namespace WebBanHang215.Controllers
{
    public class DangNhapController : Controller
    {
        private readonly WebBanHangCong215Context _context;

        public DangNhapController(WebBanHangCong215Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("TenDangNhap") == null)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Login(NguoiDung user)
        {
            if (HttpContext.Session.GetString("TenDangNhap") == null)
            {
                var u = _context.NguoiDungs
                    .FirstOrDefault(x => x.TenDangNhap == user.TenDangNhap && x.MatKhau == user.MatKhau);

                if (u != null)
                {
                    HttpContext.Session.SetString("TenDangNhap", u.TenDangNhap);
                    return RedirectToAction("Index", "Home");
                }

                ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng!";
            }

            return View();
        }

       
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("TenDangNhap");
            return RedirectToAction("Login", "DangNhap");
        }
    }
}
