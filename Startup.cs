using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MowManager.Models;
using Microsoft.EntityFrameworkCore;

namespace MowManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Environment variables to emulate the typical connection string in appsettings.json to containerized db
            var host = Configuration["DBHOST"] ?? "db";
            var db = Configuration["DBNAME"] ?? "sql-docker";
            var port = Configuration["DBPORT"] ?? "1433";
            var username = Configuration["DBUSERNAME"] ?? "SA";
            var password = Configuration["DBPASSWORD"] ?? "Sql!Expre55";

            string connStr = $"Data Source={host},{port};Integrated Security=False;";
            connStr += $"User ID={username};Password={password};Database={db};";
            connStr += $"Connect Timeout=30;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            // Add DBContext as a service to the application
            services.AddDbContext<PricingContext>(options => options.UseSqlServer(connStr));
            services.AddDbContext<ServiceContext>(options => options.UseSqlServer(connStr));
            services.AddDbContext<CrewContext>(options => options.UseSqlServer(connStr));
            services.AddDbContext<CustomerContext>(options => options.UseSqlServer(connStr));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}
