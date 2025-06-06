using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace WebBanHang215.ViewModels
{
    public class SanPhamAnhViewModel
    {
        public int MaSp { get; set; }
        public List<IFormFile> AnhMoi { get; set; } = new();
    }
}
