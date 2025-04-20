using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazor_wasm.Shared;
using Blazor_wasm.Models.ModbusModels;

namespace Blazor_wasm.Pages
{
    public partial class Index
    {
        private Timer dataTimer;
        private async Task WriteToRegister(ushort address, int value)
        {
            waitFeedback = true;
            dataTimer?.Dispose();
            dataTimer = new Timer(new TimerCallback((s) =>
            {
                waitFeedback = false;
                dataTimer?.Dispose();
            }), null, 8000, Timeout.Infinite);

            if(address == 10)
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
                        MainLayout.washingValve[0] = d1ToggleInput[0] ? "yellow" : null;
                        break;
                    case 2:
                        d1ToggleInput[1] = !d1ToggleInput[1];
                        MainLayout.reagValve[0] = d1ToggleInput[1] ? "yellow" : null;
                        break;
                    case 4:
                        d1ToggleInput[2] = !d1ToggleInput[2];
                        MainLayout.bufAValve[0] = d1ToggleInput[2] ? "yellow" : null;
                        break;
                    case 8:
                        d1ToggleInput[3] = !d1ToggleInput[3];
                        MainLayout.bufBValve[0] = d1ToggleInput[3] ? "yellow" : null;
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
                        MainLayout.washingValve[1] = d2ToggleInput[0] ? "yellow" : null;
                        break;
                    case 2:
                        d2ToggleInput[1] = !d2ToggleInput[1];
                        MainLayout.reagValve[1] = d2ToggleInput[1] ? "yellow" : null;
                        break;
                    case 4:
                        d2ToggleInput[2] = !d2ToggleInput[2];
                        MainLayout.bufAValve[1] = d2ToggleInput[2] ? "yellow" : null;
                        break;
                    case 8:
                        d2ToggleInput[3] = !d2ToggleInput[3];
                        MainLayout.bufBValve[1] = d2ToggleInput[3] ? "yellow" : null;
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
            await dataService.PostModbusDevicesData(postModbusApiModel);
        }
    }
}
