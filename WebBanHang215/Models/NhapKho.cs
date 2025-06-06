using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBanHang215.Models
{

    [Table("NhapKho")]
    public class NhapKho

    {
        [Key]
        [StringLength(20)]
        public string MaPhieuNhap { get; set; } = null!; // Ví dụ: "NK20240506001"

        [Required]
        public DateTime NgayNhap { get; set; } = DateTime.Now;

        // ✅ Thêm khóa ngoại tới NhaCungCap
        public int MaNcc { get; set; }

        [ForeignKey("MaNcc")]
        public NhaCungCap NhaCungCap { get; set; } = null!;

        public string? GhiChu { get; set; }

        // Liên kết 1-n với ChiTietNhapKho
        public virtual ICollection<ChiTietNhapKho> ChiTietNhapKhos { get; set; } = new List<ChiTietNhapKho>();
    }
}