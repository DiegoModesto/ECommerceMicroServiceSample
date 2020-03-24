using Ecommerce.Api.Products.Db;
using Ecommerce.Api.Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Products.Profiles
{
    public class ProductProfile : AutoMapper.Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductModel, ProductEntity>();
            CreateMap<ProductEntity, ProductModel>();
        }
    }
}
