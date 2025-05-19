using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Blazor_wasm.Models;
using Blazor_wasm.Models.AuthModels;
using Blazor_wasm.Models.PageModels;
using Blazor_wasm.Pages;

namespace Blazor_wasm.Services
{
    public interface IAppService
    {
        public string accessToken { get; set; }
        public JwtSecurityToken jwtAccessToken { get; set; }

        public ForgotPasswordModel forgotPasswordModel { get; set; }
        public ResetPasswordModel resetPasswordModel { get; set; }
        Task<MainResponse> RefreshToken();
        public Task<MainResponse> AuthenticateUser(LoginModel loginModel);
        Task<MainResponse> RegisterUser(RegistrationModel registerUser);
        Task<MainResponse> ForgotPassword(string email);
        Task<MainResponse> ResetPassword(ResetPasswordModel resetPasswordModel);
        Task<MainResponse> UpdateUser(UpdateModel updateModel);
        Task<MainResponse> DeleteUsers(List<UsersEmail> usersEmail);
        Task<(MainResponse, UpdateModel)> GetUser();
        Task<(MainResponse, List<UsersEmail>)> GetAllUsers();
    }
}
