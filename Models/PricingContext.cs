using Microsoft.EntityFrameworkCore;

namespace MowManager.Models
{
    public class PricingContext : DbContext
    {
        public PricingContext (DbContextOptions<PricingContext> options) : base(options)
        {

        }

        public DbSet<Pricing> PricingItems { get; set; }
    }
}