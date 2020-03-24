using Ecommerce.API.Products.Models;
using System;
using System.Threading.Tasks;

namespace Ecommerce.API.Products.Interfaces
{
    public interface IOrdersProvider
    {
        Task<(bool IsSuccess, OrderModel Orders, string ErrorMessage)> GetOrdersAsync(Guid customerId);
    }
}
