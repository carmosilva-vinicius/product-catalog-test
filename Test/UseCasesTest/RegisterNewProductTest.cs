
using System.ComponentModel.DataAnnotations;

using Catalog.Application.UseCases;
using Catalog.Domain.Entities;
using Catalog.Domain.Enums;
using Catalog.Infra.Repositories.Interfaces;
using Moq;

using Test.TestHelpers;

namespace Test.UseCasesTest;

public class RegisterNewProductTest
{
    private readonly Mock<IProductRepository> _repositoryMock;

    public RegisterNewProductTest()
    {
        _repositoryMock = new Mock<IProductRepository>();

        _repositoryMock
            .Setup(x => x.Create(It.IsAny<Product>()))
            .Returns(Task.CompletedTask);
    }

    [Fact]
    public async Task RegisterNewProductTest_Executed()
    {
        var useCase = new RegisterNewProduct(_repositoryMock.Object);
        var product = TestHelper.GenerateProductsExamples(1)[0];
        
        await useCase.Execute(product);

        _repositoryMock
            .Verify(x => x.Create(product), Times.Once);
    }

    [Fact]
    public void NewProductModelValidationTest()
    {
        var product = new Product{};

        var context = new ValidationContext(product, null, null);
        var results = new List<ValidationResult>();
        var isValid = Validator
            .TryValidateObject(product, context, results, true);
        
        Assert.False(isValid);
        Assert.Equal(2, results.Count);
        Assert.Equal("O nome do produto deve ser informado", results[0].ErrorMessage);
        Assert.Equal("A descrição do produto deve ser informada", results[1].ErrorMessage);
    }
}
