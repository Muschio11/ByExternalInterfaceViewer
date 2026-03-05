using ByExternalInterfaceViewer.Services;
using ByExternalInterfaceViewer.Services.AWSAccessiDB;
using ByExternalInterfaceViewer.ViewModels;
using ByExternalInterfaceViewer.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace ByExternalInterfaceViewer;

using System;
using System.Windows;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ByExternalInterfaceViewer.Services.AWSAccessiDB;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private IHost? _host;

    public App()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddDbContextFactory<AppDBContextLogin>(options => options.UseSqlServer(AWSAccessiConnectionString.GetConnectionStringToAwsAccessi()));
                services.AddSingleton<LoginView>();
                services.AddSingleton<LoginViewModel>();
                services.AddSingleton<MainWindow>();
            })
            .Build();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        _host?.Start();
        base.OnStartup(e);

        // Diagnostic DB connectivity check to provide immediate, clear feedback
        try
        {
            var factory = _host.Services.GetRequiredService<IDbContextFactory<AppDBContextLogin>>();
            using var ctx = factory.CreateDbContext();
            if (!ctx.Database.CanConnect())
            {
                MessageBox.Show("Unable to connect to the database using the configured connection string. Verify server/port, SQL Browser, and firewall settings.", "Database connection error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Database connectivity check failed: {ex.Message}", "Database connectivity error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        var loginView = _host.Services.GetRequiredService<LoginView>();
        loginView.ShowDialog();

    }

    protected override async void OnExit(ExitEventArgs e)
    {
        if (_host != null)
        {
            await _host.StopAsync();
            _host.Dispose();
        }

        base.OnExit(e);
    }
}
