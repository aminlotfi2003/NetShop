using NetShop.SharedKernel.Domain.ValueObjects;

namespace NetShop.Sales.Domain.Orders;

public sealed class OrderLine
{
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; } = null!;
    public Money UnitPrice { get; private set; } = null!;
    public int Quantity { get; private set; }

    private OrderLine() { } // EF

    public OrderLine(Guid productId, string productName, Money unitPrice, int quantity)
    {
        if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));
        ProductId = productId;
        ProductName = string.IsNullOrWhiteSpace(productName) ? throw new ArgumentException("name") : productName.Trim();
        UnitPrice = unitPrice ?? throw new ArgumentNullException(nameof(unitPrice));
        Quantity = quantity;
    }

    public void Increase(int by)
    {
        if (by <= 0) throw new ArgumentOutOfRangeException(nameof(by));
        Quantity += by;
    }

    public Money Subtotal => UnitPrice.Multiply(Quantity);
}
