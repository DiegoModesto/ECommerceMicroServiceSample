using Ecommerce.Api.Products.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.Api.Products.Interfaces
{
    public interface IProductsProvider
    {
        Task<(bool IsSuccess, IEnumerable<ProductModel> Products, string ErrorMessage)> GetProductAsync();
        Task<(bool IsSuccess, ProductModel Product, string ErrorMessage)> GetProductAsync(Guid id);
    }
}
