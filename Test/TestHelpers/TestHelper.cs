

using Catalog.Domain.Entities;
using Catalog.Domain.Enums;

namespace Test.TestHelpers;

public static class TestHelper
{
    public static List<Product> GenerateProductsExamples(int count)
    {
        var products = new List<Product>();
        for (int i = 0; i < 7; i++)
        {
            products.Add(
                new Product { 
                    Id = Guid.NewGuid(),
                    Name = $"Name {i}",
                    Description = $"Description {i}",
                    SalePrice = i * 5.00M,
                    Quantity = i,
                    Type = i % 2 == 1? ProductType.NonOrganic : ProductType.Organic,
                    RegistrationDate = DateTime.Now,
                }
            );
        }

        return products;
    }
}