using System;
using System.Runtime.InteropServices;

namespace OnlyEnergySave.Modules.Power
{
    internal class TurboBoostManager
    {
        static Guid GUID_PROCESSOR_SETTINGS_SUBGROUP = new Guid("54533251-82BE-4824-96C1-47B60B740D00");
        static Guid GUID_PROCESSOR_MAXIMUM_PERFORMANCE = new Guid("BC5038F7-23E0-4960-96DA-33ABAF5935EC");

        [DllImport("powrprof.dll", SetLastError = true)]
        public static extern uint PowerGetActiveScheme(IntPtr UserRootPowerKey, out IntPtr ActivePolicyGuid);

        [DllImport("powrprof.dll", SetLastError = true)]
        public static extern uint PowerSetActiveScheme(IntPtr UserRootPowerKey, ref Guid SchemeGuid);

        [DllImport("powrprof.dll", SetLastError = true)]
        public static extern uint PowerWriteDCValueIndex(
            IntPtr PowerWriteDCValueIndex,
            ref Guid SchemeGuid,
            ref Guid SubGroupOfPowerSettingsGuid,
            ref Guid PowerSettingGuid,
            uint DcValueIndex
            );

        public static void SetCpuLimit(uint percent)
        {
            IntPtr activeSchemePtr;
            PowerGetActiveScheme(IntPtr.Zero, out activeSchemePtr);
            Guid activeScheme = Marshal.PtrToStructure<Guid>(activeSchemePtr);

            // Для роботи від батареї, тобто DC це постійний струм
            PowerWriteDCValueIndex(
                IntPtr.Zero,
                ref activeScheme,
                ref GUID_PROCESSOR_SETTINGS_SUBGROUP,
                ref GUID_PROCESSOR_MAXIMUM_PERFORMANCE,
                percent
            );

            // Активуємо зміни
            PowerSetActiveScheme(IntPtr.Zero, ref activeScheme);

            Marshal.FreeHGlobal(activeSchemePtr);
        }
    }
}