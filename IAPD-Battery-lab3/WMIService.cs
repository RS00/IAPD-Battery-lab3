using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace IAPD_Battery_lab3
{
    class WMIService
    {
        private ManagementScope scope;

        public WMIService()
        {
            connectToWMI();
        }

        private void connectToWMI()
        {
            ManagementScope scope = new ManagementScope("\\root\\cimv2");
            scope.Connect();
        }
        
        public ManagementObjectCollection getMethods(string req)
        {
            ManagementScope scope = new ManagementScope("root\\WMI");
            SelectQuery query = new SelectQuery(req);
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
            return searcher.Get();
        }

        public ManagementObjectCollection getObject(string req)
        {
            ObjectQuery query = new ObjectQuery(req);
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
            return searcher.Get();
        }

    }
}
