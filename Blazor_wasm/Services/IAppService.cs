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
        Task<bool> RefreshToken();
        public Task<MainResponse> AuthenticateUser(LoginModel loginModel);
        Task<(bool IsSuccess, string ErrorMessage)> RegisterUser(RegistrationModel registerUser);
        Task<(bool IsSuccess, string ErrorMessage)> ForgotPassword(string email);
        Task<(bool IsSuccess, string ErrorMessage)> ResetPassword(ResetPasswordModel resetPasswordModel);
        Task<(bool IsSuccess, string ErrorMessage)> UpdateUser(UpdateModel updateModel);
        Task<(bool IsSuccess, string ErrorMessage)> DeleteUsers(List<UsersEmail> usersEmail);
        Task<UpdateModel> GetUser();
        Task<List<UsersEmail>> GetAllUsers();
    }
}
