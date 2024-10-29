
using Catalog.Domain.Entities;

namespace Catalog.Application.UseCases.Interfaces;

public interface IGetProductDetails
{
    Task<Product?> Execute(Guid id);
}
