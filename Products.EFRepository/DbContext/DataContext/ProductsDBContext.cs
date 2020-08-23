using Microsoft.EntityFrameworkCore;
using Products.Entity;

namespace Products.EFRepository
{
    public class ProductsDBContext : DbContext
    {
        public ProductsDBContext(DbContextOptions<ProductsDBContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductOption> ProductOptions { get; set; }
    }
}
