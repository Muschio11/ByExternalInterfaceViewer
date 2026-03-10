using ByExternalInterfaceViewer.Services.AWSAccessiDB;
using ByExternalInterfaceViewer.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ByExternalInterfaceViewer.Services;

public class StartupService : IStartupService
{
    
    private readonly IDbContextFactory<AppDBContextLogin> _dbContextLogin;
    public bool IsLoggedIn { get; set; }
    public StartupService(IDbContextFactory<AppDBContextLogin> dbContextLogin)
    {
        
        _dbContextLogin = dbContextLogin;
    }

    public async Task<bool> StartAsync(IProgress<string> progress)
    {

        progress.Report("Application is starting...");

        await Task.Delay(800);

        progress.Report("Connecting to database...");

        bool isConnected = await Task.Run(() =>
            {
                using var ctx = _dbContextLogin.CreateDbContext();
                return ctx.Database.CanConnect();
            });

        await Task.Delay(2000);

        
        return isConnected;




    }
}