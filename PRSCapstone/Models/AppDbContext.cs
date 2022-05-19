using Microsoft.EntityFrameworkCore;

namespace PRSCapstone.Models {
    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<RequestLine>(entity =>
                entity.HasCheckConstraint("Quantity", "[Quantity] >= 0"));
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Vendor> Vendor { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Request> Requests { get; set; } = null!;
        public DbSet<RequestLine> RequestLines { get; set; } = null!;
    }
}
