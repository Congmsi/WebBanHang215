using WebBanHang215.Models;

namespace WebBanHang215.Repository
{
    public class LoaiSanPhamRepository : ILoaiSanPhamRepository
    {
        private readonly WebBanHangCong215Context _context;

        public LoaiSanPhamRepository(WebBanHangCong215Context context)
        {
            _context = context;
        }
        public LoaiSanPham Add(LoaiSanPham loaiSp)
        {
            _context.LoaiSanPhams.Add(loaiSp);
            _context.SaveChanges();
            return loaiSp;
        }



        public void Delete(int maLoai)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LoaiSanPham> GetAllLoaiSanPham()
        {
            return _context.LoaiSanPhams;
        }

        public LoaiSanPham? GetLoaiSanPham(int maLoai)
        {
            return _context.LoaiSanPhams.Find(maLoai);
        }

        public LoaiSanPham Update(LoaiSanPham loaiSp)
        {
            _context.Update(loaiSp);
            _context.SaveChanges();
            return loaiSp;

        }
    }
}
