using Ecommerce.API.Products.Db;
using Ecommerce.API.Products.Models;

namespace Ecommerce.API.Products.Profiles
{
    public class OrdersProfile : AutoMapper.Profile
    {
        public OrdersProfile()
        {
            CreateMap<OrderEntity, OrderModel>();
            CreateMap<OrderItemEntity, OrderItemModel>();
        }
    }
}
