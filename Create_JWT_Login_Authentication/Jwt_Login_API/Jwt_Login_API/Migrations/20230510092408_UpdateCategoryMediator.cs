using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jwt_Login_API.Migrations
{
    public partial class UpdateCategoryMediator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreate",
                table: "CategoryMediators",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUpdate",
                table: "CategoryMediators",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Descriptions",
                table: "CategoryMediators",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CategoryMediators",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreate",
                table: "CategoryMediators");

            migrationBuilder.DropColumn(
                name: "DateUpdate",
                table: "CategoryMediators");

            migrationBuilder.DropColumn(
                name: "Descriptions",
                table: "CategoryMediators");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CategoryMediators");
        }
    }
}
