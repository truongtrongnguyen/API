using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DonHangs",
                columns: table => new
                {
                    MaDonHang = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgayDat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayGiao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TinhTrangDonHang = table.Column<int>(type: "int", nullable: false),
                    NguoiNhan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiachiGiao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonHangs", x => x.MaDonHang);
                });

            migrationBuilder.CreateTable(
                name: "Loai",
                columns: table => new
                {
                    MaLoai = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loai", x => x.MaLoai);
                });

            migrationBuilder.CreateTable(
                name: "NguoiDung",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PassWork = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDung", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "HangHoa",
                columns: table => new
                {
                    Mahanghoa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tenhanghoa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dongia = table.Column<double>(type: "float", nullable: false),
                    Giamgia = table.Column<byte>(type: "tinyint", nullable: false),
                    LoaiID = table.Column<int>(type: "int", nullable: false),
                    ForeignKey_LoaiID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HangHoa", x => x.Mahanghoa);
                    table.ForeignKey(
                        name: "FK_HangHoa_Loai_ForeignKey_LoaiID",
                        column: x => x.ForeignKey_LoaiID,
                        principalTable: "Loai",
                        principalColumn: "MaLoai");
                });

            migrationBuilder.CreateTable(
                name: "DonHangChiTiets",
                columns: table => new
                {
                    DonHangChiTietID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    DonGia = table.Column<double>(type: "float", nullable: false),
                    GiamGia = table.Column<byte>(type: "tinyint", nullable: false),
                    DonHangID = table.Column<int>(type: "int", nullable: false),
                    ForeignKey_MaDonHang = table.Column<int>(type: "int", nullable: true),
                    HangHoaID = table.Column<int>(type: "int", nullable: false),
                    ForeignKey_MaHangHoa = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonHangChiTiets", x => x.DonHangChiTietID);
                    table.ForeignKey(
                        name: "FK_DonHangChiTiets_DonHangs_ForeignKey_MaDonHang",
                        column: x => x.ForeignKey_MaDonHang,
                        principalTable: "DonHangs",
                        principalColumn: "MaDonHang");
                    table.ForeignKey(
                        name: "FK_DonHangChiTiets_HangHoa_ForeignKey_MaHangHoa",
                        column: x => x.ForeignKey_MaHangHoa,
                        principalTable: "HangHoa",
                        principalColumn: "Mahanghoa");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DonHangChiTiets_ForeignKey_MaDonHang",
                table: "DonHangChiTiets",
                column: "ForeignKey_MaDonHang");

            migrationBuilder.CreateIndex(
                name: "IX_DonHangChiTiets_ForeignKey_MaHangHoa",
                table: "DonHangChiTiets",
                column: "ForeignKey_MaHangHoa");

            migrationBuilder.CreateIndex(
                name: "IX_DonHangChiTiets_HangHoaID_DonHangID",
                table: "DonHangChiTiets",
                columns: new[] { "HangHoaID", "DonHangID" });

            migrationBuilder.CreateIndex(
                name: "IX_HangHoa_ForeignKey_LoaiID",
                table: "HangHoa",
                column: "ForeignKey_LoaiID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DonHangChiTiets");

            migrationBuilder.DropTable(
                name: "NguoiDung");

            migrationBuilder.DropTable(
                name: "DonHangs");

            migrationBuilder.DropTable(
                name: "HangHoa");

            migrationBuilder.DropTable(
                name: "Loai");
        }
    }
}
