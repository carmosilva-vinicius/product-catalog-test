using Catalog.Domain.Entities;

namespace Catalog.Infra.Repositories.Interfaces;
public interface IProductRepository
{
    IEnumerable<Product> GetProducts(int page, int pageSize);
    Task<Product?> GetProductById(Guid id);
    int GetProductsCount();
    Task Create(Product product);
    Task Update(Product product);
    Task Delete(Product product);
}