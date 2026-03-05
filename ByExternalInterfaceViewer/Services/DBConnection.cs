using ByExternalInterfaceViewer.Models.AWSAccessiDBModelsodels;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace ByExternalInterfaceViewer.Services;


/// <summary>
/// Set the connection strings for the databases in the app.config file and use this class to retrieve them when needed.
/// </summary>
public class DBConnectionStrings
{
    public static string GetConnectionStringToAwsDatabase3()
    {
        return ConfigurationManager.ConnectionStrings["AWSDatabase3"].ConnectionString;
    }

    
    public static string GetConnectionStringToExternalInterface()
    {
        return ConfigurationManager.ConnectionStrings["ExternalInterface"].ConnectionString;
    }
}