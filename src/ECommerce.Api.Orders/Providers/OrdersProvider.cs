using AutoMapper;
using Ecommerce.API.Products.Db;
using Ecommerce.API.Products.Interfaces;
using Ecommerce.API.Products.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.API.Products.Providers
{
    public class OrdersProvider : IOrdersProvider
    {
        public readonly OrderDbContext _dbContext;
        public readonly ILogger<OrdersProvider> _logger;
        public readonly IMapper _mapper;

        public OrdersProvider(OrderDbContext dbContext, ILogger<OrdersProvider> logger, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._logger = logger;
            this._mapper = mapper;

            SeedData();
        }

        public async Task<(bool IsSuccess, OrderModel Orders, string ErrorMessage)> GetOrdersAsync(Guid customerId)
        {
            try
            {
                var order = await _dbContext.Orders.FirstOrDefaultAsync(x => x.CustomerId.Equals(customerId));
                if (order != null)
                {
                    var orderModel = this._mapper.Map<OrderEntity, OrderModel>(order);
                    
                    var items = await _dbContext.OrderItems.Where(x => x.OrderId.Equals(order.Id)).ToListAsync();

                    orderModel.Items = this._mapper.Map<List<OrderItemEntity>, List<OrderItemModel>>(items);
                    orderModel.Total = orderModel.Items.Sum(x => x.Quantity * x.UnitPrice);

                    return (true, orderModel, null);
                }

                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                this._logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        private void SeedData()
        {
            if (!_dbContext.Orders.Any())
            {
                #region [Order]
                var orderId01 = Guid.Parse("B2194D5F-2F10-4FA0-9F3D-052FBA43A5BC");
                _dbContext.Orders.Add(new OrderEntity()
                {
                    Id = orderId01,
                    CustomerId = Guid.Parse("70B76F1D-1CDE-401F-B07E-18417DF7A430"),
                    OrderDate = DateTime.UtcNow.AddDays(-23)
                });
                #endregion
                #region [Order Items]
                var productId01 = Guid.Parse("f02db3f7-d55d-4ef1-bcef-7ea7a5ab7669");
                _dbContext.OrderItems.Add(new OrderItemEntity()
                {
                    Id = Guid.NewGuid(),
                    OrderId = orderId01,
                    ProductId = productId01,
                    UnitPrice = 20,
                    Quantity = 7
                });

                var productId02 = Guid.Parse("48c71845-a01c-40b6-80b5-633a781f6c68");
                _dbContext.OrderItems.Add(new OrderItemEntity()
                {
                    Id = Guid.NewGuid(),
                    OrderId = orderId01,
                    ProductId = productId02,
                    UnitPrice = 35,
                    Quantity = 3
                });

                var productId03 = Guid.Parse("213c62a0-9b1d-491f-a771-320da745d766");
                _dbContext.OrderItems.Add(new OrderItemEntity()
                {
                    Id = Guid.NewGuid(),
                    OrderId = orderId01,
                    ProductId = productId03,
                    UnitPrice = 30,
                    Quantity = 9
                });

                var productId04 = Guid.Parse("9c1b575b-9844-442e-aa5a-c9fd6f853533");
                _dbContext.OrderItems.Add(new OrderItemEntity()
                {
                    Id = Guid.NewGuid(),
                    OrderId = orderId01,
                    ProductId = productId04,
                    UnitPrice = 50,
                    Quantity = 2
                });
                #endregion

                _dbContext.SaveChanges();
            }
        }
    }
}
