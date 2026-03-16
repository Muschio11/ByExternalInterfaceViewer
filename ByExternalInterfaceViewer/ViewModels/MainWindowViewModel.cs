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

        [ObservableProperty]
        private ObservableObject _currentViewModel;


        public MainWindowViewModel(CassetteContentsViewModel cassetteContentsViewModel, MovementsListViewModel movementsListViewModel)
        {
            _cassetteContents = cassetteContentsViewModel;
            _movementsList = movementsListViewModel;

            CurrentViewModel = _movementsList;
        }

        [RelayCommand]
        private void Exit()
        {
            Application.Current.Shutdown();

        }

        [RelayCommand]
        private void ShowCassetteContentList()
        {
            CurrentViewModel = _cassetteContents;
        }

        [RelayCommand]
        private void ShowMovementsList()
        {
            CurrentViewModel = _movementsList;
        }
    }

  
    
}

