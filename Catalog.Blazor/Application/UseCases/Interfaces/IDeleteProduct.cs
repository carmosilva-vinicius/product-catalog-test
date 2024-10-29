
using Catalog.Domain.Entities;

namespace Catalog.Application.UseCases.Interfaces;

public interface IDeleteProduct
{
    Task Execute(Product product);
}