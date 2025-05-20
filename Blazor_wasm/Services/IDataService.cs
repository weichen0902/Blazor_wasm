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
        Task<MainResponse<List<RealTimepH>>> GetpH(long startTimestamp, long endTimestamp);
        Task<MainResponse<List<D1CalData>>> GetD1AllCAL();
        Task<MainResponse<List<D2CalData>>> GetD2AllCAL();
        Task<MainResponse<List<AlarmData>>> GetAllAlarm();
        Task<MainResponse<List<SchedulerModel>>> GetScheduler();
        Task<MainResponse<DevicesDataModelDTO>> GetModbusDevicesData();
        Task<MainResponse<FieldDataModel>> GetFieldData();


        Task<MainResponse<object>> PostpH(RealTimepH realTimepH);
        Task<MainResponse<object>> PostD1CALList(List<D1CalData> data);
        Task<MainResponse<object>> PostD2CALList(List<D2CalData> data);
        Task<MainResponse<object>> PostD1CalData(D1CalData data);
        Task<MainResponse<object>> PostD2CalData(D2CalData data);
        Task<MainResponse<object>> PostAlarmData(AlarmData alarmData);
        Task<MainResponse<object>> PostScheduler(SchedulerModel schedulerData);
        Task<MainResponse<object>> PostModbusDevicesData(PostModbusApiModel postModbusApiModel);
        Task<MainResponse<object>> PostFieldData(FieldDataModel postModbusApiModel);

        Task<MainResponse<object>> UpdateAlarmStatus(AlarmData alarmData);
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
