using Ecommerce.API.Products.Db;
using Ecommerce.API.Products.Models;

namespace Ecommerce.API.Products.Profiles
{
    public class CustomerProfile : AutoMapper.Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerModel, CustomerEntity>();
            CreateMap<CustomerEntity, CustomerModel>();
        }
    }
}
