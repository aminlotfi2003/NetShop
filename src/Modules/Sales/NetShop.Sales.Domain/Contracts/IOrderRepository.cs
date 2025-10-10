using NetShop.Sales.Domain.Orders;

namespace NetShop.Sales.Domain.Contracts;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task AddAsync(Order order, CancellationToken ct = default);
}
