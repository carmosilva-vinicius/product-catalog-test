
using Catalog.Application.UseCases;
using Catalog.Domain.Entities;
using Catalog.Infra.Repositories.Interfaces;

using Moq;

using Test.TestHelpers;

namespace Test.UseCasesTest;

public class DeleteProductTest
{
    private readonly Mock<IProductRepository> _repositoryMock;

    public DeleteProductTest()
    {
        _repositoryMock = new Mock<IProductRepository>();
        _repositoryMock
            .Setup(x => x.Delete(It.IsAny<Product>()))
            .Returns(Task.CompletedTask);
    }

    [Fact]
    public async Task DeleteProductTest_Executed()
    {
        var useCase = new DeleteProduct(_repositoryMock.Object);
        var product = TestHelper.GenerateProductsExamples(1)[0];
        await useCase.Execute(product);
        _repositoryMock
            .Verify(x => x.Delete(product), Times.Once);
    }
}
