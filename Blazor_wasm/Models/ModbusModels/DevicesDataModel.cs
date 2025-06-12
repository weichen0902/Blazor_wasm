using Blazor_wasm.Controller;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor_wasm.Models.ModbusModels
{
    public class DevicesDataModel
    {
        public void NotifyStateChanged() => OnStateChange?.Invoke();
        public event Action? OnStateChange;
        public void NotifyCalStateChanged() => OnCalStateChange?.Invoke();
        public event Action? OnCalStateChange;
        public void NotifyAlarmStateChanged(bool f) => OnAlarmStateChange?.Invoke(f);
        public event Action<bool>? OnAlarmStateChange;

        public bool boolStateChanged, boolCalStateChanged, boolAlarmStateChanged;

        private double[] hbmpH = new double[2];
        private double[] hbmElec = new double[2];
        private double[] hbmTemp = new double[2];
        private double[] hbmZero = new double[2];
        private double[] hbmSlope = new double[2];
        private ushort[] hbmHealth = new ushort[2];

        private ushort[] washingStepSetting = new ushort[12];
        private ushort[] washingStepTimeSetting = new ushort[12];
        private ushort[] calStepSetting = new ushort[12];
        private ushort[] calStepTimeSetting = new ushort[12];

        private ushort[] totalNumberOfSteps = new ushort[2];
        private ushort[] totalCycleOfSteps = new ushort[2];
        private ushort[] totalCurrentCycleOfSteps = new ushort[2];
        private ushort[] currentStep = new ushort[2];

        private ushort[] countdown = new ushort[2];
        private ushort[] countdownAdd = new ushort[2];
        private ushort[] preMotionCountdown = new ushort[1];

        private ushort[] hbmRunState = new ushort[2];
        private ushort[] hbmFailRetryCount = new ushort[2];
        private ushort[] hbmStbyLiftTimeSetting = new ushort[2];
        private ushort[] hoisterDownCycleSettingCount = new ushort[2];
        private ushort[] hoisterDownCycleCount = new ushort[2];
        private ushort[] hoisterDownSpecifiedCountToSlowDown = new ushort[2];
        private ushort[] hoisterLimitTriggerCount = new ushort[2];

        private ushort[] valveState = new ushort[2];

        private ushort[] driverAlarm = new ushort[2];
        private ushort[] systemAlarm1 = new ushort[2];
        private ushort[] commonAlarm = new ushort[1];

        private ushort[] systemStatus = new ushort[1];
        private ushort[] deviceStatus = new ushort[2];

        public void TriggerNotifyChanged()
        {
            if (boolStateChanged)
            {
                NotifyStateChanged();
            }
            boolStateChanged = false;
            if (boolCalStateChanged)
            {
                NotifyCalStateChanged();
            }
            boolCalStateChanged = false;
            if (boolAlarmStateChanged)
            {
                NotifyAlarmStateChanged(false);
            }
            boolAlarmStateChanged = false;
        }

        public object this[int index, string arrayType]
        {
            get
            {
                switch (arrayType)
                {
                    case "hbmpH":
                        return hbmpH[index];
                    case "hbmElec":
                        return hbmElec[index];
                    case "hbmTemp":
                        return hbmTemp[index];
                    case "hbmZero":
                        return hbmZero[index];
                    case "hbmSlope":
                        return hbmSlope[index];
                    case "hbmHealth":
                        return hbmHealth[index];
                    case "washingStepSetting":
                        return washingStepSetting[index];
                    case "washingStepTimeSetting":
                        return washingStepTimeSetting[index];
                    case "calStepSetting":
                        return calStepSetting[index];
                    case "calStepTimeSetting":
                        return calStepTimeSetting[index];
                    case "totalNumberOfSteps":
                        return totalNumberOfSteps[index];
                    case "totalCycleOfSteps":
                        return totalCycleOfSteps[index];
                    case "totalCurrentCycleOfSteps":
                        return totalCurrentCycleOfSteps[index];
                    case "currentStep":
                        return currentStep[index];
                    case "countdown":
                        return countdown[index];
                    case "countdownAdd":
                        return countdownAdd[index];
                    case "preMotionCountdown":
                        return preMotionCountdown[index];
                    case "hbmRunState":
                        return hbmRunState[index];
                    case "hbmFailRetryCount":
                        return hbmFailRetryCount[index];
                    case "hbmStbyLiftTimeSetting":
                        return hbmStbyLiftTimeSetting[index];
                    case "hoisterDownCycleSettingCount":
                        return hoisterDownCycleSettingCount[index];
                    case "hoisterDownCycleCount":
                        return hoisterDownCycleCount[index];
                    case "hoisterDownSpecifiedCountToSlowDown":
                        return hoisterDownSpecifiedCountToSlowDown[index];
                    case "hoisterLimitTriggerCount":
                        return hoisterLimitTriggerCount[index];
                    case "valveState":
                        return valveState[index];
                    case "driverAlarm":
                        return driverAlarm[index];
                    case "systemAlarm1":
                        return systemAlarm1[index];
                    case "commonAlarm":
                        return commonAlarm[index];
                    case "systemStatus":
                        return systemStatus[index];
                    case "deviceStatus":
                        return deviceStatus[index];
                    default:
                        throw new ArgumentException("Invalid array type.");
                }
            }
            set
            {
                if (value is double || value is ushort)
                {
                    switch (arrayType)
                    {
                        case "hbmpH":
                            if (hbmpH[index] != (double)value)
                            {
                                hbmpH[index] = (double)value;
                                boolStateChanged = true;
                            }
                            break;
                        case "hbmElec":
                            if (hbmElec[index] != (double)value)
                            {
                                hbmElec[index] = (double)value;                               
                            }
                            break;
                        case "hbmTemp":
                            if (hbmTemp[index] != (double)value)
                            {
                                hbmTemp[index] = (double)value;                                
                            }
                            break;
                        case "hbmZero":
                            if (hbmZero[index] != (double)value && (double)value != 0)
                            {
                                hbmZero[index] = (double)value;                                
                            }
                            break;
                        case "hbmSlope":
                            if (hbmSlope[index] != (double)value && (double)value != 0)
                            {
                                hbmSlope[index] = (double)value;
                                boolCalStateChanged = true;
                            }
                            break;
                        case "hbmHealth":
                            if (hbmHealth[index] != (ushort)value)
                            {
                                hbmHealth[index] = (ushort)value;                               
                            }
                            break;
                        case "washingStepSetting":
                            if (washingStepSetting[index] != (ushort)value)
                            {
                                washingStepSetting[index] = (ushort)value;
                                boolStateChanged = true;
                            }
                            break;
                        case "washingStepTimeSetting":
                            if (washingStepTimeSetting[index] != (ushort)value)
                            {
                                washingStepTimeSetting[index] = Convert.ToUInt16((ushort)value/10);                               
                                boolStateChanged = true;
                            }
                            break;
                        case "calStepSetting":
                            if (calStepSetting[index] != (ushort)value)
                            {
                                calStepSetting[index] = (ushort)value;
                                boolStateChanged = true;
                            }
                            break;
                        case "calStepTimeSetting":
                            if (calStepTimeSetting[index] != (ushort)value)
                            {
                                calStepTimeSetting[index] = Convert.ToUInt16((ushort)value / 10);
                                boolStateChanged = true;
                            }
                            break;
                        case "totalNumberOfSteps":
                            if (totalNumberOfSteps[index] != (ushort)value)
                            {
                                totalNumberOfSteps[index] = (ushort)value;
                            }
                            break;
                        case "totalCycleOfSteps":
                            if (totalCycleOfSteps[index] != (ushort)value)
                            {
                                totalCycleOfSteps[index] = (ushort)value;
                            }
                            break;
                        case "totalCurrentCycleOfSteps":
                            if (totalCurrentCycleOfSteps[index] != (ushort)value)
                            {
                                totalCurrentCycleOfSteps[index] = (ushort)value;
                            }
                            break;
                        case "currentStep":
                            if (currentStep[index] != (ushort)value)
                            {
                                currentStep[index] = (ushort)value;
                            }
                            break;
                        case "countdown":
                            if (countdown[index] != (ushort)value)
                            {
                                countdown[index] = (ushort)value;
                            }
                            break;
                        case "countdownAdd":
                            if (countdownAdd[index] != (ushort)value)
                            {
                                countdownAdd[index] = (ushort)value;
                            }
                            break;
                        case "preMotionCountdown":
                            if (preMotionCountdown[index] != (ushort)value)
                            {
                                preMotionCountdown[index] = (ushort)value;
                            }
                            break;
                        case "hbmRunState":
                            if (hbmRunState[index] != (ushort)value)
                            {
                                hbmRunState[index] = (ushort)value;
                                boolStateChanged = true;
                            }
                            break;
                        case "hbmFailRetryCount":
                            if (hbmFailRetryCount[index] != (ushort)value)
                            {
                                hbmFailRetryCount[index] = (ushort)value;
                            }
                            break;
                        case "hbmStbyLiftTimeSetting":
                            if (hbmStbyLiftTimeSetting[index] != (ushort)value)
                            {
                                hbmStbyLiftTimeSetting[index] = (ushort)value;
                            }
                            break;
                        case "hoisterDownCycleSettingCount":
                            if (hoisterDownCycleSettingCount[index] != (ushort)value)
                            {
                                hoisterDownCycleSettingCount[index] = (ushort)value;
                                boolStateChanged = true;
                            }
                            break;
                        case "hoisterDownCycleCount":
                            if (hoisterDownCycleCount[index] != (ushort)value)
                            {
                                hoisterDownCycleCount[index] = (ushort)value;
                                boolStateChanged = true;
                            }
                            break;
                        case "hoisterDownSpecifiedCountToSlowDown":
                            if (hoisterDownSpecifiedCountToSlowDown[index] != (ushort)value)
                            {
                                hoisterDownSpecifiedCountToSlowDown[index] = (ushort)value;
                            }
                            break;
                        case "hoisterLimitTriggerCount":
                            if (hoisterLimitTriggerCount[index] != (ushort)value)
                            {
                                hoisterLimitTriggerCount[index] = (ushort)value;
                            }
                            break;
                        case "valveState":
                            if (valveState[index] != (ushort)value)
                            {
                                valveState[index] = (ushort)value;
                                boolStateChanged = true;
                            }                          
                            break;
                        case "driverAlarm":
                            if (driverAlarm[index] != (ushort)value)
                            {
                                driverAlarm[index] = (ushort)value;
                                boolAlarmStateChanged = true;
                            }
                            break;
                        case "systemAlarm1":
                            if (systemAlarm1[index] != (ushort)value)
                            {
                                systemAlarm1[index] = (ushort)value;
                                boolAlarmStateChanged = true;
                            }
                            break;
                        case "commonAlarm":
                            if (commonAlarm[index] != (ushort)value)
                            {
                                commonAlarm[index] = (ushort)value;
                                boolAlarmStateChanged = true;
                            }
                            break;
                        case "systemStatus":
                            if (systemStatus[index] != (ushort)value)
                            {
                                systemStatus[index] = (ushort)value;
                                boolStateChanged = true;
                            }
                            break;
                        case "deviceStatus":
                            if (deviceStatus[index] != (ushort)value)
                            {
                                deviceStatus[index] = (ushort)value;
                                boolStateChanged = true;
                            }
                            break;
                        default:
                            throw new ArgumentException("Invalid array type.");
                    }
                }
            }
        }
    }
}
