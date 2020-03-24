using System;

namespace Ecommerce.API.Products.Db
{
    public class ProductEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Inventory { get; set; }
    }
}
