using Microsoft.AspNetCore.Mvc;

namespace FakeInventoryApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventoryController : ControllerBase
    {


        [HttpGet(Name = "Get")]
        public IActionResult Get(string id)
        {
            // 1 in 5 chance (20%) to return NotFound
            if (Random.Shared.Next(5) == 0)
            {
                return NotFound();
            }

            // Return random inventory data
            var response = new InventoryResponse
            {
                ProductId = id,
                Price = (decimal)Math.Round(Random.Shared.Next(10, 1000) + Random.Shared.NextDouble(), 2),
                Stock = Random.Shared.Next(0, 100)
            };

            return Ok(response);
        }
    }
}
