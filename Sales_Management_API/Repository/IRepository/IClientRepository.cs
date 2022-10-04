using Sales_Management_API.Model;
using System.Linq.Expressions;

namespace Sales_Management_API.Repository.IRepository
{
    public interface IClientRepository : IRepository<Client>
    {
        Task<Client> UpdateAsync(Client entity);
        Task<Client> DeleteAsync(Client entity);

    }
}
