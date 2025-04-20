using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Blazor_wasm.Models.DatabaseModels;
using Blazor_wasm.Models.ModbusModels;

namespace Blazor_wasm.Services
{
    public interface IDataService
    {
        Task<List<RealTimepH>> GetpH(long startTimestamp, long endTimestamp);
        Task<List<D1CalData>> GetD1AllCAL();
        Task<List<D2CalData>> GetD2AllCAL();
        Task<List<AlarmData>> GetAllAlarm();
        Task<List<SchedulerModel>> GetScheduler();
        Task<DevicesDataModelDTO> GetModbusDevicesData();
        Task<FieldDataModel> GetFieldData();


        Task<(bool IsSuccess, string ErrorMessage)> PostpH(RealTimepH realTimepH);
        Task<(bool IsSuccess, string ErrorMessage)> PostD1CALList(List<D1CalData> data);
        Task<(bool IsSuccess, string ErrorMessage)> PostD2CALList(List<D2CalData> data);
        Task<(bool IsSuccess, string ErrorMessage)> PostD1CalData(D1CalData data);
        Task<(bool IsSuccess, string ErrorMessage)> PostD2CalData(D2CalData data);
        Task<(bool IsSuccess, string ErrorMessage)> PostAlarmData(AlarmData alarmData);
        Task<(bool IsSuccess, string ErrorMessage)> PostScheduler(SchedulerModel schedulerData);
        Task<(bool IsSuccess, string ErrorMessage)> PostModbusDevicesData(PostModbusApiModel postModbusApiModel);
        Task<(bool IsSuccess, string ErrorMessage)> PostFieldData(FieldDataModel postModbusApiModel);

        Task<(bool IsSuccess, string ErrorMessage)> UpdateAlarmStatus(AlarmData alarmData);
        Task<(bool IsSuccess, string ErrorMessage)> UpdateScheduler(SchedulerModel schedulerData);
        Task<(bool IsSuccess, string ErrorMessage)> UpdateFieldData(FieldDataModel data);

        Task<(bool IsSuccess, string ErrorMessage)> DeletepH();
        Task<(bool IsSuccess, string ErrorMessage)> DeleteD1CAL();
        Task<(bool IsSuccess, string ErrorMessage)> DeleteD2CAL();
        Task<(bool IsSuccess, string ErrorMessage)> DeleteAlarmData();
        Task<(bool IsSuccess, string ErrorMessage)> DeleteScheduler(SchedulerModel schedulerData);

        Task<(bool IsSuccess, string ErrorMessage)> WriteRegister(int address, int value);
    }
}
