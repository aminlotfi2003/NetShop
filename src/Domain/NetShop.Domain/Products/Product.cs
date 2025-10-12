using NetShop.SharedKernel.Domain.Abstractions;
using NetShop.SharedKernel.Domain.ValueObjects;
using NetShop.Domain.Products.Events;

namespace NetShop.Domain.Products;

public sealed class Product : Entity, IAggregateRoot
{
    public Sku Sku { get; private set; } = null!;
    public string Name { get; private set; } = null!;
    public Money Price { get; private set; } = null!;
    public int StockQuantity { get; private set; }

    private Product() { } // EF

    public Product(Sku sku, string name, Money price, int initialStock = 0)
    {
        Sku = sku;
        Rename(name, raiseEvent: false);
        ChangePrice(price, raiseEvent: false);
        if (initialStock < 0) throw new ArgumentOutOfRangeException(nameof(initialStock));
        StockQuantity = initialStock;

        Raise(new ProductCreated(
            productId: this.Id,
            sku: Sku.Value,
            name: Name,
            priceAmount: Price.Amount,
            currency: Price.Currency,
            initialStock: StockQuantity));
    }

    public void Rename(string name, bool raiseEvent = true)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name required");
        var newName = name.Trim();
        if (newName == Name) return;

        var oldName = Name;
        Name = newName;

        if (raiseEvent && !string.IsNullOrEmpty(oldName))
            Raise(new ProductRenamed(this.Id, oldName, Name));
    }

    public void ChangePrice(Money newPrice, bool raiseEvent = true)
    {
        if (newPrice is null) throw new ArgumentNullException(nameof(newPrice));
        if (Price is not null &&
            (newPrice.Amount == Price.Amount && newPrice.Currency == Price.Currency))
            return;

        var oldAmount = Price?.Amount ?? 0m;
        var currency = newPrice.Currency;

        Price = newPrice;

        if (raiseEvent && oldAmount != 0m)
            Raise(new ProductPriceChanged(this.Id, oldAmount, newPrice.Amount, currency));
    }

    public void IncreaseStock(int by)
    {
        if (by <= 0) throw new ArgumentOutOfRangeException(nameof(by));
        StockQuantity += by;

        Raise(new ProductStockIncreased(this.Id, by, StockQuantity));
    }

    public void DecreaseStock(int by)
    {
        if (by <= 0) throw new ArgumentOutOfRangeException(nameof(by));
        if (by > StockQuantity) throw new InvalidOperationException("Not enough stock");

        StockQuantity -= by;

        Raise(new ProductStockDecreased(this.Id, by, StockQuantity));
    }
}
