using Microsoft.EntityFrameworkCore;
using Sales_Management_API.Data;
using Sales_Management_API.Model;
using Sales_Management_API.Repository.IRepository;
using System.Linq.Expressions;

namespace Sales_Management_API.Repository
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private readonly ApplicationDbContext _db;
        public EmployeeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Employee> DeleteAsync(Employee entity)
        {
            entity.Status = false;
            entity.UpdatedDate = DateTime.Now;
            _db.Employees.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<Employee> UpdateAsync(Employee entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.Employees.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
