using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace MowManager.Models
{
    public static class PrepDB
    {
        // Get the scope of our DB Context
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var pricingContext = serviceScope.ServiceProvider.GetService<PricingContext>();
                var serviceContext = serviceScope.ServiceProvider.GetService<ServiceContext>();
                var crewContext = serviceScope.ServiceProvider.GetService<CrewContext>();
                var customerContext = serviceScope.ServiceProvider.GetService<CustomerContext>();
                
                // Output to console
                System.Console.WriteLine("Applying Migrations...");

                // Apply migration files to database
                pricingContext.Database.Migrate();
                serviceContext.Database.Migrate();
                crewContext.Database.Migrate();
                customerContext.Database.Migrate();

                if (!pricingContext.PricingItems.Any())
                {
                    System.Console.WriteLine("Applying migrations to Pricing table");
                    pricingContext.PricingItems.AddRange(
                        new Pricing() {Name="Standard", Rate=20.00M, RateTaxIncluded=21.60M, TaxValue=0.08M},
                        new Pricing() {Name="Presidential", Rate=40.00M, RateTaxIncluded=43.20M, TaxValue=0.08M},
                        new Pricing() {Name="Dictorian", Rate=60.00M, RateTaxIncluded=64.80M, TaxValue=0.08M}
                    );
                    pricingContext.SaveChanges();
                }
                else if (!serviceContext.ServiceItems.Any())
                {
                    System.Console.WriteLine("Applying migrations to Service table");
                    serviceContext.SaveChanges();
                }
                else if (!crewContext.CrewItems.Any())
                {
                    System.Console.WriteLine("Applying migrations to Crew table");
                    crewContext.SaveChanges();
                }
                else if (!customerContext.CustomerItems.Any())
                {
                    System.Console.WriteLine("Applying migrations to Customer table");
                    customerContext.SaveChanges();
                }
                else
                {
                    System.Console.WriteLine("There are no new migrations to be applied to the database.");
                }
            }
        }
    }
}