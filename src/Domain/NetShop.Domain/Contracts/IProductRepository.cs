using NetShop.Domain.Products;

namespace NetShop.Domain.Contracts;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Product?> GetBySkuAsync(string sku, CancellationToken ct = default);
    Task AddAsync(Product product, CancellationToken ct = default);
    Task<List<Product>> ListAsync(CancellationToken ct = default);
}
