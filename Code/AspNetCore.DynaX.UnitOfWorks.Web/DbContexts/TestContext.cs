using AspNetCore.DynaX.UnitOfWorks.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.DynaX.UnitOfWorks.Web.DbContexts
{
    public class TestContext : DbContext
    {
        protected TestContext() { }

        public TestContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<WarehouseInfo> WarehouseInfos { get; set; }
        public virtual DbSet<TestData> TestDatas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 仓库信息
            modelBuilder.Entity<WarehouseInfo>(entity =>
            {
                entity.ToTable("WarehouseInfo");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Province).IsRequired().HasMaxLength(50);
                entity.Property(e => e.City).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Address).IsRequired().HasMaxLength(400);
                entity.Property(e => e.Longitude).HasPrecision(10, 7);
                entity.Property(e => e.Latitude).HasPrecision(10, 7);
            });

            //测试数据
            modelBuilder.Entity<TestData>(entity =>
            {
                entity.ToTable("TestData");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.String).IsRequired().HasMaxLength(100);
            });
        }
    }
}
