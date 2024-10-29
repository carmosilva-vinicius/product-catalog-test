
using Catalog.Application.UseCases.Interfaces;
using Catalog.Domain.Entities;
using Catalog.Infra.Repositories.Interfaces;

namespace Catalog.Application.UseCases;

public class RegisterNewProduct : IRegisterNewProduct
{
    private readonly IProductRepository _productRepository;

    public RegisterNewProduct(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Execute(Product product)
    {
        await _productRepository.Create(product);
    }
}
