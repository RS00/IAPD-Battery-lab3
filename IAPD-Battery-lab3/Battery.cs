using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Runtime.InteropServices;
using IAPD_Battery_lab3;

namespace IAPD_Battery_lab3
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct GLOBAL_POWER_POLICY
    {
        public GLOBAL_USER_POWER_POLICY UserPolicy;
        public GLOBAL_MACHINE_POWER_POLICY MachinePolicy;
    }

    [Flags]
    enum GlobalPowerPolicyFlags : uint
    {
        EnableSysTrayBatteryMeter = 0x01,
        EnableMultiBatteryDisplay = 0x02,
        EnablePasswordAtLogon = 0x04,
        EnableWakeOnRing = 0x08,
        EnableVideoDimDisplay = 0x10,
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct GLOBAL_USER_POWER_POLICY
    {
        public const int NUM_DISCHARGE_POLICIES = 4;

        public uint Revision;
        public POWER_ACTION_POLICY PowerButtonAc;
        public POWER_ACTION_POLICY PowerButtonDc;
        public POWER_ACTION_POLICY SleepButtonAc;
        public POWER_ACTION_POLICY SleepButtonDc;
        public POWER_ACTION_POLICY LidCloseAc;
        public POWER_ACTION_POLICY LidCloseDc;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = NUM_DISCHARGE_POLICIES)]
        public SYSTEM_POWER_LEVEL[] DischargePolicy;
        public GlobalPowerPolicyFlags GlobalFlags;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct GLOBAL_MACHINE_POWER_POLICY
    {
        public uint Revision;
        public SYSTEM_POWER_STATE LidOpenWakeAc;
        public SYSTEM_POWER_STATE LidOpenWakeDc;
        public uint BroadcastCapacityResolution;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct SYSTEM_POWER_LEVEL
    {
        public bool Enable;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public byte[] Spare;
        public uint BatteryLevel;
        public POWER_ACTION_POLICY PowerPolicy;
        public SYSTEM_POWER_STATE MinSystemState;
    }

    enum SYSTEM_POWER_STATE
    {
        PowerSystemUnspecified = 0,
        PowerSystemWorking = 1,
        PowerSystemSleeping1 = 2,
        PowerSystemSleeping2 = 3,
        PowerSystemSleeping3 = 4,
        PowerSystemHibernate = 5,
        PowerSystemShutdown = 6,
        PowerSystemMaximum = 7
    }

    enum POWER_ACTION : uint
    {
        PowerActionNone = 0,       // No system power action. 
        PowerActionReserved,       // Reserved; do not use. 
        PowerActionSleep,      // Sleep. 
        PowerActionHibernate,      // Hibernate. 
        PowerActionShutdown,       // Shutdown. 
        PowerActionShutdownReset,  // Shutdown and reset. 
        PowerActionShutdownOff,    // Shutdown and power off. 
        PowerActionWarmEject,      // Warm eject.
    }

    [Flags]
    enum PowerActionFlags : uint
    {
        POWER_ACTION_QUERY_ALLOWED = 0x00000001, // Broadcasts a PBT_APMQUERYSUSPEND event to each application to request permission to suspend operation.
        POWER_ACTION_UI_ALLOWED = 0x00000002, // Applications can prompt the user for directions on how to prepare for suspension. Sets bit 0 in the Flags parameter passed in the lParam parameter of WM_POWERBROADCAST.
        POWER_ACTION_OVERRIDE_APPS = 0x00000004, // Ignores applications that do not respond to the PBT_APMQUERYSUSPEND event broadcast in the WM_POWERBROADCAST message.
        POWER_ACTION_LIGHTEST_FIRST = 0x10000000, // Uses the first lightest available sleep state.
        POWER_ACTION_LOCK_CONSOLE = 0x20000000, // Requires entry of the system password upon resume from one of the system standby states.
        POWER_ACTION_DISABLE_WAKES = 0x40000000, // Disables all wake events.
        POWER_ACTION_CRITICAL = 0x80000000, // Forces a critical suspension.
    }

    [Flags]
    enum PowerActionEventCode : uint
    {
        POWER_LEVEL_USER_NOTIFY_TEXT = 0x00000001, // User notified using the UI. 
        POWER_LEVEL_USER_NOTIFY_SOUND = 0x00000002, // User notified using sound. 
        POWER_LEVEL_USER_NOTIFY_EXEC = 0x00000004, // Specifies a program to be executed. 
        POWER_USER_NOTIFY_BUTTON = 0x00000008, // Indicates that the power action is in response to a user power button press. 
        POWER_USER_NOTIFY_SHUTDOWN = 0x00000010, // Indicates a power action of shutdown/off.
        POWER_FORCE_TRIGGER_RESET = 0x80000000, // Clears a user power button press. 
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct POWER_ACTION_POLICY
    {
        public POWER_ACTION Action;
        public PowerActionFlags Flags;
        public PowerActionEventCode EventCode;
    }

    struct POWER_POLICY
    {
        public USER_POWER_POLICY user;
        public MACHINE_POWER_POLICY mach;
    }

    struct MACHINE_POWER_POLICY
    {
        uint Revision;
        SYSTEM_POWER_STATE MinSleepAc;
        SYSTEM_POWER_STATE MinSleepDc;
        SYSTEM_POWER_STATE ReducedLatencySleepAc;
        SYSTEM_POWER_STATE ReducedLatencySleepDc;
        uint DozeTimeoutAc;
        uint DozeTimeoutDc;
        uint DozeS4TimeoutAc;
        uint DozeS4TimeoutDc;
        byte MinThrottleAc;
        byte MinThrottleDc;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        byte[] pad1;
        POWER_ACTION_POLICY OverThrottledAc;
        POWER_ACTION_POLICY OverThrottledDc;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    struct USER_POWER_POLICY
    {
        public uint Revision;
        public POWER_ACTION_POLICY IdleAc;
        public POWER_ACTION_POLICY IdleDc;
        public uint IdleTimeoutAc;
        public uint IdleTimeoutDc;
        public byte IdleSensitivityAc;
        public byte IdleSensitivityDc;
        public byte ThrottlePolicyAc;
        public byte ThrottlePolicyDc;
        public SYSTEM_POWER_STATE MaxSleepAc;
        public SYSTEM_POWER_STATE MaxSleepDc;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public uint[] Reserved;
        public uint VideoTimeoutAc;
        public uint VideoTimeoutDc;
        public uint SpindownTimeoutAc;
        public uint SpindownTimeoutDc;
        [MarshalAs(UnmanagedType.I1)]
        public bool OptimizeForPowerAc;
        [MarshalAs(UnmanagedType.I1)]
        public bool OptimizeForPowerDc;
        public byte FanThrottleToleranceAc;
        public byte FanThrottleToleranceDc;
        public byte ForcedThrottleAc;
        public byte ForcedThrottleDc;
    }

    struct SYSTEM_BATTERY_STATE
    {
        [MarshalAs(UnmanagedType.I1)]
        public bool AcOnLine;
        [MarshalAs(UnmanagedType.I1)]
        public bool BatteryPresent;
        [MarshalAs(UnmanagedType.I1)]
        public bool Charging;
        [MarshalAs(UnmanagedType.I1)]
        public bool Discharging;
        public byte Spare0;
        public byte Spare1;
        public byte Spare2;
        public byte Spare3;
        public uint MaxCapacity;
        public uint RemainingCapacity;
        public int Rate;
        public uint EstimatedTime;
        public uint DefaultAlert1;
        public uint DefaultAlert2;
    }

    enum InformationLevel
    {
        AdministratorPowerPolicy = 9,
        LastSleepTime = 15,
        LastWakeTime = 14,
        ProcessorInformation = 11,
        ProcessorPowerPolicyAc = 18,
        ProcessorPowerPolicyCurrent = 22,
        ProcessorPowerPolicyDc = 19,
        SystemBatteryState = 5,
        SystemExecutionState = 16,
        SystemPowerCapabilities = 4,
        SystemPowerInformation = 12,
        SystemPowerPolicyAc = 0,
        SystemPowerPolicyCurrent = 8,
        SystemPowerPolicyDc = 1,
        SystemReserveHiberFile = 10,
        VerifyProcessorPowerPolicyAc = 20,
        VerifyProcessorPowerPolicyDc = 21,
        VerifySystemPolicyAc = 2,
        VerifySystemPolicyDc = 3
    };


    class Battery
    {

        [DllImport("powrprof.dll", SetLastError = true)]
        private static extern UInt32 CallNtPowerInformation(
        InformationLevel InformationLevel,
        IntPtr lpInputBuffer,
        UInt32 nInputBufferSize,
        IntPtr lpOutputBuffer,
        UInt32 nOutputBufferSize
        );
   

        private static SYSTEM_BATTERY_STATE getSystemBatteryStateStruct()
        {
            IntPtr batteryState = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(SYSTEM_BATTERY_STATE)));
            uint status = CallNtPowerInformation(InformationLevel.SystemBatteryState, IntPtr.Zero, 0, batteryState, (uint)Marshal.SizeOf(typeof(SYSTEM_BATTERY_STATE)));
            return (SYSTEM_BATTERY_STATE)Marshal.PtrToStructure(batteryState, typeof(SYSTEM_BATTERY_STATE));
        }

        public static string getChargeLevel()
        {
            /*WMIService wmi = new WMIService();
            ManagementObjectCollection collection = wmi.getObject("SELECT * FROM Win32_Battery");
            string result = "";
            foreach (ManagementObject obj in collection)
            {
                result += obj["EstimatedChargeRemaining"];
            }
            return result;*/
            SYSTEM_BATTERY_STATE batteryState = getSystemBatteryStateStruct();
            double result = batteryState.RemainingCapacity * 100 / batteryState.MaxCapacity;
            return result.ToString();

        }

        public static string getTime()
        {
            if (getPowerType() == "AC")
                return "-";
            WMIService wmi = new WMIService();
            ManagementObjectCollection collection = wmi.getObject("SELECT * FROM Win32_Battery");
            string result = "";
            foreach (ManagementObject obj in collection)
            {
                result += obj["EstimatedRunTime"];
            }
            return TimeSpan.FromMinutes(Double.Parse(result)).ToString(@"hh\:mm");
            /*SYSTEM_BATTERY_STATE batteryState = getSystemBatteryStateStruct();
            double result = batteryState.EstimatedTime;
            return TimeSpan.FromMinutes(result).ToString(@"hh\:mm"); */
        }

        public static string getPowerType()
        {
            /*WMIService wmi = new WMIService();
            ManagementObjectCollection collection = wmi.getObject("SELECT * FROM Win32_Battery");
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
                return "Unknown";*/
            SYSTEM_BATTERY_STATE batteryState = getSystemBatteryStateStruct();
            if (batteryState.AcOnLine == true)
                return "AC";
            else
                return "Battery";
        }
    }
}
