using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;
using TheStore.Web.Domain;

namespace TheStore.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Category>().HasOptional(x => x.ParentCategory).WithMany(x => x.Categories).HasForeignKey(x => x.ParentCategoryId).WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>().HasMany(p => p.Options).WithMany(o => o.Products).Map(o =>
            {
                o.MapLeftKey("ProductId");
                o.MapRightKey("OptionId");
                o.ToTable("ProductsAndOptions");
            });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Characteristic> Characteristics { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<DeliveryDetails> DeliveryDetails { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<ActionLog> ActionLogs { get; set; }
    }
}