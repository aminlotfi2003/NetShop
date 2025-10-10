using System.Text.RegularExpressions;

namespace NetShop.SharedKernel.Domain.ValueObjects;

public sealed record Sku
{
    public string Value { get; init; } = null!;

    private Sku() { } // For EF

    private Sku(string raw)
    {
        var v = raw?.Trim().ToUpperInvariant();
        if (string.IsNullOrWhiteSpace(v))
            throw new ArgumentException("Sku required");

        if (!Regex.IsMatch(v, "^[A-Z0-9-_.]{3,32}$"))
            throw new ArgumentException("Invalid Sku format");

        Value = v;
    }

    public static Sku From(string value) => new(value);

    public override string ToString() => Value;
}
