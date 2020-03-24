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
    public class CustomerProvider : ICustomerProvider
    {
        public readonly CustomerDbContext _dbContext;
        public readonly ILogger<CustomerProvider> _logger;
        public readonly IMapper _mapper;

        public CustomerProvider(CustomerDbContext dbContext, ILogger<CustomerProvider> logger, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._logger = logger;
            this._mapper = mapper;

            SeedData();
        }

        public async Task<(bool IsSuccess, IEnumerable<CustomerModel> Customers, string ErrorMessage)> GetCustomerAsync()
        {
            try
            {
                var products = await _dbContext.Customers.ToListAsync();
                if (products != null && products.Any())
                {
                    var result = this._mapper.Map<IEnumerable<CustomerEntity>, IEnumerable<CustomerModel>>(products);
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

        public async Task<(bool IsSuccess, CustomerModel Customer, string ErrorMessage)> GetCustomerAsync(Guid id)
        {
            try
            {
                var teste = await _dbContext.Customers.ToListAsync();
                var teste2 = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);
                var product = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id.Equals(id));
                if (product != null)
                {
                    var result = this._mapper.Map<CustomerEntity, CustomerModel>(product);
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
            if (!_dbContext.Customers.Any())
            {
                _dbContext.Customers.Add(new CustomerEntity() { Id = Guid.Parse("70B76F1D-1CDE-401F-B07E-18417DF7A430"), Name = "Customer 01", Address = "Address 01" });
                _dbContext.Customers.Add(new CustomerEntity() { Id = Guid.Parse("F21A6FAA-EFE5-4D5E-9F15-1088E73F274F"), Name = "Customer 02", Address = "Address 02" });
                _dbContext.Customers.Add(new CustomerEntity() { Id = Guid.Parse("875447D9-7A4E-44AF-BE4C-8BCF883B94BE"), Name = "Customer 06", Address = "Address 06" });

                _dbContext.SaveChanges();
            }
        }
    }
}
