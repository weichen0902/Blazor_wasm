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
        public async Task<MainResponse> RegisterUser(RegistrationModel registerUser)
		{         
            MainResponse result = new MainResponse();

            try
            {
                var url = $"{Setting.BaseUrl}{APIs.RegisterUser}";

                var json = JsonConvert.SerializeObject(registerUser);
                var response = await client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
                var responseStr = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<MainResponse>(responseStr);

                if (!response.IsSuccessStatusCode)
                    result.StatusCode = (int)response.StatusCode;        
            }

            catch (Exception ex) 
            {
                result.ExMessage = ex.Message;
            }
						
			return result;
		}

		public async Task<MainResponse> AuthenticateUser(LoginModel loginModel)
        {
            MainResponse result = new MainResponse();
            
			try
			{
				var url = $"{Setting.BaseUrl}{APIs.AuthenticateUser}";

				var serializedStr = JsonConvert.SerializeObject(loginModel);

				var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));
                var responseStr = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<MainResponse>(responseStr);

                if (response.IsSuccessStatusCode)
				{
                    var tokenResponse = JsonConvert.DeserializeObject<AuthenticateRequestAndResponse>(result.Content.ToString());
					var handler = new JwtSecurityTokenHandler();
					jwtAccessToken = handler.ReadToken(tokenResponse.AccessToken) as JwtSecurityToken;
                    Setting.UserBasicDetail.AccessTokenExpire = jwtAccessToken.ValidTo;
                    Setting.UserBasicDetail.AccessToken = tokenResponse.AccessToken;
                    Setting.UserBasicDetail.RefreshToken = tokenResponse.RefreshToken;
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

		public async Task<(MainResponse, UpdateModel)> GetUser()
		{
			UpdateModel updateModel = new UpdateModel();
            MainResponse result = new MainResponse();

            try
            {
                var url = $"{Setting.BaseUrl}{APIs.GetUser}?email={Setting.UserBasicDetail.Email}";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);
                var response = await client.GetAsync(url);
                var responseStr = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<MainResponse>(responseStr);

                if (response.IsSuccessStatusCode)
                    updateModel = JsonConvert.DeserializeObject<UpdateModel>(responseStr);
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

            return (result, updateModel);
        }

		public async Task<(MainResponse, List<UsersEmail>)> GetAllUsers()
		{
            MainResponse result = new MainResponse();
            List<UsersEmail> roleOfUsersEmail = new List<UsersEmail>();
            
            try
            {
                var url = $"{Setting.BaseUrl}{APIs.GetAllUsers}";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);
                var response = await client.GetAsync(url);
                var responseStr = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<MainResponse>(responseStr);

                if (response.IsSuccessStatusCode)
                {     
                    var usersEmail = JsonConvert.DeserializeObject<List<UsersEmail>>(responseStr);
                    roleOfUsersEmail = usersEmail.Where(r => r.Role == "User").Select(r => new UsersEmail
                    {
                        Role = r.Role,
                        Email = r.Email
                    }).ToList();
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

            return (result, roleOfUsersEmail);
		}

		public async Task<MainResponse> ForgotPassword(string email)
        {
            MainResponse result = new MainResponse();
            resetPasswordModel.Email = email;

            try
            {
                var url = $"{Setting.BaseUrl}{APIs.ForgotPassword}?email={email}";
                var serializedStr = JsonConvert.SerializeObject(email);

                var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));
                var responseStr = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<MainResponse>(responseStr);

                if (response.IsSuccessStatusCode)
                {
                    forgotPasswordModel = JsonConvert.DeserializeObject<ForgotPasswordModel>(responseStr);
                    resetPasswordModel.Token = forgotPasswordModel.Token;
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
        
		public async Task<MainResponse> ResetPassword(ResetPasswordModel resetPwdModel)
        {
            MainResponse result = new MainResponse();
            resetPwdModel.Token = forgotPasswordModel!.Token;

            try
            {
                var url = $"{Setting.BaseUrl}{APIs.ResetPassword}";
                var serializedStr = JsonConvert.SerializeObject(resetPwdModel);
                var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));
                var responseStr = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<MainResponse>(responseStr);

                if (!response.IsSuccessStatusCode)
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

        public async Task<MainResponse> UpdateUser(UpdateModel updateModel)
		{
            MainResponse result = new MainResponse();

            try
            {
                var url = $"{Setting.BaseUrl}{APIs.UpdateUser}";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);
                var serializedStr = JsonConvert.SerializeObject(updateModel);
                var response = await client.PutAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));
                var responseStr = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<MainResponse>(responseStr);

                if (!response.IsSuccessStatusCode)
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

		public async Task<MainResponse> DeleteUsers(List<UsersEmail> userEmail)
		{
            MainResponse result = new MainResponse();

            try
            {
                var url = $"{Setting.BaseUrl}{APIs.DeleteUsers}";

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);
                var request = new HttpRequestMessage(HttpMethod.Delete, url)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(userEmail), Encoding.UTF8, "application/json")
                };

                var response = await client.SendAsync(request);
                var responseStr = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<MainResponse>(responseStr);

                if(!response.IsSuccessStatusCode)
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

		public async Task<MainResponse> RefreshToken()
        {
            MainResponse result = new MainResponse();
            bool isTokenRefreshed = false;

            try
            {
                var url = $"{Setting.BaseUrl}{APIs.RefreshToken}";

                var serializedStr = JsonConvert.SerializeObject(new AuthenticateRequestAndResponse
                {
                    RefreshToken = Setting.UserBasicDetail.RefreshToken,
                    AccessToken = Setting.UserBasicDetail.AccessToken
                });

                var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));
                var responseStr = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<MainResponse>(responseStr);

                if (response.IsSuccessStatusCode)
                {
                    var tokenDetails = JsonConvert.DeserializeObject<AuthenticateRequestAndResponse>(result.Content.ToString());
                    Setting.UserBasicDetail.AccessToken = tokenDetails.AccessToken;
                    Setting.UserBasicDetail.RefreshToken = tokenDetails.RefreshToken;

                    accessToken = tokenDetails.AccessToken;
                    var handler = new JwtSecurityTokenHandler();
                    jwtAccessToken = handler.ReadToken(tokenDetails.AccessToken) as JwtSecurityToken;
                    Setting.UserBasicDetail.AccessTokenExpire = jwtAccessToken.ValidTo;

                    string userDetailsStr = JsonConvert.SerializeObject(Setting.UserBasicDetail);
                    await _localStorage.SetItemAsStringAsync(nameof(Setting.UserBasicDetail), userDetailsStr);
                    isTokenRefreshed = true;                    
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
