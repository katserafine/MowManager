using Microsoft.EntityFrameworkCore;

namespace MowManager.Models
{
    public class ServiceContext : DbContext
    {
        public ServiceContext (DbContextOptions<ServiceContext> options) : base(options)
        {

        }

        public DbSet<Service> ServiceItems { get; set; }
    }
}