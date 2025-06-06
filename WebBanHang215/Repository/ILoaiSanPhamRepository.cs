using WebBanHang215.Models;

namespace WebBanHang215.Repository
{
    public interface ILoaiSanPhamRepository
    {
        LoaiSanPham Add(LoaiSanPham loaiSp);
        LoaiSanPham Update(LoaiSanPham loaiSp);
        void Delete(int maLoai); // ❗ vì MaLoai là int
        LoaiSanPham? GetLoaiSanPham(int maLoai);
        IEnumerable<LoaiSanPham> GetAllLoaiSanPham();
    }
}
