using AutoMapper;
using WebApi.Entities;
using WebApi.Models;

namespace WebApi.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>();
        }
    }
}