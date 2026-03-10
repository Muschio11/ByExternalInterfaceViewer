using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ByExternalInterfaceViewer.ViewModels
{
    public partial class LoadingWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _progressStatus;
    }
}
