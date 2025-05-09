using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazor_wasm.Models.ModbusModels;
using Blazor_wasm.Pages;
using Blazor_wasm.Pages.Components.Csharp;

namespace Blazor_wasm.Shared
{
    public partial class MainLayout
    {
        private Timer dataTimer;
        public static string[] hbmWashingMotionControl = new string[2];
        public static string[] hbmCalMotionControl = new string[2];

        public static bool[] systemPreWash = new bool[2];
        public static bool[] systemPreCal = new bool[2];

        public static string[] electrodeReturnUp = new string[2];
        public static string[] electrodeReturnSpecifyLocation = new string[2];
        public static string[] electrodeManualInchUp = new string[2];
        public static string[] electrodeManualInchDown = new string[2];
        public static string[] electrodeStandbyPointSetting = new string[2];

        public static string[] washingValve = new string[2];
        public static string[] reagValve = new string[2];
        public static string[] bufAValve = new string[2];
        public static string[] bufBValve = new string[2];
        public static string[] airPressureValve = new string[2];

        public static string[] systemStatus = new string[1];
        public static string[] deviceStatus = new string[2];

        ushort[] serverSystemAlarm1ErrorCode = new ushort[2];
        char[] serverSystemAlarm1ErrorCodeBinaryCharArray = new char[16];

        ushort[] clientSystemAlarm1ErrorCode = new ushort[2];
        char[] clientSystemAlarm1ErrorCodeBinaryCharArray = new char[16];

        ushort[] serverDriverAlarmErrorCode = new ushort[2];
        ushort[] clientDriverAlarmErrorCode = new ushort[2];

        ushort[] serverCommonAlarmErrorCode = new ushort[1];
        char[] serverCommonAlarmErrorCodeBinaryCharArray = new char[16];

        ushort[] clientCommonAlarmErrorCode = new ushort[1];
        char[] clientCommonAlarmErrorCodeBinaryCharArray = new char[16];

        public static char[] systemStatusIntBinaryCharArray = new char[16];

        public static char[] d1DeviceStatusIntBinaryCharArray = new char[16];

        public static char[] d2DeviceStatusIntBinaryCharArray = new char[16];

        public static char[] d1ElectrodeCommandBinaryCharArray = new char[16];

        public static char[] d2ElectrodeCommandBinaryCharArray = new char[16];

        public static char[] d1valveStateBinaryCharArray = new char[16];

        public static char[] d2valveStateBinaryCharArray = new char[16];


        public static DevicesDataModelDTO response;

