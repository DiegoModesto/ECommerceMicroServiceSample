using Ecommerce.API.Products.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.API.Products.Interfaces
{
    public interface ICustomerProvider
    {
        Task<(bool IsSuccess, IEnumerable<CustomerModel> Customers, string ErrorMessage)> GetCustomerAsync();
        Task<(bool IsSuccess, CustomerModel Customer, string ErrorMessage)> GetCustomerAsync(Guid id);
    }
}
