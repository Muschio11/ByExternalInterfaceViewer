using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ByExternalInterfaceViewer.Models.LoginModels;
using ByExternalInterfaceViewer.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace ByExternalInterfaceViewer.ViewModels
{


    
    public partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<string> username;
        MainWindow mainWindow;
        private readonly LoginView _currentLoginView;

        [ObservableProperty]
        private string password;

        public LoginViewModel(LoginView window)
        {
            _currentLoginView = window;
            GetUsername(); 
        }

        private void GetUsername()
        {
            List<string> name = new List<string>();
            name.Add("Aaaaa");
            name.Add("Bssssss");
            Username = new ObservableCollection<string>(name);
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
            mainWindow = new MainWindow();
            mainWindow.Show();
            _currentLoginView.Close();

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
    
    
}
