using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Products.Db
{
    public class CustomerDbContext : DbContext
    {
        public DbSet<CustomerEntity> Customers { get; set; }

        public CustomerDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
