using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ByExternalInterfaceViewer.ViewModels;
namespace ByExternalInterfaceViewer.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView
    {
        public LoginView( LoginViewModel viewModel)
        {
            
            InitializeComponent();
            DataContext = viewModel;
            viewModel.AttachView(this);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Passwordbox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var pb = sender as PasswordBox;
            var vm = DataContext as LoginViewModel;
            if (vm != null)
                vm.Password = pb.Password;
        }
    }
}
