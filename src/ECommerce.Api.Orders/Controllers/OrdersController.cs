using Ecommerce.API.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Ecommerce.API.Products.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersProvider _orderProvider;

        public OrdersController(IOrdersProvider orderProvider)
        {
            this._orderProvider = orderProvider;
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetOrdersAsync(Guid customerId)
        {
            var (IsSuccess, Order, ErrorMessage) = await this._orderProvider.GetOrdersAsync(customerId);
            return IsSuccess
                ? Ok(Order)
                : Ok(ErrorMessage);
        }
    }
}