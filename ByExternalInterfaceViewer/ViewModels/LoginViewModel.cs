using ByExternalInterfaceViewer.Models.AWSAccessiDBModelsodels;
using ByExternalInterfaceViewer.Services.AWSAccessiDB;
using ByExternalInterfaceViewer.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace ByExternalInterfaceViewer.ViewModels;



public partial class LoginViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<string> _username;
    private LoginView _currentLoginView;
   
    private readonly IDbContextFactory<AppDBContextLogin> _dbContextAccessi;
    private readonly IServiceProvider _serviceProvider;

    [ObservableProperty]
    private string _selectedUserName;


    [ObservableProperty]
    private string _password;
    [ObservableProperty]
    private string _SwVersion;
    private object _mainwindow;

    public LoginViewModel(IDbContextFactory<AppDBContextLogin> dbContextFactory, IServiceProvider serviceProvider)
    {
        _dbContextAccessi = dbContextFactory;
        GlobalVersion globalVersion = new GlobalVersion();
        SwVersion = globalVersion.Version;
        _serviceProvider = serviceProvider;



    }

    public void AttachView(LoginView view)
    {
        _currentLoginView = view;
        // Load usernames after view is attached;
        _ = LoadUsernamesAsync();
    }

    private async Task LoadUsernamesAsync()
    {
        try
        {
            await using var context = await _dbContextAccessi.CreateDbContextAsync();
            var logins = await context.Logins.ToListAsync();

            var name = new List<string>();
            foreach (var login in logins)
            {
                name.Add(login.Username);
            }

            if (Application.Current?.Dispatcher?.CheckAccess() == true)
            {
                Username = new ObservableCollection<string>(name);
            }
            else
            {
                await Application.Current.Dispatcher.InvokeAsync(() => Username = new ObservableCollection<string>(name));
            }
        }
        catch (Exception ex)
        {
            // Provide clear, non-crashing feedback and keep UI stable.
            MessageBox.Show($"Cannot load usernames: {ex.Message}", "Database connection error", MessageBoxButton.OK, MessageBoxImage.Warning);
            Username = new ObservableCollection<string>();
        }
    }

    /// <summary>
    /// Comando per chiudere l'applicazione login
    /// </summary>
    [RelayCommand]
    private void CloseLoginView()
    {
        App.Current.Shutdown();

    }

    /// <summary>
    /// Comando per eseguirer il login. Se password non compilate, bottone login disattivo
    /// </summary>
    [RelayCommand(CanExecute = nameof(IsEnableLogin))]
    private async Task Login()
    {
        var valid = await ValidatePasswordAsync();
        if (valid)
        {

            
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
            _currentLoginView?.Close();
        }
    }

    private async Task<bool> ValidatePasswordAsync()
    {
        try
        {
            await using var context = await _dbContextAccessi.CreateDbContextAsync();
            var selectedUser = await context.Logins.FirstOrDefaultAsync(u => u.Username == SelectedUserName);

            // Use ViewModel.Password (kept in sync by code-behind PasswordChanged)
            if (!string.IsNullOrEmpty(SelectedUserName) && selectedUser != null && Password == selectedUser.Password)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }
        catch (Exception ex)
        {
            // Provide clear, non-crashing feedback and keep UI stable.
            MessageBox.Show($"Cannot validate credentials: {ex.Message}", "Database connection error", MessageBoxButton.OK, MessageBoxImage.Warning);
            Username = new ObservableCollection<string>();
            return false;
        }
    }
    private bool IsEnableLogin()
    {
        return !string.IsNullOrEmpty(Password);
    }
    partial void OnPasswordChanged(string oldValue, string newValue)
    {
        LoginCommand.NotifyCanExecuteChanged();
    }
}


