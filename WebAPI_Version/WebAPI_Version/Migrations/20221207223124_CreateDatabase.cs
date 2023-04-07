using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI_Version.Migrations
{
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HangHoa",
                columns: table => new
                {
                    Mahanghoa = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tenhanghoa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dongia = table.Column<double>(type: "float", nullable: false),
                    Giamgia = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HangHoa", x => x.Mahanghoa);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HangHoa");
        }
    }
}
