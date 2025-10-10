namespace NetShop.SharedKernel.Domain.ValueObjects;

public sealed record Money
{
    public decimal Amount { get; init; }
    public string Currency { get; init; } = "EUR";

    private Money() { }

    private Money(decimal amount, string currency)
    {
        if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount));
        if (string.IsNullOrWhiteSpace(currency)) throw new ArgumentException("Currency required");

        Amount = decimal.Round(amount, 2);
        Currency = currency.Trim().ToUpperInvariant();
    }

    public static Money From(decimal amount, string currency = "EUR") => new(amount, currency);

    public Money Add(Money other)
    {
        if (other.Currency != Currency) throw new InvalidOperationException("Currency mismatch");
        return new Money(Amount + other.Amount, Currency);
    }

    public Money Multiply(int qty)
    {
        if (qty <= 0) throw new ArgumentOutOfRangeException(nameof(qty));
        return new Money(Amount * qty, Currency);
    }

    public static Money Zero(string currency = "EUR") => new(0m, currency);
}
