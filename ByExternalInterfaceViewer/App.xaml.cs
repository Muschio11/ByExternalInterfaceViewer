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
                services.AddSingleton<IStartupService, StartupService>();
                services.AddSingleton<LoadingWindowView>();
                services.AddSingleton<LoadingWindowViewModel>();
                services.AddSingleton<LoginView>();
                services.AddSingleton<LoginViewModel>();
                services.AddSingleton<MainWindow>();
                services.AddSingleton<MainWindowViewModel>();
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
