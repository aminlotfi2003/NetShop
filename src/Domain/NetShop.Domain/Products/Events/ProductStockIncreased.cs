using NetShop.SharedKernel.Domain.Abstractions;

namespace NetShop.Domain.Products.Events;

public sealed class ProductStockIncreased : IDomainEvent
{
    public Guid ProductId { get; }
    public int By { get; }
    public int NewStock { get; }
    public DateTime OccurredOnUtc { get; } = DateTime.UtcNow;

    public ProductStockIncreased(Guid productId, int by, int newStock)
    {
        ProductId = productId;
        By = by;
        NewStock = newStock;
    }
}
