using Sales_Management_API.Model;
using System.Linq.Expressions;

namespace Sales_Management_API.Repository.IRepository
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> UpdateAsync(Order entity);
        Task <Order> DeleteAsync(Order entity);

    }
}
