using System;
using System.Management;

namespace OnlyEnergySave.Modules.Power
{
    public class BatteryStatus
    {
        static public double VoltageBattery()
        {
            double voltageBattery = 0;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Battery");
            foreach (var obj in searcher.Get())
                voltageBattery = Convert.ToDouble(obj["DesignVoltage"]) / 1000.0;
            return voltageBattery;
        }

        static public int PercentBattery()
        {
            int percentBattery = 1;



            return percentBattery;
        }
    }
}