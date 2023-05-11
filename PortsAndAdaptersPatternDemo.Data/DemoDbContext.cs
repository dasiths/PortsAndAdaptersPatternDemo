using Microsoft.EntityFrameworkCore;

namespace PortsAndAdaptersPatternDemo.Data
{
    public class DemoDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "DemoDb");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasMany(e => e.Products)
                .WithMany(e => e.Orders);
        }

        public static void SeedData()
        {
            using var context = new DemoDbContext();
            context.Database.EnsureCreated();

            var products = Enumerable.Range(1, 10).Select(i => new Product()
            {
                Name = $"Product {i}",
                ProductId = i
            });

            context.Products.AddRangeAsync(products);
            context.SaveChanges();

            var rnd = new Random();

            var orders = Enumerable.Range(1, 10).Select(i => new Order()
            {
                OrderId = i,
                Customer = $"Customer {i}",
                Products = context.Products.OrderBy(_ => rnd.Next()).Take(5).ToList()
            });

            context.Orders.AddRangeAsync(orders);

            context.SaveChanges();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}