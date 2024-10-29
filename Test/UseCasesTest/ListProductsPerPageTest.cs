
using Catalog.Application.UseCases;
using Catalog.Domain.Entities;
using Catalog.Infra.Repositories.Interfaces;
using Moq;
using Test.TestHelpers;

namespace Test.UseCasesTest;

public class ListProductsPerPageTest
{
  private readonly Mock<IProductRepository> _repositoryMock;

  public ListProductsPerPageTest()
  {
    _repositoryMock = new Mock<IProductRepository>();

    var productsExamples = TestHelper.GenerateProductsExamples(7);

    _repositoryMock
      .Setup(x => x.GetProducts(1, 5))
      .Returns(productsExamples.Take(5).ToList());
    
    _repositoryMock
      .Setup(x => x.GetProducts(2, 5))
      .Returns(productsExamples.Skip(5).Take(5).ToList());

    _repositoryMock
      .Setup(x => x.GetProducts(3, 5))
      .Returns(new List<Product>());

    _repositoryMock
      .Setup(x => x.GetProductsCount())
      .Returns(7);
  }

  [Fact]
  public void ListProductsPerPage_UseCaseTest()
  {
        var listProductsPerPage = new ListProductsPerPage(_repositoryMock.Object);

        var page1 = listProductsPerPage.Execute(1, 5);
        var page2 = listProductsPerPage.Execute(2, 5);

        Assert.Equal(5, page1.Count());
        Assert.Equal(2, page2.Count());  
  }

  [Fact]
  public void ListProductsPerPage_OutOfRange()
  {
    var listProductsPerPage = new ListProductsPerPage(_repositoryMock.Object);

    var page = listProductsPerPage.Execute(3, 5);

    Assert.Empty(page);
  }

  [Fact]
  public void GetTotalPagesTest()
  {
    var listProductsPerPage = new ListProductsPerPage(_repositoryMock.Object);

    var totalPages = listProductsPerPage.TotalPages(5);

    Assert.Equal(2, totalPages);
  }
}
