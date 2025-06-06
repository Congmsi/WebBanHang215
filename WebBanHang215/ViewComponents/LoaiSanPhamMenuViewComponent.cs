using WebBanHang215.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlTypes;
using WebBanHang215.Repository;
namespace WebBanHang215.ViewComponents
{
    public class LoaiSanPhamMenuViewComponent: ViewComponent
    {
        private readonly ILoaiSanPhamRepository _LoaiSP;
        public LoaiSanPhamMenuViewComponent(ILoaiSanPhamRepository loaiSanPhamRepository)
        {
            _LoaiSP = loaiSanPhamRepository;
        }

        public IViewComponentResult Invoke()
        {
            var LoaiSP = _LoaiSP.GetAllLoaiSanPham().OrderBy(x => x.TenLoai);
            return View(LoaiSP);
        }
    }
}
 