using Sales_Management_API.Model;
using System.Linq.Expressions;

namespace Sales_Management_API.Repository.IRepository
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<Employee> UpdateAsync(Employee entity);
        Task <Employee> DeleteAsync(Employee entity);

    }
}
