using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Products.Db
{
    public class OrderDbContext : DbContext
    {
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<OrderItemEntity> OrderItems { get; set; }

        public OrderDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
