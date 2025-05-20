using Blazor_wasm.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.IdentityModel.Tokens.Jwt;

using Blazor_wasm.Models.APIModels;
using Blazor_wasm.Models.PageModels;
using Blazor_wasm.Models.AuthModels;
using Blazored.LocalStorage;
using Syncfusion.Blazor.Notifications;

namespace Blazor_wasm.Services
{
    public class AppService : IAppService
    {
        private readonly ILocalStorageService _localStorage;
        public string accessToken { get; set; }
        public JwtSecurityToken jwtAccessToken { get; set; }

        public ForgotPasswordModel forgotPasswordModel { get; set; } = new ForgotPasswordModel();

        public ResetPasswordModel resetPasswordModel { get; set; } = new ResetPasswordModel();

        private static readonly HttpClient client = new HttpClient();
        
        public AppService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }
        public async Task<MainResponse<object>> RegisterUser(RegistrationModel registerUser)
		{
            var result = new MainResponse<object>();

            try
            {
                var url = $"{Setting.BaseUrl}{APIs.RegisterUser}";

                var json = JsonConvert.SerializeObject(registerUser);
                var response = await client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
               
                if (response.IsSuccessStatusCode)
                    result.IsSuccess = true;
                else
                    result.StatusCode = (int)response.StatusCode;        
            }

            catch (Exception ex) 
            {
                result.ExMessage = ex.Message;
            }
						
			return result;
		}

		public async Task<MainResponse<object>> AuthenticateUser(LoginModel loginModel)
        {
            var result = new MainResponse<object>();

            try
			{
				var url = $"{Setting.BaseUrl}{APIs.AuthenticateUser}";

				var json = JsonConvert.SerializeObject(loginModel);

				var response = await client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
               
                if (response.IsSuccessStatusCode)
				{
                    result.IsSuccess = true;
                    var responseStr = await response.Content.ReadAsStringAsync();
                    var mainResponse = JsonConvert.DeserializeObject<MainResponse<object>>(responseStr);

                    var tokenResponse = JsonConvert.DeserializeObject<AuthenticateRequestAndResponse>(mainResponse.Content.ToString());
					var handler = new JwtSecurityTokenHandler();
					jwtAccessToken = handler.ReadToken(tokenResponse.AccessToken) as JwtSecurityToken;

                    if (Setting.UserBasicDetail == null)
                        Setting.UserBasicDetail = new UserBasicDetail();
                    Setting.UserBasicDetail.AccessTokenExpire = jwtAccessToken.ValidTo;
                    Setting.UserBasicDetail.AccessToken = tokenResponse.AccessToken;
                    Setting.UserBasicDetail.RefreshToken = tokenResponse.RefreshToken;
                    Console.WriteLine(Setting.UserBasicDetail.AccessTokenExpire);
                    Console.WriteLine(Setting.UserBasicDetail.AccessToken);
                    Console.WriteLine(Setting.UserBasicDetail.RefreshToken);
                    result.Content = mainResponse.Content.ToString();
                }
				else
				{
                    result.StatusCode = (int)response.StatusCode;
				}			
			}
            catch (Exception ex)
            {
                result.ExMessage = ex.Message;
            }

            return result;
        }

