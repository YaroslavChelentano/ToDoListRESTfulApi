using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TasksListAPI.Domain;

namespace TasksListAPI.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
 
        public DbSet<CustomTask> CustomTasks { get; set; }
        public DbSet<SmartTask> SmartTasks { get; set; }
    }
}
