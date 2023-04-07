using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI_Version.Migrations
{
    public partial class AddDongHang_DHCT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DonHang",
                columns: table => new
                {
                    MaDH = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NgayDat = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    NgayGiao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TinhTrangDonHang = table.Column<int>(type: "int", nullable: false),
                    NguoiNhan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiachiGiao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonHang", x => x.MaDH);
                });

            migrationBuilder.CreateTable(
                name: "DonHangChiTiet",
                columns: table => new
                {
                    MaHH = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaDH = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    DonGia = table.Column<double>(type: "float", nullable: false),
                    GiamGia = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonHangChiTiet", x => new { x.MaHH, x.MaDH });
                    table.ForeignKey(
                        name: "FK_DHCT_DH",
                        column: x => x.MaDH,
                        principalTable: "DonHang",
                        principalColumn: "MaDH",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DHCT_HH",
                        column: x => x.MaHH,
                        principalTable: "HangHoa",
                        principalColumn: "Mahanghoa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DonHangChiTiet_MaDH",
                table: "DonHangChiTiet",
                column: "MaDH");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DonHangChiTiet");

            migrationBuilder.DropTable(
                name: "DonHang");
        }
    }
}
