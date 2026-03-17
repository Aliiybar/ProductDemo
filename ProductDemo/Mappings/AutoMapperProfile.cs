using ProductDemo.Features.Products.Commands;
using ProductDemo.DAL.Entities;
using ProductDemo.DTO;
using AutoMapper;


namespace ProductDemo.Mappings
{
    public class AutoMapperProfile :Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateProductCommand, Product>();
            CreateMap<Product, ProductDto>();
        }
    }
}
