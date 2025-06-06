using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBanHang215.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NhapKho",
                columns: table => new
                {
                    MaPhieuNhap = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NgayNhap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaNcc = table.Column<int>(type: "int", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhapKho", x => x.MaPhieuNhap);
                    table.ForeignKey(
                        name: "FK_NhapKho_NhaCungCap_MaNcc",
                        column: x => x.MaNcc,
                        principalTable: "NhaCungCap",
                        principalColumn: "MaNcc",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietNhapKho",
                columns: table => new
                {
                    MaChiTietNk = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    MaPhieuNhap = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MaSp = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    DonGiaNhap = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietNhapKho", x => x.MaChiTietNk);
                    table.ForeignKey(
                        name: "FK_ChiTietNhapKho_NhapKho_MaPhieuNhap",
                        column: x => x.MaPhieuNhap,
                        principalTable: "NhapKho",
                        principalColumn: "MaPhieuNhap",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietNhapKho_SanPham_MaSp",
                        column: x => x.MaSp,
                        principalTable: "SanPham",
                        principalColumn: "MaSp",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietNhapKho_MaPhieuNhap",
                table: "ChiTietNhapKho",
                column: "MaPhieuNhap");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietNhapKho_MaSp",
                table: "ChiTietNhapKho",
                column: "MaSp");

            migrationBuilder.CreateIndex(
                name: "IX_NhapKho_MaNcc",
                table: "NhapKho",
                column: "MaNcc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietNhapKho");

            migrationBuilder.DropTable(
                name: "NhapKho");
        }
    }
}
