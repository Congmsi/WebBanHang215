using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBanHang215.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnsToDonHang : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GioiTinh",
                columns: table => new
                {
                    MaGioiTinh = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenGioiTinh = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__GioiTinh__46AF33FAA3D4A0C2", x => x.MaGioiTinh);
                });

            migrationBuilder.CreateTable(
                name: "HangSanXuat",
                columns: table => new
                {
                    MaHang = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenHang = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__HangSanX__19C0DB1D9931CE2C", x => x.MaHang);
                });

            migrationBuilder.CreateTable(
                name: "KhuyenMai",
                columns: table => new
                {
                    MaKm = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenKhuyenMai = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PhanTramGiam = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    NgayBatDau = table.Column<DateOnly>(type: "date", nullable: true),
                    NgayKetThuc = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__KhuyenMa__2725CF75552DF546", x => x.MaKm);
                });

            migrationBuilder.CreateTable(
                name: "LoaiSanPham",
                columns: table => new
                {
                    MaLoai = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoai = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LoaiSanP__730A575941BF782D", x => x.MaLoai);
                });

            migrationBuilder.CreateTable(
                name: "MauSac",
                columns: table => new
                {
                    MaMau = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenMau = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MaMauHex = table.Column<string>(type: "char(7)", unicode: false, fixedLength: true, maxLength: 7, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MauSac__3A5BBB7D4863F143", x => x.MaMau);
                });

            migrationBuilder.CreateTable(
                name: "NguoiDung",
                columns: table => new
                {
                    MaNd = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDangNhap = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SoDienThoai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    VaiTro = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    HoVaTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__NguoiDun__2725D7048DBFE1DE", x => x.MaNd);
                });

            migrationBuilder.CreateTable(
                name: "NhaCungCap",
                columns: table => new
                {
                    MaNcc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenNhaCungCap = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SoDienThoai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__NhaCungC__3A1951E314162D6D", x => x.MaNcc);
                });

            migrationBuilder.CreateTable(
                name: "Size",
                columns: table => new
                {
                    MaSize = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoaiSize = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TenSize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Size__A787E7ED6018E466", x => x.MaSize);
                });

            migrationBuilder.CreateTable(
                name: "TrangThaiSanPham",
                columns: table => new
                {
                    MaTrangThai = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTrangThai = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TrangTha__AADE41382D6E30CA", x => x.MaTrangThai);
                });

            migrationBuilder.CreateTable(
                name: "DonHang",
                columns: table => new
                {
                    MaDh = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNd = table.Column<int>(type: "int", nullable: false),
                    NgayDatHang = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    TongTien = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MaKm = table.Column<int>(type: "int", nullable: true),
                    DiaChiGiaoHang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayGiaoDuKien = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayGiaoThucTe = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HinhThucThanhToan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThaiThanhToan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaGiaoDich = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DonHang__27258641201D96E5", x => x.MaDh);
                    table.ForeignKey(
                        name: "FK_DonHang_KhuyenMai",
                        column: x => x.MaKm,
                        principalTable: "KhuyenMai",
                        principalColumn: "MaKm");
                    table.ForeignKey(
                        name: "FK_DonHang_NguoiDung",
                        column: x => x.MaNd,
                        principalTable: "NguoiDung",
                        principalColumn: "MaNd");
                });

            migrationBuilder.CreateTable(
                name: "GioHang",
                columns: table => new
                {
                    MaGioHang = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNd = table.Column<int>(type: "int", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__GioHang__F5001DA3CE6ED011", x => x.MaGioHang);
                    table.ForeignKey(
                        name: "FK_GioHang_NguoiDung",
                        column: x => x.MaNd,
                        principalTable: "NguoiDung",
                        principalColumn: "MaNd");
                });

            migrationBuilder.CreateTable(
                name: "SanPham",
                columns: table => new
                {
                    MaSp = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenSp = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gia = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GiaNhap = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GiaKhuyenMai = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SoLuongNhap = table.Column<int>(type: "int", nullable: true),
                    MaLoai = table.Column<int>(type: "int", nullable: false),
                    MaHang = table.Column<int>(type: "int", nullable: false),
                    MaNcc = table.Column<int>(type: "int", nullable: false),
                    MaTrangThai = table.Column<int>(type: "int", nullable: false),
                    MaGioiTinh = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SanPham__2725087C19D07A26", x => x.MaSp);
                    table.ForeignKey(
                        name: "FK_SanPham_GioiTinh",
                        column: x => x.MaGioiTinh,
                        principalTable: "GioiTinh",
                        principalColumn: "MaGioiTinh");
                    table.ForeignKey(
                        name: "FK_SanPham_HangSanXuat",
                        column: x => x.MaHang,
                        principalTable: "HangSanXuat",
                        principalColumn: "MaHang");
                    table.ForeignKey(
                        name: "FK_SanPham_LoaiSanPham",
                        column: x => x.MaLoai,
                        principalTable: "LoaiSanPham",
                        principalColumn: "MaLoai");
                    table.ForeignKey(
                        name: "FK_SanPham_NhaCungCap",
                        column: x => x.MaNcc,
                        principalTable: "NhaCungCap",
                        principalColumn: "MaNcc");
                    table.ForeignKey(
                        name: "FK_SanPham_TrangThai",
                        column: x => x.MaTrangThai,
                        principalTable: "TrangThaiSanPham",
                        principalColumn: "MaTrangThai");
                });

            migrationBuilder.CreateTable(
                name: "AnhSanPham",
                columns: table => new
                {
                    MaAnh = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaSp = table.Column<int>(type: "int", nullable: false),
                    URL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__AnhSanPh__356240DFD406E406", x => x.MaAnh);
                    table.ForeignKey(
                        name: "FK_AnhSanPham_SanPham",
                        column: x => x.MaSp,
                        principalTable: "SanPham",
                        principalColumn: "MaSp",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietDonHang",
                columns: table => new
                {
                    MaChiTietDH = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDh = table.Column<int>(type: "int", nullable: false),
                    MaSp = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: true),
                    DonGia = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChiTietD__651E6E6AF0163EB8", x => x.MaChiTietDH);
                    table.ForeignKey(
                        name: "FK_CTDonHang_DonHang",
                        column: x => x.MaDh,
                        principalTable: "DonHang",
                        principalColumn: "MaDh");
                    table.ForeignKey(
                        name: "FK_CTDonHang_SanPham",
                        column: x => x.MaSp,
                        principalTable: "SanPham",
                        principalColumn: "MaSp");
                });

            migrationBuilder.CreateTable(
                name: "ChiTietGioHang",
                columns: table => new
                {
                    MaChiTietGH = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaGioHang = table.Column<int>(type: "int", nullable: false),
                    MaSp = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChiTietG__651E71CECF02EB26", x => x.MaChiTietGH);
                    table.ForeignKey(
                        name: "FK_CTGioHang_GioHang",
                        column: x => x.MaGioHang,
                        principalTable: "GioHang",
                        principalColumn: "MaGioHang");
                    table.ForeignKey(
                        name: "FK_CTGioHang_SanPham",
                        column: x => x.MaSp,
                        principalTable: "SanPham",
                        principalColumn: "MaSp");
                });

            migrationBuilder.CreateTable(
                name: "ChiTietSanPham",
                columns: table => new
                {
                    MaCtsp = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaSp = table.Column<int>(type: "int", nullable: false),
                    MaSize = table.Column<int>(type: "int", nullable: false),
                    MaMau = table.Column<int>(type: "int", nullable: false),
                    SoLuongTon = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChiTietS__368D46B3F49A3762", x => x.MaCtsp);
                    table.ForeignKey(
                        name: "FK_CTSP_Mau",
                        column: x => x.MaMau,
                        principalTable: "MauSac",
                        principalColumn: "MaMau");
                    table.ForeignKey(
                        name: "FK_CTSP_SanPham",
                        column: x => x.MaSp,
                        principalTable: "SanPham",
                        principalColumn: "MaSp");
                    table.ForeignKey(
                        name: "FK_CTSP_Size",
                        column: x => x.MaSize,
                        principalTable: "Size",
                        principalColumn: "MaSize");
                });

            migrationBuilder.CreateTable(
                name: "KhuyenMai_SanPham",
                columns: table => new
                {
                    MaKm = table.Column<int>(type: "int", nullable: false),
                    MaSp = table.Column<int>(type: "int", nullable: false),
                    NgayBatDau = table.Column<DateOnly>(type: "date", nullable: true),
                    NgayKetThuc = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhuyenMai_SanPham", x => new { x.MaKm, x.MaSp });
                    table.ForeignKey(
                        name: "FK_KM_SP_KM",
                        column: x => x.MaKm,
                        principalTable: "KhuyenMai",
                        principalColumn: "MaKm");
                    table.ForeignKey(
                        name: "FK_KM_SP_SP",
                        column: x => x.MaSp,
                        principalTable: "SanPham",
                        principalColumn: "MaSp");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnhSanPham_MaSp",
                table: "AnhSanPham",
                column: "MaSp");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonHang_MaDh",
                table: "ChiTietDonHang",
                column: "MaDh");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonHang_MaSp",
                table: "ChiTietDonHang",
                column: "MaSp");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietGioHang_MaGioHang",
                table: "ChiTietGioHang",
                column: "MaGioHang");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietGioHang_MaSp",
                table: "ChiTietGioHang",
                column: "MaSp");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietSanPham_MaMau",
                table: "ChiTietSanPham",
                column: "MaMau");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietSanPham_MaSize",
                table: "ChiTietSanPham",
                column: "MaSize");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietSanPham_MaSp",
                table: "ChiTietSanPham",
                column: "MaSp");

            migrationBuilder.CreateIndex(
                name: "IX_DonHang_MaKm",
                table: "DonHang",
                column: "MaKm");

            migrationBuilder.CreateIndex(
                name: "IX_DonHang_MaNd",
                table: "DonHang",
                column: "MaNd");

            migrationBuilder.CreateIndex(
                name: "IX_GioHang_MaNd",
                table: "GioHang",
                column: "MaNd");

            migrationBuilder.CreateIndex(
                name: "IX_KhuyenMai_SanPham_MaSp",
                table: "KhuyenMai_SanPham",
                column: "MaSp");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_MaGioiTinh",
                table: "SanPham",
                column: "MaGioiTinh");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_MaHang",
                table: "SanPham",
                column: "MaHang");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_MaLoai",
                table: "SanPham",
                column: "MaLoai");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_MaNcc",
                table: "SanPham",
                column: "MaNcc");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_MaTrangThai",
                table: "SanPham",
                column: "MaTrangThai");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnhSanPham");

            migrationBuilder.DropTable(
                name: "ChiTietDonHang");

            migrationBuilder.DropTable(
                name: "ChiTietGioHang");

            migrationBuilder.DropTable(
                name: "ChiTietSanPham");

            migrationBuilder.DropTable(
                name: "KhuyenMai_SanPham");

            migrationBuilder.DropTable(
                name: "DonHang");

            migrationBuilder.DropTable(
                name: "GioHang");

            migrationBuilder.DropTable(
                name: "MauSac");

            migrationBuilder.DropTable(
                name: "Size");

            migrationBuilder.DropTable(
                name: "SanPham");

            migrationBuilder.DropTable(
                name: "KhuyenMai");

            migrationBuilder.DropTable(
                name: "NguoiDung");

            migrationBuilder.DropTable(
                name: "GioiTinh");

            migrationBuilder.DropTable(
                name: "HangSanXuat");

            migrationBuilder.DropTable(
                name: "LoaiSanPham");

            migrationBuilder.DropTable(
                name: "NhaCungCap");

            migrationBuilder.DropTable(
                name: "TrangThaiSanPham");
        }
    }
}
