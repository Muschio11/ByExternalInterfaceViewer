using ByExternalInterfaceViewer.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ByExternalInterfaceViewer.Views
{
    /// <summary>
    /// Interaction logic for MovementsListView.xaml
    /// </summary>
    public partial class MovementsListView
    {
        public MovementsListView(MovementsListViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
