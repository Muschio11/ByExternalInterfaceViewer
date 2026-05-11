using ByExternalInterfaceViewer.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ByExternalInterfaceViewer.Services
{
    public partial class MovementsFilterService : ObservableObject
    {
        [ObservableProperty]
        private int _selectedLines = 10;

        public IFilterService? ActiveViewModel { get; set; }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            _ = RefreshAsync();
        }

        public async Task RefreshAsync()
        {
            if (ActiveViewModel != null)
            {
                await ActiveViewModel.RefreshAsync();
            }
        }


    }
    
}
