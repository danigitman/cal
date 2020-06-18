using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apical
{
    public class AppSettings
    {
        public Credentials Credentials { get; set; }
        public ApiUrls ApiUrls { get; set; }
    }
    public class Credentials
    {
        public string Url { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class ApiUrls
    {
        public string BaseUrl { get; set; }
        public string Authorization { get; set; }
        public string GetTransactions { get; set; }
        public string SaveTransactions { get; set; } 
    }
}
