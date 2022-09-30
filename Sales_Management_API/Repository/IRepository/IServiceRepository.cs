using Sales_Management_API.Model;
using System.Linq.Expressions;

namespace Sales_Management_API.Repository.IRepository
{
    public interface IServiceRepository : IRepository<Service>
    {
        Task<Service> UpdateAsync(Service entity);
        Task <Service> DeleteAsync(Service entity);

    }
}
