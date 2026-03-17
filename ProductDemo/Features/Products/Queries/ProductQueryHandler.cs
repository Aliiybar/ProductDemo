using AutoMapper;
using MediatR;
using ProductDemo.DAL.Entities;
using ProductDemo.DAL.Repositories;
using ProductDemo.DTO;

namespace ProductDemo.Features.Products.Queries
{
    public class ProductQueryHandler : IRequestHandler<ProductQuery, ProductDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(ProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.Get(Guid.Parse(request.Id));
            var productDto = _mapper.Map<Product, ProductDto>(product);

            return productDto;
        }
    }
}
