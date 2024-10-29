

using Catalog.Application.UseCases.Interfaces;
using Catalog.Domain.Entities;
using Catalog.Infra.Repositories.Interfaces;

namespace Catalog.Application.UseCases;
public class ListProductsPerPage : IListProductsPerPage
{
    private readonly IProductRepository _repository;

    public ListProductsPerPage(IProductRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<Product> Execute(int page, int pageSise)
    {
        return _repository.GetProducts(page, pageSise);
    }

    public int TotalPages(int pageSise) =>
        (int)Math.Ceiling(_repository.GetProductsCount() / (double)pageSise);
}
