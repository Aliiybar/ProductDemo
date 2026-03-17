using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductDemo.Helper;
using ProductDemo.DTO;
using ProductDemo.Features.Products.Commands;
using ProductDemo.Features.Products.Queries;

namespace ProductDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ISender _sender;

        public ProductsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductCommand command)
        {
             var productId = await _sender.Send(command);
             return Ok(productId);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(string id)
        {
            var query = new ProductQuery { Id = id };
            var apiResponse = await _sender.Send(query);

            if (apiResponse.Status == EnumHelper.GetDescription(ApiStatusCode.Fail))
            {
                return NotFound();
            }
            return Ok(apiResponse);
        }
    }
}
