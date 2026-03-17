using ProductDemo.DAL.Entities;

namespace ProductDemo.DAL.Repositories
{
    public class ProductRepository(ProductDbContext dalContext) : BaseRepository<Product>(dalContext), IProductRepository
    {
    }
}
