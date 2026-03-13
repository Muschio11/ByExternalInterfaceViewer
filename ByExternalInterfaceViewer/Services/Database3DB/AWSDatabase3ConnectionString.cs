using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace ByExternalInterfaceViewer.Services.Database3DB
{
    public class AWSDatabase3ConnectionString
    {
        public static string GetConnectionStringToAwsDatabase3()
        {
            return ConfigurationManager.ConnectionStrings["AWSDatabase3"].ConnectionString;
        }
    }
}
