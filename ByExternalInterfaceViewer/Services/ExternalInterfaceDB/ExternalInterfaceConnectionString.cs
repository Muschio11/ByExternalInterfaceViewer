using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace ByExternalInterfaceViewer.Services.ExternalInterfaceDB
{
    public class ExternalInterfaceConnectionString
    {
        public static string GetConnectionStringToExternalInterface()
        {
            return ConfigurationManager.ConnectionStrings["ExternalInterface"].ConnectionString;
        }
    }
}
