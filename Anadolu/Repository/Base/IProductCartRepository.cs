using Anadolu.Models;

namespace Anadolu.Repository.Base
{
    public interface IProductCartRepository : IRepository<ProductCart>
    {
         int GetCountofItems(string userid);
    }
}
