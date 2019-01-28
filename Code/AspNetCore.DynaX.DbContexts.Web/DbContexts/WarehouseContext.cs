using AspNetCore.DynaX.DbContexts.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.DynaX.DbContexts.Web.DbContexts
{
    public class WarehouseContext : DbContext
    {
        protected WarehouseContext() { }

        public WarehouseContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<WarehouseInfo> WarehouseInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 仓库信息
            modelBuilder.Entity<WarehouseInfo>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Province).IsRequired().HasMaxLength(50);
                entity.Property(e => e.City).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Address).IsRequired().HasMaxLength(400);
                entity.Property(e => e.Longitude).HasPrecision(10, 7); //.HasColumnType("decimal(10,7)")
                entity.Property(e => e.Latitude).HasPrecision(10, 7);
            });
        }
    }
}
