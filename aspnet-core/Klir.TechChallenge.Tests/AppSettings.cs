using System;
using System.Collections.Generic;
using System.Text;

namespace Klir.TechChallenge.Tests
{
    public class AppSettings
    {
        public ConnectionStrings ConnectionStrings { get; set; }
    }

    public class ConnectionStrings
    {
        public string Main { get; set; }
    }

}
