using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Blazor_wasm.Models.APIModels;
using Blazor_wasm.Models.AuthModels;
using Blazor_wasm.Models.DatabaseModels;
using Blazor_wasm.Models.ModbusModels;

using Newtonsoft.Json;


namespace Blazor_wasm.Services
{
    
    public class DataService:IDataService
    {
        private IAppService _appService;
        private DevicesDataModel _devicesDataModel;
        private readonly HttpClient client = new HttpClient();        

        public DataService(IAppService appService, DevicesDataModel devicesDataModel)
        {
            _appService = appService;
            _devicesDataModel = devicesDataModel;
        }
       
        public async Task<MainResponse<List<RealTimepH>>> GetpH(long startTimestamp, long endTimestamp)
        {
            var result = new MainResponse<List<RealTimepH>>();
            
            try
            {
                var url = $"{Setting.BaseUrl}{APIs.GetpH}?startTimestamp={startTimestamp}&endTimestamp={endTimestamp}";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);

                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    result.IsSuccess = true;
                    var responseStr = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<RealTimepH>>(responseStr);
                    result.Content = data;
                }
                else
                {
                    result.StatusCode = (int)response.StatusCode;

                    if (Setting.UserBasicDetail.AccessToken != null)
                    {
                        if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                        {
                            await _appService.RefreshToken();
                            return await GetpH(startTimestamp, endTimestamp);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.ExMessage = ex.Message;
            }

            return result;                             
        }
        public async Task<MainResponse<List<D1CalData>>> GetD1AllCAL()
        {
            var result = new MainResponse<List<D1CalData>>();

            try
            {
                var url = $"{Setting.BaseUrl}{APIs.GetD1AllCAL}";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);
                Console.WriteLine($"111111111111{Setting.UserBasicDetail.AccessToken}");
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    result.IsSuccess = true;
                    var responseStr = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<D1CalData>>(responseStr);
                    result.Content = data;
                }
                else
                {
                    result.StatusCode = (int)response.StatusCode;
                    Console.WriteLine($"111111111111{Setting.UserBasicDetail.AccessToken}");
                    Console.WriteLine($"111111111111{Setting.UserBasicDetail.AccessTokenExpire}");
                    if (Setting.UserBasicDetail.AccessToken != null)
                    {
                        if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                        {
                            await _appService.RefreshToken();
                            return await GetD1AllCAL();
                        }
                    }
                }                                  
            }
            catch (Exception ex)
            {
                result.ExMessage = ex.Message;
            }

            return result;
        }

