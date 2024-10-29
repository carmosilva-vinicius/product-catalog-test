
using Catalog.Application.UseCases.Interfaces;
using Catalog.Domain.Entities;
using Catalog.Infra.Repositories.Interfaces;

namespace Catalog.Application.UseCases;

public class GetProductDetails : IGetProductDetails
{
    private readonly IProductRepository _productRepository;

    public GetProductDetails(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product?> Execute(Guid id)
    {
        return await _productRepository.GetProductById(id);
    }
}
