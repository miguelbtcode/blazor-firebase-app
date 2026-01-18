using NetFirebase.Api.Models.Domain;

namespace NetFirebase.Api.Services.Products;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProductsAsync(CancellationToken cancellationToken = default);
    Task<Product?> GetProductByIdAsync(int productId);
    Task<List<Product>> GetProductByNameAsync(string productName);
    Task CreateProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    Task DeleteProductAsync(int productId);
    Task<bool> SaveChangesAsync();
}
