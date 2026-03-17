namespace FakeInventoryApi
{
    public class InventoryResponse
    {
        public string ProductId { get; set; } = default!;
        public decimal Price { get; set; }
        public int Stock { get; set; }

    }
}
