using Microsoft.EntityFrameworkCore;
using Sales_Management_API.Model;

namespace Sales_Management_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Department> Departments { get; set; }
    }
}
