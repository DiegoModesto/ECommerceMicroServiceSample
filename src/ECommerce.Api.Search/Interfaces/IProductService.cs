using ECommerce.Api.Search.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Interfaces
{
    public interface IProductService
    {
        Task<(bool IsSuccess, ProductModel Produtcs, string ErrorMessage)> GetProductAsync(Guid Id);
        Task<(bool IsSuccess, IEnumerable<ProductModel> Produtcs, string ErrorMessage)> GetProductsAsync();
        Task<(bool IsSuccess, IEnumerable<ProductModel> Products, string ErrorMessage)> GetProductsAsync(List<Guid> ProductsId);
    }
}
