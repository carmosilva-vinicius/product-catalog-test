
using Catalog.Domain.Entities;

namespace Catalog.Application.UseCases.Interfaces;

public interface IUpdateProduct
{
    Task Execute(Product product);
}
