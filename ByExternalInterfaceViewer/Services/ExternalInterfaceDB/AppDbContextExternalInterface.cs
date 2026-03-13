using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ByExternalInterfaceViewer.Services.ExternalInterfaceDB
{
    public class AppDbContextExternalInterface : DbContext
    {
        public AppDbContextExternalInterface(DbContextOptions<AppDbContextExternalInterface> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContextExternalInterface).Assembly);
        }
    }
}
