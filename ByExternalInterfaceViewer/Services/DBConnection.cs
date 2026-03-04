using ByExternalInterfaceViewer.Models.LoginModels;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace ByExternalInterfaceViewer.Services;

public class AppDBContextLogin : DbContext
{
    public DbSet<Login> Logins { get; set; }
    public AppDBContextLogin(DbContextOptions<AppDBContextLogin> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDBContextLogin).Assembly);
    }
}
/// <summary>
/// Set the connection strings for the databases in the app.config file and use this class to retrieve them when needed.
/// </summary>
public class DBConnectionStrings
{
    public static string GetConnectionStringToAwsDatabase3()
    {
        return ConfigurationManager.ConnectionStrings["AWSDatabase3"].ConnectionString;
    }

    public static string GetConnectionStringToAwsAccessi()
    {
        return ConfigurationManager.ConnectionStrings["AWSAccessi"].ConnectionString;
    }
    public static string GetConnectionStringToExternalInterface()
    {
        return ConfigurationManager.ConnectionStrings["ExternalInterface"].ConnectionString;
    }
}