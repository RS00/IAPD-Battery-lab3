using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace IAPD_Battery_lab3
{
    class PowerManagement
    {
        [DllImport("powrprof.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetCurrentPowerPolicies(ref GLOBAL_POWER_POLICY pGlobalPowerPolicy,
             ref POWER_POLICY pPowerPolicy);

        [DllImport("powrprof.dll", SetLastError = true)]
        private static extern bool SetActivePwrScheme(uint uiID, IntPtr lpGlobalPowerPolicy, ref POWER_POLICY lpPowerPolicy);

        [DllImport("powrprof.dll", SetLastError = true)]
        private static extern bool GetActivePwrScheme(out uint puiID);

        public static bool setNewVideoTimeoutDC(uint sec)
        {
            uint id;
            GetActivePwrScheme(out id);
            GLOBAL_POWER_POLICY gpp = new GLOBAL_POWER_POLICY();
            POWER_POLICY pp = new POWER_POLICY();
            bool result = GetCurrentPowerPolicies(ref gpp, ref pp);
            pp.user.VideoTimeoutDc = sec;
            return SetActivePwrScheme(id, IntPtr.Zero, ref pp);
        }

        public static uint getVideoTimeoutDC()
        {
            uint id;
            GetActivePwrScheme(out id);
            GLOBAL_POWER_POLICY gpp = new GLOBAL_POWER_POLICY();
            POWER_POLICY pp = new POWER_POLICY();
            bool result = GetCurrentPowerPolicies(ref gpp, ref pp);
            return pp.user.VideoTimeoutDc;
        }
    }
}
