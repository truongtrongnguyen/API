using Microsoft.EntityFrameworkCore;
using WebAPI_Version.Models;

namespace WebAPI_Version.Data
{
    public class MyDbContext:DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> option): base(option)
        {

        }
        public DbSet<NguoiDung> NguoiDung { get; set; }
        public DbSet<Data_HangHoa> HangHoas { get; set; }
        public DbSet<Loai> Loais { get; set; }
        public DbSet<DonHang> DonHang { get; set; }
        public DbSet<DonHangChiTiet> DonHangChiTiet { get; set; }
        public DbSet<RefreshToken> RefreshTokens{ get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DonHang>(entity =>
            {
                entity.ToTable("DonHang")
                .HasKey(dh => dh.MaDH);
                entity.Property(dh => dh.NgayDat).HasDefaultValueSql("getutcdate()");
            });

            modelBuilder.Entity<DonHangChiTiet>(entity =>
            {
                entity.ToTable("DonHangChiTiet");
                entity.HasKey(dhct => new { dhct.MaHH, dhct.MaDH });

                entity.HasOne(dhct => dhct.DonHang).WithMany(dh => dh.DonHangChiTiets)
                .HasForeignKey(dhct => dhct.MaDH).HasConstraintName("FK_DHCT_DH");

                entity.HasOne(hh => hh.HangHoa).WithMany(dhct => dhct.DonHangChiTiets)
                .HasForeignKey(hh => hh.MaHH).HasConstraintName("FK_DHCT_HH");
            });

            modelBuilder.Entity<NguoiDung>(entity =>
            {
                entity.ToTable("NguoiDung").HasIndex(nd => nd.UserName).IsUnique();
            });
        }
    }
}
