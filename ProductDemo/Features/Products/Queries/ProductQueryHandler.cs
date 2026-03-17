using AutoMapper;
using MediatR;
using ProductDemo.DAL.Entities;
using ProductDemo.DAL.Repositories;
using ProductDemo.DTO;
using ProductDemo.Services;

namespace ProductDemo.Features.Products.Queries
{
    public class ProductQueryHandler(IProductRepository productRepository,
                                    IInventoryService inventoryService,
                                    IMapper mapper
                                     ) : IRequestHandler<ProductQuery, ProductDto>
    {
 
        public async Task<ProductDto> Handle(ProductQuery request, CancellationToken cancellationToken)
        {
            var product = await productRepository.Get(Guid.Parse(request.Id));
            var productDto = mapper.Map<Product, ProductDto>(product);

            var inventory = await inventoryService.GetProductAsync(request.Id, cancellationToken);
            if (inventory == null)
            {
                return productDto;
            }
            else
            {
                productDto.Price = inventory.Price;
                productDto.Stock = inventory.Stock;
            }
            return productDto; 
        }
    }
}
