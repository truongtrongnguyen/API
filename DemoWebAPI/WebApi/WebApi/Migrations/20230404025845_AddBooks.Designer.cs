﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApi.Models;

#nullable disable

namespace WebApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230404025845_AddBooks")]
    partial class AddBooks
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WebApi.Data.DonHang", b =>
                {
                    b.Property<int>("MaDonHang")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaDonHang"), 1L, 1);

                    b.Property<string>("DiachiGiao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("NgayDat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NgayGiao")
                        .HasColumnType("datetime2");

                    b.Property<string>("NguoiNhan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SoDienThoai")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TinhTrangDonHang")
                        .HasColumnType("int");

                    b.HasKey("MaDonHang");

                    b.ToTable("DonHangs");
                });

            modelBuilder.Entity("WebApi.Data.DonHangChiTiet", b =>
                {
                    b.Property<int>("DonHangChiTietID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DonHangChiTietID"), 1L, 1);

                    b.Property<double>("DonGia")
                        .HasColumnType("float");

                    b.Property<int>("DonHangID")
                        .HasColumnType("int");

                    b.Property<int?>("ForeignKey_MaDonHang")
                        .HasColumnType("int");

                    b.Property<int?>("ForeignKey_MaHangHoa")
                        .HasColumnType("int");

                    b.Property<byte>("GiamGia")
                        .HasColumnType("tinyint");

                    b.Property<int>("HangHoaID")
                        .HasColumnType("int");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.HasKey("DonHangChiTietID");

                    b.HasIndex("ForeignKey_MaDonHang");

                    b.HasIndex("ForeignKey_MaHangHoa");

                    b.HasIndex("HangHoaID", "DonHangID");

                    b.ToTable("DonHangChiTiets");
                });

            modelBuilder.Entity("WebApi.Data.HangHoa", b =>
                {
                    b.Property<int>("Mahanghoa")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Mahanghoa"), 1L, 1);

                    b.Property<double>("Dongia")
                        .HasColumnType("float");

                    b.Property<int?>("ForeignKey_LoaiID")
                        .HasColumnType("int");

                    b.Property<byte>("Giamgia")
                        .HasColumnType("tinyint");

                    b.Property<int>("LoaiID")
                        .HasColumnType("int");

                    b.Property<string>("Mota")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tenhanghoa")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Mahanghoa");

                    b.HasIndex("ForeignKey_LoaiID");

                    b.ToTable("HangHoa");
                });

            modelBuilder.Entity("WebApi.Data.Loai", b =>
                {
                    b.Property<int>("MaLoai")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaLoai"), 1L, 1);

                    b.Property<string>("TenLoai")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("MaLoai");

                    b.ToTable("Loai");
                });

            modelBuilder.Entity("WebApi.Data.NguoiDung", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HoTen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PassWork")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("ID");

                    b.ToTable("NguoiDung");
                });

            modelBuilder.Entity("WebApi.Models.Book", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"), 1L, 1);

                    b.Property<string>("DesCriptions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("WebApi.Data.DonHangChiTiet", b =>
                {
                    b.HasOne("WebApi.Data.DonHang", "DonHang")
                        .WithMany("DonHangChiTiets")
                        .HasForeignKey("ForeignKey_MaDonHang");

                    b.HasOne("WebApi.Data.HangHoa", "HangHoa")
                        .WithMany("DonHangChiTiets")
                        .HasForeignKey("ForeignKey_MaHangHoa");

                    b.Navigation("DonHang");

                    b.Navigation("HangHoa");
                });

            modelBuilder.Entity("WebApi.Data.HangHoa", b =>
                {
                    b.HasOne("WebApi.Data.Loai", "Loai")
                        .WithMany("ListHangHoa")
                        .HasForeignKey("ForeignKey_LoaiID");

                    b.Navigation("Loai");
                });

            modelBuilder.Entity("WebApi.Data.DonHang", b =>
                {
                    b.Navigation("DonHangChiTiets");
                });

            modelBuilder.Entity("WebApi.Data.HangHoa", b =>
                {
                    b.Navigation("DonHangChiTiets");
                });

            modelBuilder.Entity("WebApi.Data.Loai", b =>
                {
                    b.Navigation("ListHangHoa");
                });
#pragma warning restore 612, 618
        }
    }
}