		public async Task<MainResponse<UpdateModel>> GetUser()
		{
            var result = new MainResponse<UpdateModel>();

            try
            {
                var url = $"{Setting.BaseUrl}{APIs.GetUser}?email={Setting.UserBasicDetail.Email}";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);
                var response = await client.GetAsync(url);
               
                if (response.IsSuccessStatusCode)
                {
                    result.IsSuccess = true;    
                    var responseStr = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<UpdateModel>(responseStr);
                    result.Content = data;
                }
                    
                else
                {
                    result.StatusCode = (int)response.StatusCode;

                    if (Setting.UserBasicDetail != null)
                    {
                        if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                        {
                            await RefreshToken();
                            return await GetUser();
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

		public async Task<MainResponse<List<UsersEmail>>> GetAllUsers()
		{
            var result = new MainResponse<List<UsersEmail>>();
            var roleOfUsersEmail = new List<UsersEmail>();
            
            try
            {
                var url = $"{Setting.BaseUrl}{APIs.GetAllUsers}";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);
                var response = await client.GetAsync(url);
               
                if (response.IsSuccessStatusCode)
                {
                    result.IsSuccess = true;
                    var responseStr = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<UsersEmail>>(responseStr);
                   
                    roleOfUsersEmail = data.Where(r => r.Role == "User").Select(r => new UsersEmail
                    {
                        Role = r.Role,
                        Email = r.Email
                    }).ToList();

                    result.Content = roleOfUsersEmail;
                }
                else
                {
                    result.StatusCode = (int)response.StatusCode;

                    if (Setting.UserBasicDetail != null)
                    {
                        if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                        {
                            await RefreshToken();
                            return await GetAllUsers();
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

		public async Task<MainResponse<object>> ForgotPassword(string email)
        {
            var result = new MainResponse<object>();
            resetPasswordModel.Email = email;

            try
            {
                var url = $"{Setting.BaseUrl}{APIs.ForgotPassword}?email={email}";
                var serializedStr = JsonConvert.SerializeObject(email);

                var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));
               
                if (response.IsSuccessStatusCode)
                {
                    result.IsSuccess = true;
                    var responseStr = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<ForgotPasswordModel>(responseStr);
                    resetPasswordModel.Token = data?.Token ;
                }
                else
                {
                    result.StatusCode = (int)response.StatusCode;

                    if (Setting.UserBasicDetail != null)
                    {
                        if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                        {
                            await RefreshToken();
                            return await ForgotPassword(email);
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
        
		public async Task<MainResponse<object>> ResetPassword(ResetPasswordModel resetPwdModel)
        {
            var result = new MainResponse<object>();
            resetPwdModel.Token = forgotPasswordModel!.Token;

            try
            {
                var url = $"{Setting.BaseUrl}{APIs.ResetPassword}";
                var serializedStr = JsonConvert.SerializeObject(resetPwdModel);
                var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                    result.IsSuccess = true;
                else
                {
                    result.StatusCode = (int)response.StatusCode;

                    if (Setting.UserBasicDetail != null)
                    {
                        if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                        {
                            await RefreshToken();
                            return await ResetPassword(resetPwdModel);
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

        public async Task<MainResponse<object>> UpdateUser(UpdateModel updateModel)
		{
            var result = new MainResponse<object>();

            try
            {
                var url = $"{Setting.BaseUrl}{APIs.UpdateUser}";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);
                var serializedStr = JsonConvert.SerializeObject(updateModel);
                var response = await client.PutAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));
                
                if (response.IsSuccessStatusCode)
                    result.IsSuccess = true;
                else
                {
                    result.StatusCode = (int)response.StatusCode;

                    if (Setting.UserBasicDetail != null)
                    {
                        if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                        {
                            await RefreshToken();
                            return await UpdateUser(updateModel);
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

		public async Task<MainResponse<object>> DeleteUsers(List<UsersEmail> userEmail)
		{
            var result = new MainResponse<object>();

            try
            {
                var url = $"{Setting.BaseUrl}{APIs.DeleteUsers}";

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);
                var request = new HttpRequestMessage(HttpMethod.Delete, url)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(userEmail), Encoding.UTF8, "application/json")
                };

                var response = await client.SendAsync(request);              

                if (response.IsSuccessStatusCode)
                    result.IsSuccess = true;
                else
                {
                    result.StatusCode = (int)response.StatusCode;

                    if (Setting.UserBasicDetail != null)
                    {
                        if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                        {
                            await RefreshToken();
                            return await DeleteUsers(userEmail);
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

		public async Task<MainResponse<object>> RefreshToken()
        {
            var result = new MainResponse<object>();          

            try
            {
                var url = $"{Setting.BaseUrl}{APIs.RefreshToken}";

                var serializedStr = JsonConvert.SerializeObject(new AuthenticateRequestAndResponse
                {
                    RefreshToken = Setting.UserBasicDetail.RefreshToken,
                    AccessToken = Setting.UserBasicDetail.AccessToken
                });

                var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));
               
                if (response.IsSuccessStatusCode)
                {
                    result.IsSuccess = true;
                    var responseStr = await response.Content.ReadAsStringAsync();

                    var tokenDetails = JsonConvert.DeserializeObject<AuthenticateRequestAndResponse>(responseStr);
                    Setting.UserBasicDetail.AccessToken = tokenDetails.AccessToken;
                    Setting.UserBasicDetail.RefreshToken = tokenDetails.RefreshToken;

                    var handler = new JwtSecurityTokenHandler();
                    jwtAccessToken = handler.ReadToken(tokenDetails.AccessToken) as JwtSecurityToken;
                    Setting.UserBasicDetail.AccessTokenExpire = jwtAccessToken.ValidTo;

                    string userDetailsStr = JsonConvert.SerializeObject(Setting.UserBasicDetail);
                    await _localStorage.SetItemAsStringAsync(nameof(Setting.UserBasicDetail), userDetailsStr);                                    
                }

                else
                {
                    result.StatusCode = (int)response.StatusCode;

                    if (Setting.UserBasicDetail != null)
                    {
                        if (Setting.UserBasicDetail.AccessTokenExpire < DateTime.UtcNow)
                        {
                            return await RefreshToken();     
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
