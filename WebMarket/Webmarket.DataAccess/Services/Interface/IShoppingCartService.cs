using System.Linq.Expressions;
using WebMarket.Models;
using WebMarket.Models.ViewModels;
using ShoppingCart = WebMarket.Models.ShoppingCart;

namespace WebMarket.DataAccess.Services.Interface
{
    public interface IShoppingCartService
    {
        public void Add(ShoppingCart entity);
        public IEnumerable<ShoppingCart> GetAll(string? id);
        public ShoppingCart GetFirstOrDefault(Expression<Func<ShoppingCart, bool>> filter);
        public void Remove(ShoppingCart entity);
        public void RemoveRange(IEnumerable<ShoppingCart> entities);
        public void Update(ShoppingCart entity);

        int IncrementCount(ShoppingCart shoppingCart, int count);

        int DecrementCount(ShoppingCart shoppingCart, int count);
    }
}
