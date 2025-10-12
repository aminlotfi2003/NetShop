using NetShop.SharedKernel.Domain.Abstractions;

namespace NetShop.Domain.Products.Events;

public sealed class ProductPriceChanged : IDomainEvent
{
    public Guid ProductId { get; }
    public decimal OldAmount { get; }
    public decimal NewAmount { get; }
    public string Currency { get; }
    public DateTime OccurredOnUtc { get; } = DateTime.UtcNow;

    public ProductPriceChanged(Guid productId, decimal oldAmount, decimal newAmount, string currency)
    {
        ProductId = productId;
        OldAmount = oldAmount;
        NewAmount = newAmount;
        Currency = currency;
    }
}
