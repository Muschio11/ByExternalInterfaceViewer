using ByExternalInterfaceViewer.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace ByExternalInterfaceViewer.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly MovementsFilterService _filterService;

        [ObservableProperty]
        private object _currentViewModel;

        [ObservableProperty]
        private object _currentFilterViewModel;


        public MainWindowViewModel(IServiceProvider serviceProvider, MovementsFilterService filterService)
        {
            _serviceProvider = serviceProvider;
            _filterService = filterService;

            ShowMovementsList();


        }

        [RelayCommand]
        private void Exit()
        {
            Application.Current.Shutdown();

        }

        [RelayCommand]
        private async void ShowCassetteContentList()
        {
           
            CurrentViewModel = _serviceProvider.GetRequiredService<CassetteContentsViewModel>();
            
        }

        [RelayCommand]
        private async void ShowMovementsList()
        {
           var vm = _serviceProvider.GetRequiredService<MovementsListViewModel>();
            _filterService.ActiveViewModel = vm;
            CurrentViewModel = vm;
            CurrentFilterViewModel = _serviceProvider.GetRequiredService<FilterMovementsViewModel>();
        }
    }

  
    
}

