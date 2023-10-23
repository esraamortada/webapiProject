using Anadolu.Models;

namespace Anadolu.Repository.Base
{
    public interface IProductOrderRepository : IRepository<ProductOrder>
    {
        List<ProductOrder> GetByOrderId(int orderId, Func<Order, bool> predicate);
    }
}
