using Ecommerce.Api.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Ecommerce.Api.Products.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsProvider _productsProvider;

        public ProductsController(IProductsProvider productsProvider)
        {
            this._productsProvider = productsProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var (IsSuccess, Products, ErrorMessage) = await this._productsProvider.GetProductAsync();
            return IsSuccess
                ? Ok(Products)
                : Ok(ErrorMessage);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductAsync(Guid id)
        {
            var (IsSuccess, Product, ErrorMessage) = await this._productsProvider.GetProductAsync(id);
            return IsSuccess
                ? Ok(Product)
                : Ok(ErrorMessage);
        }
    }
}