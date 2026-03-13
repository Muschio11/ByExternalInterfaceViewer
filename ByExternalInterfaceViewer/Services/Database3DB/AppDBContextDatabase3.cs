using ByExternalInterfaceViewer.Models.Database3DBModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ByExternalInterfaceViewer.Services.Database3DB
{
    public class AppDBContextDatabase3 :DbContext
    {
        public DbSet<LocationModel> Locations { get; set; }

        public AppDBContextDatabase3(DbContextOptions<AppDBContextDatabase3> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDBContextDatabase3).Assembly);
        }
    }
}
