using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Blazor_wasm.Models.APIModels;
using Blazor_wasm.Models.DatabaseModels;
using Blazor_wasm.Models.ModbusModels;

using Newtonsoft.Json;


namespace Blazor_wasm.Services
{
    
    public class DataService:IDataService
    {
        private IAppService _appService;
        private DevicesDataModel _devicesDataModel;
        public static bool modbusResponseStatus;
        public DataService(IAppService appService, DevicesDataModel devicesDataModel)
        {
            _appService = appService;
            _devicesDataModel = devicesDataModel;
        }
       
        public async Task<List<RealTimepH>> GetpH(long startTimestamp, long endTimestamp)
        {
            List<RealTimepH> realTimepH = new List<RealTimepH>();

            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{Setting.BaseUrl}{APIs.GetpH}?startTimestamp={startTimestamp}&endTimestamp={endTimestamp}";
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);

                    var result = await client.GetAsync(url);
                    if (result.IsSuccessStatusCode)
                    {
                        var response = await result.Content.ReadAsStringAsync();
                        realTimepH = JsonConvert.DeserializeObject<List<RealTimepH>>(response);
                    }
                    else
                    {
                        if (Setting.UserBasicDetail != null)
                        {
                            if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                            {
                                await _appService.RefreshToken();
                                await GetpH(startTimestamp, endTimestamp);
                            }
                        }                                               
                    }                   
                }               
            }
            catch (Exception ex) { }

            return realTimepH;
        }
        public async Task<List<D1CalData>> GetD1AllCAL()
        {
            List<D1CalData> d1CalData = new List<D1CalData>();
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{Setting.BaseUrl}{APIs.GetD1AllCAL}";
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);

                    var result = await client.GetAsync(url);
                    if (result.IsSuccessStatusCode)
                    {
                        var response = await result.Content.ReadAsStringAsync();
                        d1CalData = JsonConvert.DeserializeObject<List<D1CalData>>(response);
                    }
                    else
                    {
                        if (Setting.UserBasicDetail != null)
                        {
                            if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                            {
                                await _appService.RefreshToken();
                                await GetD1AllCAL();
                            }
                        }
                    }                   
                }
            }
            catch (Exception ex) { }

            return d1CalData;

        }

        public async Task<List<D2CalData>> GetD2AllCAL()
        {
            List<D2CalData> d2CalData = new List<D2CalData>();
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{Setting.BaseUrl}{APIs.GetD2AllCAL}";
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);

                    var result = await client.GetAsync(url);
                    if (result.IsSuccessStatusCode)
                    {
                        var response = await result.Content.ReadAsStringAsync();
                        d2CalData = JsonConvert.DeserializeObject<List<D2CalData>>(response);
                    }
                    else
                    {
                        if (Setting.UserBasicDetail != null)
                        {
                            if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                            {
                                await _appService.RefreshToken();
                                await GetD2AllCAL();
                            }
                        }
                    }                    
                }
            }
            catch (Exception ex) { }

            return d2CalData;

        }

        public async Task<List<AlarmData>> GetAllAlarm()
        {
            List<AlarmData> alarmData = new List<AlarmData>();
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{Setting.BaseUrl}{APIs.GetAllAlarm}";
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);

                    var result = await client.GetAsync(url);
                    if (result.IsSuccessStatusCode)
                    {
                        var response = await result.Content.ReadAsStringAsync();
                        alarmData = JsonConvert.DeserializeObject<List<AlarmData>>(response);
                    }
                    else
                    {
                        if (Setting.UserBasicDetail != null)
                        {
                            if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                            {
                                await _appService.RefreshToken();
                                await GetAllAlarm();
                            }
                        }
                    }                  
                }
            }
            catch (Exception ex) { }

            return alarmData;
        }

        public async Task<List<SchedulerModel>> GetScheduler()
        {
            List<SchedulerModel> schedulerData = new List<SchedulerModel>();
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{Setting.BaseUrl}{APIs.GetScheduler}";
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);

                    var result = await client.GetAsync(url);
                    if (result.IsSuccessStatusCode)
                    {
                        var response = await result.Content.ReadAsStringAsync();
                        schedulerData = JsonConvert.DeserializeObject<List<SchedulerModel>>(response);
                    }
                    else
                    {
                        if (Setting.UserBasicDetail != null)
                        {
                            if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                            {
                                await _appService.RefreshToken();
                                await GetScheduler();
                            }
                        }
                    }                   
                }
            }
            catch (Exception ex) { }

            return schedulerData;
        }

        public async Task<DevicesDataModelDTO> GetModbusDevicesData()
        {      
            DevicesDataModelDTO devicesDataModelDTO = new DevicesDataModelDTO();
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{Setting.BaseUrl}{APIs.GetModbusDevicesData}";
                       client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);

                    var result = await client.GetAsync(url);
                    if (result.IsSuccessStatusCode)
                    {
                        modbusResponseStatus = true;
                        var response = await result.Content.ReadAsStringAsync();
                        devicesDataModelDTO = JsonConvert.DeserializeObject<List<DevicesDataModelDTO>>(response).FirstOrDefault();
                    }
                    else
                    {
                        if (Setting.UserBasicDetail != null)
                        {
                            if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                            {
                                await _appService.RefreshToken();
                                await GetModbusDevicesData();
                            }
                        }
                    }                   
                }
            }
            catch (Exception ex) { }
            return devicesDataModelDTO;

        }

        public async Task<FieldDataModel> GetFieldData()
        {
            FieldDataModel fieldData = new FieldDataModel();
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{Setting.BaseUrl}{APIs.GetFieldData}";
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);

                    var result = await client.GetAsync(url);
                    if (result.IsSuccessStatusCode)
                    {
                        var response = await result.Content.ReadAsStringAsync();
                        fieldData = JsonConvert.DeserializeObject<FieldDataModel>(response);
                    }
                    else
                    {
                        if (Setting.UserBasicDetail != null)
                        {
                            if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                            {
                                await _appService.RefreshToken();
                                await GetFieldData();
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { }

            return fieldData;

        }

        public async Task<(bool IsSuccess, string ErrorMessage)> PostpH(RealTimepH realTimepH)
        {
            string errorMessage = string.Empty;
            bool isSuccess = false;
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{Setting.BaseUrl}{APIs.PostpH}";
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);
                    var serializedStr = JsonConvert.SerializeObject(realTimepH);
                    var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));
                    if (response.IsSuccessStatusCode)
                    {
                        isSuccess = true;
                    }
                    else
                    {
                        if (Setting.UserBasicDetail != null)
                        {
                            if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                            {
                                await _appService.RefreshToken();
                                await PostpH(realTimepH);
                            }
                            errorMessage = await response.Content.ReadAsStringAsync();
                        }
                    }                    
                }
            }
            catch (Exception ex) { }
            return (isSuccess, errorMessage);

        }

        public async Task<(bool IsSuccess, string ErrorMessage)> PostD1CALList(List<D1CalData> data)
        {
            string errorMessage = string.Empty;
            bool isSuccess = false;
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{Setting.BaseUrl}{APIs.PostD1CALList}";
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);
                    var serializedStr = JsonConvert.SerializeObject(data);
                    var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));
                    if (response.IsSuccessStatusCode)
                    {
                        isSuccess = true;
                    }
                    else
                    {
                        if (Setting.UserBasicDetail != null)
                        {
                            if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                            {
                                await _appService.RefreshToken();
                                await PostD1CALList(data);
                            }
                            errorMessage = await response.Content.ReadAsStringAsync();
                        }
                    }                    
                }
            }
            catch (Exception ex) { }
            return (isSuccess, errorMessage);

        }

        public async Task<(bool IsSuccess, string ErrorMessage)> PostD2CALList(List<D2CalData> data)
        {
            string errorMessage = string.Empty;
            bool isSuccess = false;
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{Setting.BaseUrl}{APIs.PostD2CALList}";
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);
                    var serializedStr = JsonConvert.SerializeObject(data);
                    var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));
                    if (response.IsSuccessStatusCode)
                    {
                        isSuccess = true;
                    }
                    else
                    {
                        if (Setting.UserBasicDetail != null)
                        {
                            if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                            {
                                await _appService.RefreshToken();
                                await PostD2CALList(data);
                            }
                            errorMessage = await response.Content.ReadAsStringAsync();
                        }
                    }                   
                }
            }
            catch (Exception ex) { }
            return (isSuccess, errorMessage);

        }

        public async Task<(bool IsSuccess, string ErrorMessage)> PostD1CalData(D1CalData data)
        {
            string errorMessage = string.Empty;
            bool isSuccess = false;
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{Setting.BaseUrl}{APIs.PostD1CalData}";
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);
                    var serializedStr = JsonConvert.SerializeObject(data);
                    var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));
                    if (response.IsSuccessStatusCode)
                    {
                        isSuccess = true;
                    }
                    else
                    {
                        if (Setting.UserBasicDetail != null)
                        {
                            if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                            {
                                await _appService.RefreshToken();
                                await PostD1CalData(data);
                            }
                            errorMessage = await response.Content.ReadAsStringAsync();
                        }
                    }                   
                }
            }
            catch (Exception ex) { }
            return (isSuccess, errorMessage);

        }

        public async Task<(bool IsSuccess, string ErrorMessage)> PostD2CalData(D2CalData data)
        {
            string errorMessage = string.Empty;
            bool isSuccess = false;
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{Setting.BaseUrl}{APIs.PostD2CalData}";
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);
                    var serializedStr = JsonConvert.SerializeObject(data);
                    var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));
                    if (response.IsSuccessStatusCode)
                    {
                        isSuccess = true;
                    }
                    else
                    {
                        if (Setting.UserBasicDetail != null)
                        {
                            if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                            {
                                await _appService.RefreshToken();
                                await PostD2CalData(data);
                            }
                            errorMessage = await response.Content.ReadAsStringAsync();
                        }
                    }                   
                }
            }
            catch (Exception ex) { }
            return (isSuccess, errorMessage);

        }

        public async Task<(bool IsSuccess, string ErrorMessage)> PostAlarmData(AlarmData alarmData)
        {
            string errorMessage = string.Empty;
            bool isSuccess = false;
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{Setting.BaseUrl}{APIs.PostAlarmData}";
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);
                    var serializedStr = JsonConvert.SerializeObject(alarmData);
                    var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));
                    if (response.IsSuccessStatusCode)
                    {
                        isSuccess = true;
                    }
                    else
                    {
                        if (Setting.UserBasicDetail != null)
                        {
                            if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                            {
                                await _appService.RefreshToken();
                                await PostAlarmData(alarmData);
                            }
                            errorMessage = await response.Content.ReadAsStringAsync();
                        }
                    }                    
                }
            }
            catch (Exception ex) { }
            return (isSuccess, errorMessage);

        }

        public async Task<(bool IsSuccess, string ErrorMessage)> PostScheduler(SchedulerModel schedulerData)
        {
            string errorMessage = string.Empty;
            bool isSuccess = false;
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{Setting.BaseUrl}{APIs.PostScheduler}";
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);
                    var serializedStr = JsonConvert.SerializeObject(schedulerData);
                    var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));
                    if (response.IsSuccessStatusCode)
                    {
                        isSuccess = true;
                    }
                    else
                    {
                        if (Setting.UserBasicDetail != null)
                        {
                            if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                            {
                                await _appService.RefreshToken();
                                await PostScheduler(schedulerData);
                            }
                            errorMessage = await response.Content.ReadAsStringAsync();
                        }
                    }                   
                }
            }
            catch (Exception ex) { }
            return (isSuccess, errorMessage);

        }

        public async Task<(bool IsSuccess, string ErrorMessage)> PostModbusDevicesData(PostModbusApiModel postModbusApiModel)
        {
            string errorMessage = string.Empty;
            bool isSuccess = false;
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{Setting.BaseUrl}{APIs.PostModbusDevicesData}";
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);
                    var serializedStr = JsonConvert.SerializeObject(postModbusApiModel);
                    var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));
                    if (response.IsSuccessStatusCode)
                    {
                        isSuccess = true;
                    }
                    else
                    {
                        if (Setting.UserBasicDetail != null)
                        {
                            if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                            {
                                await _appService.RefreshToken();
                                await PostModbusDevicesData(postModbusApiModel);
                            }
                            errorMessage = await response.Content.ReadAsStringAsync();
                        }
                    }                    
                }
            }
            catch (Exception ex) { }
            return (isSuccess, errorMessage);

        }

        public async Task<(bool IsSuccess, string ErrorMessage)> PostFieldData(FieldDataModel fieldData)
        {
            string errorMessage = string.Empty;
            bool isSuccess = false;
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{Setting.BaseUrl}{APIs.PostFieldData}";
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);
                    var serializedStr = JsonConvert.SerializeObject(fieldData);
                    var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));
                    if (response.IsSuccessStatusCode)
                    {
                        isSuccess = true;
                    }
                    else
                    {
                        if (Setting.UserBasicDetail != null)
                        {
                            if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                            {
                                await _appService.RefreshToken();
                                await PostFieldData(fieldData);
                            }
                            errorMessage = await response.Content.ReadAsStringAsync();
                        }
                    }
                }
            }
            catch (Exception ex) { }
            return (isSuccess, errorMessage);
        }


        public async Task<(bool IsSuccess, string ErrorMessage)> UpdateAlarmStatus(AlarmData alarmData)
        {
            string errorMessage = string.Empty;
            bool isSuccess = false;
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{Setting.BaseUrl}{APIs.UpdateAlarmStatus}";
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);
                    var serializedStr = JsonConvert.SerializeObject(alarmData);
                    var response = await client.PutAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));
                    if (response.IsSuccessStatusCode)
                    {
                        isSuccess = true;
                    }
                    else
                    {
                        if (Setting.UserBasicDetail != null)
                        {
                            if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                            {
                                await _appService.RefreshToken();
                                await UpdateAlarmStatus(alarmData);
                            }
                            errorMessage = await response.Content.ReadAsStringAsync();
                        }
                    }                   
                }
            }
            catch (Exception ex) { }
            return (isSuccess, errorMessage);

        }

        public async Task<(bool IsSuccess, string ErrorMessage)> UpdateScheduler(SchedulerModel schedulerData)
        {
            string errorMessage = string.Empty;
            bool isSuccess = false;
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{Setting.BaseUrl}{APIs.UpdateScheduler}";
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);
                    var serializedStr = JsonConvert.SerializeObject(schedulerData);
                    var response = await client.PutAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));
                    if (response.IsSuccessStatusCode)
                    {
                        isSuccess = true;
                    }
                    else
                    {
                        if (Setting.UserBasicDetail != null)
                        {
                            if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                            {
                                await _appService.RefreshToken();
                                await UpdateScheduler(schedulerData);
                            }
                            errorMessage = await response.Content.ReadAsStringAsync();
                        }
                    }                   
                }
            }
            catch (Exception ex) { }
            return (isSuccess, errorMessage);

        }

        public async Task<(bool IsSuccess, string ErrorMessage)> UpdateFieldData(FieldDataModel fieldData)
        {
            string errorMessage = string.Empty;
            bool isSuccess = false;
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{Setting.BaseUrl}{APIs.UpdateFieldData}";
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);
                    var serializedStr = JsonConvert.SerializeObject(fieldData);
                    var response = await client.PutAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Error: {Setting.UserBasicDetail.AccessToken}");
                        isSuccess = true;
                    }
                    else
                    {
                        Console.WriteLine($"Error: {Setting.UserBasicDetail.AccessToken}");
                        if (Setting.UserBasicDetail != null)
                        {
                            if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                            {
                                await _appService.RefreshToken();
                                await UpdateFieldData(fieldData);
                            }
                            errorMessage = await response.Content.ReadAsStringAsync();
                        }
                    }
                }
            }
            catch (Exception ex) { }
            return (isSuccess, errorMessage);

        }
      
        public async Task<(bool IsSuccess, string ErrorMessage)> DeletepH()
        {
            string errorMessage = string.Empty;
            bool isSuccess = false;
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{Setting.BaseUrl}{APIs.DeletepH}";
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);
                    var responseDelete = await client.DeleteAsync(url);

                    if (responseDelete.IsSuccessStatusCode)
                    {
                        isSuccess = true;
                    }
                    else
                    {
                        if (Setting.UserBasicDetail != null)
                        {
                            if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                            {
                                await _appService.RefreshToken();
                                await DeletepH();
                            }
                            errorMessage = await responseDelete.Content.ReadAsStringAsync();
                        }
                    }                   
                }
            }
            catch (Exception ex) { }
            return (isSuccess, errorMessage);

        }

        public async Task<(bool IsSuccess, string ErrorMessage)> DeleteD1CAL()
        {
            string errorMessage = string.Empty;
            bool isSuccess = false;
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{Setting.BaseUrl}{APIs.DeleteD1CAL}";
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);
                    var responseDelete = await client.DeleteAsync(url);

                    if (responseDelete.IsSuccessStatusCode)
                    {
                        isSuccess = true;
                    }
                    else
                    {
                        if (Setting.UserBasicDetail != null)
                        {
                            if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                            {
                                await _appService.RefreshToken();
                                await DeleteD1CAL();
                            }
                            errorMessage = await responseDelete.Content.ReadAsStringAsync();
                        }
                    }                   
                }
            }
            catch (Exception ex) { }
            return (isSuccess, errorMessage);

        }

        public async Task<(bool IsSuccess, string ErrorMessage)> DeleteD2CAL()
        {
            string errorMessage = string.Empty;
            bool isSuccess = false;
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{Setting.BaseUrl}{APIs.DeleteD2CAL}";
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);
                    var responseDelete = await client.DeleteAsync(url);

                    if (responseDelete.IsSuccessStatusCode)
                    {
                        isSuccess = true;
                    }
                    else
                    {
                        if (Setting.UserBasicDetail != null)
                        {
                            if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                            {
                                await _appService.RefreshToken();
                                await DeleteD2CAL();
                            }
                            errorMessage = await responseDelete.Content.ReadAsStringAsync();
                        }
                    }                  
                }
            }
            catch (Exception ex) { }
            return (isSuccess, errorMessage);

        }

        public async Task<(bool IsSuccess, string ErrorMessage)> DeleteAlarmData()
        {
            string errorMessage = string.Empty;
            bool isSuccess = false;
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{Setting.BaseUrl}{APIs.DeleteAlarmData}";
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);
                    var responseDelete = await client.DeleteAsync(url);

                    if (responseDelete.IsSuccessStatusCode)
                    {
                        isSuccess = true;
                    }
                    else
                    {
                        if (Setting.UserBasicDetail != null)
                        {
                            if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                            {
                                await _appService.RefreshToken();
                                await DeleteAlarmData();
                            }
                            errorMessage = await responseDelete.Content.ReadAsStringAsync();
                        }
                    }                  
                }
            }
            catch (Exception ex) { }
            return (isSuccess, errorMessage);

        }

        public async Task<(bool IsSuccess, string ErrorMessage)> DeleteScheduler(SchedulerModel schedulerData)
        {
            string errorMessage = string.Empty;
            bool isSuccess = false;
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{Setting.BaseUrl}{APIs.DeleteScheduler}";

                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);
                    var request = new HttpRequestMessage(HttpMethod.Delete, url)
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(schedulerData), Encoding.UTF8, "application/json")
                    };

                    var responseDelete = await client.SendAsync(request);
                    if (responseDelete.IsSuccessStatusCode)
                    {
                        isSuccess = true;
                    }
                    else
                    {
                        if (Setting.UserBasicDetail != null)
                        {
                            if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                            {
                                await _appService.RefreshToken();
                                await DeleteScheduler(schedulerData);
                            }
                            errorMessage = await responseDelete.Content.ReadAsStringAsync();
                        }                          
                    }                   
                }
            }
            catch (Exception ex) { }
            return (isSuccess, errorMessage);

        }

        public async Task<(bool IsSuccess, string ErrorMessage)> WriteRegister(int address, int value)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{Setting.BaseUrl}{APIs.WriteRegister}";
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);

                    var data = new { Address = address, Value = value };
                    var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

                    var result = await client.PostAsync(url, content);
                    if (result.IsSuccessStatusCode)
                    {
                        return (true, string.Empty);
                    }
                    else
                    {
                        if (Setting.UserBasicDetail != null)
                        {
                            if(Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                            {
                                await _appService.RefreshToken();
                                return await WriteRegister(address, value);
                            }                           
                        }
                        return (false, $"Failed to write register. Status code: {result.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error writing register: {ex.Message}");
            }
        }
    }
}
