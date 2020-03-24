using System;
using System.Collections.Generic;

namespace Ecommerce.API.Products.Models
{
    public class OrderModel
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }
        public List<OrderItemModel> Items { get; set; }
    }
}
