using NetShop.SharedKernel.Domain.Abstractions;

namespace NetShop.Domain.Products.Events;

public sealed class ProductCreated : IDomainEvent
{
    public Guid ProductId { get; }
    public string Sku { get; }
    public string Name { get; }
    public decimal PriceAmount { get; }
    public string Currency { get; }
    public int InitialStock { get; }
    public DateTime OccurredOnUtc { get; } = DateTime.UtcNow;

    public ProductCreated(Guid productId, string sku, string name, decimal priceAmount, string currency, int initialStock)
    {
        ProductId = productId;
        Sku = sku;
        Name = name;
        PriceAmount = priceAmount;
        Currency = currency;
        InitialStock = initialStock;
    }
}
