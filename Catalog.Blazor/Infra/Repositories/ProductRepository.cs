
using Catalog.Infra;
using Catalog.Domain.Entities;
using Catalog.Infra.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Catalog.Infra.Repositories;
public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Create(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Product product)
    {
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }

    public async Task<Product?> GetProductById(Guid id)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
    }

    public IEnumerable<Product> GetProducts(int page, int pageSize)
    {
        return _context.Products
            .Skip((page - 1) * pageSize)
            .Take(pageSize);
    }

    public int GetProductsCount()
    {
        return _context.Products.Count();
    }

    public async Task Update(Product product)
    {
       _context.Attach(product).State = EntityState.Modified;
       await _context.SaveChangesAsync(); 
    }
}
