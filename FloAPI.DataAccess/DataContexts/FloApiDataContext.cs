using FloAPI.DataAccess.Entities;
using FloAPI.DataAccess.Extensions;
using Microsoft.EntityFrameworkCore;

namespace FloAPI.DataAccess.DataContexts
{
    public class FloApiDataContext : DbContext
    {
        public FloApiDataContext(DbContextOptions<FloApiDataContext> options):
            base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseIdentityColumns();
            
            modelBuilder.Entity<Material>()
                .Property(m => m.Id)
                .UseIdentityAlwaysColumn();
            
            modelBuilder.Entity<Barcode>()
                .Property(b => b.Id)
                .UseIdentityAlwaysColumn();
            
            modelBuilder.Entity<Record>()
                .Property(r => r.Id)
                .UseIdentityAlwaysColumn();
            
            modelBuilder.Entity<Record>()
                .HasMany(m => m.Materials)
                .WithOne(r => r.Record)
                .OnDelete(DeleteBehavior.ClientNoAction);

            modelBuilder.Entity<Barcode>()
                .HasMany(m => m.Materials)
                .WithOne(b => b.Barcode)
                .OnDelete(DeleteBehavior.ClientNoAction);

            modelBuilder.Seed();
        }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Barcode> Barcodes { get; set; }
        public DbSet<Record> Records { get; set;  }
    }
}
