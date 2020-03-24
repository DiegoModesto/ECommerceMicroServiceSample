using AutoMapper;
using Ecommerce.Api.Products.Db;
using Ecommerce.Api.Products.Interfaces;
using Ecommerce.Api.Products.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        public readonly ProductsDbContext _dbContext;
        public readonly ILogger<ProductsProvider> _logger;
        public readonly IMapper _mapper;

        public ProductsProvider(ProductsDbContext dbContext, ILogger<ProductsProvider> logger, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._logger = logger;
            this._mapper = mapper;

            SeedData();
        }

        public async Task<(bool IsSuccess, IEnumerable<ProductModel> Products, string ErrorMessage)> GetProductAsync()
        {
            try
            {
                var products = await _dbContext.Products.ToListAsync();
                if(products != null && products.Any())
                {
                    var result = this._mapper.Map<IEnumerable<ProductEntity>, IEnumerable<ProductModel>>(products);
                    return (true, result, null);
                }

                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                this._logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, ProductModel Product, string ErrorMessage)> GetProductAsync(Guid id)
        {
            try
            {
                var teste = await _dbContext.Products.ToListAsync();
                var teste2 = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
                var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id.Equals(id));
                if (product != null)
                {
                    var result = this._mapper.Map<ProductEntity, ProductModel>(product);
                    return (true, result, null);
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
            if (!_dbContext.Products.Any())
            {
                _dbContext.Products.Add(new ProductEntity() { Id = Guid.Parse("f02db3f7-d55d-4ef1-bcef-7ea7a5ab7669"), Name = "Teclado", Price = 20, Inventory = 100 });
                _dbContext.Products.Add(new ProductEntity() { Id = Guid.Parse("59f34477-a14a-471a-ae36-e7f5f19ee508"), Name = "Mouse", Price = 15, Inventory = 200 });
                _dbContext.Products.Add(new ProductEntity() { Id = Guid.Parse("48c71845-a01c-40b6-80b5-633a781f6c68"), Name = "Monitor", Price = 35, Inventory = 100 });
                _dbContext.Products.Add(new ProductEntity() { Id = Guid.Parse("d26cab14-934e-498d-b238-df700f866d3e"), Name = "Cooler", Price = 5, Inventory = 100 });
                _dbContext.Products.Add(new ProductEntity() { Id = Guid.Parse("213c62a0-9b1d-491f-a771-320da745d766"), Name = "HD 500gb", Price = 30, Inventory = 200 });
                _dbContext.Products.Add(new ProductEntity() { Id = Guid.Parse("9c1b575b-9844-442e-aa5a-c9fd6f853533"), Name = "Gabinete", Price = 50, Inventory = 100 });

                _dbContext.SaveChanges();
            }
        }
    }
}
