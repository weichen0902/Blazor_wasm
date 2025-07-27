using Blazor_wasm.Models.ModbusModels;
using Blazor_wasm.Services;
using Blazor_wasm.Shared;

namespace Blazor_wasm.Controller
{
    public class ModbusToWrite
    {
        protected readonly IDataService _dataService;
        public bool waitFeedback;
        public bool[] d1ToggleInput = new bool[5];
        public bool[] d2ToggleInput = new bool[5];

        private Timer dataTimer;

        public ModbusToWrite(IDataService dataService)
        {
            _dataService = dataService;
        }
        public async Task WriteToRegister(ushort address, int value)
        {
            waitFeedback = true;
            dataTimer?.Dispose();
            dataTimer = new Timer(new TimerCallback((s) =>
            {
                waitFeedback = false;
                dataTimer?.Dispose();
            }), null, 1000, Timeout.Infinite);

            if (address == 0)
            {
                if (MainLayout.systemStatusIntBinaryCharArray[0] == '0')
                {
                    MainLayout.systemStatusIntBinaryCharArray[0] = '1';
                    MainLayout.systemStatus[0] = "red";
                }
                else
                {
                    MainLayout.systemStatusIntBinaryCharArray[0] = '0';
                    MainLayout.systemStatus[0] = "blue";
                }
            }

            if (address == 10)
            {
                switch (value)
                {
                    case 1:
                        MainLayout.hbmWashingMotionControl[0] = "yellow";
                        MainLayout.hbmCalMotionControl[0] = null;
                        break;
                    case 2:
                        MainLayout.hbmCalMotionControl[0] = "yellow";
                        MainLayout.hbmWashingMotionControl[0] = null;
                        break;
                    case 4:
                        MainLayout.hbmWashingMotionControl[0] = null;
                        MainLayout.hbmCalMotionControl[0] = null;
                        break;
                }
            }
            else if (address == 20)
            {
                switch (value)
                {
                    case 1:
                        MainLayout.hbmWashingMotionControl[1] = "yellow";
                        MainLayout.hbmCalMotionControl[1] = null;
                        break;
                    case 2:
                        MainLayout.hbmCalMotionControl[1] = "yellow";
                        MainLayout.hbmWashingMotionControl[1] = null;
                        break;
                    case 4:
                        MainLayout.hbmWashingMotionControl[1] = null;
                        MainLayout.hbmCalMotionControl[1] = null;
                        break;
                }
            }

            if (address == 1100)
            {
                switch (value)
                {
                    case 1:
                        d1ToggleInput[0] = !d1ToggleInput[0];
                        MainLayout.bufAValve[0] = d1ToggleInput[0] ? "yellow" : null;
                        break;
                    case 2:
                        d1ToggleInput[1] = !d1ToggleInput[1];
                        MainLayout.bufBValve[0] = d1ToggleInput[1] ? "yellow" : null;
                        break;
                    case 4:
                        d1ToggleInput[2] = !d1ToggleInput[2];
                        MainLayout.reagValve[0] = d1ToggleInput[2] ? "yellow" : null;
                        break;
                    case 8:
                        d1ToggleInput[3] = !d1ToggleInput[3];
                        MainLayout.washingValve[0] = d1ToggleInput[3] ? "yellow" : null;
                        break;
                    case 16:
                        d1ToggleInput[4] = !d1ToggleInput[4];
                        MainLayout.airPressureValve[0] = d1ToggleInput[4] ? "yellow" : null;
                        break;
                    case 64:
                        MainLayout.electrodeReturnUp[0] = "yellow";
                        MainLayout.electrodeReturnSpecifyLocation[0] = null;
                        break;
                    //case 256:                     
                    //    MainLayout.electrodeManualInchUp[0] = "yellow";
                    //    break;
                    //case 512:
                    //    MainLayout.electrodeManualInchDown[0] = "yellow";
                    //    break;
                    case 128:
                        MainLayout.electrodeReturnSpecifyLocation[0] = "yellow";
                        MainLayout.electrodeReturnUp[0] = null;
                        break;
                    case 1024:
                        MainLayout.electrodeStandbyPointSetting[0] = "yellow";
                        break;
                }
            }
            else if (address == 2100)
            {
                switch (value)
                {
                    case 1:
                        d2ToggleInput[0] = !d2ToggleInput[0];
                        MainLayout.bufAValve[1] = d2ToggleInput[0] ? "yellow" : null;
                        break;
                    case 2:
                        d2ToggleInput[1] = !d2ToggleInput[1];
                        MainLayout.bufBValve[1] = d2ToggleInput[1] ? "yellow" : null;
                        break;
                    case 4:
                        d2ToggleInput[2] = !d2ToggleInput[2];
                        MainLayout.reagValve[1] = d2ToggleInput[2] ? "yellow" : null;
                        break;
                    case 8:
                        d2ToggleInput[3] = !d2ToggleInput[3];
                        MainLayout.washingValve[1] = d2ToggleInput[3] ? "yellow" : null;
                        break;
                    case 16:
                        d2ToggleInput[4] = !d2ToggleInput[4];
                        MainLayout.airPressureValve[1] = d2ToggleInput[4] ? "yellow" : null;
                        break;
                    case 64:
                        MainLayout.electrodeReturnUp[1] = "yellow";
                        MainLayout.electrodeReturnSpecifyLocation[1] = null;
                        break;
                    //case 256:
                    //    MainLayout.electrodeManualInchUp[1] = "yellow";
                    //    break;
                    //case 512:
                    //    MainLayout.electrodeManualInchDown[1] = "yellow";
                    //    break;
                    case 128:
                        MainLayout.electrodeReturnSpecifyLocation[1] = "yellow";
                        MainLayout.electrodeReturnUp[1] = null;
                        break;
                    case 1024:
                        MainLayout.electrodeStandbyPointSetting[1] = "yellow";
                        break;
                }
            }

            PostModbusApiModel postModbusApiModel = new PostModbusApiModel() { startAddress = address, value = value };
            await _dataService.PostModbusDevicesData(postModbusApiModel);
        }

        public class ButtonModel
        {
            public string Name { get; set; }
            public string ButtonStyle { get; set; }
            public string Background { get; set; }
            public ushort Address { get; set; }
            public ushort Value { get; set; }
        }
    }
}
