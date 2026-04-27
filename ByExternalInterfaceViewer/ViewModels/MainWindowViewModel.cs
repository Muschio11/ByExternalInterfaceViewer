using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ByExternalInterfaceViewer.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private readonly CassetteContentsViewModel _cassetteContents;
        private readonly MovementsListViewModel _movementsList;
        private readonly FilterMovementsViewModel _filterMovements;

        [ObservableProperty]
        private ObservableObject _currentViewModel;
        [ObservableProperty]
        private FilterMovementsViewModel _filterViewModel;


        public MainWindowViewModel(CassetteContentsViewModel cassetteContentsViewModel, MovementsListViewModel movementsListViewModel, FilterMovementsViewModel filterMovementsViewModel)
        {
            _cassetteContents = cassetteContentsViewModel;
            _movementsList = movementsListViewModel;
            _filterMovements = filterMovementsViewModel;

            CurrentViewModel = _movementsList;
            FilterViewModel = _filterMovements;
        }

        [RelayCommand]
        private void Exit()
        {
            Application.Current.Shutdown();

        }

        [RelayCommand]
        private async void ShowCassetteContentList()
        {
            await _cassetteContents.GetCassetteContentsAsync();
            CurrentViewModel = _cassetteContents;
        }

        [RelayCommand]
        private async void ShowMovementsList()
        {
            await _movementsList.GetMovementsAsync();
            CurrentViewModel = _movementsList;
            FilterViewModel= _filterMovements;
        }
    }

  
    
}

