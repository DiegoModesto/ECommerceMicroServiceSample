using AutoMapper;
using Ecommerce.API.Products.Db;
using Ecommerce.API.Products.Profiles;
using Ecommerce.API.Products.Providers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ECommerce.Epi.Products.Tests
{
    public class ProductsServiceTest
    {
        [Fact]
        public async Task GetProductsRetrnsAllProducts()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsRetrnsAllProducts))
                .Options;
            var dbContext = new ProductsDbContext(options);
            
            this.CreateProducts(dbContext);

            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(productProfile));
            var mapper = new Mapper(configuration);

            var productsProvider = new ProductsProvider(dbContext, null, mapper);

            var product = await productsProvider.GetProductAsync();

            Assert.True(product.IsSuccess);
            Assert.True(product.Products.Any());
            Assert.Null(product.ErrorMessage);
        }

        [Fact]
        public async Task GetProductUsingInvalidId()
        {
            var productGuid = Guid.NewGuid();
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductUsingInvalidId))
                .Options;
            var dbContext = new ProductsDbContext(options);

            this.CreateProductWithId(dbContext, productGuid);

            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(productProfile));
            var mapper = new Mapper(configuration);

            var productsProvider = new ProductsProvider(dbContext, null, mapper);

            var product = await productsProvider.GetProductAsync(Guid.NewGuid());

            Assert.False(product.IsSuccess);
            Assert.Null(product.Product);
            Assert.NotNull(product.ErrorMessage);
        }

        [Fact]
        public async Task GetProductUsingValidId()
        {
            var productGuid = Guid.NewGuid();
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductUsingValidId))
                .Options;
            var dbContext = new ProductsDbContext(options);

            this.CreateProductWithId(dbContext, productGuid);

            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(productProfile));
            var mapper = new Mapper(configuration);

            var productsProvider = new ProductsProvider(dbContext, null, mapper);

            var product = await productsProvider.GetProductAsync(productGuid);

            Assert.True(product.IsSuccess);
            Assert.NotNull(product.Product);
            Assert.True(product.Product.Id.Equals(productGuid));
            Assert.Null(product.ErrorMessage);
        }

        #region [Private Methods]
        /// <summary>
        /// Create A list of products at DataBase
        /// </summary>
        private void CreateProducts(ProductsDbContext dbContext)
        {
            for (int i = 0; i < 10; i++)
            {
                dbContext.Products.Add(new ProductEntity()
                {
                    Id = Guid.NewGuid(),
                    Name = $"Produto {i.ToString().PadLeft(3, '0')}",
                    Price = (decimal)(i * Math.PI),
                    Inventory = 100
                });
            }

            dbContext.SaveChanges();
        }
        /// <summary>
        /// Create a single one product at DataBase
        /// </summary>
        /// <param name="ProductId">Product ID to be created</param>
        private void CreateProductWithId(ProductsDbContext dbContext, Guid ProductId)
        {
            dbContext.Products.Add(new ProductEntity()
            {
                Id = ProductId,
                Name = $"Produto 00",
                Price = (decimal)(1 * Math.PI),
                Inventory = 100
            });
            dbContext.SaveChanges();
        }
        #endregion
    }
}
