using Bogus;
using NetFirebase.Api.Data;
using NetFirebase.Api.Models.Domain;

namespace NetFirebase.Api.Extensions;

public static class TestDataExtensions
{
    public static async void UseTestData(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var service = scope.ServiceProvider;
        var dbContext = service.GetRequiredService<DatabaseContext>();

        if (!dbContext.Products.Any())
        {
            var productCollection = new List<Product>();
            var faker = new Faker();
            for (var i = 1; i <= 100; i++)
            {
                productCollection.Add(
                    new Product
                    {
                        Name = faker.Commerce.ProductName(),
                        Description = faker.Commerce.ProductDescription(),
                        Price = faker.Random.Decimal(100, 500),
                    }
                );
            }

            await dbContext.Products.AddRangeAsync(productCollection);
            await dbContext.SaveChangesAsync();
        }
    }
}
