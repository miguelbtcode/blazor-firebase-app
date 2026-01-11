using Microsoft.EntityFrameworkCore;
using NetFirebase.Api.Data;
using NetFirebase.Api.Models.Domain;

namespace NetFirebase.Api.Services.Products;

public class ProductService : IProductService
{
    private readonly DatabaseContext _dbContext;

    public ProductService(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateProductAsync(Product product)
    {
        try
        {
            await _dbContext.Database.ExecuteSqlAsync(
                @$"CALL sp_insert_product({product.Price}, {product.Name}, {product.Description});"
            );
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while creating the product.", ex);
        }
    }

    public async Task DeleteProductAsync(int productId)
    {
        try
        {
            await _dbContext.Database.ExecuteSqlAsync(@$"CALL sp_delete_product({productId})");
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while deleting the product.", ex);
        }
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _dbContext
            .Database.SqlQuery<Product>(@$"SELECT * FROM fx_query_product_all();")
            .ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int productId)
    {
        var result = await _dbContext
            .Database.SqlQuery<Product>(@$"SELECT * FROM fx_query_product_by_id({productId});")
            .ToListAsync();

        var product = result.FirstOrDefault();
        return product;
    }

    public async Task<List<Product>> GetProductByNameAsync(string productName)
    {
        return await _dbContext
            .Database.SqlQuery<Product>(@$"SELECT * FROM fx_query_product_by_name({productName});")
            .ToListAsync();
    }

    public Task<bool> SaveChangesAsync()
    {
        throw new NotImplementedException();
    }

    public async Task UpdateProductAsync(Product product)
    {
        try
        {
            await _dbContext.Database.ExecuteSqlAsync(
                @$"CALL sp_update_product({product.Id}, {product.Price}, {product.Name}, {product.Description});"
            );
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while updating the product.", ex);
        }
    }
}
