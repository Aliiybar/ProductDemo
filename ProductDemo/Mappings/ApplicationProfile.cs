using ProductDemo.Features.Products.Commands;
using ProductDemo.DAL.Entities;
using ProductDemo.DTO;
using AutoMapper;


namespace ProductDemo.Mappings
{
    public class ApplicationProfile :Profile
    {
        public ApplicationProfile()
        {
            CreateMap<CreateProductCommand, Product>();
            CreateMap<Product, ProductDto>();
        }
    }
}
