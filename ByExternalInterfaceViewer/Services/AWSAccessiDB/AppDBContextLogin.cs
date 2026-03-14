using ByExternalInterfaceViewer.Models.AWSAccessiDBModelsodels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ByExternalInterfaceViewer.Services.AWSAccessiDB
{
    public class AppDBContextLogin : DbContext
    {
        public DbSet<LoginModel> Logins { get; set; }
        public AppDBContextLogin(DbContextOptions<AppDBContextLogin> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDBContextLogin).Assembly);
            
        }
    }
}
