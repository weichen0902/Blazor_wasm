using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Blazor_wasm.Models.APIModels
{
    internal class APIs
    {
        public const string AuthenticateUser = "/api/Users/AuthenticateUser";
        public const string RegisterUser = "/api/Users/RegisterUser";
        public const string ForgotPassword = "/api/Users/ForgotPassword";
        public const string ResetPassword = "/api/Users/ResetPassword";
        public const string UpdateUser = "/api/Users/UpdateUser";
        public const string GetUser = "/api/Users/GetUser";
		public const string GetAllUsers = "/api/Users/GetAllUsers";
        public const string DeleteUsers = "/api/Users/DeleteUsers";
        public const string RefreshToken = "/api/Users/RefreshToken";

        public const string GetpH = "/api/Data/Getph";
        public const string GetD1AllCAL = "/api/Data/GetD1AllCAL";
        public const string GetD2AllCAL = "/api/Data/GetD2AllCAL";
        public const string GetAllAlarm = "/api/Data/GetAllAlarm";
        public const string GetScheduler = "/api/Data/GetScheduler";
        public const string GetModbusDevicesData = "/api/Data/GetModbusDevicesData";
        public const string GetFieldData = "/api/Data/GetFieldData";

        public const string PostpH = "/api/Data/Postph";
        public const string PostD1CALList = "/api/Data/PostD1CALList";
        public const string PostD2CALList = "/api/Data/PostD2CALList";
        public const string PostD1CalData = "/api/Data/PostD1CalData";
        public const string PostD2CalData = "/api/Data/PostD2CalData";    
        public const string PostAlarmData = "/api/Data/PostAlarmData";
        public const string PostScheduler = "/api/Data/PostScheduler";
        public const string PostModbusDevicesData = "/api/Data/PostModbusDevicesData";
        public const string WriteRegister = "/api/Data/WriteRegister";
        public const string PostFieldData = "/api/Data/PostFieldData";

        public const string UpdateAlarmStatus = "/api/Data/UpdateAlarmStatus";
        public const string UpdateScheduler = "/api/Data/UpdateScheduler";
        public const string UpdateFieldData = "/api/Data/UpdateFieldData";

        public const string DeletepH = "/api/Data/DeletepH";
        public const string DeleteD1CAL = "/api/Data/DeleteD1CAL";
        public const string DeleteD2CAL = "/api/Data/DeleteD2CAL";
        public const string DeleteAlarmData = "/api/Data/DeleteAlarmData";
        public const string DeleteScheduler = "/api/Data/DeleteScheduler";
    }
}
