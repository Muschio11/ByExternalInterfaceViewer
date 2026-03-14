using ByExternalInterfaceViewer.Services;
using ByExternalInterfaceViewer.Services.AWSAccessiDB;
using ByExternalInterfaceViewer.ViewModels;
using ByExternalInterfaceViewer.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace ByExternalInterfaceViewer;

using ByExternalInterfaceViewer.Services.AWSAccessiDB;
using ByExternalInterfaceViewer.Services.Database3DB;
using ByExternalInterfaceViewer.Services.ExternalInterfaceDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;

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
                services.AddDbContextFactory<AppDBContextLogin>(login => login.UseSqlServer(AWSAccessiConnectionString.GetConnectionStringToAwsAccessi()));
                services.AddDbContextFactory<AppDBContextDatabase3>(db3 => db3.UseSqlServer(AWSDatabase3ConnectionString.GetConnectionStringToAwsDatabase3()));
                services.AddDbContextFactory<AppDbContextExternalInterface>(extInt => extInt.UseSqlServer(ExternalInterfaceConnectionString.GetConnectionStringToExternalInterface()));
                services.AddSingleton<IStartupService, StartupService>();
                services.AddSingleton<LoadingWindowView>();
                services.AddSingleton<LoadingWindowViewModel>();
                services.AddTransient<LoginView>();
                services.AddTransient<LoginViewModel>();
                services.AddTransient<MainWindow>();
                services.AddTransient<MainWindowViewModel>();
                services.AddTransient<CassetteContentsViewModel>();
                services.AddTransient<MovementsListViewModel>();
            })
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        _host?.Start();
        
        var loadingView = _host?.Services.GetRequiredService<LoadingWindowView>();
        var loadingViewModel = _host?.Services.GetRequiredService<LoadingWindowViewModel>();
        loadingView.Show();

        var progress = new Progress<string>(message =>
        {
            loadingViewModel.ProgressStatus = message;
        });


        var startupService = _host?.Services.GetRequiredService<IStartupService>();
        
        bool connected = await startupService.StartAsync(progress);

        var loginView = _host?.Services.GetRequiredService<LoginView>();
        loadingView.Close();

        if (!connected)
        {
            MessageBox.Show("Unable to connect to the database. Please check your connection and try again.", "Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
            App.Current.Shutdown();
        }
        else
        {
            loginView.ShowDialog();
        }

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
