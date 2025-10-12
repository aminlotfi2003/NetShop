using NetShop.SharedKernel.Domain.Abstractions;

namespace NetShop.Domain.Products.Events;

public sealed class ProductRenamed : IDomainEvent
{
    public Guid ProductId { get; }
    public string OldName { get; }
    public string NewName { get; }
    public DateTime OccurredOnUtc { get; } = DateTime.UtcNow;

    public ProductRenamed(Guid productId, string oldName, string newName)
    {
        ProductId = productId;
        OldName = oldName;
        NewName = newName;
    }
}
