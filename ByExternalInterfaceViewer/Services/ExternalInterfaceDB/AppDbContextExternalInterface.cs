using Microsoft.EntityFrameworkCore;
using ByExternalInterfaceViewer.Models.ExternalinterfaceDBModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ByExternalInterfaceViewer.Services.ExternalInterfaceDB
{
    public class AppDbContextExternalInterface : DbContext
    {
        public DbSet<MovementsListModel> MovementsList { get; set; }
        public DbSet<CassetteContentsListModel> CassetteContentsList { get; set; }
        public AppDbContextExternalInterface(DbContextOptions<AppDbContextExternalInterface> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContextExternalInterface).Assembly);
        }
    }
}
