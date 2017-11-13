using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace BatteryInfo
{
    class WMIService
    {
        private ManagementScope scope;

        public WMIService()
        {
            ConnectToWMI();
        }

        private void ConnectToWMI()
        {
            ManagementScope scope = new ManagementScope("\\root\\cimv2");
            scope.Connect();
        }
        /*
        
        public ManagementObjectCollection getMethods(string req)
        {
            ManagementScope scope = new ManagementScope("root\\WMI");
            SelectQuery query = new SelectQuery(req);
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
            return searcher.Get();
        }
        */
        public ManagementObjectCollection GetObject(string req)
        {
            ObjectQuery query = new ObjectQuery(req);
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
            return searcher.Get();
        }

    }
}
