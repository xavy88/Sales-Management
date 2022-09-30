using Microsoft.EntityFrameworkCore;
using Sales_Management_API.Data;
using Sales_Management_API.Model;
using Sales_Management_API.Repository.IRepository;
using System.Linq.Expressions;

namespace Sales_Management_API.Repository
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        private readonly ApplicationDbContext _db;
        public DepartmentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Department> DeleteAsync(Department entity)
        {
            entity.Status = false;
            entity.UpdatedDate = DateTime.Now;
            _db.Departments.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<Department> UpdateAsync(Department entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.Departments.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
