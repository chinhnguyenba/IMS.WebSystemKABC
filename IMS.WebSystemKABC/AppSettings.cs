using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMS.WebSystemKingHub
{
    public sealed class AppSettings
    {
        public string AllowedHosts { get; set; }
        public Server Server { get; set; }        

        public static AppSettings Current { get; set; }
        public AppSettings()
        {
            Current = this;
        }
    }

    public sealed class Server
    {
        public string IPAddress { get; set; }
        public int Port { get; set; }        
    }    
}
