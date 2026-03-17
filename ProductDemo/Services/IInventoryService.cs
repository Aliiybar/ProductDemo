using ProductDemo.DTO;

namespace ProductDemo.Services
{
    public interface IInventoryService
    {
        Task<InventoryResponse?> GetProductAsync(string productId, CancellationToken cancellationToken = default);
    }
}
