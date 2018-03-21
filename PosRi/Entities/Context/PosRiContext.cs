using Microsoft.EntityFrameworkCore;

namespace PosRi.Entities.Context
{
    public class PosRiContext : DbContext
    {
        public PosRiContext(DbContextOptions<PosRiContext> options)
            : base (options)
        {
            Database.Migrate();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<CashFound> CashFounds { get; set; }
        public DbSet<CashRegister> CashRegisters { get; set; }
        public DbSet<CashRegisterMove> CashRegisterMoves { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<ClientDebt> ClientDebts { get; set; }
        public DbSet<ClientPayment> ClientPayments { get; set; }
        public DbSet<InventoryProduct> InventoryProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductHeader> ProductHeaders { get; set; }
        public DbSet<PurchaseDetail> PurchaseDetails { get; set; }
        public DbSet<PurchaseHeader> PurchaseHeaders { get; set; }
        public DbSet<SaleDetail> SaleDetails { get; set; }
        public DbSet<SaleHeader> SaleHeaders { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<VendorDebt> VendorDebts { get; set; }
        public DbSet<VendorPayment> VendorPayments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>()
                .HasKey(t => new {t.UserId, t.RoleId});

            modelBuilder.Entity<UserRole>()
                .HasOne(pt => pt.User)
                .WithMany(p => p.Roles)
                .HasForeignKey(pt => pt.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(pt => pt.Role)
                .WithMany(t => t.Users)
                .HasForeignKey(pt => pt.RoleId);


            modelBuilder.Entity<VendorBrand>()
                .HasKey(t => new { t.VendorId, t.BrandId });

            modelBuilder.Entity<VendorBrand>()
                .HasOne(vb => vb.Vendor)
                .WithMany(v => v.Brands)
                .HasForeignKey(vb => vb.VendorId);

            modelBuilder.Entity<VendorBrand>()
                .HasOne(vb => vb.Brand)
                .WithMany(b => b.Vendors)
                .HasForeignKey(vb => vb.BrandId);

            modelBuilder.Entity<UserStore>()
                .HasKey(t => new { t.UserId, t.StoreId });

            modelBuilder.Entity<UserStore>()
                .HasOne(us => us.User)
                .WithMany(s => s.Stores)
                .HasForeignKey(us => us.UserId);

            modelBuilder.Entity<UserStore>()
                .HasOne(us => us.Store)
                .WithMany(s => s.Users)
                .HasForeignKey(us => us.StoreId);

        }


    }
}
