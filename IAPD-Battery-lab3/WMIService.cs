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

        public ManagementObjectCollection getObject(string from)
        {
            ObjectQuery query = new ObjectQuery("SELECT * FROM " + from);
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
            return searcher.Get();
        }
    }
}