        public async Task<MainResponse<List<D2CalData>>> GetD2AllCAL()
        {
            var result = new MainResponse<List<D2CalData>>();

            try
            {
                var url = $"{Setting.BaseUrl}{APIs.GetD2AllCAL}";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);

                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    result.IsSuccess = true;
                    var responseStr = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<D2CalData>>(responseStr);
                    result.Content = data;
                }
                else
                {
                    result.StatusCode = (int)response.StatusCode;

                    if (Setting.UserBasicDetail.AccessToken != null)
                    {
                        if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                        {
                            await _appService.RefreshToken();
                            return await GetD2AllCAL();
                        }
                    }
                }                                  
            }
            catch (Exception ex)
            {
                result.ExMessage = ex.Message;
            }

            return result;
        }

        public async Task<MainResponse<List<AlarmData>>> GetAllAlarm()
        {
            var result = new MainResponse<List<AlarmData>>();

            try
            {
                var url = $"{Setting.BaseUrl}{APIs.GetAllAlarm}";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);

                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    result.IsSuccess = true;
                    var responseStr = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<AlarmData>>(responseStr);
                    result.Content = data;
                }
                else
                {
                    result.StatusCode = (int)response.StatusCode;

                    if (Setting.UserBasicDetail.AccessToken != null)
                    {
                        if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                        {
                            await _appService.RefreshToken();
                            return await GetAllAlarm();
                        }
                    }
                }                                  
            }
            catch (Exception ex)
            {
                result.ExMessage = ex.Message;
            }

            return result;
        }

        public async Task<MainResponse<List<SchedulerModel>>> GetScheduler()
        {
            var result = new MainResponse<List<SchedulerModel>>();

            try
            {
                var url = $"{Setting.BaseUrl}{APIs.GetScheduler}";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);

                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    result.IsSuccess = true ;
                    var responseStr = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<SchedulerModel>>(responseStr);
                    result.Content = data;
                }
                else
                {
                    result.StatusCode = (int)response.StatusCode;

                    if (Setting.UserBasicDetail.AccessToken != null)
                    {
                        if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                        {
                            await _appService.RefreshToken();
                            return await GetScheduler();
                        }
                    }
                }                                   
            }
            catch (Exception ex)
            {
                result.ExMessage = ex.Message;
            }

            return result;
        }

        public async Task<MainResponse<DevicesDataModelDTO>> GetModbusDevicesData()
        {
            var result = new MainResponse<DevicesDataModelDTO>();

            try
            {
                var url = $"{Setting.BaseUrl}{APIs.GetModbusDevicesData}";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);

                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    result.IsSuccess = true ;
                    var responseStr = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<DevicesDataModelDTO>>(responseStr).FirstOrDefault();
                    result.Content = data;
                }
                else
                {
                    result.StatusCode = (int)response.StatusCode;

                    if (Setting.UserBasicDetail.AccessToken != null)
                    {
                        if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                        {
                            await _appService.RefreshToken();
                            return await GetModbusDevicesData();
                        }
                    }
                }                                   
            }
            catch (Exception ex)
            {
                result.ExMessage = ex.Message;
            }

            return result;
        }

        public async Task<MainResponse<FieldDataModel>> GetFieldData()
        {
            var result = new MainResponse<FieldDataModel>();
            Console.WriteLine($"111111111111{Setting.UserBasicDetail.AccessTokenExpire}");
            try
            {
                var url = $"{Setting.BaseUrl}{APIs.GetFieldData}";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);

                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    result.IsSuccess = true;
                    var responseStr = await response.Content.ReadAsStringAsync();                   
                    var data = JsonConvert.DeserializeObject<FieldDataModel>(responseStr);
                    result.Content = data;
                }
                else
                {
                    result.StatusCode = (int)response.StatusCode;
                    Console.WriteLine($"111111111111{Setting.UserBasicDetail.AccessToken}");
                    Console.WriteLine($"111111111111{Setting.UserBasicDetail.AccessTokenExpire}");
                    if (Setting.UserBasicDetail.AccessToken != null)
                    {
                        if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                        {
                            await _appService.RefreshToken();
                            return await GetFieldData();
                        }
                    }
                }                
            }
            catch (Exception ex)
            {
                result.ExMessage = ex.Message;
                return await GetFieldData();
            }

            return result;
        }

        public async Task<MainResponse<object>> PostpH(RealTimepH realTimepH)
        {
            var result = new MainResponse<object>();

            try
            {
                var url = $"{Setting.BaseUrl}{APIs.PostpH}";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);
                var json = JsonConvert.SerializeObject(realTimepH);

                var response = await client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                    result.IsSuccess = true;
                else
                {
                    result.StatusCode = (int)response.StatusCode;

                    if (Setting.UserBasicDetail.AccessToken != null)
                    {
                        if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                        {
                            await _appService.RefreshToken();
                            return await PostpH(realTimepH);
                        }                      
                    }
                }                                  
            }
            catch (Exception ex)
            {
                result.ExMessage = ex.Message;
            }

            return result;
        }

        public async Task<MainResponse<object>> PostD1CALList(List<D1CalData> data)
        {
            var result = new MainResponse<object>();

            try
            {
                var url = $"{Setting.BaseUrl}{APIs.PostD1CALList}";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);
                var json = JsonConvert.SerializeObject(data);

                var response = await client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                    result.IsSuccess = true;                   
                else
                {
                    result.StatusCode = (int)response.StatusCode;

                    if (Setting.UserBasicDetail.AccessToken != null)
                    {
                        if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                        {
                            await _appService.RefreshToken();
                            return await PostD1CALList(data);
                        }                    
                    }
                }                                   
            }
            catch (Exception ex)
            {
                result.ExMessage = ex.Message;
            }

            return result;
        }

        public async Task<MainResponse<object>> PostD2CALList(List<D2CalData> data)
        {
            var result = new MainResponse<object>();

            try
            {
                var url = $"{Setting.BaseUrl}{APIs.PostD2CALList}";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);
                var json = JsonConvert.SerializeObject(data);

                var response = await client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                    result.IsSuccess = true;
                else
                {
                    result.StatusCode = (int)response.StatusCode;

                    if (Setting.UserBasicDetail.AccessToken != null)
                    {
                        if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                        {
                            await _appService.RefreshToken();
                            return await PostD2CALList(data);
                        }
                    }
                }                   
            }
            catch (Exception ex)
            {
                result.ExMessage = ex.Message;
            }

            return result;
        }

        public async Task<MainResponse<object>> PostD1CalData(D1CalData data)
        {
            var result = new MainResponse<object>();

            try
            {
                var url = $"{Setting.BaseUrl}{APIs.PostD1CalData}";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);
                var json = JsonConvert.SerializeObject(data);

                var response = await client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                    result.IsSuccess = true;
                else
                {
                    result.StatusCode = (int)response.StatusCode;

                    if (Setting.UserBasicDetail.AccessToken != null)
                    {
                        if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                        {
                            await _appService.RefreshToken();
                            return await PostD1CalData(data);
                        }
                    }
                }                   
            }
            catch (Exception ex)
            {
                result.ExMessage = ex.Message;
            }

            return result;
        }

        public async Task<MainResponse<object>> PostD2CalData(D2CalData data)
        {
            var result = new MainResponse<object>();

            try
            {
                var url = $"{Setting.BaseUrl}{APIs.PostD2CalData}";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);
                var json = JsonConvert.SerializeObject(data);

                var response = await client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                    result.IsSuccess = true;
                else
                {
                    result.StatusCode = (int)response.StatusCode;

                    if (Setting.UserBasicDetail.AccessToken != null)
                    {
                        if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                        {
                            await _appService.RefreshToken();
                            return await PostD2CalData(data);
                        }
                    }
                }                   
            }
            catch (Exception ex)
            {
                result.ExMessage = ex.Message;
            }

            return result;
        }

        public async Task<MainResponse<object>> PostAlarmData(AlarmData alarmData)
        {
            var result = new MainResponse<object>();

            try
            {
                var url = $"{Setting.BaseUrl}{APIs.PostAlarmData}";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);
                var json = JsonConvert.SerializeObject(alarmData);

                var response = await client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                    result.IsSuccess = true;
                else
                {
                    result.StatusCode = (int)response.StatusCode;

                    if (Setting.UserBasicDetail.AccessToken != null)
                    {
                        if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                        {
                            await _appService.RefreshToken();
                            return await PostAlarmData(alarmData);
                        }
                    }
                }                    
            }
            catch (Exception ex)
            {
                result.ExMessage = ex.Message;
            }

            return result;
        }

        public async Task<MainResponse<object>> PostScheduler(SchedulerModel schedulerData)
        {
            var result = new MainResponse<object>();

            try
            {
                var url = $"{Setting.BaseUrl}{APIs.PostScheduler}";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);
                var json = JsonConvert.SerializeObject(schedulerData);

                var response = await client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                    result.IsSuccess = true;
                else
                {
                    result.StatusCode = (int)response.StatusCode;

                    if (Setting.UserBasicDetail.AccessToken != null)
                    {
                        if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                        {
                            await _appService.RefreshToken();
                            return await PostScheduler(schedulerData);
                        }
                    }
                }                   
            }
            catch (Exception ex)
            {
                result.ExMessage = ex.Message;
            }

            return result;
        }

        public async Task<MainResponse<object>> PostModbusDevicesData(PostModbusApiModel postModbusApiModel)
        {
            var result = new MainResponse<object>();

            try
            {
                var url = $"{Setting.BaseUrl}{APIs.PostModbusDevicesData}";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);
                var json = JsonConvert.SerializeObject(postModbusApiModel);

                var response = await client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                    result.IsSuccess = true;
                else
                {
                    result.StatusCode = (int)response.StatusCode;

                    if (Setting.UserBasicDetail.AccessToken != null)
                    {
                        if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                        {
                            await _appService.RefreshToken();
                            return await PostModbusDevicesData(postModbusApiModel);
                        }
                    }
                }                    
            }
            catch (Exception ex)
            {
                result.ExMessage = ex.Message;
            }

            return result;
        }

        public async Task<MainResponse<object>> PostFieldData(FieldDataModel fieldData)
        {
            var result = new MainResponse<object>();

            try
            {
                var url = $"{Setting.BaseUrl}{APIs.PostFieldData}";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);
                var json = JsonConvert.SerializeObject(fieldData);

                var response = await client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                    result.IsSuccess = true;
                else
                {
                    result.StatusCode = (int)response.StatusCode;

                    if (Setting.UserBasicDetail.AccessToken != null)
                    {
                        if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                        {
                            await _appService.RefreshToken();
                            return await PostFieldData(fieldData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.ExMessage = ex.Message;
            }

            return result;
        }


        public async Task<MainResponse<object>> UpdateAlarmStatus(AlarmData alarmData)
        {
            var result = new MainResponse<object>();

            try
            {
                var url = $"{Setting.BaseUrl}{APIs.UpdateAlarmStatus}";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);
                var json = JsonConvert.SerializeObject(alarmData);

                var response = await client.PutAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                    result.IsSuccess = true;
                else
                {
                    result.StatusCode = (int)response.StatusCode;

                    if (Setting.UserBasicDetail.AccessToken != null)
                    {
                        if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                        {
                            await _appService.RefreshToken();
                            return await UpdateAlarmStatus(alarmData);
                        }
                    }
                }                   
            }
            catch (Exception ex)
            {
                result.ExMessage = ex.Message;
            }

            return result;
        }

        public async Task<MainResponse<object>> UpdateScheduler(SchedulerModel schedulerData)
        {
            var result = new MainResponse<object>();

            try
            {
                var url = $"{Setting.BaseUrl}{APIs.UpdateScheduler}";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);
                var json = JsonConvert.SerializeObject(schedulerData);

                var response = await client.PutAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                    result.IsSuccess = true;
                else
                {
                    result.StatusCode = (int)response.StatusCode;

                    if (Setting.UserBasicDetail.AccessToken != null)
                    {
                        if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                        {
                            await _appService.RefreshToken();
                            return await UpdateScheduler(schedulerData);
                        }
                    }
                }                   
            }
            catch (Exception ex)
            {
                result.ExMessage = ex.Message;
            }

            return result;
        }

        public async Task<MainResponse<object>> UpdateFieldData(FieldDataModel fieldData)
        {
            var result = new MainResponse<object>();

            try
            {
                var url = $"{Setting.BaseUrl}{APIs.UpdateFieldData}";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);
                var json = JsonConvert.SerializeObject(fieldData);

                var response = await client.PutAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                    result.IsSuccess = true;
                else
                {
                    result.StatusCode = (int)response.StatusCode;

                    if (Setting.UserBasicDetail.AccessToken != null)
                    {
                        if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                        {
                            await _appService.RefreshToken();
                            return await UpdateFieldData(fieldData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.ExMessage = ex.Message;
            }

            return result;
        }
      
        public async Task<MainResponse<object>> DeletepH()
        {
            var result = new MainResponse<object>();

            try
            {
                var url = $"{Setting.BaseUrl}{APIs.DeletepH}";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);
                
                var response = await client.DeleteAsync(url);
                if (response.IsSuccessStatusCode)
                    result.IsSuccess = true;
                else
                {
                    result.StatusCode = (int)response.StatusCode;

                    if (Setting.UserBasicDetail.AccessToken != null)
                    {
                        if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                        {
                            await _appService.RefreshToken();
                            return await DeletepH();
                        }
                    }
                }                   
            }
            catch (Exception ex)
            {
                result.ExMessage = ex.Message;
            }

            return result;
        }

        public async Task<MainResponse<object>> DeleteD1CAL()
        {
            var result = new MainResponse<object>();

            try
            {
                var url = $"{Setting.BaseUrl}{APIs.DeleteD1CAL}";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);
                
                var response = await client.DeleteAsync(url);
                if (response.IsSuccessStatusCode)
                    result.IsSuccess = true;
                else
                {
                    result.StatusCode = (int)response.StatusCode;

                    if (Setting.UserBasicDetail.AccessToken != null)
                    {
                        if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                        {
                            await _appService.RefreshToken();
                            return await DeleteD1CAL();
                        }
                    }
                }                   
            }
            catch (Exception ex)
            {
                result.ExMessage = ex.Message;
            }

            return result;
        }

        public async Task<MainResponse<object>> DeleteD2CAL()
        {
            var result = new MainResponse<object>();

            try
            {
                var url = $"{Setting.BaseUrl}{APIs.DeleteD2CAL}";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);
                var response = await client.DeleteAsync(url);

                if (response.IsSuccessStatusCode)
                    result.IsSuccess = true;
                else
                {
                    result.StatusCode = (int)response.StatusCode;

                    if (Setting.UserBasicDetail.AccessToken != null)
                    {
                        if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                        {
                            await _appService.RefreshToken();
                            return await DeleteD2CAL();
                        }
                    }
                }                  
            }
            catch (Exception ex)
            {
                result.ExMessage = ex.Message;
            }

            return result;
        }

        public async Task<MainResponse<object>> DeleteAlarmData()
        {
            var result = new MainResponse<object>();

            try
            {
                var url = $"{Setting.BaseUrl}{APIs.DeleteAlarmData}";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);
                
                var response = await client.DeleteAsync(url);
                if (response.IsSuccessStatusCode)
                    result.IsSuccess = true;
                else
                {
                    result.StatusCode = (int)response.StatusCode;

                    if (Setting.UserBasicDetail.AccessToken != null)
                    {
                        if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                        {
                            await _appService.RefreshToken();
                            return await DeleteAlarmData();
                        }
                    }
                }                  
            }
            catch (Exception ex)
            {
                result.ExMessage = ex.Message;
            }

            return result;
        }

        public async Task<MainResponse<object>> DeleteScheduler(SchedulerModel schedulerData)
        {
            var result = new MainResponse<object>();

            try
            {
                var url = $"{Setting.BaseUrl}{APIs.DeleteScheduler}";

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);
                var request = new HttpRequestMessage(HttpMethod.Delete, url)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(schedulerData), Encoding.UTF8, "application/json")
                };

                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                    result.IsSuccess = true;
                else
                {
                    result.StatusCode = (int)response.StatusCode;

                    if (Setting.UserBasicDetail.AccessToken != null)
                    {
                        if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                        {
                            await _appService.RefreshToken();
                            return await DeleteScheduler(schedulerData);
                        }
                    }                          
                }                   
            }
            catch (Exception ex)
            {
                result.ExMessage = ex.Message;
            }

            return result;
        }

        public async Task<MainResponse<object>> WriteRegister(int address, int value)
        {
            var result = new MainResponse<object>();

            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{Setting.BaseUrl}{APIs.WriteRegister}";
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);

                    var data = new { Address = address, Value = value };
                    var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(url, content);
                    if (response.IsSuccessStatusCode)
                        result.IsSuccess = true;
                    else
                    {
                        result.StatusCode = (int)response.StatusCode;

                        if (Setting.UserBasicDetail.AccessToken != null)
                        {
                            if(Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                            {
                                await _appService.RefreshToken();
                                return await WriteRegister(address, value);
                            }                           
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.ExMessage = ex.Message;
            }

            return result;
        }
    }
}
