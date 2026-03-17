using MediatR;
using ProductDemo.DAL.Repositories;
using ProductDemo.DAL.Entities;
using AutoMapper;

namespace ProductDemo.Features.Products.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var newProduct = _mapper.Map<Product>(request);

            _productRepository.Add(newProduct);
            await _productRepository.SaveChangesAsync();

            return newProduct.Id;
        }
    }
}
