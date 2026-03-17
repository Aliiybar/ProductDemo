using MediatR;

namespace ProductDemo.Features.Products.Commands
{
    public class CreateProductCommand :IRequest<Guid>
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}
