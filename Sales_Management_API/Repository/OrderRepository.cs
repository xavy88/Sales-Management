using Microsoft.EntityFrameworkCore;
using Sales_Management_API.Data;
using Sales_Management_API.Model;
using Sales_Management_API.Repository.IRepository;
using System.Linq.Expressions;

namespace Sales_Management_API.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly ApplicationDbContext _db;
        public OrderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Order> DeleteAsync(Order entity)
        {
            entity.Status = false;
            entity.UpdatedDate = DateTime.Now;
            _db.Orders.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<Order> UpdateAsync(Order entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.Orders.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
