using System.Linq.Expressions;
using WebMarket.Models;

namespace WebMarket.DataAccess.Services.Interface
{
    public interface IOrderDetailService
    {
        public void Add(OrderDetails entity);
        public IEnumerable<OrderDetails> GetAll();
        public OrderDetails GetFirstOrDefault(Expression<Func<OrderDetails, bool>> filter);
        public void Remove(OrderDetails entity);
        public void RemoveRange(IEnumerable<OrderDetails> entities);
        public void Update(OrderDetails orderDetails);
    }
}
