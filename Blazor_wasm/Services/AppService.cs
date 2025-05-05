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

namespace Blazor_wasm.Services
{
    public class AppService : IAppService
    {
        private readonly ILocalStorageService _localStorage;
        public string accessToken { get; set; }
        public JwtSecurityToken jwtAccessToken { get; set; }

        public ForgotPasswordModel forgotPasswordModel { get; set; } = new ForgotPasswordModel();

        public ResetPasswordModel resetPasswordModel { get; set; } = new ResetPasswordModel();

		public AppService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }
        public async Task<(bool IsSuccess, string ErrorMessage)> RegisterUser(RegistrationModel registerUser)
		{
			string errorMessage = string.Empty;
			bool isSuccess = false;
			using (var client = new HttpClient())
			{
				var url = $"{Setting.BaseUrl}{APIs.RegisterUser}";

				var serializedStr = JsonConvert.SerializeObject(registerUser);
				var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));
				if (response.IsSuccessStatusCode)
				{
					isSuccess = true;
				}
				else
				{
					errorMessage = await response.Content.ReadAsStringAsync();
				}
			}
			return (isSuccess, errorMessage);
		}

		public async Task<MainResponse> AuthenticateUser(LoginModel loginModel)
        {
            var returnResponse = new MainResponse();
			try
			{
				using (var client = new HttpClient())
				{
					var url = $"{Setting.BaseUrl}{APIs.AuthenticateUser}";

					var serializedStr = JsonConvert.SerializeObject(loginModel);

					var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));

					if (response.IsSuccessStatusCode)
					{
						string contentStr = await response.Content.ReadAsStringAsync();
						returnResponse = JsonConvert.DeserializeObject<MainResponse>(contentStr);

						var tokenResponse = JsonConvert.DeserializeObject<AuthenticateRequestAndResponse>(returnResponse.Content.ToString());
						var handler = new JwtSecurityTokenHandler();
						jwtAccessToken = handler.ReadToken(tokenResponse.AccessToken) as JwtSecurityToken;
						accessToken = tokenResponse.AccessToken;
					}
				}
			}
			catch (Exception ex) { }
            
            return returnResponse;
        }

		public async Task<UpdateModel> GetUser()
		{
			UpdateModel updateModel = new UpdateModel();
            using (var client = new HttpClient())
            {
                var url = $"{Setting.BaseUrl}{APIs.GetUser}?email={Setting.UserBasicDetail.Email}";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);

                var result = await client.GetAsync(url);
				if (result.IsSuccessStatusCode)
				{
					var response = await result.Content.ReadAsStringAsync();
                    updateModel = JsonConvert.DeserializeObject<UpdateModel>(response);
                }
				else
				{
                    if (jwtAccessToken.ValidTo < DateTime.UtcNow)
                    {
                        await RefreshToken();
                        await GetUser();
                    }
                }
				return updateModel;
            }
        }

		public async Task<List<UsersEmail>> GetAllUsers()
		{
			List<UsersEmail> roleOfUsersEmail = new List<UsersEmail>();

			using (var client = new HttpClient())
			{
				var url = $"{Setting.BaseUrl}{APIs.GetAllUsers}";
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);
				
				var result = await client.GetAsync(url);
				if (result.IsSuccessStatusCode)
				{
					var response = await result.Content.ReadAsStringAsync();
					var usersEmail = JsonConvert.DeserializeObject<List<UsersEmail>>(response);
					roleOfUsersEmail = usersEmail.Where(r => r.Role == "User").Select(r => new UsersEmail
					{
						Role = r.Role,
						Email = r.Email
					}).ToList();
				}
				else
				{
					if (jwtAccessToken.ValidTo < DateTime.UtcNow)
					{
						await RefreshToken();
						await GetAllUsers();
					}
				}											
			}
			return roleOfUsersEmail;
		}

		public async Task<(bool IsSuccess, string ErrorMessage)> ForgotPassword(string email)
        {
            bool isSuccess = false;
            string errorMessage = string.Empty;
			string message = string.Empty;

			resetPasswordModel.Email = email;

            using (var client = new HttpClient())
            {
                var url = $"{Setting.BaseUrl}{APIs.ForgotPassword}?email={email}";
                var serializedStr = JsonConvert.SerializeObject(email);
				try
				{
                    var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));
                    if (response.IsSuccessStatusCode)
                    {
                        isSuccess = true;
                        message = await response.Content.ReadAsStringAsync();
                        forgotPasswordModel = JsonConvert.DeserializeObject<ForgotPasswordModel>(message);
                        resetPasswordModel.Token = forgotPasswordModel.Token;
                    }
                    else
                    {
                        errorMessage = await response.Content.ReadAsStringAsync();
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                }

            }
            return (isSuccess, errorMessage);
        }

		public async Task<(bool IsSuccess, string ErrorMessage)> ResetPassword(ResetPasswordModel resetPwdModel)
        {
            string errorMessage = string.Empty;
            bool isSuccess = false;
			resetPwdModel.Token = forgotPasswordModel!.Token;
            using (var client = new HttpClient())
            {
                var url = $"{Setting.BaseUrl}{APIs.ResetPassword}";
                var serializedStr = JsonConvert.SerializeObject(resetPwdModel);
                var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    isSuccess = true;
                }
                else
                {
                    errorMessage = await response.Content.ReadAsStringAsync();
                }
            }
            return (isSuccess, errorMessage);
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> UpdateUser(UpdateModel updateModel)
		{
			string errorMessage = string.Empty;
			bool isSuccess = false;
			using (var client = new HttpClient())
			{
				var url = $"{Setting.BaseUrl}{APIs.UpdateUser}";
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);
				var serializedStr = JsonConvert.SerializeObject(updateModel);
				var response = await client.PutAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));
				if (response.IsSuccessStatusCode)
				{
					isSuccess = true;
				}
				else
				{
					errorMessage = await response.Content.ReadAsStringAsync();
				}
			}
			return (isSuccess, errorMessage);
		}

		public async Task<(bool IsSuccess, string ErrorMessage)> DeleteUsers(List<UsersEmail> userEmail)
		{
			string errorMessage = string.Empty;
			bool isSuccess = false;
			using (var client = new HttpClient())
			{
				var url = $"{Setting.BaseUrl}{APIs.DeleteUsers}";

				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Setting.UserBasicDetail.AccessToken);
				var request = new HttpRequestMessage(HttpMethod.Delete, url)
				{
					Content = new StringContent(JsonConvert.SerializeObject(userEmail), Encoding.UTF8, "application/json")
				};

				var responseDelete = await client.SendAsync(request);
				if (responseDelete.IsSuccessStatusCode)
				{
					isSuccess = true;
				}
				else
				{
                    if (jwtAccessToken.ValidTo < DateTime.UtcNow)
                    {
                        await RefreshToken();
                        await DeleteUsers(userEmail);
                    }
                    errorMessage = await responseDelete.Content.ReadAsStringAsync();
				}
				return (isSuccess, errorMessage);
			}
		}

		public async Task<bool> RefreshToken()
        {
            bool isTokenRefreshed = false;
            using (var client = new HttpClient())
            {
                var url = $"{Setting.BaseUrl}{APIs.RefreshToken}";

                var serializedStr = JsonConvert.SerializeObject(new AuthenticateRequestAndResponse
                {
                    RefreshToken = Setting.UserBasicDetail.RefreshToken,
                    AccessToken = Setting.UserBasicDetail.AccessToken
                });

                try
                {
                    var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));
                    if (response.IsSuccessStatusCode)
                    {
                        string contentStr = await response.Content.ReadAsStringAsync();
                        var mainResponse = JsonConvert.DeserializeObject<MainResponse>(contentStr);
                        if (mainResponse.IsSuccess)
                        {
                            var tokenDetails = JsonConvert.DeserializeObject<AuthenticateRequestAndResponse>(mainResponse.Content.ToString());
                            Setting.UserBasicDetail.AccessToken = tokenDetails.AccessToken;
                            Setting.UserBasicDetail.RefreshToken = tokenDetails.RefreshToken;

                            accessToken = tokenDetails.AccessToken;
                            var handler = new JwtSecurityTokenHandler();
                            jwtAccessToken = handler.ReadToken(tokenDetails.AccessToken) as JwtSecurityToken;

                            string userDetailsStr = JsonConvert.SerializeObject(Setting.UserBasicDetail);
                            await _localStorage.SetItemAsStringAsync(nameof(Setting.UserBasicDetail), userDetailsStr);
                            isTokenRefreshed = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                }
            }
            return isTokenRefreshed;
        }        
    }
}
