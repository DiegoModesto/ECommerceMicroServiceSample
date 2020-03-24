using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Service
{
    public class OrderService : IOrderService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<OrderService> _logger;

        public OrderService(IHttpClientFactory httpFactory, ILogger<OrderService> logger)
        {
            this._httpClientFactory = httpFactory;
            this._logger = logger;
        }
        public async Task<(bool IsSuccess, OrderModel Orders, string ErrorMessage)> GetOrderAsync(Guid customerId)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("OrderService");
                var response = await client.GetAsync($"api/order/{customerId}");
                if(response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

                    var result = JsonSerializer.Deserialize<OrderModel>(content, options);

                    return (true, result, null);
                }

                return (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
