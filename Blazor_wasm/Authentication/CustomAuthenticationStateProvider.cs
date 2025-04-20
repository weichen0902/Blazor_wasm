using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Blazor_wasm.Models;
using Blazor_wasm.Models.AuthModels;
using Blazored.LocalStorage;


namespace Blazor_wasm.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private ClaimsPrincipal anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthenticationStateProvider(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                //Get Usersession from secure storage
                string getUserSessionFromStorage = await _localStorage.GetItemAsStringAsync("UserBasicDetail");
                if (string.IsNullOrEmpty(getUserSessionFromStorage))
                    return await Task.FromResult(new AuthenticationState(anonymous));

                //Desrialize into and UserSession object.
                var DesrializedUserSession = JsonConvert.DeserializeObject<UserBasicDetail>(getUserSessionFromStorage);
                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, DesrializedUserSession.Name!),
                new Claim(ClaimTypes.Email, DesrializedUserSession.Email!),
                new Claim(ClaimTypes.Role, DesrializedUserSession.Role!)

            }, "CustomAuth"));
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch
            {
                return await Task.FromResult(new AuthenticationState(anonymous));
            }
        }

        public async Task UpdateAuthenticationState(UserBasicDetail userBasicDetail)
        {
            ClaimsPrincipal claimsPrincipal;
            if (!string.IsNullOrEmpty(userBasicDetail.Name) || !string.IsNullOrEmpty(userBasicDetail.Email))
            {
                string serializeUserSession = JsonConvert.SerializeObject(userBasicDetail);
                await _localStorage.SetItemAsStringAsync("UserBasicDetail", serializeUserSession);

                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, userBasicDetail.Name!),
                new Claim(ClaimTypes.Email, userBasicDetail.Email!),
                new Claim(ClaimTypes.Role, userBasicDetail.Role!)
            }));
            }
            else
            {
                await _localStorage.RemoveItemAsync("UserBasicDetail");
                claimsPrincipal = anonymous;
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
    }
}

