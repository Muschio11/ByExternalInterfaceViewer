using ByExternalInterfaceViewer.Services.AWSAccessiDB;
using ByExternalInterfaceViewer.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Windows;

namespace ByExternalInterfaceViewer.Services
{
    public class StartupService : IStartupService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IDbContextFactory<AppDBContextLogin> _dbContextLogin;
        public StartupService(IServiceProvider serviceProvider, IDbContextFactory<AppDBContextLogin> dbContextLogin)
        {
            _serviceProvider = serviceProvider;
            _dbContextLogin = dbContextLogin;
        }
        public async Task StartAsync()
        {
            var loadingView = _serviceProvider.GetRequiredService<LoadingWindowView>();
            loadingView.Show();

            bool isConnected = false;

            try
            {
                isConnected = await Task.Run(() =>
                {
                        using var ctx = _dbContextLogin.CreateDbContext();
                        return ctx.Database.CanConnect();
                });

                if(!isConnected)
                {
                    MessageBox.Show("Unable to connect to the database using the configured connection string. Verify server/port, SQL Browser, and firewall settings.", "Database connection error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database connectivity check failed: {ex.Message}", "Database connectivity error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            

            var loginView = _serviceProvider.GetRequiredService<LoginView>();
            loadingView.Close();
            loginView.Show();
        }
    }
}