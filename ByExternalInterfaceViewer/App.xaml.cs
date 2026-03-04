using System.Windows;
using ByExternalInterfaceViewer.Services;
using ByExternalInterfaceViewer.ViewModels;
using ByExternalInterfaceViewer.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ByExternalInterfaceViewer;

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
                services.AddDbContextFactory<AppDBContextLogin>(options => options.UseSqlServer(DBConnectionStrings.GetConnectionStringToAwsAccessi()));
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

        var loginView = _host.Services.GetRequiredService<LoginView>();
        loginView.Show();
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        if(_host != null)
        {
            _host?.StopAsync();
            _host?.Dispose();
        }
        
        base.OnExit(e);
    }
}
