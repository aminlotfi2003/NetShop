using NetShop.SharedKernel.Domain.Abstractions;
using NetShop.SharedKernel.Domain.ValueObjects;
using NetShop.Domain.Orders.Events;

namespace NetShop.Domain.Orders;

public sealed class Order : Entity, IAggregateRoot
{
    public Guid CustomerId { get; private set; }
    public DateTime CreatedAtUtc { get; private set; } = DateTime.UtcNow;
    public OrderStatus Status { get; private set; } = OrderStatus.Draft;
    public string Currency { get; private set; } = "EUR";

    private readonly List<OrderLine> _lines = new();
    public IReadOnlyList<OrderLine> Lines => _lines;

    private Order() { } // EF

    public Order(Guid customerId, string currency = "EUR")
    {
        CustomerId = customerId;
        Currency = string.IsNullOrWhiteSpace(currency) ? "EUR" : currency.Trim().ToUpperInvariant();
    }

    public void AddLine(Guid productId, string productName, Money unitPrice, int quantity)
    {
        if (unitPrice.Currency != Currency)
            throw new InvalidOperationException("Order currency mismatch");

        var existing = _lines.FirstOrDefault(l => l.ProductId == productId);
        if (existing is not null) existing.Increase(quantity);
        else _lines.Add(new OrderLine(productId, productName, unitPrice, quantity));
    }

    public Money Total()
    {
        var total = Money.Zero(Currency);
        foreach (var l in _lines) total = total.Add(l.Subtotal);
        return total;
    }

    public void Place()
    {
        if (_lines.Count == 0) throw new InvalidOperationException("Order has no items");
        if (Status != OrderStatus.Draft) throw new InvalidOperationException("Order cannot be placed");

        Status = OrderStatus.Placed;
        Raise(new OrderPlaced(this.Id, this.CustomerId, this.Currency));
    }

    public void Cancel()
    {
        if (Status == OrderStatus.Placed) throw new InvalidOperationException("Placed orders cannot be cancelled in MVP");
        Status = OrderStatus.Cancelled;
    }
}
