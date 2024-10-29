
using Catalog.Application.UseCases.Interfaces;
using Catalog.Domain.Entities;
using Catalog.Infra.Repositories.Interfaces;

namespace Catalog.Application.UseCases;

public class DeleteProduct : IDeleteProduct
{
    private readonly IProductRepository _repository;

    public DeleteProduct(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task Execute(Product product)
    {
        await _repository.Delete(product);
    }
}
