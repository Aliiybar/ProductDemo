using AutoMapper;
using MediatR;
using ProductDemo.DAL.Entities;
using ProductDemo.DAL.Repositories;
using ProductDemo.DTO;
using ProductDemo.Helper;
using ProductDemo.Services;

namespace ProductDemo.Features.Products.Queries
{
    public class ProductQueryHandler(IProductRepository productRepository,
                                    IInventoryService inventoryService,
                                    IMapper mapper
                                     ) : IRequestHandler<ProductQuery, ApiResponse>
    {
 
        public async Task<ApiResponse> Handle(ProductQuery request, CancellationToken cancellationToken)
        {
            var product = await productRepository.Get(Guid.Parse(request.Id));
            if( product == null ) {
                return new ApiResponse
                {
                    Status = EnumHelper.GetDescription(ApiStatusCode.Fail),
                    Data = null
                };
            }
            var productDto = mapper.Map<Product, ProductDto>(product);

            try
            {
                var inventory = await inventoryService.GetProductAsync(request.Id, cancellationToken);
                if (inventory != null)
                {
                    productDto.Price = inventory.Price;
                    productDto.Stock = inventory.Stock;
                }
                return new ApiResponse
                {
                    Status = EnumHelper.GetDescription(inventory == null? ApiStatusCode.DataUnavailable: ApiStatusCode.Success),
                    Data = productDto
                };
            }
            catch (Exception)
            {
                return new ApiResponse
                {
                    Status = EnumHelper.GetDescription(ApiStatusCode.DataUnavailable),
                    Data = productDto
                };
            }

        }
    }
}
