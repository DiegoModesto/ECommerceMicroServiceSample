using ECommerce.Api.Search.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Service
{
    public class SearchService : ISearchService
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;

        public SearchService(IOrderService orderService, IProductService productService, ICustomerService customerService)
        {
            this._orderService = orderService;
            this._productService = productService;
            this._customerService = customerService;
        }
        public async Task<(bool IsSuccess, dynamic SearchResults, string ErrorMessage)> SearchAsync(Guid customerId)
        {
            var customerResult = await _customerService.GetCustomerAsync(customerId);
            var orderResult = await _orderService.GetOrderAsync(customerId);
            var productResult = await _productService.GetProductsAsync();

            foreach (var orderItem in orderResult.Orders.Items)
            {
                orderItem.ProductName = productResult.IsSuccess
                    ? productResult.Produtcs.FirstOrDefault(x => x.Id.Equals(orderItem.ProductId))?.Name
                    : "Product information is not available";
            }

            var result = new
            {
                Customer = customerResult.IsSuccess
                            ? customerResult.Customer
                            : new { Name = "Customer information is not available" },
                Order = orderResult.Orders
            };

            return (true, result, null);
        }

    }
}
