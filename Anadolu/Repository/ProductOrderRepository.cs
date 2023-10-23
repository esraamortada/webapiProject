using Anadolu.Models;
using Anadolu.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace Anadolu.Repository
{
    public class ProductOrderRepository : Repository<ProductOrder>, IProductOrderRepository
    {
        private readonly Context Context;
        public ProductOrderRepository(Context _Context) : base(_Context)
        {
            Context = _Context;
        }

        public List<ProductOrder> GetByOrderId(int orderId, Func<Order, bool> predicate) 
        {
            List<ProductOrder> ProductOrders = Context.ProductOrders.Include(po=>po.Product).Where(p=>p.OrderId == orderId).ToList();
            return ProductOrders;
        }
    }
}
