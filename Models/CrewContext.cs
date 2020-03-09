using Microsoft.EntityFrameworkCore;

namespace MowManager.Models
{
    public class CrewContext : DbContext
    {
        public CrewContext (DbContextOptions<CrewContext> options) : base(options)
        {

        }

        public DbSet<Crew> CrewItems { get; set; }
    }
}