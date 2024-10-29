
using Catalog.Domain.Entities;

namespace Catalog.Application.UseCases.Interfaces;

public interface IListProductsPerPage
{
    IEnumerable<Product> Execute(int page, int pageSise); 
    int TotalPages(int pageSise);
}
