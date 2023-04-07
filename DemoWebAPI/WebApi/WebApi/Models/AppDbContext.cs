using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;

namespace WebApi.Models
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> option) : base(option)
        {
            
        }
        public DbSet<DonHang> DonHangs { get; set; }
        public DbSet<DonHangChiTiet> DonHangChiTiets { get; set; }
        public DbSet<HangHoa> HangHoas { get; set; }
        public DbSet<Loai> Loais { get; set; }
        public DbSet<NguoiDung> NguoiDungs { get; set; }
        public DbSet<Book> Books { get; set; }


        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);

            model.Entity<DonHangChiTiet>(entity =>
            {
                entity.HasIndex(z => new { z.HangHoaID, z.DonHangID });
                //entity.HasOne(c => c.DonHang).WithMany(c => c.DonHangChiTiets).HasConstraintName("FK_DonHang_DonHangChiTiet");
                //entity.HasOne(c => c.HangHoa).WithMany(c => c.DonHangChiTiets).HasConstraintName("FK_HangHoa_DonHangChiTiet");
            });
        }
    }
}
