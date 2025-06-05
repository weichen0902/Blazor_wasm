using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor_wasm.Models.ModbusModels
{
    public class DevicesDataModelDTO
    {
        public double[] hbmpH { get; set; } = new double[2];
        public double[] hbmElec { get; set; } = new double[2];
        public double[] hbmTemp { get; set; } = new double[2];
        public double[] hbmZero { get; set; } = new double[2];
        public double[] hbmSlope { get; set; } = new double[2];
        public ushort[] hbmHealth { get; set; } = new ushort[2];

        public ushort[] washingStepSetting { get; set; } = new ushort[12];
        public ushort[] washingStepTimeSetting { get; set; } = new ushort[12];
        public ushort[] calStepSetting { get; set; } = new ushort[24];
        public ushort[] calStepTimeSetting { get; set; } = new ushort[24];

        public ushort[] totalNumberOfSteps { get; set; } = new ushort[2];
        public ushort[] totalCycleOfSteps { get; set; } = new ushort[2];
        public ushort[] totalCurrentCycleOfSteps { get; set; } = new ushort[2];
        public ushort[] currentStep { get; set; } = new ushort[2];

        public ushort[] countdown { get; set; } = new ushort[2];
        public ushort[] countdownAdd { get; set; } = new ushort[2];
        public ushort[] preMotionCountdown { get; set; } = new ushort[1];

        public ushort[] hbmRunState { get; set; } = new ushort[2];
        public ushort[] hbmFailRetryCount { get; set; } = new ushort[2];
        public ushort[] hbmStbyLiftTimeSetting { get; set; } = new ushort[2];
        public ushort[] hoisterDownCycleSettingCount { get; set; } = new ushort[2];
        public ushort[] hoisterDownCycleCount { get; set; } = new ushort[2];
        public ushort[] hoisterDownSpecifiedCountToSlowDown { get; set; } = new ushort[2];
        public ushort[] hoisterLimitTriggerCount { get; set; } = new ushort[2];

        public ushort[] manualControl { get; set; } = new ushort[2];
        public ushort[] valveState { get; set; } = new ushort[2];

        public ushort[] driverAlarm { get; set; } = new ushort[2];
        public ushort[] systemAlarm1 { get; set; } = new ushort[2];
        public ushort[] commonAlarm { get; set; } = new ushort[1];
       
        public ushort[] systemStatus { get; set; } = new ushort[1];
        public ushort[] deviceStatus { get; set; } = new ushort[2];
    }
}
