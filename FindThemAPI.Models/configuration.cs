using System;
using System.Collections.Generic;
using System.Text;

namespace FindThemAPI.Models
{
    public class Configuration
    {
        public static string stringConnection { get { return "Data Source=SQLServer,1433;Initial Catalog=FindThemDB;User ID=sa;Password=Sample123$"; } }
    }
}
