using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazor_wasm.Models.AuthModels;
using Blazor_wasm.Models.DatabaseModels;
using Blazor_wasm.Models.ModbusModels;

namespace Blazor_wasm.Services
{
    public interface IDataService
    {
        Task<MainResponse<object>> GetpH(long startTimestamp, long endTimestamp);
        Task<MainResponse<object>> GetD1AllCAL();
        Task<MainResponse<object>> GetD2AllCAL();
        Task<MainResponse<object>> GetAllAlarm();
        Task<MainResponse<object>> GetScheduler();
        Task<MainResponse<object>> GetModbusDevicesData();
        Task<MainResponse<object>> GetFieldData();


        Task<MainResponse<object>> PostpH(RealTimepHModel realTimepH);
        Task<MainResponse<object>> PostD1CALList(List<CalDataModel> data);
        Task<MainResponse<object>> PostD2CALList(List<CalDataModel> data);
        Task<MainResponse<object>> PostD1CalData(CalDataModel data);
        Task<MainResponse<object>> PostD2CalData(CalDataModel data);
        Task<MainResponse<object>> PostAlarmData(AlarmDataModel alarmData);
        Task<MainResponse<object>> PostScheduler(SchedulerModel schedulerData);
        Task<MainResponse<object>> PostModbusDevicesData(PostModbusApiModel postModbusApiModel);
        Task<MainResponse<object>> PostFieldData(FieldDataModel postModbusApiModel);

        Task<MainResponse<object>> UpdateAlarmStatus(AlarmDataModel alarmData);
        Task<MainResponse<object>> UpdateScheduler(SchedulerModel schedulerData);
        Task<MainResponse<object>> UpdateFieldData(FieldDataModel data);

        Task<MainResponse<object>> DeletepH();
        Task<MainResponse<object>> DeleteD1CAL();
        Task<MainResponse<object>> DeleteD2CAL();
        Task<MainResponse<object>> DeleteAlarmData();
        Task<MainResponse<object>> DeleteScheduler(SchedulerModel schedulerData);

        Task<MainResponse<object>> WriteRegister(int address, int value);
    }
}
