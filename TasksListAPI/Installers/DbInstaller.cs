using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasksListAPI.Data;
using TasksListAPI.Services;

namespace TasksListAPI.Installers
{
    public class DbInstaller : IInstaller
    {
        public void IntallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<DataContext>();

            services.AddScoped<ISmartTaskService, SmartTaskService>(); // For Entity Framework 
            services.AddScoped<ICustomTaskService, CustomTaskService>(); // For Entity Framework 
           // services.AddSingleton<ICustomTaskService, CosmosCustomTaskService>(); // For CosmosDb
        }
    }
}
