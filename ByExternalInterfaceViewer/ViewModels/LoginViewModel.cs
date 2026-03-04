using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ByExternalInterfaceViewer.Models.LoginModels;
using ByExternalInterfaceViewer.Services;
using ByExternalInterfaceViewer.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;


namespace ByExternalInterfaceViewer.ViewModels;




public partial class LoginViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<string> username;
    private LoginView _currentLoginView;
    private readonly IDbContextFactory<AppDBContextLogin> _dbContextAccessi;    

    [ObservableProperty]
    private string password;
    [ObservableProperty]
    private string _SwVersion;

    public LoginViewModel(IDbContextFactory<AppDBContextLogin> dbContextFactory)
    {
        _dbContextAccessi = dbContextFactory;
        GlobalVersion globalVersion = new GlobalVersion();
        SwVersion = globalVersion.Version;
        //GetUsernameFromDB();


    }

    public void AttachView(LoginView view)
    {
        _currentLoginView = view;
    }

    public void GetUsernameFromDB()
    {
        using (var context = _dbContextAccessi.CreateDbContext())
        {
            var logins = context.Logins.ToList();
            
            List<string> name = new List<string>();
            foreach (var login in logins)
            {
                name.Add(login.Usermane);
            }
            Username = new ObservableCollection<string>(name);
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
    private void Login()
    {
        var mainWindow = new MainWindow();
        mainWindow.Show();
        _currentLoginView?.Close();

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


