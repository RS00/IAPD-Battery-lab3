using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using IAPD_Battery_lab3;

namespace IAPD_Battery_lab3
{
    class Battery
    {
        public string getChargeLevel()
        {
            WMIService wmi = new WMIService();
            ManagementObjectCollection collection = wmi.getObject("Win32_Battery");
            string result = "";
            foreach (ManagementObject obj in collection)
            {
                result += obj["EstimatedChargeRemaining"];
            }
            return result;
        }

        public string getPowerType()
        {
            WMIService wmi = new WMIService();
            ManagementObjectCollection collection = wmi.getObject("Win32_Battery");
            string result = "";
            foreach (ManagementObject obj in collection)
            {
                result += obj["BatteryStatus"];
            }
            if (result == "1" || result == "3" || result == "4" || result == "5" || result == "11")
                return "Battery";
            if (result == "2" || result == "6" || result == "7" || result == "8" || result == "9")
                return "AC";
            else
                return "Unknown";
        }
    }
}
