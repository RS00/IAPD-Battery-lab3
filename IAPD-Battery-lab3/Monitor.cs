using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace IAPD_Battery_lab3
{
    class Monitor
    {
        public static void SetBrightness(byte targetBrightness)
        {
            WMIService wmi = new WMIService();
            ManagementObjectCollection objectCollection = wmi.getMethods("WmiMonitorBrightnessMethods");
            foreach (ManagementObject mObj in objectCollection)
            {
                mObj.InvokeMethod("WmiSetBrightness",
                    new Object[] { UInt32.MaxValue, targetBrightness });
                break;
            }
            
        }
    }
}
