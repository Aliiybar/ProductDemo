using ProductDemo.DTO;

namespace ProductDemo.Services
{
    public class InventoryService(HttpClient httpClient, IConfiguration configuration) : IInventoryService
    {
        public async Task<InventoryResponse?> GetProductAsync(string productId, CancellationToken cancellationToken = default)
        {
           
            var response = await httpClient.GetAsync($"/inventory?id={productId}", cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var content = await response.Content.ReadFromJsonAsync<InventoryResponse>(cancellationToken: cancellationToken);
            return content;
        }
    }
}
