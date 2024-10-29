using Catalog.Application.UseCases;
using Catalog.Domain.Entities;
using Catalog.Infra.Repositories.Interfaces;

using Moq;

using Test.TestHelpers;

namespace Test.UseCasesTest;
public class UpdateProductTest
{
    private readonly Mock<IProductRepository> _repositoryMock;
    
    public UpdateProductTest()
    {
        _repositoryMock = new Mock<IProductRepository>();   

        _repositoryMock
            .Setup(x => x.Update(It.IsAny<Product>()))
            .Returns(Task.CompletedTask);
    }

    [Fact]
    public async Task UpdateProductTest_Executed()
    {
        var useCase = new UpdateProduct(_repositoryMock.Object);
        
        var product = TestHelper.GenerateProductsExamples(1)[0];

        await useCase.Execute(product); 
        _repositoryMock
            .Verify(x => x.Update(product), Times.Once);
    }
}