        private void UpdateState()
        {
            dataTimer = new Timer(new TimerCallback(async (s) =>
            {
                response = await dataService.GetModbusDevicesData();
                await InvokeAsync(async () =>
                {
                    for (int i = 0; i < 2; i++)
                    {
                        if ((double)devicesDataModel[i, "hbmSlope"] != response.hbmSlope[i])
                        {
                            getAllCalData.UpdateData();
                            StateHasChanged();
                        }

                        await Task.Delay(2000);

                        devicesDataModel[i, "hbmpH"] = response.hbmpH[i];
                        if (i == 0)
                            d1pH = (double)devicesDataModel[i, "hbmpH"];
                        else if (i == 1)
                            d2pH = (double)devicesDataModel[i, "hbmpH"];

                        devicesDataModel[i, "hbmElec"] = response.hbmElec[i];
                        devicesDataModel[i, "hbmTemp"] = response.hbmTemp[i];
                        devicesDataModel[i, "hbmZero"] = response.hbmZero[i];
                        devicesDataModel[i, "hbmSlope"] = response.hbmSlope[i];
                        devicesDataModel[i, "hbmHealth"] = response.hbmHealth[i];

                        devicesDataModel[i, "systemAlarm1"] = response.systemAlarm1[i];
                        devicesDataModel[i, "driverAlarm"] = response.driverAlarm[i];

                        devicesDataModel[i, "totalNumberOfSteps"] = response.totalNumberOfSteps[i];
                        devicesDataModel[i, "totalCycleOfSteps"] = response.totalCycleOfSteps[i];
                        devicesDataModel[i, "totalCurrentCycleOfSteps"] = response.totalCurrentCycleOfSteps[i];
                        devicesDataModel[i, "currentStep"] = response.currentStep[i];
                        devicesDataModel[i, "countdown"] = response.countdown[i];
                        devicesDataModel[i, "countdownAdd"] = response.countdownAdd[i];

                        devicesDataModel[i, "motionControl"] = response.motionControl[i];
                        devicesDataModel[i, "hbmRunState"] = response.hbmRunState[i];
                        devicesDataModel[i, "hbmFailRetryCount"] = response.hbmFailRetryCount[i];
                        devicesDataModel[i, "hbmStbyLiftTimeSetting"] = response.hbmStbyLiftTimeSetting[i];
                        devicesDataModel[i, "hoisterDownCycleSettingCount"] = response.hoisterDownCycleSettingCount[i];
                        devicesDataModel[i, "hoisterDownCycleCount"] = response.hoisterDownCycleCount[i];
                        devicesDataModel[i, "hoisterDownSpecifiedCountToSlowDown"] = response.hoisterDownSpecifiedCountToSlowDown[i];
                        devicesDataModel[i, "hoisterLimitTriggerCount"] = response.hoisterLimitTriggerCount[i];
                        devicesDataModel[i, "hoisterResetButtonPressCount"] = response.hoisterResetButtonPressCount[i];

                        devicesDataModel[i, "electrodeCommand"] = response.electrodeCommand[i];
                        devicesDataModel[i, "valveState"] = response.valveState[i];

                        devicesDataModel[i, "deviceStatus"] = response.deviceStatus[i];

                        var serverSystemAlarm1ErrorCodeBinary = Convert.ToString(serverSystemAlarm1ErrorCode[i], 2);
                        var _serverSystemAlarm1ErrorCodeBinaryCharArray = serverSystemAlarm1ErrorCodeBinary.ToCharArray();
                        Array.Reverse(_serverSystemAlarm1ErrorCodeBinaryCharArray);

                        var clientSystemAlarm1ErrorCodeBinary = Convert.ToString(clientSystemAlarm1ErrorCode[i], 2);
                        var _clientSystemAlarm1ErrorCodeBinaryCharArray = clientSystemAlarm1ErrorCodeBinary.ToCharArray();
                        Array.Reverse(_clientSystemAlarm1ErrorCodeBinaryCharArray);

                        if (i == 0)
                        {
                            devicesDataModel[i, "preMotionCountdown"] = response.preMotionCountdown[i];
                            devicesDataModel[i, "motorState"] = response.motorState[i];
                            devicesDataModel[i, "systemStatus"] = response.systemStatus[i];
                            devicesDataModel[i, "commonAlarm"] = response.commonAlarm[i];
                            serverCommonAlarmErrorCode[i] = response.commonAlarm[i];

                            var systemStatusInt = Convert.ToString((ushort)devicesDataModel[i, "systemStatus"], 2);
                            var _systemStatusIntBinaryCharArray = systemStatusInt.ToCharArray();
                            Array.Reverse(_systemStatusIntBinaryCharArray);

                            var d1DeviceStatusInt = Convert.ToString((ushort)devicesDataModel[i, "deviceStatus"], 2);
                            var _d1DeviceStatusIntBinaryCharArray = d1DeviceStatusInt.ToCharArray();
                            Array.Reverse(_d1DeviceStatusIntBinaryCharArray);

                            var d1ElectrodeCommandBinary = Convert.ToString((ushort)devicesDataModel[i, "electrodeCommand"], 2);
                            var _d1ElectrodeCommandBinaryCharArray = d1ElectrodeCommandBinary.ToCharArray();
                            Array.Reverse(_d1ElectrodeCommandBinaryCharArray);
                           
                            var d1valveStateBinary = Convert.ToString((ushort)devicesDataModel[i, "valveState"], 2);
                            var _d1valveStateBinaryCharArray = d1valveStateBinary.ToCharArray();
                            Array.Reverse(_d1valveStateBinaryCharArray);
                            
                            var serverCommonAlarmErrorCodeBinary = Convert.ToString(serverCommonAlarmErrorCode[i], 2);
                            var _serverCommonAlarmErrorCodeBinaryCharArray = serverCommonAlarmErrorCodeBinary.ToCharArray();
                            Array.Reverse(_serverCommonAlarmErrorCodeBinaryCharArray);

                            var clientCommonAlarmErrorCodeBinary = Convert.ToString(clientCommonAlarmErrorCode[i], 2);
                            var _clientCommonAlarmErrorCodeBinaryCharArray = clientCommonAlarmErrorCodeBinary.ToCharArray();
                            Array.Reverse(_clientCommonAlarmErrorCodeBinaryCharArray);

                            for (int j = 0; j < _systemStatusIntBinaryCharArray.Length; j++)
                            {
                                for (int k = _systemStatusIntBinaryCharArray.Length; k < 16; k++)
                                {
                                    systemStatusIntBinaryCharArray[k] = '0';
                                }

                                systemStatusIntBinaryCharArray[j] = _systemStatusIntBinaryCharArray[j];
                            }

                            for (int j = 0; j < _d1DeviceStatusIntBinaryCharArray.Length; j++)
                            {
                                for (int k = _d1DeviceStatusIntBinaryCharArray.Length; k < 16; k++)
                                {
                                    d1DeviceStatusIntBinaryCharArray[k] = '0';
                                }

                                d1DeviceStatusIntBinaryCharArray[j] = _d1DeviceStatusIntBinaryCharArray[j];
                            }

                            for (int j = 0; j < _d1ElectrodeCommandBinaryCharArray.Length; j++)
                            {
                                for (int k = _d1ElectrodeCommandBinaryCharArray.Length; k < 16; k++)
                                {
                                    d1ElectrodeCommandBinaryCharArray[k] = '0';
                                }

                                d1ElectrodeCommandBinaryCharArray[j] = _d1ElectrodeCommandBinaryCharArray[j];
                            }

                            for (int j = 0; j < _d1valveStateBinaryCharArray.Length; j++)
                            {
                                for (int k = _d1valveStateBinaryCharArray.Length; k < 16; k++)
                                {
                                    d1valveStateBinaryCharArray[k] = '0';
                                }

                                d1valveStateBinaryCharArray[j] = _d1valveStateBinaryCharArray[j];
                            }

                            if (Pages.Index.waitFeedback == false)
                            {
                                systemStatus[i] = d1DeviceStatusIntBinaryCharArray[0] == '1' ? "red" : "blue";

                                hbmWashingMotionControl[i] = d1DeviceStatusIntBinaryCharArray[3] == '1' ? "yellow" : null;
                                hbmCalMotionControl[i] = d1DeviceStatusIntBinaryCharArray[4] == '1' ? "yellow" : null;

                                electrodeReturnUp[i] = d1ElectrodeCommandBinaryCharArray[0] == '1' ? "yellow" : null;
                                electrodeReturnSpecifyLocation[i] = d1ElectrodeCommandBinaryCharArray[1] == '1' ? "yellow" : null;
                                electrodeStandbyPointSetting[i] = d1ElectrodeCommandBinaryCharArray[5] == '1' ? "yellow" : null;
                           
                                if (d1valveStateBinaryCharArray[0] == '1')
                                {
                                    washingValve[i] = "yellow";
                                    Pages.Index.d1ToggleInput[0] = true;
                                }
                                else if (d1valveStateBinaryCharArray[0] == '0')
                                {
                                    washingValve[i] = null;
                                    Pages.Index.d1ToggleInput[0] = false;
                                }

                                if (d1valveStateBinaryCharArray[1] == '1')
                                {
                                    reagValve[i] = "yellow";
                                    Pages.Index.d1ToggleInput[1] = true;
                                }
                                else if (d1valveStateBinaryCharArray[1] == '0')
                                {
                                    reagValve[i] = null;
                                    Pages.Index.d1ToggleInput[1] = false;
                                }

                                if (d1valveStateBinaryCharArray[2] == '1')
                                {
                                    bufAValve[i] = "yellow";
                                    Pages.Index.d1ToggleInput[2] = true;
                                }
                                else if (d1valveStateBinaryCharArray[2] == '0')
                                {
                                    bufAValve[i] = null;
                                    Pages.Index.d1ToggleInput[2] = false;
                                }

                                if (d1valveStateBinaryCharArray[3] == '1')
                                {
                                    bufBValve[i] = "yellow";
                                    Pages.Index.d1ToggleInput[3] = true;
                                }
                                else if (d1valveStateBinaryCharArray[3] == '0')
                                {
                                    bufBValve[i] = null;
                                    Pages.Index.d1ToggleInput[3] = false;
                                }

                                if (d1valveStateBinaryCharArray[4] == '1')
                                {
                                    airPressureValve[i] = "yellow";
                                    Pages.Index.d1ToggleInput[4] = true;
                                }
                                else if (d1valveStateBinaryCharArray[4] == '0')
                                {
                                    airPressureValve[i] = null;
                                    Pages.Index.d1ToggleInput[4] = false;
                                }                                                                                            
                            }
                                                    
                            if (clientCommonAlarmErrorCode[i] != serverCommonAlarmErrorCode[i])
                            {                             
                                for (int j = 0; j < clientCommonAlarmErrorCodeBinaryCharArray.Length; j++)
                                {
                                    serverCommonAlarmErrorCodeBinaryCharArray[j] = (_serverCommonAlarmErrorCodeBinaryCharArray.Length > j) ? _serverCommonAlarmErrorCodeBinaryCharArray[j] : '0';
                                    clientCommonAlarmErrorCodeBinaryCharArray[j] = (_clientCommonAlarmErrorCodeBinaryCharArray.Length > j) ? _clientCommonAlarmErrorCodeBinaryCharArray[j] : '0';

                                    //if (clientCommonAlarmErrorCodeBinaryCharArray[j] < serverCommonAlarmErrorCodeBinaryCharArray[j])
                                    //    toastService.ShowWarning(AlarmCollection.CollectionOfCommonAlarm[j]);

                                    clientCommonAlarmErrorCodeBinaryCharArray[j] = serverCommonAlarmErrorCodeBinaryCharArray[j];
                                }
                                clientCommonAlarmErrorCode[i] = serverCommonAlarmErrorCode[i];
                            }                           
                        }

                        if (i == 1)
                        {
                            var d2DeviceStatusInt = Convert.ToString((ushort)devicesDataModel[1, "deviceStatus"], 2);
                            var _d2DeviceStatusIntBinaryCharArray = d2DeviceStatusInt.ToCharArray();
                            Array.Reverse(_d2DeviceStatusIntBinaryCharArray);

                            var d2ElectrodeCommandBinary = Convert.ToString((ushort)devicesDataModel[1, "electrodeCommand"], 2);
                            var _d2ElectrodeCommandBinaryCharArray = d2ElectrodeCommandBinary.ToCharArray();
                            Array.Reverse(_d2ElectrodeCommandBinaryCharArray);

                            var d2valveStateBinary = Convert.ToString((ushort)devicesDataModel[1, "valveState"], 2);
                            var _d2valveStateBinaryCharArray = d2valveStateBinary.ToCharArray();
                            Array.Reverse(_d2valveStateBinaryCharArray);
                           
                            for (int j = 0; j < _d2DeviceStatusIntBinaryCharArray.Length; j++)
                            {
                                for (int k = _d2DeviceStatusIntBinaryCharArray.Length; k < 16; k++)
                                {
                                    d2DeviceStatusIntBinaryCharArray[k] = '0';
                                }

                                d2DeviceStatusIntBinaryCharArray[j] = _d2DeviceStatusIntBinaryCharArray[j];
                            }

                            for (int j = 0; j < _d2ElectrodeCommandBinaryCharArray.Length; j++)
                            {
                                for (int k = _d2ElectrodeCommandBinaryCharArray.Length; k < 16; k++)
                                {
                                    d2ElectrodeCommandBinaryCharArray[k] = '0';
                                }

                                d2ElectrodeCommandBinaryCharArray[j] = _d2ElectrodeCommandBinaryCharArray[j];
                            }

                            for (int j = 0; j < _d2valveStateBinaryCharArray.Length; j++)
                            {
                                for (int k = _d2valveStateBinaryCharArray.Length; k < 16; k++)
                                {
                                    d2valveStateBinaryCharArray[k] = '0';
                                }

                                d2valveStateBinaryCharArray[j] = _d2valveStateBinaryCharArray[j];
                            }

                            if (Pages.Index.waitFeedback == false)
                            {

                                hbmWashingMotionControl[i] = d2DeviceStatusIntBinaryCharArray[3] == '1' ? "yellow" : null;
                                hbmCalMotionControl[i] = d2DeviceStatusIntBinaryCharArray[4] == '1' ? "yellow" : null;

                                electrodeReturnUp[i] = d2ElectrodeCommandBinaryCharArray[0] == '1' ? "yellow" : null;
                                electrodeReturnSpecifyLocation[i] = d2ElectrodeCommandBinaryCharArray[1] == '1' ? "yellow" : null;
                                electrodeStandbyPointSetting[i] = d2ElectrodeCommandBinaryCharArray[5] == '1' ? "yellow" : null;

                                if (d2valveStateBinaryCharArray[0] == '1')
                                {
                                    washingValve[i] = "yellow";
                                    Pages.Index.d2ToggleInput[0] = true;
                                }
                                else if (d2valveStateBinaryCharArray[0] == '0')
                                {
                                    washingValve[i] = null;
                                    Pages.Index.d2ToggleInput[0] = false;
                                }

                                if (d2valveStateBinaryCharArray[1] == '1')
                                {
                                    reagValve[i] = "yellow";
                                    Pages.Index.d2ToggleInput[1] = true;
                                }
                                else if (d2valveStateBinaryCharArray[1] == '0')
                                {
                                    reagValve[i] = null;
                                    Pages.Index.d2ToggleInput[1] = false;
                                }

                                if (d2valveStateBinaryCharArray[2] == '1')
                                {
                                    bufAValve[i] = "yellow";
                                    Pages.Index.d2ToggleInput[2] = true;
                                }
                                else if (d2valveStateBinaryCharArray[2] == '0')
                                {
                                    bufAValve[i] = null;
                                    Pages.Index.d2ToggleInput[2] = false;
                                }

                                if (d2valveStateBinaryCharArray[3] == '1')
                                {
                                    bufBValve[i] = "yellow";
                                    Pages.Index.d2ToggleInput[3] = true;
                                }
                                else if (d2valveStateBinaryCharArray[3] == '0')
                                {
                                    bufBValve[i] = null;
                                    Pages.Index.d2ToggleInput[3] = false;
                                }

                                if (d2valveStateBinaryCharArray[4] == '1')
                                {
                                    airPressureValve[i] = "yellow";
                                    Pages.Index.d2ToggleInput[4] = true;
                                }
                                else if (d2valveStateBinaryCharArray[4] == '0')
                                {
                                    airPressureValve[i] = null;
                                    Pages.Index.d2ToggleInput[4] = false;
                                }
                            }                           
                        }

                        serverSystemAlarm1ErrorCode[i] = response.systemAlarm1[i];
                   
                        if (clientSystemAlarm1ErrorCode[i] != serverSystemAlarm1ErrorCode[i])
                        {                           
                            for (int j = 0; j < clientSystemAlarm1ErrorCodeBinaryCharArray.Length; j++)
                            {
                                serverSystemAlarm1ErrorCodeBinaryCharArray[j] = (_serverSystemAlarm1ErrorCodeBinaryCharArray.Length > j) ? _serverSystemAlarm1ErrorCodeBinaryCharArray[j] : '0';
                                clientSystemAlarm1ErrorCodeBinaryCharArray[j] = (_clientSystemAlarm1ErrorCodeBinaryCharArray.Length > j) ? _clientSystemAlarm1ErrorCodeBinaryCharArray[j] : '0';

                                //if (clientSystemAlarm1ErrorCodeBinaryCharArray[j] < serverSystemAlarm1ErrorCodeBinaryCharArray[j])
                                //    toastService.ShowWarning($"#{i + 1}_" + AlarmCollection.CollectionOfSystemAlarm1[j]);

                                clientSystemAlarm1ErrorCodeBinaryCharArray[j] = serverSystemAlarm1ErrorCodeBinaryCharArray[j];
                            }
                            clientSystemAlarm1ErrorCode[i] = serverSystemAlarm1ErrorCode[i];
                        }

                        serverDriverAlarmErrorCode[i] = response.driverAlarm[i];

                        //if (clientDriverAlarmErrorCode[i] != serverDriverAlarmErrorCode[i])
                        //    toastService.ShowWarning($"#{i + 1}_" + AlarmCollection.CollectionOfDriverAlarm[serverDriverAlarmErrorCode[i]]);
                    }

                    for (int i = 0; i < 12; i++)
                    {
                        devicesDataModel[i, "washingStepSetting"] = response.washingStepSetting[i];
                        devicesDataModel[i, "washingStepTimeSetting"] = (response.washingStepTimeSetting[i]);
                    }

                    for (int i = 0; i < 24; i++)
                    {
                        devicesDataModel[i, "calStepSetting"] = response.calStepSetting[i];
                        devicesDataModel[i, "calStepTimeSetting"] = (response.calStepTimeSetting[i]);
                    }

                    if (devicesDataModel.boolStateChanged || devicesDataModel.boolCalStateChanged || devicesDataModel.boolAlarmStateChanged)
                    {
                        devicesDataModel.TriggerNotifyChanged();
                    }
                });
            }), null, TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(0.5));
        }
    }
}
