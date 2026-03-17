using MediatR;
using ProductDemo.DTO;

namespace ProductDemo.Features.Products.Queries
{
    public class ProductQuery : IRequest<ApiResponse>
    {
        public required string Id { get; set; }
    }
}
