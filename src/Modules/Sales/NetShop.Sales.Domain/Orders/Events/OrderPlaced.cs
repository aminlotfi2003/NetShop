using NetShop.SharedKernel.Domain.Abstractions;

namespace NetShop.Sales.Domain.Orders.Events;

public sealed class OrderPlaced : IDomainEvent
{
    public Guid OrderId { get; }
    public Guid CustomerId { get; }
    public string Currency { get; }
    public DateTime OccurredOnUtc { get; } = DateTime.UtcNow;

    public OrderPlaced(Guid orderId, Guid customerId, string currency)
    {
        OrderId = orderId;
        CustomerId = customerId;
        Currency = currency;
    }
}
