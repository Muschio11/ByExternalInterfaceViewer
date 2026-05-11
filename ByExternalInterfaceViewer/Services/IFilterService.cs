using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ByExternalInterfaceViewer.Services;

public interface IFilterService
{
    Task RefreshAsync();

}
