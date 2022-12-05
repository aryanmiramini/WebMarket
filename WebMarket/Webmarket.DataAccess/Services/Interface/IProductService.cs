using System.Linq.Expressions;
using WebMarket.Models;
using WebMarket.Models.ViewModels;

namespace WebMarket.DataAccess.Services.Interface
{
    public interface IProductService
    {
        public void Add(ProductVM entity);
        public IEnumerable<Product> GetAll();
        public Product GetFirstOrDefault(Expression<Func<Product, bool>> filter);
        public void Remove(Product entity);
        public void RemoveRange(IEnumerable<Product> entities);
        public void Update(Product product);
    }
}
