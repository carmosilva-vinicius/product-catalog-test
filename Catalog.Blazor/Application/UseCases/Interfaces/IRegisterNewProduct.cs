
using Catalog.Domain.Entities;

namespace Catalog.Application.UseCases.Interfaces;

public interface IRegisterNewProduct
{
    Task Execute(Product product);
}