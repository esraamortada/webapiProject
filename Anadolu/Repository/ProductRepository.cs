using Anadolu.Models;
using Anadolu.Repository.Base;

namespace Anadolu.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly Context Context;
        public ProductRepository(Context _Context) : base(_Context)
        {
            Context = _Context;
        }

        public List<Product> GetAllProductsinDiscount(Func<Product, bool> predicate)
        {
            var products = Context.Products.ToList();

            return products;
        }

        public Product GetProductById(int productid, Func<Product, bool> predicate)
        {
            var Product = Context.Products.Where(p => p.Id == productid)
                .FirstOrDefault();





            return Product;
        }
        public List<Product> GetAllProducts(Func<Product, bool> predicate)
        {
            var products = Context.Products.Where(predicate).ToList();

            return products;

        }
        public List<Product> Getproduct(Func<Product, bool> predicate, int id)
        {
            var pro = Context.Products.Where(a => a.SubCategoryId == id).Where(predicate).Take(3).ToList();
            return pro;


        }
    }
}
