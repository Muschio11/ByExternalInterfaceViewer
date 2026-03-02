using System.Configuration;
using System.Data;
using System.Windows;
using ByExternalInterfaceViewer.ViewModels;
using ByExternalInterfaceViewer.Views;

namespace ByExternalInterfaceViewer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var loginView = new LoginView();
            loginView.ShowDialog();
        }
    }

}
