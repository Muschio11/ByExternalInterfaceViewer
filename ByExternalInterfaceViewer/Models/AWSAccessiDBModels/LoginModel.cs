using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ByExternalInterfaceViewer.Models.AWSAccessiDBModelsodels;

public class LoginModel
{
    public string Username { get; set; }
    public string Password { get; set; }    
}
