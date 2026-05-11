using ByExternalInterfaceViewer.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ByExternalInterfaceViewer.ViewModels
{
    public partial class FilterMovementsViewModel : ObservableObject
    {
        public MovementsFilterService FilterService { get; }

        public List<int> TakeOptions { get; } = new() { 5, 10, 100, 200, 500, 1000 };

        public FilterMovementsViewModel(MovementsFilterService filterService)
        {
            FilterService = filterService;
        }

    }
}
