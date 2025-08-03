using Microsoft.EntityFrameworkCore;

namespace AdventureWorksLINQ.Console.EFCore
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }

        public DbSet<Tag> Tags { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<TagProduct> TagProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TagProduct>()
            .HasKey(tp => new { tp.ProductId, tp.TagId });

            modelBuilder.Entity<TagProduct>()
            .HasOne(tp => tp.Product)
            .WithMany(p => p.TagProducts)
            .HasForeignKey(tp => tp.ProductId);

            modelBuilder.Entity<TagProduct>()
            .HasOne(tp => tp.Tag)
            .WithMany(t => t.TagProducts)
            .HasForeignKey(tp => tp.TagId);
        }
    }

    public class ProductTag
    {
        public static void Run()
        {
            var optionsBuilder = new DbContextOptionsBuilder<StoreContext>();
            optionsBuilder.UseInMemoryDatabase("StoreDB");

            using (var context = new StoreContext(optionsBuilder.Options))
            {
                var product1 = new Product { Name = "Laptop", Price = 799.99m };
                var product2 = new Product { Name = "Smartphone", Price = 699.99m };
                var product3 = new Product { Name = "Tablet", Price = 599.99m };
                var product4 = new Product { Name = "TV", Price = 499.99m };

                var tag1 = new Tag { Name = "Electronics" };
                var tag2 = new Tag { Name = "Portable" };
                var tag3 = new Tag { Name = "Gadget" };

                context.Products.AddRange(product1, product2, product3, product4);
                context.Tags.AddRange(tag1, tag2, tag3);

                context.TagProducts.AddRange(
                    new TagProduct { Product = product1, Tag = tag1, AddedDate = DateTime.Now },
                    new TagProduct { Product = product2, Tag = tag2, AddedDate = DateTime.Now.AddDays(-1) },
                    new TagProduct { Product = product3, Tag = tag3, AddedDate = DateTime.Now.AddDays(-2) },
                    new TagProduct { Product = product4, Tag = tag1, AddedDate = DateTime.Now },
                    new TagProduct { Product = product2, Tag = tag2, AddedDate = DateTime.Now.AddDays(-1) },
                    new TagProduct { Product = product3, Tag = tag1, AddedDate = DateTime.Now }
                );

                context.SaveChanges();

                var productsWithMultipleTags = context.Products
                .Include(p => p.TagProducts)
                .ThenInclude(tp => tp.Tag)
                .Where(p => p.TagProducts.Count > 1)
                .Select(p => new
                {
                    p.Name,
                    p.Price,
                    Tags = p.TagProducts.Select(tp => new
                    {
                        tp.Tag.Name,
                        tp.AddedDate
                    }).ToList()
                }).ToList();


                System.Console.WriteLine("Products with more than one tag:");
                System.Console.WriteLine("---------------------------------");
                foreach (var product in productsWithMultipleTags)
                {
                    System.Console.WriteLine($"Product: {product.Name}, Price: ${product.Price}");
                    System.Console.WriteLine("Tags:");
                    foreach (var tag in product.Tags)
                    {
                        System.Console.WriteLine($"  - {tag.Name} (Added: {tag.AddedDate:yyyy-MM-dd})");
                    }
                    System.Console.WriteLine("---------------------------------");
                }
            }
        }
    }
}