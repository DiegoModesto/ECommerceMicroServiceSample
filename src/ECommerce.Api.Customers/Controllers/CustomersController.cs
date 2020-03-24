using Ecommerce.API.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Ecommerce.API.Products.Controllers
{
    [ApiController]
    [Route("api/customer")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerProvider _customerProvider;

        public CustomersController(ICustomerProvider customerProvider)
        {
            this._customerProvider = customerProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomersAsync()
        {
            var (IsSuccess, Customers, ErrorMessage) = await this._customerProvider.GetCustomerAsync();
            return IsSuccess
                ? Ok(Customers)
                : Ok(ErrorMessage);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerAsync(Guid id)
        {
            var (IsSuccess, Customer, ErrorMessage) = await this._customerProvider.GetCustomerAsync(id);
            return IsSuccess
                ? Ok(Customer)
                : Ok(ErrorMessage);
        }
    }
}