using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace ByExternalInterfaceViewer.Services.AWSAccessiDB
{
    public class AWSAccessiConnectionString
    {
        public static string GetConnectionStringToAwsAccessi()
        {
            return ConfigurationManager.ConnectionStrings["AWSAccessi"].ConnectionString;
        }
    }
}
