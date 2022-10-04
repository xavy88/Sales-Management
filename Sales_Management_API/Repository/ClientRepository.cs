using Microsoft.EntityFrameworkCore;
using Sales_Management_API.Data;
using Sales_Management_API.Model;
using Sales_Management_API.Repository.IRepository;
using System.Linq.Expressions;

namespace Sales_Management_API.Repository
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        private readonly ApplicationDbContext _db;
        public ClientRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Client> DeleteAsync(Client entity)
        {
            entity.Status = false;
            entity.UpdatedDate = DateTime.Now;
            _db.Clients.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<Client> UpdateAsync(Client entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.Clients.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
