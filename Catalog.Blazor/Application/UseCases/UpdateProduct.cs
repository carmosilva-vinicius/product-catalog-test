
using Catalog.Application.UseCases.Interfaces;
using Catalog.Domain.Entities;
using Catalog.Infra.Repositories.Interfaces;

namespace Catalog.Application.UseCases;

public class UpdateProduct : IUpdateProduct
{
    private readonly IProductRepository _repository;

    public UpdateProduct(IProductRepository productRepository)
    {
        _repository = productRepository;
    }

    public async Task Execute(Product product)
    {
        await _repository.Update(product);
    }
}
