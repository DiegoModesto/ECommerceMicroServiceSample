﻿using System;

namespace Ecommerce.API.Products.Db
{
    public class OrderItemEntity
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity{ get; set; }
        public decimal UnitPrice { get; set; }
    }
}
