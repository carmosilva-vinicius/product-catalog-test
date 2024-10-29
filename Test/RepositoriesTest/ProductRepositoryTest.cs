

using Catalog.Domain.Entities;
using Catalog.Domain.Enums;
using Catalog.Infra;
using Catalog.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Update;
using Xunit.Abstractions;
using Test.TestHelpers;

namespace Test.RepositoriesTest;

public class ProductRepositoryTest
{
    private ITestOutputHelper _outputHelper;
    private readonly ApplicationDbContext _context;
    private readonly DbContextOptions<ApplicationDbContext> _contextOptions;
    private readonly List<Product> _productsExample;
    public ProductRepositoryTest(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;

        _contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .ConfigureWarnings(b => b.Ignore
                (InMemoryEventId.TransactionIgnoredWarning))
                .Options;

        _context = new ApplicationDbContext(_contextOptions);

        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();


        _productsExample = TestHelper.GenerateProductsExamples(7);
        _context.AddRange( _productsExample );
        _context.SaveChanges();
    }

    [Fact]
    public void GetProductsRepositoryTest()
    {
        var repository = new ProductRepository(_context);
        var page1 = repository.GetProducts(page: 1, pageSize: 5);
        Assert.Equal(5, page1.Count());

        var page2 = repository.GetProducts(page: 2, pageSize: 5);
        Assert.Equal(2, page2.Count());
    }
    
    [Fact]
    public void GetProductsRepositoryTest_OutOfRange()
    {
        var repository = new ProductRepository(_context);
        var page = repository.GetProducts(page: 3, pageSize: 5);
        Assert.Empty(page);
    }
    
    [Fact]
    public async Task GetNonExistentProductByIdRepositoryTest()
    {
        var repository = new ProductRepository(_context);
        var product = await repository.GetProductById(Guid.NewGuid());
        Assert.Null(product);
    }

    [Fact]
    public async Task GetProductByIdRepositoryTest()
    {
        var repository = new ProductRepository(_context);
        var product = await repository.GetProductById(_productsExample[0].Id);
        
        Assert.NotNull(product);
        Assert.Equal(_productsExample[0].Id, product.Id);
        Assert.Equal(_productsExample[0].Name, product.Name);
        Assert.Equal(_productsExample[0].Description, product.Description);
        Assert.Equal(_productsExample[0].SalePrice, product.SalePrice);
        Assert.Equal(_productsExample[0].Quantity, product.Quantity);
        Assert.Equal(_productsExample[0].Type, product.Type);
        Assert.Equal(_productsExample[0].RegistrationDate, product.RegistrationDate);
    }

    [Fact]
    public async Task CreateProductRepositoryTest()
    {
        var repository = new ProductRepository(_context);
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            Description = "Test",
            SalePrice = 1.00M,
            Quantity = 1,
            Type = ProductType.Organic,
            RegistrationDate = DateTime.Now
        };
        await repository.Create(product);
        var createdProduct = await repository.GetProductById(product.Id);

        Assert.NotNull(createdProduct);
        Assert.Equal(product.Id, createdProduct.Id);
        Assert.Equal(product.Name, createdProduct.Name);
        Assert.Equal(product.Description, createdProduct.Description);
        Assert.Equal(product.SalePrice, createdProduct.SalePrice);
        Assert.Equal(product.Quantity, createdProduct.Quantity);
        Assert.Equal(product.Type, createdProduct.Type);
        Assert.Equal(product.RegistrationDate, createdProduct.RegistrationDate);
        Assert.Equal(8, repository.GetProductsCount());
    }

    [Fact]
    public async Task UpdateProductRepositoryTest()
    {
        var repository = new ProductRepository(_context);
        var product = new Product
        {
            Id = _productsExample[0].Id,
            Name = "UpdatedName",
            Description = "UpdatedDescription",
            SalePrice = 1.00M,
            Quantity = 1,
            Type = ProductType.Organic,
            RegistrationDate = DateTime.Now
        };
        var existingProduct = await repository.GetProductById(product.Id);

        _context.Entry(existingProduct).State = EntityState.Detached;

        existingProduct.Name = product.Name;
        existingProduct.Description = product.Description;
        existingProduct.SalePrice = product.SalePrice;
        existingProduct.Quantity = product.Quantity;
        existingProduct.Type = product.Type;
        existingProduct.RegistrationDate = product.RegistrationDate;

        await repository.Update(existingProduct);
        var updatedProduct = await repository.GetProductById(product.Id);

        Assert.NotNull(updatedProduct);
        Assert.Equal(product.Id, updatedProduct.Id);
        Assert.Equal(product.Name, updatedProduct.Name);
        Assert.Equal(product.Description, updatedProduct.Description);
        Assert.Equal(product.SalePrice, updatedProduct.SalePrice);
        Assert.Equal(product.Quantity, updatedProduct.Quantity);
        Assert.Equal(product.Type, updatedProduct.Type);
        Assert.Equal(product.RegistrationDate, updatedProduct.RegistrationDate);
    }

    [Fact]
    public async Task DeleteProductRepositoryTest()
    {
        var repository = new ProductRepository(_context);
        await repository.Delete(_productsExample[0]);

        var deletedProduct = await repository.GetProductById(_productsExample[0].Id);

        Assert.Equal(6, repository.GetProductsCount());
        Assert.Null(deletedProduct);
    }
}
