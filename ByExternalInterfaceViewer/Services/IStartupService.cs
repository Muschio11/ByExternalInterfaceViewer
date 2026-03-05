using System;
using System.Collections.Generic;
using System.Text;

namespace ByExternalInterfaceViewer.Services;

public interface IStartupService
{
    Task StartAsync();
}
