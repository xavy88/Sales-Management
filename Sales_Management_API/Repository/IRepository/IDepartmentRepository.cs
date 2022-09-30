using Sales_Management_API.Model;
using System.Linq.Expressions;

namespace Sales_Management_API.Repository.IRepository
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        Task<Department> UpdateAsync(Department entity);
        Task <Department> DeleteAsync(Department entity);

    }
}
