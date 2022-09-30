using Microsoft.EntityFrameworkCore;
using Sales_Management_API.Data;
using Sales_Management_API.Model;
using Sales_Management_API.Repository.IRepository;
using System.Linq.Expressions;

namespace Sales_Management_API.Repository
{
    public class ServiceRepository : Repository<Service>, IServiceRepository
    {
        private readonly ApplicationDbContext _db;
        public ServiceRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Service> DeleteAsync(Service entity)
        {
            entity.Status = false;
            entity.UpdatedDate = DateTime.Now;
            _db.Services.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<Service> UpdateAsync(Service entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.Services.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
