
using Catalog.Application.UseCases;
using Catalog.Domain.Entities;
using Catalog.Infra.Repositories.Interfaces;
using Moq;
using Test.TestHelpers;

namespace Test.UseCasesTest;

public class GetProductDetailsTest
{
    private readonly Mock<IProductRepository> _repositoryMock;
    private readonly Product _producExample;

    public GetProductDetailsTest()
    {
        var productsExample = TestHelper.GenerateProductsExamples(1);
        _producExample = productsExample[0];
      
        _repositoryMock = new Mock<IProductRepository>();
      
        _repositoryMock
            .Setup(x => x.GetProductById(productsExample[0].Id))
            .ReturnsAsync(productsExample[0]);

        _repositoryMock
            .Setup(x => x.GetProductById(Guid.NewGuid()))
            .ReturnsAsync((Product)null);
    }

    [Fact]
    public async Task GetProductDetails_UseCaseTest_Null()
    {
        var getProductDetails = new GetProductDetails(_repositoryMock.Object);
        var product = await getProductDetails.Execute(Guid.NewGuid());
        Assert.Null(product);
    }

    [Fact]
    public async Task GetProductDetailsTest_UseCaseTest_NotNull()
    {
        var getProductDetails = new GetProductDetails(_repositoryMock.Object);
        var product = await getProductDetails.Execute(_producExample.Id);
        Assert.NotNull(product);
        Assert.Equal(_producExample.Id, product.Id);
        Assert.Equal(_producExample.Name, product.Name);
        Assert.Equal(_producExample.Description, product.Description);
        Assert.Equal(_producExample.SalePrice, product.SalePrice);
        Assert.Equal(_producExample.Quantity, product.Quantity);
        Assert.Equal(_producExample.Type, product.Type);
        Assert.Equal(_producExample.RegistrationDate, product.RegistrationDate);
    }
}
