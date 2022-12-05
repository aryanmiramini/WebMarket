using System.Linq.Expressions;
using Webmarket.Models;

namespace WebMarket.DataAccess.Services.Interface
{
    public interface ICategoryService
    {
        public void Add(Category entity);
        public IEnumerable<Category> GetAll();
        public Category GetFirstOrDefault(Expression<Func<Category, bool>> filter);
        public void Remove(Category entity);
        public void RemoveRange(IEnumerable<Category> entities);
        public void Update(Category category);
    }
}
