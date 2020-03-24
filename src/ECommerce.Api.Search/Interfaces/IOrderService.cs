using ECommerce.Api.Search.Models;
using System;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Interfaces
{
    public interface IOrderService
    {
        Task<(bool IsSuccess, OrderModel Orders, string ErrorMessage)> GetOrderAsync(Guid customerId);
    }
}
