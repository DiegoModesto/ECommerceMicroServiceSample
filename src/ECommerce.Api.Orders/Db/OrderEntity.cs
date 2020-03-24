using System;
using System.Collections.Generic;

namespace Ecommerce.API.Products.Db
{
    public class OrderEntity
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }
        public List<OrderItemEntity> Items { get; set; }
    }
}
